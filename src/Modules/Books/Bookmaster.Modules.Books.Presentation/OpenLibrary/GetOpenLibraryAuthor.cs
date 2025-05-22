using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryAuthor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.OpenLibrary;

internal sealed class GetOpenLibraryAuthor : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Endpoints.OpenLibrary + "/authors/{key}", async(
            IQueryHandler<OpenLibraryAuthorQuery, OpenLibraryAuthorResponse> handler,
            CancellationToken cancellationToken,
            string key) =>
        { 
            Result<OpenLibraryAuthorResponse> result = await handler.Handle(
                new OpenLibraryAuthorQuery(key), cancellationToken);

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
