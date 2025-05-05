using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.Library.AddTagsToLibraryEntry;
using Bookmaster.Modules.Books.Features.Library.CreateLibraryEntry;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using static Bookmaster.Modules.Books.Presentation.Library.CreateLibraryEntry;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class AddTagsToLibraryEntry : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost(EndpointRoots.LibraryEntries + "/tags", async (ISender sender, AddTagsToLibraryEntryRequest request) =>
        {
            Result result = await sender.Send(new AddTagsToLibraryEntryCommand(request.LibraryEntryId, request.Tags));

            return result.Match(Results.NoContent, ApiResults.Problem);
        });
    }

    internal sealed class AddTagsToLibraryEntryRequest
    {
        public Guid LibraryEntryId { get; init; }
        public string[] Tags { get; init; }
    }
}
