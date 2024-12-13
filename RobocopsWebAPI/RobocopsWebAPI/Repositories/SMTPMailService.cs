using System.Net;
using System.Net.Mail;

namespace RobocopsWebAPI.Repositories
{
    public class SMTPMailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;
        private readonly string _smtpFromAddress;
        private readonly bool _smtpEnableSSL;
        private readonly string _smtpSenderName;
        public SMTPMailService() {

            _smtpHost = Environment.GetEnvironmentVariable("SMTP_HOST") ?? throw new ArgumentNullException("SMTP_HOST");
            _smtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));
            _smtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME");
            _smtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
            _smtpFromAddress = Environment.GetEnvironmentVariable("SMTP_FROMADDRESS");
            _smtpEnableSSL = bool.Parse(Environment.GetEnvironmentVariable("SMTP_ENABLESSL"));
            _smtpSenderName = Environment.GetEnvironmentVariable("SMTP_SENDERNAME") ?? throw new ArgumentNullException("SMTP_SENDERNAME");
        }

        public async Task SendMail(string to, string subject, string body)
        {
            try
            {
                var message = new MailMessage();
                message.Subject = subject;
                message.To.Add(to);
                message.Body = body;
                message.From = new MailAddress(_smtpFromAddress);
                message.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials= new NetworkCredential(_smtpUsername, _smtpPassword);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Host= _smtpHost;
                smtpClient.Port= _smtpPort;
                smtpClient.EnableSsl =  _smtpEnableSSL;
                smtpClient.Send(message);

            }
            catch (Exception ex)
            {
            }
        }
    }
}
