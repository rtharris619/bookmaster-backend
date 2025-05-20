using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Microsoft.Extensions.Logging;
using Refit;

namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryBook;

internal sealed class OpenLibraryBookQueryHandler(
    IOpenLibraryApi openLibraryApi,
    ILogger<OpenLibraryBookQueryHandler> logger)
    : IQueryHandler<OpenLibraryBookQuery, OpenLibraryBookResponse>
{
    public async Task<Result<OpenLibraryBookResponse>> Handle(OpenLibraryBookQuery request, CancellationToken cancellationToken)
    {
        ApiResponse<OpenLibraryBookResponse> response = await openLibraryApi.GetBook(request.BookKey, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Failed to get Book from the Open Library API. Status code {StatusCode}", response.StatusCode);
            return null;
        }

        if (response.Error is not null || response.Content is null)
        {
            logger.LogWarning("Failed to get Book from the Open Library API. Error: {Error}", response.Error?.Content);
            return null;
        }

        return response.Content;
    }
}
