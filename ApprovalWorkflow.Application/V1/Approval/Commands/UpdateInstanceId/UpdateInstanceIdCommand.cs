using MediatR;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.UpdateInstanceId
{
    public record UpdateInstanceIdCommand(Guid Id, string InstanceId) : IRequest<bool>;
}
