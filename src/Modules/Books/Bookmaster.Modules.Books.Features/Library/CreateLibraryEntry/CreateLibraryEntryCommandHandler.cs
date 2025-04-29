using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Dates;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Domain.People;
using Bookmaster.Modules.Books.Features.Abstractions;
using Bookmaster.Modules.Books.Features.Books;
using Bookmaster.Modules.Books.Features.Books.GoogleBookSearch;
using Refit;

namespace Bookmaster.Modules.Books.Features.Library.CreateLibraryEntry;

internal sealed class CreateLibraryEntryCommandHandler(
    IGoogleBooksApi googleBooksApi,
    IBookRepository bookRepository,
    IAuthorRepository authorRepository,
    IPersonRepository personRepository,
    ILibraryEntryRepository libraryEntryRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider) : ICommandHandler<CreateLibraryEntryCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateLibraryEntryCommand request, CancellationToken cancellationToken)
    {
        ApiResponse<GoogleBookSearchResponseItem> response = await googleBooksApi.GetBook(request.GoogleBookId, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<Guid>(BookErrors.ApiResponseFailure());
        }

        if (response.Content is null)
        {
            return Result.Failure<Guid>(BookErrors.NotFound(request.GoogleBookId));
        }

        GoogleBookSearchResponseItem googleBookResult = response.Content;

        Book? book = await bookRepository.GetByGoogleBookIdAsync(googleBookResult.Id, cancellationToken);

        if (book is null)
        {
            List<Author> authors = await GetAuthors(googleBookResult.VolumeInfo.Authors, authorRepository);

            Result<Book> bookResult = Book.Create(
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
                googleBookResult.VolumeInfo.PublishedDate);

            if (bookResult.IsFailure)
            {
                return Result.Failure<Guid>(bookResult.Error);
            }

            bookRepository.Insert(bookResult.Value);

            book = bookResult.Value;
        }

        Person? person = await personRepository.GetAsync(request.PersonId, cancellationToken);

        if (person is null)
        {
            return Result.Failure<Guid>(PersonErrors.NotFound(request.PersonId));
        }

        bool libraryEntryExists = await libraryEntryRepository.ExistsAsync(person.Id, book.Id, cancellationToken);

        if (libraryEntryExists)
        {
            return Result.Failure<Guid>(LibraryEntryErrors.Duplicate(person.Id, book.Id));
        }

        Result<LibraryEntry> libraryEntryResult = LibraryEntry.Create(
            book,
            person,
            dateTimeProvider.UtcNow);

        if (libraryEntryResult.IsFailure)
        {
            return Result.Failure<Guid>(libraryEntryResult.Error);
        }

        libraryEntryRepository.Insert(libraryEntryResult.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return libraryEntryResult.Value.Id;
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
