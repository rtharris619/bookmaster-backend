using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Modules.Books.Features.Books.CreateBook;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Bookmaster.Modules.Books.Features.Libraries.CreateLibraryEntry;
using Microsoft.AspNetCore.Http;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class CreateLibraryEntry : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost("library-entry", async (ISender sender, CreateLibraryEntryRequest request) =>
        {
            Result<Guid> result = await sender.Send(new CreateLibraryEntryCommand(request.GoogleBookId, request.PersonId));

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }

    internal sealed class CreateLibraryEntryRequest
    {
        public string GoogleBookId { get; init; }
        public Guid PersonId { get; init; }
    }
}
