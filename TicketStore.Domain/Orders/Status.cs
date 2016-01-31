namespace TicketStore.Domain.Orders
{
    public enum Status : byte
    {
        Created = 1,
        Processing = 2,
        PaymentReview = 3,
        PaymentReceived = 4,
        Closed = 5,
        Cancelled = 6
    }
}
