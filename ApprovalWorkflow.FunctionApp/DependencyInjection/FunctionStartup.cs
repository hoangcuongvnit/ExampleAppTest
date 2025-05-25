using ApprovalWorkflow.Application.Interfaces;
using ApprovalWorkflow.Infrastructure.Email;
using ApprovalWorkflow.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ApprovalWorkflow.FunctionApp.DependencyInjection
{
    public static class FunctionStartup
    {
        public static void DIServiceCollection(this IServiceCollection services)
        {
            services.AddSingleton<IEmailService, SendGridEmailService>();
            services.AddSingleton<SendGridEmailService>();
            services.AddSingleton<EmailServiceFactory>();
        }
    }
}
