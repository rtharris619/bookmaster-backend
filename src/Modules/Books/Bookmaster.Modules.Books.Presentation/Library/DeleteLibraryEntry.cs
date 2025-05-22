using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Common.Presentation.Results;
using Bookmaster.Modules.Books.Features.Library.DeleteLibraryEntry;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Bookmaster.Modules.Books.Presentation.Library;

internal sealed class DeleteLibraryEntry : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routeBuilder)
    {
        routeBuilder.MapDelete(Endpoints.LibraryEntries + "/{id}", async (
            ICommandHandler<DeleteLibraryEntryCommand> handler,
            CancellationToken cancellationToken,
            Guid Id) =>
        {
            Result result = await handler.Handle(new DeleteLibraryEntryCommand(Id), cancellationToken);

            return result.Match(Results.NoContent, ApiResults.Problem);
        });
    }
}
