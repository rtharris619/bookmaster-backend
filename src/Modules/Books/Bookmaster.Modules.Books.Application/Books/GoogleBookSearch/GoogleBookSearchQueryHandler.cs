using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Microsoft.Extensions.Logging;
using Refit;

namespace Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;

internal sealed class GoogleBookSearchQueryHandler(
    IGoogleBooksApi googleBooksApi,
    ILogger<GoogleBookSearchQueryHandler> logger)
    : IQueryHandler<GoogleBookSearchQuery, IReadOnlyCollection<GoogleBookSearchResponse>>
{
    public async Task<Result<IReadOnlyCollection<GoogleBookSearchResponse>>> Handle(
        GoogleBookSearchQuery request,
        CancellationToken cancellationToken)
    {
        ApiResponse<List<GoogleBookSearchResponse>> response = await googleBooksApi.GetBooks(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Failed to search books on the Google API. Status code {StatusCode}", response.StatusCode);
            return null;
        }

        return response.Content;
    }
}
