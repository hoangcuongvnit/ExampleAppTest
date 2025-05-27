using ApprovalWorkflow.Application.Common;
using ApprovalWorkflow.Application.V1.Approval.Commands.GetApprovals;
using ApprovalWorkflow.Domain.Entities;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ApprovalWorkflow.FunctionApp.HttpTriggers
{
    public class GetApprovalsFunction
    {
        private readonly IMediator _mediator;

        public GetApprovalsFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Function(nameof(GetApprovalsFunction))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "approves")] HttpRequestData req,
            FunctionContext context)
        {
            var logger = context.GetLogger("ApproveFunction");
            logger.LogInformation("Processing GetApprovals request.");

            var result = await _mediator.Send(new GetApprovalsCommand());

            var response = new Result<IEnumerable<Approval>>(result, true);
            var httpResponse = req.CreateResponse(HttpStatusCode.OK);
            await httpResponse.WriteAsJsonAsync(response);
            return httpResponse;
        }
    }
}
