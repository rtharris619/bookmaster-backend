using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.OpenLibrary.OpenLibrarySearch;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.OpenLibrary;

internal sealed class OpenLibrarySearch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Endpoints.OpenLibrary + "/search", async (
            IQueryHandler<OpenLibrarySearchQuery, OpenLibrarySearchResponse> handler,
            CancellationToken cancellationToken,
            string q,
            int? offset = 0,
            int? limit = 3) =>
        {
            Result<OpenLibrarySearchResponse>? result = await handler.Handle(
                new OpenLibrarySearchQuery(q, offset, limit), cancellationToken);

            if (result is null)
            {
                return Results.Problem(detail: "Error in retrieving books from Open Library API.");
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
