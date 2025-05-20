using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Microsoft.Extensions.Logging;
using Refit;

namespace Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryWork;

internal sealed class OpenLibraryWorkQueryHandler(
    IOpenLibraryApi openLibraryApi,
    ILogger<OpenLibraryWorkQueryHandler> logger)
    : IQueryHandler<OpenLibraryWorkQuery, OpenLibraryWorkResponse>
{
    public async Task<Result<OpenLibraryWorkResponse>> Handle(OpenLibraryWorkQuery request, CancellationToken cancellationToken)
    {
        OpenLibraryWorkResponse result = null;
        ApiResponse<OpenLibraryWorkResponse> response = await openLibraryApi.GetWork(request.WorkKey, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning("Failed to get work from the Open Library API. Status code {StatusCode}", response.StatusCode);
            return null;
        }

        if (response.Error is not null || response.Content is null)
        {
            ApiResponse<OpenLibraryWorkResponseV2> responseV2 = await openLibraryApi.GetWorkV2(request.WorkKey, cancellationToken);
            OpenLibraryWorkResponseV2 content = responseV2.Content;

            if (content is null)
            {
                return null;
            }

            result = new OpenLibraryWorkResponse(
                content.Title,
                content.Key,
                content.Authors,
                content.Covers,
                content.Description?.Value,
                content.First_Publish_Date,
                content.Subject_Places,
                content.Subjects,
                content.Subject_People,
                content.Subject_Times,
                content.Cover_Edition,
                content.Latest_Revision,
                content.Revision);
        }
        else
        {
            result = response.Content;
        }

        return result;
    }
}
