namespace ApprovalWorkflow.Domain.Events
{
    public class ApprovalCompletedEvent
    {
        public Guid ApprovalId { get; }
        public string Status { get; } // "Approved" or "Rejected"
        public string RespondedBy { get; }
        public DateTime RespondedAt { get; }

        public ApprovalCompletedEvent(Guid approvalId, string status, string respondedBy, DateTime respondedAt)
        {
            ApprovalId = approvalId;
            Status = status;
            RespondedBy = respondedBy;
            RespondedAt = respondedAt;
        }
    }
}
