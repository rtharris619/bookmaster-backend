using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;

public sealed record GoogleBookSearchQuery(string q, string printType, int startIndex, int maxResults)
    : IQuery<GoogleBookSearchResponse>;
