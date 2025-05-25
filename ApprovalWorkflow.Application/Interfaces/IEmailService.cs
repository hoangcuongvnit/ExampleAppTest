namespace ApprovalWorkflow.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string message);
    }
}
