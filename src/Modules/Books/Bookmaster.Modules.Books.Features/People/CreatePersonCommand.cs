using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.People;

public sealed record CreatePersonCommand(Guid PersonId, string Email, string FirstName, string LastName) : ICommand<Guid>;
