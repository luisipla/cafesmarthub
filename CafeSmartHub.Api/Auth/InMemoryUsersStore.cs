namespace CafeSmartHub.Api.Auth;

public record AppUser(string Username, string Password, string Role);

public static class InMemoryUsersStore
{
    // Usuarios
    private static readonly List<AppUser> _users = new()
    {
        new AppUser("admin", "admin", "Admin"),
        new AppUser("user",  "user",  "User")
    };

    public static AppUser? Validate(string username, string password)
        => _users.FirstOrDefault(u =>
            u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
            u.Password == password);
}
