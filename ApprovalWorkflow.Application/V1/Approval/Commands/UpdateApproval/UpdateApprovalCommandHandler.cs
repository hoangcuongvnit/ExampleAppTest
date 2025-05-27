using ApprovalWorkflow.Application.Interfaces;
using ApprovalWorkflow.Application.Models;
using ApprovalWorkflow.Domain.Enums;
using MediatR;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.UpdateApproval
{
    public class UpdateApprovalCommandHandler : IRequestHandler<UpdateApprovalCommand, bool>
    {
        private readonly IApprovalResponsitpory _approvalResponsitpory;
        public UpdateApprovalCommandHandler(IApprovalResponsitpory approvalResponsitpory)
        {
            _approvalResponsitpory = approvalResponsitpory;
        }
        public async Task<bool> Handle(UpdateApprovalCommand request, CancellationToken cancellationToken)
        {
            var approvalRequest = await _approvalResponsitpory.GetApprovalRequest(request.Id);
            if (approvalRequest == null)
            {
                return false; // Approval request not found
            }

            var updateApprovalRequest = new UpdateApprovalRequest
            {
                Id = request.Id,
                Status = request.Status ?? ApprovalStatus.Requested,
                Comments = request.Comments ?? approvalRequest.Comments,
                RespondedBy = request.RespondedBy ?? approvalRequest.RequestedBy,
                RespondedAt = request.Status != null ? DateTime.UtcNow : approvalRequest.RespondedAt
            };

            await _approvalResponsitpory.UpdateApprovalRequest(updateApprovalRequest);
            return true;
        }
    }
}
