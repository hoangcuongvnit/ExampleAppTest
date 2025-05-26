using ApprovalWorkflow.FunctionApp.Models;
using ApprovalWorkflow.Infrastructure.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ApprovalWorkflow.FunctionApp.Activities
{
    public class SendEmailActivity
    {
        private readonly EmailServiceFactory _emailFactory;
        private readonly ILogger<SendEmailActivity> _logger;

        public SendEmailActivity(EmailServiceFactory emailFactory, ILogger<SendEmailActivity> logger)
        {
            _emailFactory = emailFactory;
            _logger = logger;
        }

        [Function(nameof(SendEmailActivity))]
        public async Task Run(
            [ActivityTrigger] EmailMessage message)
        {
            _logger.LogInformation($"Sending email to {message.To} with subject: {message.Subject}");

            var emailService = _emailFactory.Create("Gmail");
            await emailService.SendEmailAsync(
                message.To,
                message.Subject,
                message.Body
            );
        }
    }
}
