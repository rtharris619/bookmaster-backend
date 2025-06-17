using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Users.Domain.Users;

public sealed class UserRegisteredDomainEvent(Guid UserId, string IdentityId) : DomainEvent
{
    public Guid UserId { get; init; } = UserId;

    public string IdentityId { get; init; } = IdentityId;
}
