using ApprovalWorkflow.FunctionApp.Constants;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ApprovalWorkflow.FunctionApp.HttpTriggers
{
    public class RejectFunction
    {
        [Function("Reject")]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "reject/{instanceId}")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            string instanceId,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("RejectFunction");

            // Optionally, parse who rejected or why
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var rejectionInfo = string.IsNullOrWhiteSpace(body) ? "Rejected" : body;

            // Raise event to orchestration
            await client.RaiseEventAsync(instanceId,
                ApprovalConstant.ApprovalEventName,
                ApprovalConstant.ApprovalRejected);

            logger.LogInformation($"Sent rejection to instance {instanceId}");

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"Approval instance {instanceId} was rejected.");

            return response;
        }
    }

}
