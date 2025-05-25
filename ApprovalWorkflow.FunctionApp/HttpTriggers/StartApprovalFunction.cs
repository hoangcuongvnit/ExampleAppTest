using ApprovalWorkflow.Application.Models;
using ApprovalWorkflow.FunctionApp.Orchestrations;
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
        [Function("StartApproval")]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("StartApproval");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var approvalRequest = JsonSerializer.Deserialize<ApprovalRequest>(requestBody);

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

            logger.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            var response = req.CreateResponse(HttpStatusCode.Accepted);
            await response.WriteStringAsync($"Approval started. Orchestration ID = {instanceId}");

            return response;
        }
    }

}
