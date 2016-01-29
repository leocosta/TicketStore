using System;

namespace TicketStore.Domain.Users
{
    public class CreditCard
    {
        public int CreditCardId { get; private set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public User Owner { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
