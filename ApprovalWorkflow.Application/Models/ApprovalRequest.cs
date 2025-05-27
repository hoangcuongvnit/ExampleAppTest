namespace ApprovalWorkflow.Application.Models
{
    public class ApprovalRequest
    {
        public Guid Id { get; set; }
        public string RequestedBy { get; set; }
        public string RequestedEmail { get; set; }
        public string? Comments { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;

        public ApprovalRequest() { }
        public ApprovalRequest(
                Guid id,
                string requestedBy,
                string requestedEmail,
                string? comments = null
            )
        {
            Id = id;
            RequestedBy = requestedBy;
            RequestedEmail = requestedEmail;
            Comments = comments;
        }
    }
}
