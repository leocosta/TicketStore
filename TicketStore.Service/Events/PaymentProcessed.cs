namespace TicketStore.Service.Events
{
    internal class PaymentProcessed
    {
        public int OrderId { get; private set; }
        public bool Success { get; private set; }
        public PaymentProcessed(int orderId, bool success)
        {
            OrderId = orderId;
            Success = success;
        }
    }
}
