namespace UserManagement.WebApp.Application.V1.Auth.Dtos
{
    public record SignInRequest(string UserName, string Password, bool RememberMe = false);
}
