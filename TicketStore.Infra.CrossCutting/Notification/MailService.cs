using SendGrid;
using System;
using System.Configuration;
using System.Net.Mail;
using TicketStore.Infra.CrossCutting.Notifications;

namespace TicketStore.Infra.CrossCutting.Notification
{
    public class MailService : IMessageService
    {
        public void Send(string to, string subject, string body)
        {
            if (to == null)
                throw new ArgumentNullException("to");

            if (subject == null)
                throw new ArgumentNullException("subject");

            if (body == null)
                throw new ArgumentNullException("body");

            SendGridMessage message = new SendGridMessage();
            message.AddTo(to);
            message.From = new MailAddress("leoccosta@outlook.com", "TicketStore.com");
            message.Subject = subject;
            message.Text = body;

            var transportWeb = new Web(ConfigurationManager.AppSettings["MailService.ApiKey"]);
            try
            {
                transportWeb.DeliverAsync(message);
            }
            catch (Exception)
            {
                //TODO: Log error
            }
        }
    }
}