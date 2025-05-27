using ApprovalWorkflow.Application.V1.Approval.Commands.CleanApproval;
using ApprovalWorkflow.Application.V1.Approval.Commands.CreateApproval;
using ApprovalWorkflow.Application.V1.Approval.Commands.GetApproval;
using ApprovalWorkflow.Application.V1.Approval.Commands.GetApprovals;
using ApprovalWorkflow.Application.V1.Approval.Commands.UpdateApproval;
using ApprovalWorkflow.Application.V1.Approval.Commands.UpdateInstanceId;
using ApprovalWorkflow.FunctionApp.Configuration;
using ApprovalWorkflow.FunctionApp.DependencyInjection;
using ApprovalWorkflow.Infrastructure.Persistence;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using System.Text;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.Configure();

// Register MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssemblies(
        typeof(CreateApprovalCommandHandler).Assembly,
        typeof(GetApprovalCommandHandler).Assembly,
        typeof(GetApprovalsCommandHandler).Assembly,
        typeof(UpdateApprovalCommandHandler).Assembly,
        typeof(UpdateInstanceIdCommandHandler).Assembly,
        typeof(CleanApprovalCommandHandler).Assembly
    );
});

builder.Services.DIServiceCollection();
builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

// Load from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Register PostgreSQL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Build().Run();
