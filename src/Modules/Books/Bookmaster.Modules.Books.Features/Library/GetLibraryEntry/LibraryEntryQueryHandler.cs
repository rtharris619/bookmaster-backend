using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Domain.Tags;
using Bookmaster.Modules.Books.Features.Library.LibraryEntrySearch;

namespace Bookmaster.Modules.Books.Features.Library.GetLibraryEntry;

internal sealed class LibraryEntryQueryHandler(
    ILibraryEntryRepository libraryEntryRepository)
    : IQueryHandler<LibraryEntryQuery, LibraryEntryResponse>
{
    public async Task<Result<LibraryEntryResponse>> Handle(LibraryEntryQuery request, CancellationToken cancellationToken)
    {
        LibraryEntry? libraryEntry = await libraryEntryRepository.GetAsync(request.Id, cancellationToken);

        if (libraryEntry is null)
        {
            return Result.Failure<LibraryEntryResponse>(LibraryEntryErrors.NotFound(request.Id));
        }

        Book book = libraryEntry.Book;

        IReadOnlyCollection<Author> authors = book.Authors;
        string[] authorNames = [.. authors.Select(x => x.Name)];
        string flattenedAuthors = string.Join(", ", authorNames);

        IReadOnlyCollection<BookCategory> bookCategories = book.Categories;
        string[] categoryNames = [.. bookCategories.Select(x => x.Name)];
        string flattenedCategories = string.Join(", ", categoryNames);

        IReadOnlyCollection<Tag> tags = libraryEntry.Tags;

        string[]? tagNames = [];
        string? flattenedTags = null;
        if (tags.Count > 0)
        {
            tagNames = [.. tags.Select(x => x.Name)];
            flattenedTags = string.Join(", ", tagNames);
        }

        return new LibraryEntryResponse(
            libraryEntry.Id,
            libraryEntry.BookId,
            libraryEntry.PersonId,
            book.GoogleBookId,
            book.Title,
            book.Subtitle,
            book.Description,
            authorNames,
            flattenedAuthors,
            categoryNames,
            flattenedCategories,
            tagNames,
            flattenedTags,
            book.PageCount,
            book.Thumbnail,
            book.Publisher,
            book.PublishedDate,
            book.Language,
            book.GoogleBookInfoLink,
            book.GoogleBookPreviewLink);
    }
}
