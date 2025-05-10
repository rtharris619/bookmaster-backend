using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.OpenLibrary.OpenLibrarySearch;

public sealed record OpenLibrarySearchQuery(string q, int? page = null, int? limit = null)
    : IQuery<OpenLibrarySearchResponse>;
