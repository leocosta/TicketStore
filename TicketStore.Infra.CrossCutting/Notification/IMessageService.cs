namespace TicketStore.Infra.CrossCutting.Notifications
{
    public interface IMessageService
    {
        void Send(string to, string subject, string body);
    }
}