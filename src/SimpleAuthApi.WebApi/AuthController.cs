using Microsoft.AspNetCore.Mvc;
using SimpleAuthApi.Application.Authentication.Commands;
using SimpleAuthApi.Application.Authentication.Interfaces;

namespace SimpleAuthApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authService.RegisterAsync(request);
        if (!result) return BadRequest("Email already in use.");
        return Ok("User registered");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var token = await _authService.LoginAsync(request);
        if (token == null) return Unauthorized("Invalid credentials");

        return Ok(new { Token = token });
    }
}
