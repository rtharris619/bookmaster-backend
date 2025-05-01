using Bookmaster.Common.Features.Messaging;

namespace Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;

public sealed record GoogleBookSearchQuery(
    string q, 
    string printType, 
    string projection, 
    int startIndex, 
    int maxResults)
    : IQuery<GoogleBookSearchResponse>;

public static class GoogleBookSearchProjection
{
    public const string Lite = "lite";
    public const string Full = "full";
}
