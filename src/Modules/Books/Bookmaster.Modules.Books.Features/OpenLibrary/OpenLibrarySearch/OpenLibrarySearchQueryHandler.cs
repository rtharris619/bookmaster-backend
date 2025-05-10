using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Microsoft.Extensions.Logging;
using Refit;

namespace Bookmaster.Modules.Books.Features.OpenLibrary.OpenLibrarySearch;

internal sealed class OpenLibrarySearchQueryHandler(
    IOpenLibraryApi openLibraryApi,
    ILogger<OpenLibrarySearchQueryHandler> logger)
    : IQueryHandler<OpenLibrarySearchQuery, OpenLibrarySearchResponse>
{
    public async Task<Result<OpenLibrarySearchResponse>> Handle(OpenLibrarySearchQuery request, CancellationToken cancellationToken)
    {
        ApiResponse<OpenLibrarySearchResponse> response = await openLibraryApi.Search(request.q, request.page, request.limit, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Failed to search books on the Open Library API. Status code {StatusCode}", response.StatusCode);
            return null;
        }

        return response.Content;
    }
}
