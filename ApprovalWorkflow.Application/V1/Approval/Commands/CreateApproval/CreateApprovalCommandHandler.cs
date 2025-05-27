using ApprovalWorkflow.Application.Interfaces;
using ApprovalWorkflow.Application.Models;
using MediatR;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.CreateApproval
{
    public class CreateApprovalCommandHandler : IRequestHandler<CreateApprovalCommand, bool>
    {
        private readonly IApprovalResponsitpory _approvalResponsitpory;
        public CreateApprovalCommandHandler(IApprovalResponsitpory approvalResponsitpory)
        {
            _approvalResponsitpory = approvalResponsitpory;
        }
        public async Task<bool> Handle(CreateApprovalCommand request, CancellationToken cancellationToken)
        {
            var approvalRequest = new ApprovalRequest
            {
                RequestedBy = request.RequestedBy,
                RequestedEmail = request.RequestedEmail,
                Comments = request.Comments,
            };
            await _approvalResponsitpory.AddApprovalRequest(approvalRequest);
            return true;
        }
    }
}
