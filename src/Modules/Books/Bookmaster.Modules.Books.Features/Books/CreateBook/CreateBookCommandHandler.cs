using System.Threading.Tasks;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Dates;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Features.Abstractions;
using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;
using Bookmaster.Modules.Books.Features.Services;
using Refit;

namespace Bookmaster.Modules.Books.Features.Books.CreateBook;

internal sealed class CreateBookCommandHandler(
    IGoogleBooksApi googleBooksApi,
    IBookRepository bookRepository,
    IBookService bookService,
    IAuthorRepository authorRepository,
    IBookCategoryRepository bookCategoryRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateBookCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        ApiResponse<GoogleBookSearchResponseItem> response = 
            await googleBooksApi.GetBook(request.GoogleBookId, GoogleBookSearchProjection.Full, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<Guid>(BookErrors.ApiResponseFailure());
        }

        if (response.Content is null)
        {
            return Result.Failure<Guid>(BookErrors.NotFound(request.GoogleBookId));
        }

        GoogleBookSearchResponseItem googleBookResult = response.Content;

        Book? existingBook = await bookRepository.GetByGoogleBookIdAsync(googleBookResult.Id, cancellationToken);

        if (existingBook is not null)
        {
            return Result.Failure<Guid>(BookErrors.Duplicate(googleBookResult.Id));
        }

        GoogleBookSearchResponseVolumeInfo volumeInfo = googleBookResult.VolumeInfo;

        List<Author> authors = await bookService.GetAuthors(volumeInfo.Authors, authorRepository);

        List<BookCategory>? categories = null;
        string[]? googleBookCategories = volumeInfo.Categories;

        if (googleBookCategories is not null)
        {
            categories = await bookService.GetBookCategories(googleBookCategories, bookCategoryRepository);
        }

        Result<Book> result = Book.Create(
                authors: authors,
                categories: categories,
                googleBookId: googleBookResult.Id,
                title: volumeInfo.Title,
                subTitle: volumeInfo.Subtitle,
                description: volumeInfo.Description,
                pageCount: volumeInfo.PageCount,
                printType: volumeInfo.PrintType,
                thumbnail: volumeInfo.ImageLinks.Thumbnail,
                publisher: volumeInfo.Publisher,
                publishedDate: volumeInfo.PublishedDate,
                language: volumeInfo.Language,
                googleBookInfoLink: volumeInfo.InfoLink,
                googleBookPreviewLink: volumeInfo.PreviewLink);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        bookRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
