using Bookmaster.Common.Domain;
using Bookmaster.Modules.Users.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Bookmaster.Modules.Users.Features.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(
    ILogger<UserRegisteredDomainEventHandler> logger) : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override Task Handle(UserRegisteredDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("testing 123 testing");

        return Task.CompletedTask;
    }
}
