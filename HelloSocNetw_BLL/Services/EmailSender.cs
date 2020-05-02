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

        public async Task SendEmailAsync(string receiverEmail, string subject, string htmlMessage)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.SenderEmail));
            emailMessage.To.Add(new MailboxAddress(_emailSettings.ToName, receiverEmail));

            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using var client = new SmtpClient();

            await client.ConnectAsync(_emailSettings.MailHost, _emailSettings.MailPort, false);
            await client.AuthenticateAsync(_emailSettings.SenderEmail, _emailSettings.SenderPassword);
            await client.SendAsync(emailMessage);

            await client.DisconnectAsync(true);
        }
    }
}
