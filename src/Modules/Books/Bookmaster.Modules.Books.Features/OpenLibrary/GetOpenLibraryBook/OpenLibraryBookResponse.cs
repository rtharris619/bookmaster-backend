namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryBook;

public sealed record OpenLibraryBookResponse(
    OpenLibraryBookResponseDescription? Description,
    string Title,
    OpenLibraryBookResponseKey[] Authors, // Links to the Authors
    string Publish_Date,
    OpenLibraryBookResponseTableOfContents[] Table_Of_Contents,
    string[] Covers,
    OpenLibraryBookResponseKey[] Languages,
    string[] Genres,
    string Edition_Name,
    string[] Subjects,
    string Publish_Country,
    string By_Statement,
    string[] Publishers,
    string Physical_Format,
    string[] Publish_Places,
    string Pagination,
    string[] Isbn_10,
    string[] Isbn_13,
    string[] Lccn,
    string Number_Of_Pages,
    string Latest_Revision,
    string Revision,
    string Key
    );

public sealed record OpenLibraryBookResponseDescription(string Type, string Value);

public sealed record OpenLibraryBookResponseKey(string Key);

public sealed record OpenLibraryBookResponseTableOfContents(string Level, string Title);
