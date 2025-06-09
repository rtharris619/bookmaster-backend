using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Books.Domain.People;

public static class PersonErrors
{
    public static Error NotFound(Guid personId) =>
       Error.NotFound("Persons.NotFound", $"The person with the identifier {personId} was not found");

    public static Error Duplicate(Guid personId) =>
        Error.Conflict("Persons.Duplicate", $"The person with the identifier {personId} already exists");
}
