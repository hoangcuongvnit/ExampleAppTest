using ApprovalWorkflow.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ApprovalWorkflow.Domain.Entities
{
    //CREATE TABLE "approvals".approval(
    //    id UUID PRIMARY KEY,
    //    requested_by VARCHAR(255),
    //    requested_email VARCHAR(255),
    //    instance_id VARCHAR(255),
    //    status INTEGER NOT NULL, -- Map to ApprovalStatus enum values
    //    comments TEXT,
    //    requested_at TIMESTAMP NOT NULL,
    //    responded_at TIMESTAMP,
    //    responded_by VARCHAR(255)
    //);

    public class Approval
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public string? RequestedBy { get; set; }
        public string? RequestedEmail { get; set; }
        public string? InstanceId { get; set; }
        public ApprovalStatus Status { get; set; } // Pending, Approved, Rejected
        public string? Comments { get; set; }

        public DateTime RequestedAt { get; set; }
        public DateTime? RespondedAt { get; set; }

        public string? RespondedBy { get; set; }

        public bool IsPending => Status == ApprovalStatus.Pending;
    }
}
