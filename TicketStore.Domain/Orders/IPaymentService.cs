namespace TicketStore.Domain.Orders
{
    public interface IPaymentService
    {
        PaymentResult CreateTransaction(PaymentInfo paymentInfo);
    }
}
