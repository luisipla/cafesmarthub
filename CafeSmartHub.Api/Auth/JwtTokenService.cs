using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CafeSmartHub.Api.Auth;

public class JwtTokenService
{
    private readonly JwtSettings _jwt;

    public JwtTokenService(IOptions<JwtSettings> jwtOptions)
    {
        _jwt = jwtOptions.Value;
        if (string.IsNullOrWhiteSpace(_jwt.Key))
            throw new InvalidOperationException("JWT Key no configurada en appsettings.json");
    }

    public string CreateToken(string username, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.ExpMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
