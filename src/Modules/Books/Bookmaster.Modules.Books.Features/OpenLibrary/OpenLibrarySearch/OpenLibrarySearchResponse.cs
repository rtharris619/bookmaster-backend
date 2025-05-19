namespace Bookmaster.Modules.Books.Features.OpenLibrary.OpenLibrarySearch;

public sealed record OpenLibrarySearchResponse(
    int NumFound,
    int? Offset,
    OpenLibrarySearchResponseDoc[] Docs);

public sealed record OpenLibrarySearchResponseDoc(
    string[] Author_Key,
    string[] Author_Name,
    string Cover_Edition_Key,
    string Edition_Count,
    string First_Publish_Year,
    string Key, // Link to the Work
    string Title);
