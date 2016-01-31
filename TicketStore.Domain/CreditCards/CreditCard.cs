using System;

namespace TicketStore.Domain.CreditCards
{
    public class CreditCard
    {
        private CreditCard()
        {
            CreateDate = DateTime.Now;
        }

        public int CreditCardId { get; private set; }
        public Guid InstantBuyKey { get; set; }
        public CreditCardBrand Brand { get; set; }
        public string LastFourDigits { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public virtual User Owner { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
