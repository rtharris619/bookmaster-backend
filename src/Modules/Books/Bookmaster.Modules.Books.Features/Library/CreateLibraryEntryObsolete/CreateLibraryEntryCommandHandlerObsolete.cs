using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Dates;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Domain.People;
using Bookmaster.Modules.Books.Features.Abstractions;
using Bookmaster.Modules.Books.Features.GoogleBooks;
using Bookmaster.Modules.Books.Features.GoogleBooks.GoogleBookSearch;
using Bookmaster.Modules.Books.Features.Services;
using Refit;

namespace Bookmaster.Modules.Books.Features.Library.CreateLibraryEntryObsolete;

internal sealed class CreateLibraryEntryCommandHandlerObsolete(
    IGoogleBooksApi googleBooksApi,
    IBookRepository bookRepository,
    IBookService bookService,
    IAuthorRepository authorRepository,
    IBookCategoryRepository bookCategoryRepository,
    IPersonRepository personRepository,
    ILibraryEntryRepository libraryEntryRepository,
    IUnitOfWork unitOfWork,
    IDateTimeProvider dateTimeProvider)
    : ICommandHandler<CreateLibraryEntryCommandObsolete, Guid>
{
    public async Task<Result<Guid>> Handle(CreateLibraryEntryCommandObsolete request, CancellationToken cancellationToken)
    {
        ApiResponse<GoogleBookSearchResponseItem> response = 
            await googleBooksApi.GetBook(request.GoogleBookId, GoogleBookSearchProjection.Full, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Result.Failure<Guid>(BookErrors.GoogleBookApiResponseFailure());
        }

        if (response.Content is null)
        {
            return Result.Failure<Guid>(BookErrors.GoogleBookNotFound(request.GoogleBookId));
        }

        GoogleBookSearchResponseItem googleBookResult = response.Content;

        Book? book = await bookRepository.GetByGoogleBookIdAsync(googleBookResult.Id, cancellationToken);       

        GoogleBookSearchResponseVolumeInfo volumeInfo = googleBookResult.VolumeInfo;

        List<Author> authors = await bookService.GetAuthors(volumeInfo.Authors, authorRepository);

        string[] googleBookCategories = volumeInfo.Categories;
        List<BookCategory> categories = await bookService.GetBookCategories(googleBookCategories, bookCategoryRepository);
        
        if (book is null)
        {           
            Result<Book> bookResult = Book.Create(
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

            if (bookResult.IsFailure)
            {
                return Result.Failure<Guid>(bookResult.Error);
            }

            bookRepository.Insert(bookResult.Value);

            book = bookResult.Value;
        }
        else
        {
            book.Update(
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
}
