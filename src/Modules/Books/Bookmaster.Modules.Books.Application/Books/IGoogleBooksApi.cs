using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;
using Refit;

namespace Bookmaster.Modules.Books.Features.Books;

public interface IGoogleBooksApi
{
    [Get("/volumes")]
    Task<ApiResponse<List<GoogleBookSearchResponse>>> GetBooks(
        GoogleBookSearchQuery query,
        CancellationToken cancellationToken = default);
}
