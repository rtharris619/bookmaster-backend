using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;
using Refit;

namespace Bookmaster.Modules.Books.Features.Books;

public interface IGoogleBooksApi
{
    [Get("/volumes")]
    Task<ApiResponse<GoogleBookSearchResponse>> GetBooks(
        GoogleBookSearchQuery query,
        CancellationToken cancellationToken = default);

    [Get("/volumes/{id}")]
    Task<ApiResponse<GoogleBooksSearchResponseItem>> GetBook(
        string id,
        CancellationToken cancellationToken = default);
}
