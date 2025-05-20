using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Books.Domain.Books;

public static class BookErrors
{
    public static Error GoogleBookApiResponseFailure() =>
        Error.Failure("Book.GoogleBooksApiResponseFailure", $"Failed to fetch data from Google Books API");

    public static Error GoogleBookNotFound(string googleBookId) =>
        Error.NotFound("Book.GoogleBookNotFound", $"The book with the Google Book Id {googleBookId} was not found");

    public static Error OpenLibraryApiResponseFailure() =>
        Error.Failure("Book.OpenLibraryApiResponseFailure", $"Failed to fetch data from Open Library API");

    public static Error OpenLibraryWorkNotFound(string key) =>
        Error.NotFound("Book.OpenLibraryWorkNotFound", $"The work with the Open Library key {key} was not found");

    public static Error OpenLibraryWorkError(string key, string error) =>
        Error.NotFound("Book.OpenLibraryWorkError", $"Failed to fetch work with the Open Library key {key} with the error {error}");

    public static Error OpenLibraryBookNotFound(string key) =>
        Error.NotFound("Book.OpenLibraryBookNotFound", $"The book with the Open Library key {key} was not found");

    public static Error OpenLibraryBookError(string key, string error) =>
        Error.NotFound("Book.OpenLibraryBookError", $"Failed to fetch book with the Open Library key {key} with the error {error}");

    public static Error OpenLibraryAuthorNotFound(string key) =>
        Error.NotFound("Book.OpenLibraryAuthorNotFound", $"The author with the Open Library key {key} was not found");

    public static Error OpenLibraryAuthorError(string key, string error) =>
        Error.NotFound("Book.OpenLibraryAuthorError", $"Failed to fetch author with the Open Library key {key} with the error {error}");
}
