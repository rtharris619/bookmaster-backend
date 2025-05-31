using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Users.Features.Users.RegisterUser;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Users.Presentation.Users;

internal sealed class RegisterUser : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("users/register", async (
            ICommandHandler<RegisterUserCommand, Guid> handler,
            CancellationToken cancellationToken,
            RegisterUserRequest request) =>
        {
            Result<Guid> result = await handler.Handle(new RegisterUserCommand(
                request.Email,
                request.Password,
                request.FirstName,
                request.LastName), cancellationToken);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .AllowAnonymous();
    }

    internal sealed class RegisterUserRequest
    {
        public string Email { get; init; }

        public string Password { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }
    }
}
