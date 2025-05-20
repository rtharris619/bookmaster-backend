using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Dates;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Domain.People;
using Bookmaster.Modules.Books.Features.Abstractions;
using Bookmaster.Modules.Books.Features.GoogleBooks;
using Bookmaster.Modules.Books.Features.OpenLibrary;
using Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryAuthor;
using Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryBook;
using Bookmaster.Modules.Books.Features.OpenLibrary.GetOpenLibraryWork;
using Refit;

namespace Bookmaster.Modules.Books.Features.Library.CreateLibraryEntry;

public sealed class CreateLibraryEntryCommandHandler(
    IOpenLibraryApi openLibraryApi)
    : ICommandHandler<CreateLibraryEntryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateLibraryEntryCommand request, CancellationToken cancellationToken)
    {
        // 1. Get the Open Library Work

        ApiResponse<OpenLibraryWorkResponse> getOpenLibraryWorkResponse =
            await openLibraryApi.GetWork(request.OpenLibraryWorkKey, cancellationToken);

        if (!getOpenLibraryWorkResponse.IsSuccessStatusCode)
        {
            return Result.Failure<Guid>(BookErrors.OpenLibraryApiResponseFailure());
        }

        OpenLibraryWorkResponse openLibraryWorkResult = null;

        if (getOpenLibraryWorkResponse.Error is not null)
        {
            // Get work v2
            ApiResponse<OpenLibraryWorkResponseV2> getOpenLibraryWorkResponseV2 =
                await openLibraryApi.GetWorkV2(request.OpenLibraryWorkKey, cancellationToken);

            if (!getOpenLibraryWorkResponseV2.IsSuccessStatusCode)
            {
                return Result.Failure<Guid>(BookErrors.OpenLibraryApiResponseFailure());
            }

            if (getOpenLibraryWorkResponseV2.Error is not null)
            {
                ApiException error = getOpenLibraryWorkResponseV2.Error;
                string errorMessage = error.InnerException != null
                    ? $"{error.Message} - {error.InnerException.Message}"
                    : error.Message;
                return Result.Failure<Guid>(BookErrors.OpenLibraryWorkError(request.OpenLibraryWorkKey, errorMessage));
            }

            if (getOpenLibraryWorkResponseV2.Content is null)
            {
                return Result.Failure<Guid>(BookErrors.OpenLibraryWorkNotFound(request.OpenLibraryWorkKey));
            }

            openLibraryWorkResult = new OpenLibraryWorkResponse(
                getOpenLibraryWorkResponseV2.Content.Title,
                getOpenLibraryWorkResponseV2.Content.Key,
                getOpenLibraryWorkResponseV2.Content.Authors,
                getOpenLibraryWorkResponseV2.Content.Covers,
                getOpenLibraryWorkResponseV2.Content.Description?.Value,
                getOpenLibraryWorkResponseV2.Content.First_Publish_Date,
                getOpenLibraryWorkResponseV2.Content.Subject_Places,
                getOpenLibraryWorkResponseV2.Content.Subjects,
                getOpenLibraryWorkResponseV2.Content.Subject_People,
                getOpenLibraryWorkResponseV2.Content.Subject_Times,
                getOpenLibraryWorkResponseV2.Content.Cover_Edition,
                getOpenLibraryWorkResponseV2.Content.Latest_Revision,
                getOpenLibraryWorkResponseV2.Content.Revision);
        }
        else
        {
            openLibraryWorkResult = getOpenLibraryWorkResponse.Content;
        }

        // 2. Get the Open Library Book

        string bookKey = ExtractKey(openLibraryWorkResult!.Cover_Edition.Key);

        ApiResponse<OpenLibraryBookResponse> getOpenLibraryBookResponse =
            await openLibraryApi.GetBook(bookKey, cancellationToken);

        if (!getOpenLibraryBookResponse.IsSuccessStatusCode)
        {
            return Result.Failure<Guid>(BookErrors.OpenLibraryApiResponseFailure());
        }

        if (getOpenLibraryBookResponse.Error is not null)
        {
            ApiException error = getOpenLibraryBookResponse.Error;
            string errorMessage = error.InnerException != null
                ? $"{error.Message} - {error.InnerException.Message}"
                : error.Message;
            return Result.Failure<Guid>(BookErrors.OpenLibraryBookError(bookKey, errorMessage));
        }

        if (getOpenLibraryBookResponse.Content is null)
        {
            return Result.Failure<Guid>(BookErrors.OpenLibraryBookNotFound(bookKey));
        }

        OpenLibraryBookResponse openLibraryBookResult = getOpenLibraryBookResponse.Content;

        // 3. Get the Open Library Authors

        string authorKey = ExtractKey(openLibraryBookResult.Authors[0].Key);

        ApiResponse<OpenLibraryAuthorResponse> getOpenLibraryAuthorResponse =
            await openLibraryApi.GetAuthor(authorKey, cancellationToken);

        if (!getOpenLibraryAuthorResponse.IsSuccessStatusCode)
        {
            return Result.Failure<Guid>(BookErrors.OpenLibraryApiResponseFailure());
        }

        if (getOpenLibraryAuthorResponse.Error is not null)
        {
            ApiException error = getOpenLibraryAuthorResponse.Error;
            string errorMessage = error.InnerException != null
                ? $"{error.Message} - {error.InnerException.Message}"
                : error.Message;
            return Result.Failure<Guid>(BookErrors.OpenLibraryAuthorError(authorKey, errorMessage));
        }

        if (getOpenLibraryAuthorResponse.Content is null)
        {
            return Result.Failure<Guid>(BookErrors.OpenLibraryAuthorNotFound(authorKey));
        }

        return Result.Success(Guid.NewGuid());
    }

    private string ExtractKey(string keyUrl)
    {
        // Example: /authors/OL26320A
        return keyUrl[(keyUrl.LastIndexOf('/') + 1)..];
    }
}
