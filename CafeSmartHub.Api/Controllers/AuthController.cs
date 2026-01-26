using CafeSmartHub.Api.Auth;
using CafeSmartHub.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CafeSmartHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtTokenService _jwt;

    public AuthController(JwtTokenService jwt)
    {
        _jwt = jwt;
    }

    [HttpPost("login")]
    public ActionResult<LoginResponseDto> Login([FromBody] LoginRequestDto req)
    {
        if (string.IsNullOrWhiteSpace(req.Username) || string.IsNullOrWhiteSpace(req.Password))
        {
            return BadRequest(new LoginResponseDto
            {
                Ok = false,
                Message = "Debe ingresar usuario y contraseña."
            });
        }

        var user = InMemoryUsersStore.Validate(req.Username, req.Password);
        if (user is null)
        {
            return Unauthorized(new LoginResponseDto
            {
                Ok = false,
                Message = "Credenciales inválidas."
            });
        }

        var token = _jwt.CreateToken(user.Username, user.Role);

        return Ok(new LoginResponseDto
        {
            Ok = true,
            Message = "Login OK",
            Token = token,
            Role = user.Role,
            Username = user.Username
        });
    }
}
