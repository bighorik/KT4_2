using Application.Interfaces.Services;
using Domain.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Infrastructure.Services
{
    internal class EmailService(
        IConfiguration configuration) : IEmailService
    {
        /// <summary>
        /// Вынести в ресурсы
        /// </summary>
        private readonly string _confirmationCodeMessage = @"Ваш код подтверждения: [1]";
        public async Task SendConfirmationMessage(string email, string confirmationCode)
        {
            var settings = configuration.GetEmailSettings();
            MimeMessage message = new();
            message.From.Add(MailboxAddress.Parse(settings.From));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Код подтверждения";
            message.Body = new TextPart("html") { Text = string.Format(_confirmationCodeMessage, confirmationCode) };

            using var client = new SmtpClient();
            await client.ConnectAsync(settings.Host, settings.Port, settings.EnableSsl);
            await client.AuthenticateAsync(settings.User, settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
