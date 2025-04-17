using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.ApiResults;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Modules.Books.Features.Books.Create;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Books;

internal sealed class CreateBook : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("books", async (ISender sender, CreateBookRequest request) =>
        {
            Result<Guid> result = await sender.Send(new CreateBookCommand(request.GoogleBookId));

            if (result is null)
            {
                return Results.Problem(detail: "Error in retrieving book from Google Books API.");
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }

    internal sealed class CreateBookRequest
    {
        public string GoogleBookId { get; init; }
    }
}
