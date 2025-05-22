using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryBook;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.OpenLibrary;

internal sealed class GetOpenLibraryBook : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Endpoints.OpenLibrary + "/books/{key}", async(
            IQueryHandler<OpenLibraryBookQuery, OpenLibraryBookResponse> handler,
            CancellationToken cancellationToken,
            string key) =>
        {
            Result<OpenLibraryBookResponse> result = await handler.Handle(new OpenLibraryBookQuery(key), cancellationToken);

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
