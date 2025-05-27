using ApprovalWorkflow.Application.Models;
using ApprovalWorkflow.Application.V1.Approval.Commands.UpdateApproval;
using ApprovalWorkflow.Domain.Enums;
using ApprovalWorkflow.FunctionApp.Activities;
using ApprovalWorkflow.FunctionApp.Constants;
using ApprovalWorkflow.FunctionApp.Models;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.DurableTask;

namespace ApprovalWorkflow.FunctionApp.Orchestrations
{
    public class ApprovalOrchestration
    {
        private readonly IMediator _mediator;

        public ApprovalOrchestration(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Function(nameof(ApprovalOrchestration))]
        public async Task Run(
        [OrchestrationTrigger] TaskOrchestrationContext context)
        {
            var approvalRequest = context.GetInput<ApprovalRequest>();
            if (approvalRequest == null || string.IsNullOrEmpty(approvalRequest.RequestedEmail))
            {
                throw new ArgumentNullException(nameof(approvalRequest), "Approval request cannot be null.");
            }

            // Step 1: Send email (activity function)
            await context.CallActivityAsync(nameof(SendEmailActivity), new EmailMessage
            (
                approvalRequest.RequestedEmail,
                EmailConstant.ApprovalStarted,
                EmailConstant.ApprovalStartedBody
            ));

            // Step 2: Wait for approval decision
            string result = await context.WaitForExternalEvent<string>(ApprovalConstant.ApprovalEventName);

            // Step 3: Notify based on result
            string subject = $"{EmailConstant.Approval} {result}";
            string body = $"{EmailConstant.ApprovalBody} {result.ToLower()}.";

            var updateApprovalCommand = new UpdateApprovalCommand(
                approvalRequest.Id,
                null,
                null,
                result.Equals(ApprovalConstant.ApprovalApproved, StringComparison.OrdinalIgnoreCase) ?
                    ApprovalStatus.Approved : ApprovalStatus.Rejected
            );
            // Update the approval request in the database
            var updateTask = _mediator.Send(updateApprovalCommand);

            // Step 4: Send notification email
            var emailTask = context.CallActivityAsync(nameof(SendEmailActivity), new EmailMessage
            (
                approvalRequest.RequestedEmail,
                subject,
                body
            ));

            await Task.WhenAll(updateTask, emailTask);
        }
    }
}
