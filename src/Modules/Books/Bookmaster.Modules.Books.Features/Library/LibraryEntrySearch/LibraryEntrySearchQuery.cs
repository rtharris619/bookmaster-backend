using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Library.LibraryEntrySearch;

public sealed record LibraryEntrySearchQuery(string IdentityId, string? q = null) : IQuery<LibraryEntrySearchResponse>;
