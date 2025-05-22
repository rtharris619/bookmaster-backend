using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.Library.GetLibraryEntry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class GetLibraryEntry : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Endpoints.LibraryEntries + "/{id}", async (
            IQueryHandler<LibraryEntryQuery, LibraryEntryResponse> handler,
            CancellationToken cancellationToken,
            Guid Id) =>
        {
            Result<LibraryEntryResponse>? result = await handler.Handle(new LibraryEntryQuery(Id), cancellationToken);

            if (result is null)
            {
                return Results.Problem(detail: "Error in retrieving library entry");
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
