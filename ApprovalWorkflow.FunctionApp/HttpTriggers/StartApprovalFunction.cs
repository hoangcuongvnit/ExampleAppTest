using ApprovalWorkflow.Application.Common;
using ApprovalWorkflow.Application.Models;
using ApprovalWorkflow.Application.V1.Approval.Commands.GetApproval;
using ApprovalWorkflow.Application.V1.Approval.Commands.UpdateInstanceId;
using ApprovalWorkflow.FunctionApp.Orchestrations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ApprovalWorkflow.FunctionApp.HttpTriggers
{
    public class StartApprovalFunction
    {
        private readonly IMediator _mediator;
        public StartApprovalFunction(IMediator mediator)
        {
            _mediator = mediator;
        }


        [Function("StartApproval")]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "start-approval/{requestId}")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            Guid requestId,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger($"StartApproval: {requestId}");
            if (requestId == Guid.Empty)
            {
                var responseB = req.CreateResponse(HttpStatusCode.BadRequest);
                await responseB.WriteStringAsync("Invalid request ID.");
                return responseB;
            }

            var approvalResponse = await _mediator.Send(new GetApprovalCommand(requestId));
            if (approvalResponse == null)
            {
                var responseC = req.CreateResponse(HttpStatusCode.NotFound);
                await responseC.WriteStringAsync($"Approval request with ID {requestId} not found.");
                return responseC;
            }

            var approvalRequest = new ApprovalRequest
            {
                Id = approvalResponse.Id,
                RequestedBy = approvalResponse.RequestedBy,
                RequestedEmail = approvalResponse.RequestedEmail
            };
            // Start the orchestration
            string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
                nameof(ApprovalOrchestration),
                approvalRequest
            );

            await _mediator.Send(new UpdateInstanceIdCommand(requestId, instanceId));

            logger.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            var response = new Result<string>(instanceId, true);
            var httpResponse = req.CreateResponse(HttpStatusCode.OK);
            await httpResponse.WriteAsJsonAsync(response);
            return httpResponse;
        }
    }

}
