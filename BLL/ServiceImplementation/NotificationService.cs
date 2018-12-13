using System.Configuration;
using System.IO;
using BLL.Interface.Interfaces;
using MailKit.Net.Smtp;
using MimeKit;
using NLog;

namespace BLL.ServiceImplementation
{
    public class NotificationService : INotificationService
    {
        private Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public void Send(string destination, string body)
        {
            string server = ConfigurationManager.AppSettings["smtpServer"];
            int port = int.Parse(ConfigurationManager.AppSettings["smtpPort"]);

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Administrator", "admin@exemple.com"));
            message.To.Add(new MailboxAddress(destination));
            message.Subject = "Notification";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            try
            {
                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    client.Connect(server, port, false);
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            catch (IOException e)
            {
                _logger.Error($"Send email is failed. {e.Message}");
            }
        }
    }
}
