using ApprovalWorkflow.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

    [Table("approval", Schema = "approvals")]
    public class Approval
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("requested_by")]
        public string? RequestedBy { get; set; }

        [Column("requested_email")]
        public string? RequestedEmail { get; set; }

        [Column("instance_id")]
        public string? InstanceId { get; set; }

        [Column("status")]
        public ApprovalStatus Status { get; set; } //  Requested, Pending, Approved, Rejected

        [Column("comments")]
        public string? Comments { get; set; }

        [Column("requested_at")]
        public DateTime RequestedAt { get; set; }

        [Column("responded_at")]
        public DateTime? RespondedAt { get; set; }

        [Column("responded_by")]
        public string? RespondedBy { get; set; }
    }
}
