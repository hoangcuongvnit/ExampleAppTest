using MediatR;
using ApprovalEntity = ApprovalWorkflow.Domain.Entities.Approval;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.GetApprovals
{
    public record GetApprovalsCommand()
        : IRequest<IEnumerable<ApprovalEntity>>;
}
