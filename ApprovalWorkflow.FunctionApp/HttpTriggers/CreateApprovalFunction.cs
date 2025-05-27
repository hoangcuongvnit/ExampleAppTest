using ApprovalWorkflow.Application.Common;
using ApprovalWorkflow.Application.V1.Approval.Commands.CreateApproval;
using ApprovalWorkflow.FunctionApp.Fakes;
using MediatR;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ApprovalWorkflow.FunctionApp.HttpTriggers
{
    public class CreateApprovalFunction
    {
        private readonly IMediator _mediator;

        public CreateApprovalFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Function(nameof(CreateApprovalFunction))]
        public async Task<HttpResponseData> RunAsync(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "approves")] HttpRequestData req,
            FunctionContext context)
        {
            var logger = context.GetLogger("ApproveFunction");
            logger.LogInformation("Processing GetApprovals request.");

            var name = RandomDataGenerator.GenerateName();
            var createApproval = new CreateApprovalCommand(
                name,
                RandomDataGenerator.GenerateEmail(name),
                RandomDataGenerator.GenerateComment()
            );
            var result = await _mediator.Send(createApproval);

            var response = new Result<bool>(result, true);
            var httpResponse = req.CreateResponse(HttpStatusCode.OK);
            await httpResponse.WriteAsJsonAsync(response);
            return httpResponse;
        }
    }
}
