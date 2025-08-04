using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Users.Features.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName) : ICommand;
