using Bookmaster.Common.Domain;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Bookmaster.Modules.Books.Features.Library.CreateLibraryEntry;
using Microsoft.AspNetCore.Http;
using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class CreateLibraryEntry : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapPost(Endpoints.LibraryEntries, async (
            ICommandHandler<CreateLibraryEntryCommand, Guid> handler, 
            CancellationToken cancellationToken,
            CreateLibraryEntryRequest request) =>
        {
            Result<Guid> result = await handler.Handle(
                new CreateLibraryEntryCommand(request.GoogleBookId, request.PersonId), 
                cancellationToken);

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization();
    }

    internal sealed class CreateLibraryEntryRequest
    {
        public string GoogleBookId { get; init; }
        public Guid PersonId { get; init; }
    }
}
