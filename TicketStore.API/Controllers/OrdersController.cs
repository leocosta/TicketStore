using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TicketStore.API.Filters;
using TicketStore.API.ViewModel;
using TicketStore.Domain.Common;
using TicketStore.Domain.Events;
using TicketStore.Domain.Orders;
using TicketStore.Domain.Users;
using System.Linq;
using TicketStore.Domain.Notifications;

namespace TicketStore.API.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IPaymentService _paymentService;
        private readonly INotificationService _notificationService;

        public OrdersController(IUnitOfWork unitOfWork, IOrderRepository orderRepository, IUserRepository userRepository,
                IEventRepository eventRepository, IPaymentService paymentService, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _paymentService = paymentService;
            _notificationService = notificationService;
        }

        // GET api/orders
        public HttpResponseMessage Get()
        {
            var orders = _orderRepository.GetAll();
            var result = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);
            return Request.CreateResponse(result);
        }

        // GET api/orders/5
        public HttpResponseMessage Get(int id) { 
            var order = _orderRepository.Get(id);
            if (order == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order not found.");

            var result = Mapper.Map<Order, OrderViewModel>(order);
            return Request.CreateResponse(result);
        }

        // POST api/orders
        [ModelValidate]
        public HttpResponseMessage Post(OrderViewModel orderViewModel)
        {
            if (orderViewModel == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid request.");

            var customer = _userRepository.Get(orderViewModel.Customer.UserId.Value);
            if (customer == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Customer not found.");

            var @event = _eventRepository.Get(orderViewModel.Event.EventId.Value);
            if (@event == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Event not found.");

            var paymentInfo = Mapper.Map<PaymentInfoViewModel, PaymentInfo>(orderViewModel.PaymentInfo);
            if (orderViewModel.PaymentInfo.CreditCardId.HasValue)
            {
                var creditCard = customer.CreditCards.FirstOrDefault(a => a.CreditCardId.Equals(orderViewModel.PaymentInfo.CreditCardId.Value));
                if (creditCard == null)
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Credit Card not found.");

                paymentInfo.InstantBuyKey = creditCard.InstantBuyKey;
            }

            var order = new Order(customer, @event, orderViewModel.Quantity);
            _orderRepository.Add(order);
            _unitOfWork.Commit();

            //TODO: add order to queue 
            order.ProcessPayment(paymentInfo, _paymentService, _notificationService, _unitOfWork);

            var result = Mapper.Map<Order, OrderViewModel>(order);
            return Request.CreateResponse(result);
        }

        // OPTIONS api/orders
        public HttpResponseMessage Options()
        {
            var response = new HttpResponseMessage();
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");

            return response;
        }
    }
}
