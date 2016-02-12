using TicketStore.Domain.Orders;

namespace TicketStore.Service.Events
{
    internal class OrderPlaced
    {
        public int OrderId { get; private set; }
        public PaymentInfo PaymentInfo { get; private set; }

        public OrderPlaced(int orderId, PaymentInfo paymentInfo)
        {
            OrderId = orderId;
            PaymentInfo = paymentInfo;
        }
    }
}
