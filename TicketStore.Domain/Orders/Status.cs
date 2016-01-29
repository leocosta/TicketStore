namespace TicketStore.Domain.Orders
{
    public enum Status : byte
    {
        Awaitting,
        InProgress,
        Completed,
        Cancelled
    }
}
