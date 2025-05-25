namespace ApprovalWorkflow.Infrastructure.Configuration
{
    public record AppConfig(SendGridSettings SendGrid);

    public record SendGridSettings(string ApiKey, string Sender);
}
