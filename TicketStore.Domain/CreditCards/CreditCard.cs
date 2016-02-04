using System;

namespace TicketStore.Domain.Users
{
    public class CreditCard
    {
        public CreditCard()
        {
            CreateDate = DateTime.Now;
        }

        public CreditCard(User user, Guid instantBuyKey, CreditCardBrand creditCardBrand, string lastFourDigits, int expMonth, int expYear) 
            : this()
        {
            User = user;
            InstantBuyKey = instantBuyKey;
            Brand = creditCardBrand;
            LastFourDigits = lastFourDigits;
            ExpMonth = expMonth;
            ExpYear = expYear;
        }

        public int CreditCardId { get; private set; }
        public Guid InstantBuyKey { get; private set; }
        public CreditCardBrand Brand { get; private set; }
        public string LastFourDigits { get; private set; }
        public int ExpMonth { get; private set; }
        public int ExpYear { get; private set; }
        public virtual User User { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime? ModifyDate { get; private set; }
    }
}
