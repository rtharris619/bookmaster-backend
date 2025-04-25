using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Books.Domain.Books;

public static class BookErrors
{
    public static Error Duplicate(string googleBookId) =>
        Error.Conflict("Book.Duplicate", $"The book with the identifier {googleBookId} has already been saved");

    public static Error ApiResponseFailure() =>
        Error.Failure("Book.GoogleBooksApiResponseFailure", $"Failed to fetch book details from Google Books API");

    public static Error NotFound(string googleBookId) =>
        Error.NotFound("Book.NotFound", $"The book with the Google Book Id {googleBookId} was not found");
}
