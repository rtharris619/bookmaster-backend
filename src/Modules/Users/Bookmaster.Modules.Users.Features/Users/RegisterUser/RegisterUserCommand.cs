using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Users.Features.Users.RegisterUser;

public sealed record RegisterUserCommand(string Email, string Password, string FirstName, string LastName)
    : ICommand<Guid>;
