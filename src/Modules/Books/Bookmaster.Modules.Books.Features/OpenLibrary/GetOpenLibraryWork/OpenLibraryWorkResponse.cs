namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryWork;

public sealed record OpenLibraryWorkResponse(
    string Title,
    string Key, // Link to the Work
    string? Description,
    string[] Subject_Places,
    string[] Subjects,
    string[] Subject_People,
    string[] Subject_Times);

public sealed record OpenLibraryWorkResponseV2(
    string Title,
    string Key, // Link to the Work
    OpenLibraryWorkDescriptionResponse? Description,
    string[] Subject_Places,
    string[] Subjects,
    string[] Subject_People,
    string[] Subject_Times);

public sealed record OpenLibraryWorkDescriptionResponse(string Type, string Value);
