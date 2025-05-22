using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.OpenLibrary;

internal sealed class GetOpenLibraryWork : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Endpoints.OpenLibrary + "/works/{key}", async(
            IQueryHandler<OpenLibraryWorkQuery, OpenLibraryWorkResponse> handler,
            CancellationToken cancellationToken,
            string key) =>
        {
            Result<OpenLibraryWorkResponse> result = await handler.Handle(
                new OpenLibraryWorkQuery(key), cancellationToken);

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
