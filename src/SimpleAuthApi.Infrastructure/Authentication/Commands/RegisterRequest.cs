namespace SimpleAuthApi.Application.Authentication.Commands;

public record RegisterRequest(
    string Username,
    string Email,
    string Password
);
