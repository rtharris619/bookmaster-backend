using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Books.Domain.Library;

public static class LibraryEntryErrors
{
    public static Error NotFound(Guid id) =>
       Error.NotFound("LibraryEntry.NotFound", $"The library entry with the identifier {id} was not found");

    public static Error Duplicate(Guid personId, Guid bookId) =>
        Error.Conflict("LibraryEntry.Duplicate", $"The library entry with the person identifier {personId} and book identifier {bookId} has already been saved");
}
