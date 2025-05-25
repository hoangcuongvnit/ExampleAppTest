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
    [TestClass]
    public class RejectFunctionTest
    {
        [TestMethod]
        public async Task RejectFunction_ReturnsOkAndRaisesEvent()
        {
            // Arrange
            var mockClient = new Mock<DurableTaskClient>();
            var mockReq = new Mock<HttpRequestData>(MockBehavior.Strict, null);
            var mockContext = new Mock<FunctionContext>();
            var mockLogger = new Mock<ILogger>();

            mockContext.Setup(c => c.GetLogger(It.IsAny<string>())).Returns(mockLogger.Object);

            var response = new Mock<HttpResponseData>(mockReq.Object);
            response.SetupProperty(r => r.StatusCode);
            response.Setup(r => r.WriteStringAsync(It.IsAny<string>(), default)).Returns(Task.CompletedTask);

            mockReq.Setup(r => r.CreateResponse(HttpStatusCode.OK)).Returns(response.Object);
            mockReq.Setup(r => r.Body).Returns(new MemoryStream(Encoding.UTF8.GetBytes("")));

            var function = new RejectFunction();

            // Act
            var result = await function.RunAsync(mockReq.Object, mockClient.Object, "test-instance", mockContext.Object);

            // Assert
            mockClient.Verify(c => c.RaiseEventAsync("test-instance", ApprovalConstant.ApprovalEventName, ApprovalConstant.ApprovalRejected, default), Times.Once);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
        }
    }
}
