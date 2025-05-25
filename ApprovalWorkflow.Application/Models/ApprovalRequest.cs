namespace ApprovalWorkflow.Application.Models
{
    public class ApprovalRequest
    {
        public string RequestedBy { get; set; }
        public string RequestedEmail { get; set; }
        public string ApprovalItem { get; set; } // Could be a description or a link to a task/document
        public DateTime RequestedAt { get; set; }

        public ApprovalRequest(string requestedBy, string requestedEmail, string approvalItem)
        {
            RequestedBy = requestedBy;
            RequestedEmail = requestedEmail;
            ApprovalItem = approvalItem;
            RequestedAt = DateTime.UtcNow;
        }
    }
}
