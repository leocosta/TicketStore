using TicketStore.Domain.Orders;

namespace TicketStore.Service.Commands
{
    internal class ProcessPayment
    {
        public int OrderId { get; private set; }
        public PaymentInfo PaymentInfo { get; private set; }

        public ProcessPayment(int orderId, PaymentInfo paymentInfo)
        {
            OrderId = orderId;
            PaymentInfo = paymentInfo;
        }
    }
}
