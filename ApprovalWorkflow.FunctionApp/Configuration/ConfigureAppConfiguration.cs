using ApprovalWorkflow.Infrastructure.Configuration;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection; // Ensure this namespace is included

namespace ApprovalWorkflow.FunctionApp.Configuration
{
    public static class ConfigureAppConfiguration
    {
        public static void Configure(this FunctionsApplicationBuilder builder)
        {
            // Use the Configuration property directly instead of ConfigurationBuilder
            var config = builder.Configuration;
            builder.Services.Configure<SendGridConfig>(config);
            builder.Services.Configure<GmailConfig>(config);
        }
    }
}
