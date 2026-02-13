using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAuthApi.Infrastructure;
using SimpleAuthApi.Application.Authentication.DTOs;
using System.Security.Claims;
using SimpleAuthApi.Application.Common.Extensions;

namespace SimpleAuthApi.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class UsersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UsersController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {        
        var userId = User.GetUserId();
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return NotFound();

        var response = new UserResponse(user.Id, user.Username, user.Email);
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        var userId = User.GetUserId();
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUsername(UpdateUserRequest request)
    {
        var userId = User.GetUserId();
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return NotFound();

        _context.Entry(user).Property(u => u.Username).CurrentValue = request.Username;

        await _context.SaveChangesAsync();

        var response = new UserResponse(user.Id, request.Username, user.Email);
        return Ok(response);
    }
}
