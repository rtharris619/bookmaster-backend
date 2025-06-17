using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Bookmaster.Modules.Books.Features.Library.CreateLibraryEntry;
using Microsoft.AspNetCore.Http;
using Bookmaster.Common.Features.Messaging;
using System.Net.Http;
using System.Security.Claims;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class CreateLibraryEntry : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost(Endpoints.LibraryEntries, async (
            ICommandHandler<CreateLibraryEntryCommand, Guid> handler, 
            CancellationToken cancellationToken,
            HttpContext httpContext,
            CreateLibraryEntryRequest request) =>
        {
            string? subject = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? httpContext.User.FindFirst("sub")?.Value;

            if (subject is null)
            {
                return Results.Problem(detail: "Invalid or missing subject in JWT token");
            }

            Result<Guid> result = await handler.Handle(
                new CreateLibraryEntryCommand(request.GoogleBookId, subject), 
                cancellationToken);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization();
    }

    internal sealed class CreateLibraryEntryRequest
    {
        public string GoogleBookId { get; init; }
    }
}
