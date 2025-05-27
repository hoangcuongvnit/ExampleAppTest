using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalWorkflow.Application.V1.Approval.Commands.CleanApproval
{
    public record CleanApprovalCommand() : IRequest<bool>;
}
