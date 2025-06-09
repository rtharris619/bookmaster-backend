using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.EventBus;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Users.Domain.Users;
using Bookmaster.Modules.Users.Features.Users.GetUser;
using Bookmaster.Modules.Users.IntegrationEvents;
using Microsoft.Extensions.Logging;

namespace Bookmaster.Modules.Users.Features.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(
    IEventBus eventBus,
    IQueryHandler<GetUserQuery, UserResponse> handler) 
    : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override async Task Handle(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result<UserResponse> result = await handler.Handle(new GetUserQuery(domainEvent.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new Exception(nameof(GetUserQuery));
        }

        await eventBus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
