using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;
using Refit;

namespace Bookmaster.Modules.Books.Features.Books.Create;

internal sealed class CreateBookCommandHandler(
    IGoogleBooksApi googleBooksApi) 
    : ICommandHandler<CreateBookCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        ApiResponse<GoogleBooksSearchResponseItem> response = await googleBooksApi.GetBook(request.GoogleBookId, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<Guid>(new Error("", "Failed to fetch book details from Google Books API.", ErrorType.Failure));
        }

        // Next Up: Create a book in the database using the response data

        return Guid.NewGuid();
    }
}
