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

        public void SendPaymentReceived(Order order)
        {
            var to = order.Customer.Email;
            var subject = "Seu pedido foi autorizado!";
            var body = string.Format(@"Seu pedido no. {0} foi processado em nossa loja e sua compra foi autorizada.

                Atenciosamente,
                Equipe TicketStore", order.OrderId);

            _messageService.Send(to, subject, body);
        }

        public void SendPaymentReview(Order order)
        {
            var to = order.Customer.Email;
            var subject = "Pagamento não autorizado!";
            var body = string.Format(@"Seu pedido no. {0} foi processado em nossa loja, mas o pagamento não foi autorizado. Por favor, entre em contato com a 
                operadora de seu cartão de crédito.

                Atenciosamente,
                Equipe TicketStore", order.OrderId);

            _messageService.Send(to, subject, body);
        }
    }
}