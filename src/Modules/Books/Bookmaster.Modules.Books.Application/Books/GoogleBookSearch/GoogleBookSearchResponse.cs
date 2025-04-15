namespace Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;

public sealed record GoogleBookSearchResponse(string Kind, int TotalItems, List<GoogleBooksSearchResponseItems> Items);

public sealed record GoogleBooksSearchResponseItems(string Kind, string Id, GoogleBooksSearchResponseVolumeInfo VolumeInfo);

public sealed record GoogleBooksSearchResponseVolumeInfo(string Title, string Subtitle);
