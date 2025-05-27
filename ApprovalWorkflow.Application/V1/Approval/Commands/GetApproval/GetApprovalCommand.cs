using ApprovalWorkflow.Application.Models;
using MediatR;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.GetApproval
{
    public record GetApprovalCommand(Guid Id) : IRequest<ApprovalResponse?>;
}
