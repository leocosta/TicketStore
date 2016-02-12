using TicketStore.Domain.Orders;

namespace TicketStore.Service.Events
{
    internal class OrderPlaced
    {
        public int OrderId { get; }
        public PaymentInfo PaymentInfo { get; }

        public OrderPlaced(int orderId, PaymentInfo paymentInfo)
        {
            OrderId = orderId;
            PaymentInfo = paymentInfo;
        }
    }
}
