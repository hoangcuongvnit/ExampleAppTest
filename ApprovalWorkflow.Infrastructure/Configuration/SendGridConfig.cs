namespace ApprovalWorkflow.Infrastructure.Configuration
{
    public record SendGridConfig(SendGridSettings SendGrid);

    public record SendGridSettings(string ApiKey, string Sender);
}
