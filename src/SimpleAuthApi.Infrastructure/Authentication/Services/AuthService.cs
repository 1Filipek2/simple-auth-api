using Microsoft.EntityFrameworkCore;
using SimpleAuthApi.Application.Authentication.Commands;
using SimpleAuthApi.Application.Authentication.Interfaces;
using SimpleAuthApi.Domain.Entities;
using SimpleAuthApi.Infrastructure;
using SimpleAuthApi.Application.Common.Interfaces;
using SimpleAuthApi.Application.Common.Exceptions;

namespace SimpleAuthApi.Application.Authentication.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService; 

    public AuthService(ApplicationDbContext context, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService; 
    }

    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            throw new UserAlreadyExistsException($"User with email '{request.Email}' already exists");

        var user = new User(
            request.Username,
            request.Email,
            _passwordHasher.HashPassword(request.Password)
        );

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<string?> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null)
            return null;

        var passwordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
        if (!passwordValid)
            return null;

        var token = _tokenService.CreateToken(user);
        return token;
    }
}
