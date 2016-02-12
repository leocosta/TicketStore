using TicketStore.Domain.Orders;

namespace TicketStore.Service.Commands
{
    internal class ProcessPayment
    {
        public int OrderId { get; }
        public PaymentInfo PaymentInfo { get; }

        public ProcessPayment(int orderId, PaymentInfo paymentInfo)
        {
            OrderId = orderId;
            PaymentInfo = paymentInfo;
        }
    }
}
