namespace UserManagement.WebApp.Application.V1.Auth.Dtos
{
    public class SignInResponse(
        Guid userId,
        string username,
        string gender,
        int age,
        string firstName,
        string lastName,
        string token,
        DateTimeOffset expiresAt
    )
    {

        public Guid UserId { get; } = userId;
        public string Username { get; } = username;
        public string Gender { get; } = gender;
        public int Age { get; } = age;
        public string FirstName { get; } = firstName;
        public string LastName { get; } = lastName;
        public string Token { get; } = token;
        public DateTimeOffset ExpiresAt { get; } = expiresAt;
    }
}
