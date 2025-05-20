using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.Library.LibraryEntrySearch;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class LibraryEntrySearch : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Endpoints.LibraryEntries + "/search", async (ISender sender, Guid personId, string? q = null) =>
        {
            Result<LibraryEntrySearchResponse>? result = await sender.Send(new LibraryEntrySearchQuery(personId, q));

            if (result is null)
            {
                return Results.Problem(detail: "Error in retrieving library entries");
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
