using TicketStore.Domain.Orders;

namespace TicketStore.Service.Messages
{
    public class PlaceOrder
    {
        public int UserId { get; private set; }
        public int EventId { get; private set; }
        public int Quantity { get; private set; }
        public PaymentInfo PaymentInfo { get; private set; }

        public PlaceOrder(int userId, int eventId, int quantity, PaymentInfo paymentInfo)
        {
            this.UserId = userId;
            this.EventId = eventId;
            this.Quantity = quantity;
            this.PaymentInfo = paymentInfo;
        }
    }
}
