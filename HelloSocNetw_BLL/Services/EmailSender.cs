using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using HelloSocNetw_BLL.Entities;
using Microsoft.Extensions.Options;
using HelloSocNetw_BLL.Interfaces;

namespace HelloSocNetw_BLL.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Repair Site", _emailSettings.Sender));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort, false);
            await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
