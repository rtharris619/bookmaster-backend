using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Books.Domain.People;

public static class PersonErrors
{
    public static Error NotFound(Guid personId) =>
       Error.NotFound("Person.NotFound", $"The person with the identifier {personId} was not found");
}
