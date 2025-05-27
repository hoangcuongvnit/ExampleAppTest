using ApprovalWorkflow.Application.Interfaces;
using MediatR;
using ApprovalEntity = ApprovalWorkflow.Domain.Entities.Approval;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.GetApprovals
{
    public class GetApprovalsCommandHandler : IRequestHandler<GetApprovalsCommand, IEnumerable<ApprovalEntity>>
    {
        private readonly IApprovalResponsitpory _approvalRepository;
        public GetApprovalsCommandHandler(IApprovalResponsitpory approvalRepository)
        {
            _approvalRepository = approvalRepository;
        }

        public async Task<IEnumerable<ApprovalEntity>> Handle(GetApprovalsCommand request, CancellationToken cancellationToken)
        {
            return await _approvalRepository.GetAllApprovalRequests();
        }
    }
}
