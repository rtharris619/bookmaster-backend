using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Common.Presentation.Endpoints;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Bookmaster.Modules.Books.Features.Library.CreateLibraryEntryObsolete;
using Microsoft.AspNetCore.Http;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class CreateLibraryEntryObsolete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost(Endpoints.LibraryEntries + "-obsolete", async (ISender sender, CreateLibraryEntryObsoleteRequest request) =>
        {
            Result<Guid> result = await sender.Send(new CreateLibraryEntryCommandObsolete(request.GoogleBookId, request.PersonId));

            return result.Match(Results.Ok, ApiResults.Problem);
        });
    }

    internal sealed class CreateLibraryEntryObsoleteRequest
    {
        public string GoogleBookId { get; init; }
        public Guid PersonId { get; init; }
    }
}
