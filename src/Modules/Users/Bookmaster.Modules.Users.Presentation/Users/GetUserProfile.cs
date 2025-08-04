using System.Security.Claims;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Users.Features.Users.GetUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Bookmaster.Common.Infrastructure.Authentication;

namespace Bookmaster.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("users/profile", async (
            ClaimsPrincipal claims,
            IQueryHandler<GetUserQuery, UserResponse> handler,
            CancellationToken cancellationToken) =>
        {
            Result<UserResponse> result = await handler.Handle(new GetUserQuery(claims.GetUserId()), cancellationToken);
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization();
    }
}
