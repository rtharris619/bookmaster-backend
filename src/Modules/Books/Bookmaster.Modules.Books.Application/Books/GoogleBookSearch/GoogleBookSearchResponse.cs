namespace Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;

public sealed record GoogleBookSearchResponse(string Kind, int TotalItems, List<GoogleBooksSearchResponseItem> Items);

public sealed record GoogleBooksSearchResponseItem(
    string Kind,
    string Id,
    GoogleBooksSearchResponseVolumeInfo VolumeInfo,
    GoogleBooksSearchResponseSearchInfo? SearchInfo);

public sealed record GoogleBooksSearchResponseVolumeInfo(
    string Title, 
    string? Subtitle,
    string[] Authors,
    string? Publisher,
    DateTime? PublishedDate,
    string Description,
    int PageCount,
    string PrintType,
    string[]? Categories,
    GoogleBooksSearchResponseImageLinks ImageLinks,
    string? PreviewLink,
    string? InfoLink,
    string? CanonicalVolumeLink);

public sealed record GoogleBooksSearchResponseImageLinks(string? SmallThumbnail, string? Thumbnail);

public sealed record GoogleBooksSearchResponseSearchInfo(string? TextSnippet);
