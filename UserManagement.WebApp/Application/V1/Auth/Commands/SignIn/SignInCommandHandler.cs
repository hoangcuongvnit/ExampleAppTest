using MediatR;
using System.Text;
using UserManagement.WebApp.Application.Common;
using UserManagement.WebApp.Application.Enums;
using UserManagement.WebApp.Application.Extensions;
using UserManagement.WebApp.Application.V1.Auth.Dtos;

namespace UserManagement.WebApp.Application.V1.Auth.Commands.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, Result<SignInResponse>>
    {
        public async Task<Result<SignInResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            // Simulate user authentication logic
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Result<SignInResponse>.Failure("Email and password cannot be empty.");
            }
            await Task.Delay(100); // Simulate async operation

            // Simulate successful authentication
            var userId = Guid.NewGuid(); // Simulated user ID
            var userName = request.UserName;
            var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userId}:{userName}")); // Simulated token
            var expiresAt = DateTimeOffset.UtcNow.AddHours(1); // Token expiration time
            var response = new SignInResponse(
                userId,
                userName.Split('@')[0], // Simulated username from email
                GenderType.male.GetDescription(), //
                0, // Simulated age
                "FirstName", // Simulated first name
                "LastName", // Simulated last name
                token,
                expiresAt);

            return Result<SignInResponse>.Success(response);
        }
    }
}
