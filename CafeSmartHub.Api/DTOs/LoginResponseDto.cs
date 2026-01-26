namespace CafeSmartHub.Api.Dtos;

public class LoginResponseDto
{
    public bool Ok { get; set; }
    public string Message { get; set; } = "";
    public string? Token { get; set; }
    public string? Role { get; set; }
    public string? Username { get; set; }
}
