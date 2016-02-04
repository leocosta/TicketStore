using System;
using TicketStore.Domain.Orders;
using TicketStore.Infra.CrossCutting.Notifications;

namespace TicketStore.Domain.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IMessageService _messageService;

        public NotificationService(IMessageService messageService)
        {
            if (messageService == null)
                throw new ArgumentNullException("messageService");

            _messageService = messageService;
        }

        public void SendPaymentStatus(Order order)
        {
            var to = order.Customer.Email;
            var subject = "Seu pedido foi processado!";
            var body = string.Format(@"Seu pedido foi processado em nossa loja e enconta-se na seguinte situação: {0}. \n\n
                Atenciosamente,\n
                Equipe TicketStore", order);

            _messageService.Send(to, subject, body);
        }
    }
}