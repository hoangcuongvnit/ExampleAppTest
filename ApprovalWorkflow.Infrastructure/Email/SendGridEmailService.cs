using ApprovalWorkflow.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ApprovalWorkflow.Infrastructure.Email
{
    public class SendGridEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SendGridEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string message)
        {
            var apiKey = _configuration["SendGrid:ApiKey"];
            var sender = _configuration["SendGrid:Sender"];

            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(sender, "Approval System");
            var toEmail = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, message, message);
            var response = await client.SendEmailAsync(msg);

            // Optional: log or handle failure responses
        }
    }
}
