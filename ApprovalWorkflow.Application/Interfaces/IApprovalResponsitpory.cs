using ApprovalWorkflow.Application.Models;
using ApprovalWorkflow.Domain.Entities;

namespace ApprovalWorkflow.Application.Interfaces
{
    public interface IApprovalResponsitpory
    {
        Task AddApprovalRequest(ApprovalRequest request);
        Task<Approval?> GetApprovalRequest(Guid id);
        Task UpdateApprovalRequest(UpdateApprovalRequest request);
        Task<IEnumerable<Approval>> GetAllApprovalRequests();
        Task UpdateApprovalInstanceIdRequest(Guid id, string instanceId);
        Task CleanApprovalsRequest();
    }
}
