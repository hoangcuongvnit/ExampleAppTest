using MediatR;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.CreateApproval
{
    public record CreateApprovalCommand(
        string RequestedBy,
        string RequestedEmail,
        string? Comments) : IRequest<bool>;
}
