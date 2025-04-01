using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Security;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using System;

namespace SFMSSolution.Application.ExternalService.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var fromName = _configuration["EmailSettings:FromName"];
                var fromEmail = _configuration["EmailSettings:FromEmail"];
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var smtpUser = _configuration["EmailSettings:SmtpUser"];
                var smtpPass = _configuration["EmailSettings:SmtpPass"];

                if (string.IsNullOrEmpty(fromEmail) || string.IsNullOrEmpty(smtpServer))
                    throw new Exception("Email configuration is missing.");

                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(fromName, fromEmail));
                emailMessage.To.Add(MailboxAddress.Parse(toEmail));
                emailMessage.Subject = subject;

                var builder = new BodyBuilder
                {
                    HtmlBody = body
                };
                emailMessage.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(smtpUser, smtpPass);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                // Optional: Log the error
                Console.WriteLine($"Failed to send email: {ex.Message}");
                return false;
            }
        }
    }
}
