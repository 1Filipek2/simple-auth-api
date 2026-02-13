using System.Security.Claims;

namespace SimpleAuthApi.Application.Common.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            throw new InvalidOperationException("User ID claim is missing.");

        return Guid.Parse(userIdClaim);
    }
}
