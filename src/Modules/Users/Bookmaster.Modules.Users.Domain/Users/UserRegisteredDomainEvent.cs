using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Users.Domain.Users;

public sealed class UserRegisteredDomainEvent(Guid UserId) : DomainEvent
{
    public Guid UserId { get; init; } = UserId;
}
