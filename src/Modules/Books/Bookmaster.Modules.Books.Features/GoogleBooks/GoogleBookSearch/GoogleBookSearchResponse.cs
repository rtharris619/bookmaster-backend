namespace Bookmaster.Modules.Books.Features.GoogleBooks.GoogleBookSearch;

public sealed record GoogleBookSearchResponse(
    string Kind,
    int TotalItems,
    List<GoogleBookSearchResponseItem> Items);

public sealed record GoogleBookSearchResponseItem(
    string Kind,
    string Id,
    string ETag,
    string SelfLink,
    GoogleBookSearchResponseVolumeInfo VolumeInfo);

public sealed record GoogleBookSearchResponseVolumeInfo(
    string Title, 
    string? Subtitle,
    string[] Authors,
    string? Publisher,
    string? PublishedDate,
    string? Description,
    int PageCount,
    int? PrintedPageCount,
    string PrintType,
    string[]? Categories,
    GoogleBookSearchResponseImageLinks ImageLinks,
    GoogleBookSearchResponseIndustryIdentifiers[]? IndustryIdentifiers,
    string Language,
    string? PreviewLink,
    string? InfoLink,
    string? CanonicalVolumeLink);

public sealed record GoogleBookSearchResponseImageLinks(
    string? SmallThumbnail,
    string? Thumbnail,
    string? Small,
    string? Medium,
    string? Large,
    string? ExtraLarge);

public sealed record GoogleBookSearchResponseIndustryIdentifiers(
    string Type,
    string Identifier);
