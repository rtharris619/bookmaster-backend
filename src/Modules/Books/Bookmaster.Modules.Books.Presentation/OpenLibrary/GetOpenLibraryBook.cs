using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryBook;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.OpenLibrary;

internal sealed class GetOpenLibraryBook : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(EndpointRoots.OpenLibrary + "/books/{key}", 
            async(ISender sender, string key) =>
        {
            Result<OpenLibraryBookResponse> result = await sender.Send(new OpenLibraryBookQuery(key));

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
