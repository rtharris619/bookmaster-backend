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
    int PageCount,
    string? Thumbnail);
