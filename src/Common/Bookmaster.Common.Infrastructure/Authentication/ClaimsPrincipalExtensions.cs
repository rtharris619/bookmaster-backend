using System.Security.Claims;
using Bookmaster.Common.Features.Exceptions;

namespace Bookmaster.Common.Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirst(CustomClaims.Sub)?.Value;

        return Guid.TryParse(userId, out Guid parsedUserId) ? 
            parsedUserId : 
            throw new BookmasterException("User identifier is unavailable");
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        return principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? 
            throw new BookmasterException("User identity is unavailable");
    }
}
