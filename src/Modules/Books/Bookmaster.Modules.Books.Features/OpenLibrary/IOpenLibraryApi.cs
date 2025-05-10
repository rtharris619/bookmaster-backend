using Bookmaster.Modules.Books.Features.OpenLibrary.OpenLibrarySearch;
using Refit;

namespace Bookmaster.Modules.Books.Features.OpenLibrary;

public interface IOpenLibraryApi
{
    [Get("/search.json")]
    Task<ApiResponse<OpenLibrarySearchResponse>> Search(
        string q,
        int? page = null,
        int? limit = null,
        CancellationToken cancellationToken = default);
}
