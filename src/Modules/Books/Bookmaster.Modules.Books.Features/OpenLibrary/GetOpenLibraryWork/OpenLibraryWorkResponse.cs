namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryWork;

public sealed record OpenLibraryWorkResponse(
    string Title,
    string Key, // Link to the Work
    OpenLibraryWorkResponseAuthor[] Authors,
    string? Description,
    string[] Subject_Places,
    string[] Subjects,
    string[] Subject_People,
    string[] Subject_Times);

public sealed record OpenLibraryWorkResponseV2(
    string Title,
    string Key, // Link to the Work
    OpenLibraryWorkResponseAuthor[] Authors,
    OpenLibraryWorkResponseDescription? Description,
    string[] Subject_Places,
    string[] Subjects,
    string[] Subject_People,
    string[] Subject_Times);

public sealed record OpenLibraryWorkResponseAuthor(object Author, object Type);

public sealed record OpenLibraryWorkResponseDescription(string Type, string Value);
