using TicketStore.Domain.Orders;

namespace TicketStore.Domain.Notifications
{
    public interface INotificationService
    {
        void SendPaymentReceived(Order order);
        void SendPaymentReview(Order order);
    }
}