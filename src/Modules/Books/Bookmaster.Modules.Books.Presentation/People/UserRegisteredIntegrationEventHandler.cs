using System.Diagnostics.Eventing.Reader;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.EventBus;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Features.Library.CreateLibraryEntry;
using Bookmaster.Modules.Books.Features.People;
using Bookmaster.Modules.Users.IntegrationEvents;

namespace Bookmaster.Modules.Books.Presentation.People;

internal sealed class UserRegisteredIntegrationEventHandler(
    ICommandHandler<CreatePersonCommand, Guid> handler)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(UserRegisteredIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        Result<Guid> result = await handler.Handle(
                new CreatePersonCommand(
                    integrationEvent.Id,
                    integrationEvent.Email,
                    integrationEvent.FirstName,
                    integrationEvent.LastName
                ),
                cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(nameof(CreatePersonCommand));
        }
    }
}
