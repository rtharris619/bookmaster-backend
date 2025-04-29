using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.Books.GetGoogleBook;
using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Books;

internal sealed class GetGoogleBook : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet("books/{googleBookId}", async (
            ISender sender,
            string GoogleBookId) =>
        {
            Result<GoogleBookSearchResponseItem>? result = await sender.Send(
                new GetGoogleBookQuery(GoogleBookId));

            if (result is null)
            {
                return Results.Problem(detail: "Error in retrieving book from Google Books API.");
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
