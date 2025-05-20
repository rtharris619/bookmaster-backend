using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryEditions;
using Bookmaster.Modules.Books.Features.OpenLibrary.OpenLibrarySearch;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.OpenLibrary;

internal sealed class GetOpenLibraryEditions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(Endpoints.OpenLibrary + "/works/{key}/editions", async(
            ISender sender,
            string key,
            int limit = 3,
            int offset = 0) =>
        {
            Result<OpenLibraryEditionResponse>? result = await sender.Send(
                new OpenLibraryEditionQuery(key, limit, offset));

            if (result is null)
            {
                return Results.Problem(detail: "Error in retrieving editions from Open Library API.");
            }

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
