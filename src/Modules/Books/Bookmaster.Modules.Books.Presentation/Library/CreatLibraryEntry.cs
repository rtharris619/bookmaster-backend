using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.Library.CreateLibraryEntry;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class CreatLibraryEntry : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost(Endpoints.LibraryEntries, async (ISender sender, CreateLibraryEntryRequest request) =>
        {
            Result<Guid> result = await sender.Send(new CreateLibraryEntryCommand(request.OpenLibraryWorkKey));

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }

    internal sealed class CreateLibraryEntryRequest
    {
        public string OpenLibraryWorkKey { get; init; }
    }
}
