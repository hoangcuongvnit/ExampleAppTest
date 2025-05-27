using ApprovalWorkflow.Domain.Enums;

namespace ApprovalWorkflow.Application.Models
{
    public class UpdateApprovalRequest
    {
        public Guid Id { get; set; }
        public string? InstanceId { get; set; }
        public string? RespondedBy { get; set; }
        public DateTime? RespondedAt { get; set; }
        public string? Comments { get; set; }
        public ApprovalStatus Status { get; set; }

        public UpdateApprovalRequest()
        {
        }

        public UpdateApprovalRequest(Guid id, string? instanceId, string? respondedBy, string? comments, ApprovalStatus status, DateTime? respondedAt)
        {
            Id = id;
            InstanceId = instanceId;
            RespondedBy = respondedBy;
            Comments = comments;
            Status = status;
            RespondedAt = respondedAt;
        }
    }
}
