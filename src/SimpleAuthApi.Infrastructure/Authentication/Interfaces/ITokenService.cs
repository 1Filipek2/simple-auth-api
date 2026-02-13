using SimpleAuthApi.Domain.Entities;

namespace SimpleAuthApi.Application.Common.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
}