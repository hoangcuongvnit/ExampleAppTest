using ApprovalWorkflow.Domain.Enums;
using MediatR;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.UpdateApproval
{
    public record UpdateApprovalCommand(
        Guid Id,
        string? RespondedBy,
        string? Comments,
        ApprovalStatus? Status
        ) : IRequest<bool>;
}
