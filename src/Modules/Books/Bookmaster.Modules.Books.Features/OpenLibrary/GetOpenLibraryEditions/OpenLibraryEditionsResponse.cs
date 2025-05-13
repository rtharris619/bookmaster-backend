namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryEditions;

public sealed record OpenLibraryEditionResponse(
    OpenLibraryEditionResponseLinks Links,
    int Size,
    OpenLibraryEditionResponseEntry[] Entries);

public sealed record OpenLibraryEditionResponseLinks(string Self, string Work, string Next);

public sealed record OpenLibraryEditionResponseEntry(
    OpenLibraryEditionResponseKey[] Authors,
    OpenLibraryEditionResponseKey[] Languages,
    string[] Isbn_10,
    string[] Isbn_13,
    string Pagination,
    string Publish_Date,
    string[] Publishers,
    string Key // Link to the Book
    );

public sealed record OpenLibraryEditionResponseKey(string Key);
