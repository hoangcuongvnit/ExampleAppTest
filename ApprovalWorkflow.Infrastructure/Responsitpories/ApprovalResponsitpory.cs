using ApprovalWorkflow.Application.Interfaces;
using ApprovalWorkflow.Application.Models;
using ApprovalWorkflow.Domain.Entities;
using ApprovalWorkflow.Domain.Enums;
using ApprovalWorkflow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApprovalWorkflow.Infrastructure.Responsitpories
{
    public class ApprovalResponsitpory : IApprovalResponsitpory
    {
        private readonly AppDbContext _context;

        public ApprovalResponsitpory(AppDbContext context)
        {
            _context = context;
        }

        private void CheckDateTimeKind(Approval approval)
        {
            if (approval.RequestedAt.Kind == DateTimeKind.Unspecified)
            {
                approval.RequestedAt = DateTime.SpecifyKind(approval.RequestedAt, DateTimeKind.Utc);
            }
        }

        public async Task AddApprovalRequest(ApprovalRequest request)
        {
            var approval = new Approval
            {
                Id = Guid.NewGuid(),
                RequestedBy = request.RequestedBy,
                RequestedEmail = request.RequestedEmail,
                RequestedAt = request.RequestedAt,
            };
            CheckDateTimeKind(approval);

            _context.Approvals.Add(approval);
            await _context.SaveChangesAsync();
        }

        public async Task<Approval?> GetApprovalRequest(Guid id)
        {
            return await _context.Approvals.FindAsync(id);
        }

        public async Task UpdateApprovalRequest(UpdateApprovalRequest request)
        {
            var approval = await _context.Approvals.FindAsync(request.Id);
            if (approval != null)
            {
                approval.RespondedBy = request.RespondedBy;
                approval.RespondedAt = request.RespondedAt;
                approval.Status = request.Status;
                approval.Comments = request.Comments;
                CheckDateTimeKind(approval);

                _context.Approvals.Update(approval);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Approval>> GetAllApprovalRequests()
        {
            return await _context.Approvals.ToListAsync();
        }

        public async Task UpdateApprovalInstanceIdRequest(Guid id, string instanceId)
        {
            var approval = await _context.Approvals.FindAsync(id);
            if (approval != null)
            {
                approval.InstanceId = instanceId;
                approval.Status = ApprovalStatus.Pending;
                CheckDateTimeKind(approval);

                _context.Approvals.Update(approval);
                await _context.SaveChangesAsync();
            }
        }
    }
}
