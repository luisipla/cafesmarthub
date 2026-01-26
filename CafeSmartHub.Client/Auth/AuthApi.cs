using System.Net.Http.Json;

namespace CafeSmartHub.Client.Auth;

public class AuthApi
{
    private readonly HttpClient _http;
    public AuthApi(HttpClient http) => _http = http;

    public async Task<LoginResponse> LoginAsync(string username, string password)
    {
        var resp = await _http.PostAsJsonAsync("api/auth/login", new LoginRequest
        {
            Username = username,
            Password = password
        });

        if (!resp.IsSuccessStatusCode)
        {
            var msg = resp.StatusCode == System.Net.HttpStatusCode.Unauthorized
                ? "Credenciales inválidas."
                : "Error al iniciar sesión.";
            return new LoginResponse { Ok = false, Message = msg };
        }

        var data = await resp.Content.ReadFromJsonAsync<LoginResponse>();
        return data ?? new LoginResponse { Ok = false, Message = "Respuesta inválida del servidor." };
    }

    public class LoginRequest
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }

    public class LoginResponse
    {
        public bool Ok { get; set; }
        public string Message { get; set; } = "";
        public string? Token { get; set; }
        public string? Role { get; set; }
        public string? Username { get; set; }
    }
}
