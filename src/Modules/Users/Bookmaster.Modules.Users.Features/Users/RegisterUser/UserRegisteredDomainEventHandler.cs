using Bookmaster.Common.Domain;
using Bookmaster.Modules.Users.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Bookmaster.Modules.Users.Features.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler() 
    : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override Task Handle(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}
