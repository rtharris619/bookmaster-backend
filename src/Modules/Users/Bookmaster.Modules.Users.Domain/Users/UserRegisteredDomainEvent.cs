using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Users.Domain.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvent;
