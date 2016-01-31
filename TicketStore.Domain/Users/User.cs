using System;
using System.Collections.Generic;

namespace TicketStore.Domain.CreditCards
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public Gender Gender { get; set; }
        public virtual Address Address { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        internal void AddCreditCard(CreditCard creditCard)
        {
            this.CreditCards.Add(creditCard);
        }
    }
}
