using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryWork;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.OpenLibrary;

internal sealed class GetOpenLibraryWork : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapGet(EndpointRoots.OpenLibrary + "/works/{key}", async(
            ISender sender,
            string key) =>
        {
            Result<OpenLibraryWorkResponse> result = await sender.Send(
                new OpenLibraryWorkQuery(key));

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }
}
