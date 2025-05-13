using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Features.OpenLibrary.OpenLibrarySearch;
using Microsoft.Extensions.Logging;
using Refit;

namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryEditions;

internal sealed class OpenLibraryEditionsQueryHandler(
    IOpenLibraryApi openLibraryApi,
    ILogger<OpenLibraryEditionsQueryHandler> logger)
    : IQueryHandler<OpenLibraryEditionQuery, OpenLibraryEditionResponse>
{
    public async Task<Result<OpenLibraryEditionResponse>> Handle(OpenLibraryEditionQuery request, CancellationToken cancellationToken)
    {
        ApiResponse<OpenLibraryEditionResponse> response = await openLibraryApi.GetEditions(request.WorkKey, request.Limit, request.Offset, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Failed to retrieve Editions on the Open Library API. Status code {StatusCode}", response.StatusCode);
            return null;
        }

        return response.Content;
    }
}
