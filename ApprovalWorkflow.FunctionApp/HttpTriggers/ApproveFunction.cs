using ApprovalWorkflow.FunctionApp.Constants;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ApprovalWorkflow.FunctionApp.HttpTriggers
{
    public class ApproveFunction
    {
        [Function("Approve")]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "approve/{instanceId}")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            string instanceId,
            FunctionContext context)
        {
            var logger = context.GetLogger("ApproveFunction");

            // Raise the event "ApprovalResponse" with value "Approved"
            await client.RaiseEventAsync(instanceId,
                ApprovalConstant.ApprovalEventName,
                ApprovalConstant.ApprovalApproved);

            logger.LogInformation($"Approval event sent to orchestration instance '{instanceId}'.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"Approval for instance '{instanceId}' has been submitted.");

            return response;
        }
    }

}
