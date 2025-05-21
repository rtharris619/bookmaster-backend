using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.Library.OpenLibraryCreateLibraryEntry;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class OpenLibraryCreateLibraryEntry : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost(Endpoints.LibraryEntries + "/open-library", async (ISender sender, OpenLibraryCreateLibraryEntryRequest request) =>
        {
            Result<Guid> result = await sender.Send(new OpenLibraryCreateLibraryEntryCommand(request.OpenLibraryWorkKey));

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }

    internal sealed class OpenLibraryCreateLibraryEntryRequest
    {
        public string OpenLibraryWorkKey { get; init; }
    }
}
