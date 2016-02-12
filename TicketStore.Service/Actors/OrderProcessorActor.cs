using Akka.Actor;
using System;
using System.Linq;
using TicketStore.Domain.Common;
using TicketStore.Domain.Events;
using TicketStore.Domain.Notifications;
using TicketStore.Domain.Orders;
using TicketStore.Domain.Users;
using TicketStore.Infra.CrossCutting.Logging;
using TicketStore.Service.Commands;
using TicketStore.Service.Events;

namespace TicketStore.Service.Actors
{
    public sealed class OrderProcessorActor : ReceiveActor
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IPaymentService _paymentService;
        private readonly INotificationService _notificationService;
        public OrderProcessorActor(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IUserRepository userRepository, IEventRepository eventRepository,
            IPaymentService paymentService, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _paymentService = paymentService;
            _notificationService = notificationService;

            Ready();
        }

        private void Ready()
        {
            Receive<PlaceOrder>(cmd => placeOrderHandler(cmd));
            Receive<OrderPlaced>(cmd => orderPlacedHandler(cmd));
            Receive<ProcessPayment>(cmd => processPaymentHandler(cmd));
            Receive<PaymentProcessed>(cmd => paymentProcessedHandler(cmd));
        }
        private void placeOrderHandler(PlaceOrder placeOrder)
        {
            var customer = _userRepository.Get(placeOrder.UserId);
            if (customer == null)
                throw new InvalidOperationException("User not not found.");

            var @event = _eventRepository.Get(placeOrder.EventId);
            if (@event == null)
                throw new InvalidOperationException("Event not not found.");

            if (placeOrder.PaymentInfo.CreditCardId.HasValue)
            {
                var creditCard = customer.CreditCards.FirstOrDefault(a => a.CreditCardId.Equals(placeOrder.PaymentInfo.CreditCardId.Value));
                if (creditCard == null)
                    throw new InvalidOperationException("Credit Card not found.");

                placeOrder.PaymentInfo.InstantBuyKey = creditCard.InstantBuyKey;
            }

            var order = new Order(customer, @event, placeOrder.Quantity);
            _orderRepository.Add(order);
            _unitOfWork.Commit();

            Sender.Tell(order);

            Self.Tell(new OrderPlaced(order.OrderId, placeOrder.PaymentInfo));
        }
        private void orderPlacedHandler(OrderPlaced orderPlaced)
        {
            //TODO: Send email to customer
            Self.Tell(new ProcessPayment(orderPlaced.OrderId, orderPlaced.PaymentInfo));
        }

        private void processPaymentHandler(ProcessPayment processPayment)
        {
            var order = _orderRepository.Get(processPayment.OrderId);
            order.Processing();
            _unitOfWork.Commit();

            bool success = false;
            try
            {
                var paymentInfo = processPayment.PaymentInfo;
                paymentInfo.Amount = (long)order.GetAmount();
                var paymentResult = _paymentService.CreateTransaction(paymentInfo);
                order.PaymentReceived(paymentResult, paymentInfo);
                success = true;
            }
            catch (Exception ex)
            {
                order.PaymentReview();
                Logger.Info($"processPaymentHandler: orderId: {order.OrderId}, Error: {ex.Message} {ex.InnerException.Message}");
            }
            finally
            {
                _unitOfWork.Commit();
            }

            Self.Tell(new PaymentProcessed(order.OrderId, success));
        }

        private void paymentProcessedHandler(PaymentProcessed paymentProcessed)
        {
            var order = _orderRepository.Get(paymentProcessed.OrderId);
            try
            {
                if (paymentProcessed.Success)
                    _notificationService.SendPaymentReceived(order);
                else
                    _notificationService.SendPaymentReview(order);
            }
            catch (Exception ex)
            {
                Logger.Error($"Failed to send email: {ex}");
            }
        }
    }
}
