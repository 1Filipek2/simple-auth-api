using SimpleAuthApi.Application.Authentication.Commands;

namespace SimpleAuthApi.Application.Authentication.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterAsync(RegisterRequest request);
    Task<string?> LoginAsync(LoginRequest request);
}
