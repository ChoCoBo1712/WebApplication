using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Service.Interfaces;

namespace Service.Implementations
{
    public class EmailService : IEmailService
    {
        private string sender;
        private string smtpServer;
        private int smtpPort;
        private string username;
        private string password;
        public EmailService(string sender, string smtpServer, int smtpPort, string username, string password)
        {
            this.sender = sender;
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.username = username;
            this.password = password;
        }
        
        public async Task SendEmailAsync(string receiver, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(sender));
            emailMessage.To.Add(new MailboxAddress(receiver));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
             
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpServer, smtpPort);
                await client.AuthenticateAsync(username, password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}