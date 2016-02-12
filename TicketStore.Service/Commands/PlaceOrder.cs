using TicketStore.Domain.Orders;

namespace TicketStore.Service.Commands
{
    public class PlaceOrder
    {
        public int UserId { get; }
        public int EventId { get; }
        public int Quantity { get; }
        public PaymentInfo PaymentInfo { get; }

        public PlaceOrder(int userId, int eventId, int quantity, PaymentInfo paymentInfo)
        {
            this.UserId = userId;
            this.EventId = eventId;
            this.Quantity = quantity;
            this.PaymentInfo = paymentInfo;
        }
    }
}
