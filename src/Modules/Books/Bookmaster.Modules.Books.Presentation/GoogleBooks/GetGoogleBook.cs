using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.GoogleBooks.GetGoogleBook;
using Bookmaster.Modules.Books.Features.GoogleBooks.GoogleBookSearch;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.GoogleBooks;

internal sealed class GetGoogleBook : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Endpoints.GoogleBooks + "/{googleBookId}", async (
            IQueryHandler<GetGoogleBookQuery, GoogleBookSearchResponseItem> handler,
            CancellationToken cancellationToken,
            string GoogleBookId) =>
        {
            Result<GoogleBookSearchResponseItem>? result = await handler.Handle(
                new GetGoogleBookQuery(GoogleBookId), cancellationToken);

            if (result is null)
            {
                return Results.Problem(detail: "Error in retrieving book from Google Books API.");
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
