namespace Bookmaster.Modules.Books.Features.Library.LibraryEntrySearch;

public sealed record LibraryEntrySearchResponse(int TotalItems, List<LibraryEntriesResponse> Items);

public sealed record LibraryEntriesResponse(
    Guid Id,
    Guid BookId,
    Guid PersonId,
    string GoogleBookId,
    string Title,
    string? SubTitle,
    string? Description,
    string[] Authors,
    string FlattenedAuthors,
    string[] Categories,
    string FlattenedCategories,
    int PageCount,
    string? Thumbnail,
    string? Publisher,
    string? PublishedDate,
    string Language,
    string? GoogleBookInfoLink,
    string? GoogleBookPreviewLink);
