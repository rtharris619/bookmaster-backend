using Bookmaster.Modules.Books.Features.GoogleBooks.GoogleBookSearch;
using Refit;

namespace Bookmaster.Modules.Books.Features.GoogleBooks;

public interface IGoogleBooksApi
{
    [Get("/volumes")]
    Task<ApiResponse<GoogleBookSearchResponse>> GetBooks(
        GoogleBookSearchQuery query,
        CancellationToken cancellationToken = default);

    [Get("/volumes/{id}?projection={projection}")]
    Task<ApiResponse<GoogleBookSearchResponseItem>> GetBook(
        string id,
        string projection,
        CancellationToken cancellationToken = default);
}
