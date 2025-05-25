using ApprovalWorkflow.FunctionApp.Constants;
using ApprovalWorkflow.FunctionApp.HttpTriggers;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Text;

namespace ApprovalWorkflow.FunctionApp.UnitTests.HttpTriggers
{
    [TestClass] // Ensure the class is marked as a test class
    public class ApproveFunctionTest
    {
        [TestMethod] // Ensure the method is marked as a test method
        public async Task RunAsync_ShouldRaiseApprovalEvent_AndReturnOkResponse()
        {
            // Arrange
            var instanceId = "test-instance";
            var mockClient = new Mock<DurableTaskClient>();
            mockClient
                .Setup(c => c.RaiseEventAsync(
                    instanceId,
                    ApprovalConstant.ApprovalEventName,
                    ApprovalConstant.ApprovalApproved,
                    default))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var mockLogger = new Mock<ILogger>();
            var mockFunctionContext = new Mock<FunctionContext>();
            mockFunctionContext
                .Setup(c => c.GetLogger(It.IsAny<string>()))
                .Returns(mockLogger.Object);

            var mockRequest = new Mock<HttpRequestData>(mockFunctionContext.Object);
            var responseBody = new MemoryStream();
            var response = new Mock<HttpResponseData>(mockFunctionContext.Object);
            response.SetupProperty(r => r.StatusCode);
            response.Setup(r => r.WriteStringAsync(It.IsAny<string>(), default))
                .Returns<string, CancellationToken>((s, _) =>
                {
                    var bytes = Encoding.UTF8.GetBytes(s);
                    responseBody.Write(bytes, 0, bytes.Length);
                    return Task.CompletedTask;
                });
            response.SetupGet(r => r.Body).Returns(responseBody);

            mockRequest.Setup(r => r.CreateResponse(HttpStatusCode.OK)).Returns(response.Object);

            var function = new ApproveFunction();

            // Act
            var result = await function.RunAsync(
                mockRequest.Object,
                mockClient.Object,
                instanceId,
                mockFunctionContext.Object);

            // Assert
            mockClient.Verify();
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

            responseBody.Position = 0;
            var responseText = new StreamReader(responseBody).ReadToEnd();
            StringAssert.Contains(responseText, $"Approval for instance '{instanceId}' has been submitted.");
        }
    }
}
