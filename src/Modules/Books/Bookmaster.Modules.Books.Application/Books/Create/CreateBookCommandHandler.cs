using System.Threading.Tasks;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Dates;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Features.Abstractions;
using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;
using Refit;

namespace Bookmaster.Modules.Books.Features.Books.Create;

internal sealed class CreateBookCommandHandler(
    IGoogleBooksApi googleBooksApi,
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) 
    : ICommandHandler<CreateBookCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        ApiResponse<GoogleBooksSearchResponseItem> response = await googleBooksApi.GetBook(request.GoogleBookId, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<Guid>(new Error("", "Failed to fetch book details from Google Books API.", ErrorType.Failure));
        }

        if (response.Content is null)
        {
            return Result.Failure<Guid>(new Error("", "Book not found in Google Books API.", ErrorType.NotFound));
        }

        GoogleBooksSearchResponseItem googleBookResult = response.Content;

        List<Author> authors = await GetAuthors(googleBookResult.VolumeInfo.Authors, authorRepository);

        Result<Book> result = Book.Create(
            authors,
            googleBookResult.Id,
            googleBookResult.VolumeInfo.Title,
            googleBookResult.VolumeInfo.Subtitle,
            googleBookResult.VolumeInfo.Description,
            googleBookResult.SearchInfo?.TextSnippet,
            googleBookResult.VolumeInfo.InfoLink,
            googleBookResult.VolumeInfo.PageCount,
            googleBookResult.VolumeInfo.ImageLinks.SmallThumbnail,
            googleBookResult.VolumeInfo.ImageLinks.Thumbnail,
            googleBookResult.VolumeInfo.Publisher,
            dateTimeProvider.ConvertToUtc(googleBookResult.VolumeInfo.PublishedDate));

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        bookRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }

    private async Task<List<Author>> GetAuthors(string[] googleBookAuthors, IAuthorRepository authorRepository)
    {
        List<Author> authors = [];

        foreach (string googleBookAuthor in googleBookAuthors)
        {
            Author? author = await authorRepository.GetByNameAsync(googleBookAuthor);
            if (author is null)
            {
                author = Author.Create(googleBookAuthor);
                authorRepository.Insert(author);
            }
            authors.Add(author);
        }

        return authors;
    }
}
