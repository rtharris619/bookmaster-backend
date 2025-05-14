namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryAuthor;

public sealed record OpenLibraryAuthorResponse(
    string Personal_Name,
    string Key, // Link to the Author
    string Birth_Date,
    string Name,
    OpenLibraryAuthorResponseRemoteIds Remote_Ids,
    string Title,
    string Bio,
    string Fuller_Name,
    string[] Photos
    );

public sealed record OpenLibraryAuthorResponseRemoteIds(
    string Viaf,
    string Goodreads,
    string Storygraph,
    string Isni,
    string Librarything,
    string Amazon,
    string Wikidata);
