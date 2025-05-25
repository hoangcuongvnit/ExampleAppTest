namespace UserManagement.WebApp.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string username, IEnumerable<string> roles);
    }
}
