using System;
using TicketStore.Domain.Events;
using TicketStore.Domain.CreditCards;
using TicketStore.Infra.CrossCutting.Helpers;
using TicketStore.Domain.Common;

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

        public void ProcessPayment(PaymentInfo paymentInfo, IPaymentService paymentGatewayService, IUnitOfWork unitOfWork)
        {
            if (paymentInfo == null)
                throw new ArgumentNullException("paymentInfo");

            if (paymentGatewayService == null)
                throw new ArgumentNullException("paymentGatewayService");

            paymentInfo.Amount = (long)this.GetAmount();
            var paymentResult = paymentGatewayService.CreateTransaction(paymentInfo);
            this.CreditCardTransation = new CreditCardTransation(paymentResult.TransactionReference);

            if (paymentInfo.SaveCreditCard)
            {
                var creditCard = new CreditCard()
                {
                    InstantBuyKey = paymentResult.InstantBuyKey,
                    Brand = paymentInfo.CreditCardBrand,
                    LastFourDigits = paymentInfo.CreditCardNumber.GetLast(4),
                    ExpMonth = paymentInfo.ExpMonth,
                    ExpYear = paymentInfo.ExpYear,
                    Owner = this.Customer
                };
                this.Customer.AddCreditCard(creditCard);
            }

            unitOfWork.Commit();
        }

        private decimal GetAmount()
        {
            return Price * Quantity;
        }
    }
}
