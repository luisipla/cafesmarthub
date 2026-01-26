using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;


namespace CafeSmartHub.Client.Auth;

public class JwtAuthStateProvider : AuthenticationStateProvider
{
    private readonly TokenStorage _storage;
    public JwtAuthStateProvider(TokenStorage storage) => _storage = storage;

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _storage.GetTokenAsync();
        if (string.IsNullOrWhiteSpace(token))
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }

    public async Task SetTokenAsync(string token)
    {
        await _storage.SetTokenAsync(token);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        await _storage.ClearAsync();
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes)
                            ?? new Dictionary<string, object>();

        foreach (var kvp in keyValuePairs)
        {
            if (kvp.Key == "role" || kvp.Key.EndsWith("/role"))
                yield return new Claim(ClaimTypes.Role, kvp.Value.ToString() ?? "");
            else if (kvp.Key == "unique_name" || kvp.Key.EndsWith("/name"))
                yield return new Claim(ClaimTypes.Name, kvp.Value.ToString() ?? "");
        }
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        base64 = base64.Replace('-', '+').Replace('_', '/');
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
