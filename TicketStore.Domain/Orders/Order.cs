using System;
using TicketStore.Domain.Events;
using TicketStore.Domain.Users;

namespace TicketStore.Domain.Orders
{
    public class Order
    {
        public int OrderId { get; private set; }
        public User User { get; set; }
        public Event Event { get; set; }
        public Status Status { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}
