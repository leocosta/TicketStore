using TicketStore.Domain.Orders;

namespace TicketStore.Domain.Notifications
{
    public interface INotificationService
    {
        void SendPaymentStatus(Order order);
    }
}