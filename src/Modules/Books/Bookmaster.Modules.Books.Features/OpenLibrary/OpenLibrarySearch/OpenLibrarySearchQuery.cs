using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.OpenLibrary.OpenLibrarySearch;

public sealed record OpenLibrarySearchQuery(string Query, int? Offset = null, int? Limit = null)
    : IQuery<OpenLibrarySearchResponse>;
