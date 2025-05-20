namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryWork;

public sealed record OpenLibraryWorkResponse(
    string Title,
    string Key, // Link to the Work
    OpenLibraryWorkResponseAuthor[] Authors,
    string[] Covers,
    string? Description,
    string First_Publish_Date,
    string[] Subject_Places,
    string[] Subjects,
    string[] Subject_People,
    string[] Subject_Times,
    OpenLibraryWorkResponseKey Cover_Edition, // Link to the Book
    string Latest_Revision,
    string Revision);

public sealed record OpenLibraryWorkResponseV2(
    string Title,
    string Key, // Link to the Work
    string First_Publish_Date,
    OpenLibraryWorkResponseAuthor[] Authors,
    OpenLibraryWorkResponseDescription? Description,
    string[] Covers,
    string[] Subject_Places,
    string[] Subjects,
    string[] Subject_People,
    string[] Subject_Times,
    OpenLibraryWorkResponseKey Cover_Edition, // Link to the Book
    string Latest_Revision,
    string Revision);

public sealed record OpenLibraryWorkResponseAuthor(object Author, object Type);

public sealed record OpenLibraryWorkResponseDescription(string Type, string Value);

public sealed record OpenLibraryWorkResponseKey(string Key);
