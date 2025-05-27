namespace ApprovalWorkflow.Application.Models
{
    public record ApprovalResponse(
        Guid Id,
        string RequestedBy,
        string RequestedEmail
    );
}
