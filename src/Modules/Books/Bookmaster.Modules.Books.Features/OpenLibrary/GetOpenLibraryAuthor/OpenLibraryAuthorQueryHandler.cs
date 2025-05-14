using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Microsoft.Extensions.Logging;
using Refit;

namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryAuthor;

internal sealed class OpenLibraryAuthorQueryHandler(
    IOpenLibraryApi openLibraryApi,
    ILogger<OpenLibraryAuthorQueryHandler> logger)
    : IQueryHandler<OpenLibraryAuthorQuery, OpenLibraryAuthorResponse>
{
    public async Task<Result<OpenLibraryAuthorResponse>> Handle(OpenLibraryAuthorQuery request, CancellationToken cancellationToken)
    {
        ApiResponse<OpenLibraryAuthorResponse> response = await openLibraryApi.GetAuthor(request.AuthorKey, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Failed to get Author from the Open Library API. Status code {StatusCode}", response.StatusCode);
            return null;
        }

        if (response.Error is not null || response.Content is null)
        {
            logger.LogWarning("Failed to get Author from the Open Library API. Error: {Error}", response.Error?.Content);
            return null;
        }

        return response.Content;
    }
}
