using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Security;

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
                // Tạo email message
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_configuration["EmailSettings:FromName"],
                                                           _configuration["EmailSettings:FromEmail"]));
                emailMessage.To.Add(MailboxAddress.Parse(toEmail));
                emailMessage.Subject = subject;

                var builder = new BodyBuilder { HtmlBody = body };
                emailMessage.Body = builder.ToMessageBody();

                // Lấy cấu hình SMTP
                var smtpServer = _configuration["EmailSettings:SmtpServer"];
                var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
                var smtpUser = _configuration["EmailSettings:SmtpUser"];
                var smtpPass = _configuration["EmailSettings:SmtpPass"];

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(smtpUser, smtpPass);
                    await client.SendAsync(emailMessage, default);
                    await client.DisconnectAsync(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                // Log exception nếu cần
                return false;
            }
        }
    }
}
