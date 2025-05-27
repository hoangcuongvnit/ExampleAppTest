using MediatR;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.CleanApproval
{
    public record CleanApprovalCommand() : IRequest<bool>;
}
