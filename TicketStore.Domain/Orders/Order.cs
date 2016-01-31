using System;
using TicketStore.Domain.Events;
using TicketStore.Domain.Users;

namespace TicketStore.Domain.Orders
{
    public class Order
    {
        public int OrderId { get; private set; }
        public virtual User Customer { get; set; }
        public virtual Event Event { get; set; }
        public Status Status { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public virtual CreditCardTransation CreditCardTransation { get; set; }

        public void ProcessPayment(PaymentInfo paymentInfo, IPaymentGateway paymentGateway, IUserRepository userRepository)
        {
            paymentInfo.Amount = (long)this.GetAmount();
            paymentGateway.CreateTransaction(paymentInfo);
            if (paymentInfo.SaveCreditCard)
            {
                //var creditCard = new CreditCard.Create(paymentInfo);
                //userRepository
            }
        }

        private decimal GetAmount()
        {
            return Price * Quantity;
        }
    }

    public interface IPaymentGateway
    {
        void CreateTransaction(PaymentInfo paymentInfo);
    }

    public class PaymentInfo
    {
        public string CreditCardNumber { get; set; }
        public CreditCardBrand CreditCardBrand { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string SecurityCode { get; set; }
        public string HolderName { get; set; }
        public bool SaveCreditCard { get; set; }
        public long Amount { get; set; }
    }

    public class CreditCardTransation
    {
        public int PaymentTransationId { get; private set; }
        public string IntegrationToken { get; private set; }
        public decimal Amount { get; set; }
    }
}
