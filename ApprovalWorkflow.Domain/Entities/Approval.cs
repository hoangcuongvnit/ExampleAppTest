using ApprovalWorkflow.Domain.Enums;

namespace ApprovalWorkflow.Domain.Entities
{
    public class Approval
    {
        public Guid Id { get; set; }
        public string? RequestedBy { get; set; }
        public string? RequestedEmail { get; set; }
        public ApprovalStatus Status { get; set; } // Pending, Approved, Rejected
        public string? Comments { get; set; }

        public DateTime RequestedAt { get; set; }
        public DateTime? RespondedAt { get; set; }

        public string? RespondedBy { get; set; }

        public bool IsPending => Status == ApprovalStatus.Pending;
    }
}
