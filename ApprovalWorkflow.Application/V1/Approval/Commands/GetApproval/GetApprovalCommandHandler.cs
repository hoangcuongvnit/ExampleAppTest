using ApprovalWorkflow.Application.Interfaces;
using ApprovalWorkflow.Application.Models;
using MediatR;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.GetApproval
{
    public class GetApprovalCommandHandler : IRequestHandler<GetApprovalCommand, ApprovalResponse?>
    {
        private readonly IApprovalResponsitpory _approvalRepository;
        public GetApprovalCommandHandler(IApprovalResponsitpory approvalRepository)
        {
            _approvalRepository = approvalRepository;
        }
        public async Task<ApprovalResponse?> Handle(GetApprovalCommand request, CancellationToken cancellationToken)
        {
            var approval = await _approvalRepository.GetApprovalRequest(request.Id);

            if (approval == null)
            {
                return null;
            }

            return new ApprovalResponse(approval.Id, approval.RequestedBy, approval.RequestedEmail);
        }
    }
}
