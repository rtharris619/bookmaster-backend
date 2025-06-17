using System.Security.Claims;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.Library.LibraryEntrySearch;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class LibraryEntrySearch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Endpoints.LibraryEntries + "/search", async (
            IQueryHandler<LibraryEntrySearchQuery, LibraryEntrySearchResponse> handler,
            CancellationToken cancellationToken,
            HttpContext httpContext,
            string? q = null) =>
        {
            string? subject = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
               ?? httpContext.User.FindFirst("sub")?.Value;

            if (subject is null)
            {
                return Results.Problem(detail: "Invalid or missing subject in JWT token");
            }

            Result<LibraryEntrySearchResponse>? result = await handler.Handle(new LibraryEntrySearchQuery(subject, q), cancellationToken);

            if (result is null)
            {
                return Results.Problem(detail: "Error in retrieving library entries");
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization();
    }
}
