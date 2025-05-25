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
            // Arrange
            var mockClient = new Mock<DurableTaskClient>();
            var mockReq = new Mock<HttpRequestData>(MockBehavior.Strict, null);
            var mockContext = new Mock<FunctionContext>();
            var mockLogger = new Mock<ILogger>();

            mockContext.Setup(c => c.GetLogger(It.IsAny<string>())).Returns(mockLogger.Object);

            var approvalRequest = new ApprovalRequest("user", "user@email.com", "sd");
            var requestBody = JsonSerializer.Serialize(approvalRequest);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
            mockReq.Setup(r => r.Body).Returns(stream);

            var response = new Mock<HttpResponseData>(mockReq.Object);
            response.SetupProperty(r => r.StatusCode);
            response.Setup(r => r.WriteStringAsync(It.IsAny<string>(), default)).Returns(Task.CompletedTask);

            mockReq.Setup(r => r.CreateResponse(HttpStatusCode.Accepted)).Returns(response.Object);

            mockClient.Setup(c => c.ScheduleNewOrchestrationInstanceAsync(
                nameof(ApprovalOrchestration),
                It.IsAny<ApprovalRequest>(),
                default)).ReturnsAsync("instance-123");

            var function = new StartApprovalFunction();

            // Act
            var result = await function.RunAsync(mockReq.Object, mockClient.Object, mockContext.Object);

            // Assert
            mockClient.Verify(c => c.ScheduleNewOrchestrationInstanceAsync(
                nameof(ApprovalOrchestration),
                It.IsAny<ApprovalRequest>(),
                default), Times.Once);
            Assert.AreEqual(HttpStatusCode.Accepted, result.StatusCode);
        }
    }
}
