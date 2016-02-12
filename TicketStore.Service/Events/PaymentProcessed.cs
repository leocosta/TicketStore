namespace TicketStore.Service.Events
{
    internal class PaymentProcessed
    {
        public int OrderId { get; }
        public bool Success { get; }
        public PaymentProcessed(int orderId, bool success)
        {
            OrderId = orderId;
            Success = success;
        }
    }
}
