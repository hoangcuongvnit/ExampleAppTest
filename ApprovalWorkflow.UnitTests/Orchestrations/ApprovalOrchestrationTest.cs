using ApprovalWorkflow.Application.Models;
using ApprovalWorkflow.FunctionApp.Orchestrations;
using Microsoft.DurableTask;
using Moq;

namespace ApprovalWorkflow.FunctionApp.UnitTests.Orchestrations
{
    [TestClass]
    public class ApprovalOrchestrationTest
    {
        [TestMethod]
        public async Task ApprovalOrchestration_ThrowsOnNullInput()
        {
            // Arrange
            var mockContext = new Mock<TaskOrchestrationContext>();
            mockContext.Setup(c => c.GetInput<ApprovalRequest>()).Returns((ApprovalRequest)null);

            var orchestration = new ApprovalOrchestration();

            // Act & Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
            {
                await orchestration.Run(mockContext.Object);
            });
        }
    }
}
