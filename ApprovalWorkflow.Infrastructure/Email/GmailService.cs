using ApprovalWorkflow.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ApprovalWorkflow.Infrastructure.Email
{
    public class GmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public GmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string message)
        {
            var smtpHost = "smtp.gmail.com";
            var smtpPort = 587;
            var gmailUser = _configuration["Gmail:Username"];
            var gmailPass = _configuration["Gmail:Password"];
            var from = _configuration["Gmail:Sender"] ?? gmailUser;

            using var client = new System.Net.Mail.SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new System.Net.NetworkCredential(gmailUser, gmailPass),
                EnableSsl = true
            };

            var mail = new System.Net.Mail.MailMessage(from, to, subject, message);

            await client.SendMailAsync(mail);
        }
    }
}
