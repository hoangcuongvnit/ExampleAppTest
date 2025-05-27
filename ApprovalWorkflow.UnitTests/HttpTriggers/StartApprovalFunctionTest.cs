using ApprovalWorkflow.Application.Models;
using ApprovalWorkflow.FunctionApp.HttpTriggers;
using ApprovalWorkflow.FunctionApp.Orchestrations;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ApprovalWorkflow.FunctionApp.UnitTests.HttpTriggers
{
    [TestClass]
    public class StartApprovalFunctionTest
    {
        [TestMethod]
        public async Task StartApprovalFunction_ReturnsAcceptedAndSchedulesOrchestration()
        {
            
        }
    }
}
