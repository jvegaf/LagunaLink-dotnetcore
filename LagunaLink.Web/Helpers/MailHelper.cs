
namespace LagunaLink.Web.Helpers
{
    using MailKit.Net.Smtp;
    using Microsoft.Extensions.Configuration;
    using MimeKit;

    public class MailHelper : IMailHelper
    {
        private IConfigurationSection SMTP;

        public MailHelper(IConfiguration configuration)
        {
            
            try
            {
                // Used to access appsettings.json
                SMTP = configuration.GetSection("Smtp");
            }
            catch (System.Exception ex)
            {
                
            }
        }

        public void SendMail(string to, string subject, string body)
        {
            var from = SMTP["From"];
            var url = SMTP["Url"];
            var port = SMTP["Port"];
            var login = SMTP["Login"];
            var password = SMTP["Password"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(from));
            message.To.Add(new MailboxAddress(to));
            message.Subject = subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                client.Connect(url, int.Parse(port), false);
                client.Authenticate(login, password);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
