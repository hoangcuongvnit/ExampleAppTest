using MediatR;
using UserManagement.WebApp.Application.V1.Auth.Commands.SignIn;
using UserManagement.WebApp.Application.V1.Auth.Dtos;

namespace UserManagement.WebApp.Api
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapApprovalEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/v1/auth/sign-in", async (SignInRequest request, IMediator mediator) =>
            {
                var command = new SignInCommand(request.UserName, request.Password);
                var result = await mediator.Send(command);
                return result.IsSuccess
                    ? Results.Ok(result)
                    : Results.BadRequest(result);
            })
            .WithName("SignIn").WithTags("Auth");
            return app;
        }
    }
}
