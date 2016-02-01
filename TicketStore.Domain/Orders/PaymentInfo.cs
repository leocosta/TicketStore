using System;
using TicketStore.Domain.CreditCards;

namespace TicketStore.Domain.Orders
{
    public class PaymentInfo
    {
        public Guid? InstantBuyKey { get; set; }
        public string CreditCardNumber { get; set; }
        public CreditCardBrand CreditCardBrand { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string SecurityCode { get; set; }
        public string HolderName { get; set; }
        public bool SaveCreditCard { get; set; }
        public long Amount { get; set; }
    }

}
