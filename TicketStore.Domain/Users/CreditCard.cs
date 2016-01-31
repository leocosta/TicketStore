using System;

namespace TicketStore.Domain.Users
{
    public enum CreditCardBrand : byte
    {
        Visa = 1,
        Mastercard = 2,
        Hipercard = 3,
        Amex = 4,
        Diners = 5
    }

    public class CreditCard
    {
        public int CreditCardId { get; private set; }
        public string IntegrationToken { get; set; }
        public CreditCardBrand Brand { get; set; }
        public string LastFourDigits { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public virtual User Owner { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
