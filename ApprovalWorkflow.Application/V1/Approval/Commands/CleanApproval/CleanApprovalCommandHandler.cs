using ApprovalWorkflow.Application.Interfaces;
using MediatR;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.CleanApproval
{
    public class CleanApprovalCommandHandler : IRequestHandler<CleanApprovalCommand, bool>
    {
        private readonly IApprovalResponsitpory _approvalResponsitpory;
        public CleanApprovalCommandHandler(IApprovalResponsitpory approvalResponsitpory)
        {
            _approvalResponsitpory = approvalResponsitpory;
        }
        public async Task<bool> Handle(CleanApprovalCommand request, CancellationToken cancellationToken)
        {
            await _approvalResponsitpory.CleanApprovalsRequest();
            return true;
        }
    }
}
