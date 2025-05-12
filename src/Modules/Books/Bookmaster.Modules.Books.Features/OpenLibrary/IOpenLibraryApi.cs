using Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryWork;
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

    [Get("/works/{key}.json")]
    Task<ApiResponse<OpenLibraryWorkResponse>> GetWork(
        string key,
        CancellationToken cancellationToken = default);

    [Get("/works/{key}.json")]
    Task<ApiResponse<OpenLibraryWorkResponseV2>> GetWorkV2(
        string key,
        CancellationToken cancellationToken = default);
}
