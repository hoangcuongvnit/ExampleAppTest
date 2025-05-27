using ApprovalWorkflow.Application.Common;
using ApprovalWorkflow.Application.Models;
using ApprovalWorkflow.Application.V1.Approval.Commands.UpdateInstanceId;
using ApprovalWorkflow.FunctionApp.Orchestrations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

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
        [Authorize]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            Guid requestId,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("StartApproval");
            ApprovalRequest? approvalRequest;
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                approvalRequest = JsonSerializer.Deserialize<ApprovalRequest>(requestBody);
            }
            catch (JsonException ex)
            {
                logger.LogError(ex, "Failed to deserialize request body.");
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequest.WriteStringAsync("Invalid JSON format in request body.");
                return badRequest;
            }

            if (approvalRequest == null || string.IsNullOrEmpty(approvalRequest.RequestedBy))
            {
                var badRequest = req.CreateResponse(HttpStatusCode.BadRequest);
                await badRequest.WriteStringAsync("Invalid request");
                return badRequest;
            }

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
