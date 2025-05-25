using MediatR;
using UserManagement.WebApp.Application.Common;
using UserManagement.WebApp.Application.V1.Auth.Dtos;
namespace UserManagement.WebApp.Application.V1.Auth.Commands.SignIn
{
    public record class SignInCommand(
        string UserName,
        string Password
    ) : IRequest<Result<SignInResponse>>;
}
