using ApprovalWorkflow.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ApprovalWorkflow.Infrastructure.Email
{
    public class SendGridEmailService(IConfiguration configuration, SendGridClient sendGridClient) : IEmailService
    {
        private readonly SendGridClient _sendGridClient = sendGridClient;
        private readonly string _senderEmail = configuration["SendGrid:Sender"]
            ?? throw new ArgumentNullException(nameof(configuration), "Sender email configuration is missing.");

        public async Task SendEmailAsync(string to, string subject, string message)
        {
            var from = new EmailAddress(_senderEmail, "Approval System");
            var toEmail = new EmailAddress(to);
            var msg = MailHelper.CreateSingleEmail(from, toEmail, subject, message, message);
            var response = await _sendGridClient.SendEmailAsync(msg);
        }
    }
}
