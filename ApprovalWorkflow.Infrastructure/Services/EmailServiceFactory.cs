using ApprovalWorkflow.Application.Interfaces;
using ApprovalWorkflow.Infrastructure.Email;
using Microsoft.Extensions.DependencyInjection;

namespace ApprovalWorkflow.Infrastructure.Services
{
    public class EmailServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public EmailServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IEmailService Create(string providerName)
        {
            return providerName.ToLower() switch
            {
                "sendgrid" => _serviceProvider.GetRequiredService<SendGridEmailService>(),
                "gmail" => _serviceProvider.GetRequiredService<GmailService>(),
                // Add other providers if needed
                _ => throw new InvalidOperationException("Unknown email provider.")
            };
        }
    }
}
