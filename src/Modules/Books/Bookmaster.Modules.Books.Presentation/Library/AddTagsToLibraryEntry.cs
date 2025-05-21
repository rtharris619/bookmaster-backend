using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Features.Library.AddTagsToLibraryEntry;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class AddTagsToLibraryEntry : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost(Endpoints.LibraryEntries + "/{libraryEntryId}/tags", 
            async (ISender sender, Guid libraryEntryId, AddTagsToLibraryEntryRequest request) =>
        {
            Result result = await sender.Send(new AddTagsToLibraryEntryCommand(libraryEntryId, request.Tags));

            return result.Match(Results.NoContent, ApiResults.Problem);
        });
    }

    internal sealed class AddTagsToLibraryEntryRequest
    {
        public string[]? Tags { get; init; }
    }
}
