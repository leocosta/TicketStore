using System;
using System.Collections.Generic;
using TicketStore.Domain.Common;
using TicketStore.Domain.Events;
using TicketStore.Domain.Notifications;
using TicketStore.Domain.Users;
using TicketStore.Infra.CrossCutting.Helpers;

namespace TicketStore.Domain.Orders
{
    public class Order
    {
        public Order()
        {
            Status = Status.Created;
            CreateDate = DateTime.Now;
        }

        public Order(User customer, Event @event, int quantity) 
            : this()
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (customer == null)
                throw new ArgumentNullException("customer");

            if (quantity < 1)
                throw new InvalidOperationException("Quantity should be should be greater than zero.");

            Customer = customer;
            Event = @event;
            Price = @event.Price;
            Quantity = quantity;
        }

        public int OrderId { get; private set; }
        public virtual User Customer { get; set; }
        public virtual Event Event { get; set; }
        public Status Status { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public virtual CreditCardTransation CreditCardTransation { get; set; }

        public void ProcessPayment(PaymentInfo paymentInfo, IPaymentService paymentService, INotificationService notificationService, IUnitOfWork unitOfWork)
        {
            if (paymentInfo == null)
                throw new ArgumentNullException("paymentInfo");

            if (paymentService == null)
                throw new ArgumentNullException("paymentService");

            PaymentResult paymentResult = null;
            try
            {
                this.Status = Status.Processing;
                unitOfWork.Commit();

                paymentInfo.Amount = (long)this.getAmount();
                paymentResult = paymentService.CreateTransaction(paymentInfo);
                this.Status = Status.PaymentReceived;
            }
            catch (Exception ex)
            {
                this.Status = Status.PaymentReview;
                unitOfWork.Commit();
                throw new InvalidOperationException(string.Format("We encountered an error processing your payment: {0}", ex.Message));
            }

            registerCreditCardTransation(paymentResult);
            saveUserCreditCard(paymentInfo, paymentResult);

            unitOfWork.Commit();
            notificationService.SendPaymentStatus(this);
        }
        private decimal getAmount()
        {
            return Price * Quantity;
        }

        private void registerCreditCardTransation(PaymentResult paymentResult)
        {
            this.CreditCardTransation = new CreditCardTransation(paymentResult.TransactionReference);
        }

        private void saveUserCreditCard(PaymentInfo paymentInfo, PaymentResult paymentResult)
        {
            if (!paymentInfo.SaveCreditCard || paymentInfo.InstantBuyKey != null)
                return;

            var creditCard = new CreditCard(this.Customer, paymentResult.InstantBuyKey,
                paymentInfo.CreditCardBrand, paymentInfo.CreditCardNumber.GetLast(4), paymentInfo.ExpMonth, paymentInfo.ExpYear);
            this.Customer.AddCreditCard(creditCard);
        }

        public override string ToString()
        {
            var status = new Dictionary<Status, string>()
            {
                { Status.Created, "Criado" },
                { Status.Processing, "Processando" },
                { Status.PaymentReview, "Em análise" },
                { Status.PaymentReceived, "Pagamento recebido" },
                { Status.Closed, "Finalizado" },
                { Status.Cancelled, "Cancelado" },
            };

            return status[this.Status]; 
        }
    }
}
