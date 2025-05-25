using ApprovalWorkflow.Application.Interfaces;

namespace ApprovalWorkflow.Application.Services
{
    public class EmailNotificationService
    {
        private readonly IEmailService _emailService;

        public EmailNotificationService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendApprovalStartedAsync(string to, string approvalItem)
        {
            var subject = "Approval Started";
            var body = $"Your approval request for \"{approvalItem}\" has started and is awaiting review.";
            await _emailService.SendEmailAsync(to, subject, body);
        }

        public async Task SendApprovalCompletedAsync(string to, string approvalItem, bool approved)
        {
            var status = approved ? "approved" : "rejected";
            var subject = $"Approval {status.ToUpper()}";
            var body = $"Your approval request for \"{approvalItem}\" has been {status}.";
            await _emailService.SendEmailAsync(to, subject, body);
        }
    }
}
