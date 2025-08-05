using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.People.UpdatePerson;

public sealed record UpdatePersonCommand(Guid PersonId, string FirstName, string LastName) : ICommand;
