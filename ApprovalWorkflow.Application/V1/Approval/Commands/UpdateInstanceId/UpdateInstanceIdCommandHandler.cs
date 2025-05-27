using ApprovalWorkflow.Application.Interfaces;
using MediatR;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.UpdateInstanceId
{
    public class UpdateInstanceIdCommandHandler : IRequestHandler<UpdateInstanceIdCommand, bool>
    {
        private readonly IApprovalResponsitpory _approvalResponsitpory;
        public UpdateInstanceIdCommandHandler(IApprovalResponsitpory approvalResponsitpory)
        {
            _approvalResponsitpory = approvalResponsitpory;
        }
        public async Task<bool> Handle(UpdateInstanceIdCommand request, CancellationToken cancellationToken)
        {
            if (request == null || request.Id == Guid.Empty || string.IsNullOrEmpty(request.InstanceId))
            {
                return false;
            }
            await _approvalResponsitpory.UpdateApprovalInstanceIdRequest(request.Id, request.InstanceId);
            return true;
        }
    }
}
