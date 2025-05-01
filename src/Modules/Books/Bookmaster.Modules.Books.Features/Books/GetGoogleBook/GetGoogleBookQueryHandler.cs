using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;
using Microsoft.Extensions.Logging;
using Refit;

namespace Bookmaster.Modules.Books.Features.Books.GetGoogleBook;

internal sealed class GetGoogleBookQueryHandler(
    IGoogleBooksApi googleBooksApi)
    : IQueryHandler<GetGoogleBookQuery, GoogleBookSearchResponseItem>
{
    public async Task<Result<GoogleBookSearchResponseItem>> Handle(GetGoogleBookQuery request, CancellationToken cancellationToken)
    {
        ApiResponse<GoogleBookSearchResponseItem> response = 
            await googleBooksApi.GetBook(request.GoogleBookId, GoogleBookSearchProjection.Full, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<GoogleBookSearchResponseItem>(BookErrors.ApiResponseFailure());
        }

        if (response.Content is null)
        {
            return Result.Failure<GoogleBookSearchResponseItem>(BookErrors.NotFound(request.GoogleBookId));
        }

        GoogleBookSearchResponseItem googleBookResult = response.Content;

        return googleBookResult;
    }
}
