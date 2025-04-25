namespace Bookmaster.Modules.Books.Features.Library.LibraryEntrySearch;

public sealed record LibraryEntrySearchResponse(int TotalCount, List<LibraryEntriesResponse> LibraryEntries);

public sealed record LibraryEntriesResponse(string Title, string Description, string[] Authors);
