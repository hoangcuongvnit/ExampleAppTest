using ApprovalWorkflow.Application.Interfaces;
using ApprovalWorkflow.Infrastructure.Email;
using ApprovalWorkflow.Infrastructure.Responsitpories;
using ApprovalWorkflow.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;

namespace ApprovalWorkflow.FunctionApp.DependencyInjection
{
    public static class FunctionStartup
    {
        public static void DIServiceCollection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<SendGridClient>(sp =>
                    new SendGridClient(configuration["SendGrid:ApiKey"]));
            services.AddSingleton<IEmailService, SendGridEmailService>();
            services.AddSingleton<IEmailService, GmailService>();
            services.AddSingleton<SendGridEmailService>();
            services.AddSingleton<GmailService>();
            services.AddSingleton<EmailServiceFactory>();

            services.AddScoped<IApprovalResponsitpory, ApprovalResponsitpory>();
        }
    }
}
