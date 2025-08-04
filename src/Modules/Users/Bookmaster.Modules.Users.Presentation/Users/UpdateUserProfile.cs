using System.Security.Claims;
using System.Threading;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Common.Infrastructure.Authentication;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Users.Features.Users.UpdateUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Users.Presentation.Users;

internal sealed class UpdateUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPut("users/profile", async(
            ICommandHandler<UpdateUserCommand> handler,
            CancellationToken cancellationToken,
            UpdateUserRequest request,
            ClaimsPrincipal claims) =>
        {
            Result result = await handler.Handle(
                new UpdateUserCommand(claims.GetUserId(), request.FirstName, request.LastName),
                cancellationToken);

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization();
    }

    internal sealed class UpdateUserRequest
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}
