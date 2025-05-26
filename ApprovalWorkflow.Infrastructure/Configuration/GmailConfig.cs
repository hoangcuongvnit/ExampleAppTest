namespace ApprovalWorkflow.Infrastructure.Configuration
{
    public record GmailConfig(GmailServerConfig Gmail);

    public record GmailServerConfig(string Username, string Password, string Sender);
}
