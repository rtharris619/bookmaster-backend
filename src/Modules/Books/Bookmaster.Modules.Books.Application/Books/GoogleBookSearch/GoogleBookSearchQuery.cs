using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;

public sealed record GoogleBookSearchQuery(string Q, string PrintType, int MaxResults = 2)
    : IQuery<IReadOnlyCollection<GoogleBookSearchResponse>>;
