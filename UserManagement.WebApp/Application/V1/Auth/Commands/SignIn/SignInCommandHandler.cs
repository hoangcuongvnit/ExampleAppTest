using MediatR;
using System.Text;
using UserManagement.WebApp.Application.Common;
using UserManagement.WebApp.Application.Common.Interfaces;
using UserManagement.WebApp.Application.Enums;
using UserManagement.WebApp.Application.Extensions;
using UserManagement.WebApp.Application.V1.Auth.Dtos;

namespace UserManagement.WebApp.Application.V1.Auth.Commands.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, Result<SignInResponse>>
    {
        private readonly ITokenService _tokenService;

        public SignInCommandHandler(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

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
            IEnumerable<string> roles = new List<string> { "Admin", "User" };
            var token = _tokenService.GenerateToken(userName, roles); // Simulated token generation
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
