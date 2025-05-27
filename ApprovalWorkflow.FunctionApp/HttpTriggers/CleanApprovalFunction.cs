using ApprovalWorkflow.Application.V1.Approval.Commands.CleanApproval;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ApprovalWorkflow.FunctionApp.HttpTriggers
{
    public class CleanApprovalFunction
    {
        private readonly IMediator _mediator;

        public CleanApprovalFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Function(nameof(CleanApprovalFunction))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "approval/clean")] HttpRequestData req,
            [DurableClient] DurableTaskClient client,
            FunctionContext context)
        {
            var logger = context.GetLogger("ApproveFunction");

            await _mediator.Send(new CleanApprovalCommand());

            logger.LogInformation("Approval cleaning success!");

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync("");

            return response;
        }
    }

}
