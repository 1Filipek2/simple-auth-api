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
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var success = await _authService.RegisterAsync(request);
        if (!success) return BadRequest("Email already exists");
        return Ok("User registered");
    }
}
