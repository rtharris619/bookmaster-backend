using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.EventBus;
using Bookmaster.Common.Features.Exceptions;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Features.People.UpdatePerson;
using Bookmaster.Modules.Users.IntegrationEvents;

namespace Bookmaster.Modules.Books.Presentation.People;

internal sealed class UserProfileUpdatedIntegrationEventHandler(
    ICommandHandler<UpdatePersonCommand> handler) 
    : IntegrationEventHandler<UserProfileUpdatedIntegrationEvent>
{
    public override async Task Handle(UserProfileUpdatedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        Result result = await handler.Handle(
            new UpdatePersonCommand(
                integrationEvent.UserId,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new BookmasterException(nameof(UpdatePersonCommand), result.Error);
        }
    }
}
