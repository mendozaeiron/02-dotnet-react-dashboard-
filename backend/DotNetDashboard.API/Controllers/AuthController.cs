using Microsoft.AspNetCore.Mvc;
using DotNetDashboard.API.Services;
using DotNetDashboard.API.Models;

namespace DotNetDashboard.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    public AuthController(AuthService authService) => _authService = authService;
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var response = await _authService.Authenticate(request.Username, request.Password);
        if (response == null) return Unauthorized(new { message = "Credenciales inválidas" });
        return Ok(response);
    }
}

public class LoginRequest { public string Username { get; set; } = ""; public string Password { get; set; } = ""; }
public class LoginResponse { public string Token { get; set; } = ""; public object User { get; set; } = new(); }
