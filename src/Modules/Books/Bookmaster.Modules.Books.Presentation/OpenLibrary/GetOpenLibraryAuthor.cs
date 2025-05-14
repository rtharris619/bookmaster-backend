using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryAuthor;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.OpenLibrary;

internal sealed class GetOpenLibraryAuthor : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(EndpointRoots.OpenLibrary + "/authors/{key}", async(
            ISender sender,
            string key) =>
        { 
            Result<OpenLibraryAuthorResponse> result = await sender.Send(
                new OpenLibraryAuthorQuery(key));

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
