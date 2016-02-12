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

        public decimal GetAmount()
        {
            return Price * Quantity;
        }

        public void Processing()
        {
            Status = Status.Processing;
        }

        public void PaymentReceived(PaymentResult paymentResult, PaymentInfo paymentInfo)
        {
            Status = Status.PaymentReceived;
            registerCreditCardTransation(paymentResult);
            saveUserCreditCard(paymentInfo, paymentResult);
        }

        public void PaymentReview()
        {
            Status = Status.PaymentReview;
        }

        private void registerCreditCardTransation(PaymentResult paymentResult)
        {
            this.CreditCardTransation = new CreditCardTransation(paymentResult.TransactionReference);
        }

        private void saveUserCreditCard(PaymentInfo paymentInfo, PaymentResult paymentResult)
        {
            if (!paymentInfo.ShouldSaveCreditCard() || !paymentResult.InstantBuyKey.HasValue)
                return;

            var creditCard = new CreditCard(this.Customer, paymentResult.InstantBuyKey.Value,
                paymentInfo.CreditCardBrand, paymentInfo.CreditCardNumber.GetLast(4), paymentInfo.ExpMonth, paymentInfo.ExpYear);
            this.Customer.AddCreditCard(creditCard);
        }

    }
}
