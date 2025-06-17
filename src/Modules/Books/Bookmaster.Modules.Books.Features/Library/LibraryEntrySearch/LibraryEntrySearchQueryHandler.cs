using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Domain.People;

namespace Bookmaster.Modules.Books.Features.Library.LibraryEntrySearch;

internal sealed class LibraryEntrySearchQueryHandler(
    ILibraryEntryRepository libraryEntryRepository,
    IPersonRepository personRepository)
    : IQueryHandler<LibraryEntrySearchQuery, LibraryEntrySearchResponse>
{
    public async Task<Result<LibraryEntrySearchResponse>> Handle(LibraryEntrySearchQuery request, CancellationToken cancellationToken)
    {
        var result = new List<LibraryEntriesResponse>();

        Person? person = await personRepository.GetByIdentityIdAsync(request.IdentityId, cancellationToken);

        if (person is null)
        {
            return null;
        }

        List<LibraryEntry> libraryEntries = await libraryEntryRepository.GetByPersonIdAsync(person.Id, cancellationToken);

        foreach (LibraryEntry libraryEntry in libraryEntries)
        {   
            Book book = libraryEntry.Book;

            IReadOnlyCollection<Author> authors = book.Authors;
            string[] authorNames = [.. authors.Select(x => x.Name)];
            string flattenedAuthors = string.Join(", ", authorNames);

            IReadOnlyCollection<BookCategory> bookCategories = book.Categories;
            string[] categoryNames = [.. bookCategories.Select(x => x.Name)];
            string flattenedCategories = string.Join(", ", categoryNames);

            result.Add(new LibraryEntriesResponse(
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
                book.PageCount,
                book.Thumbnail,
                book.Publisher,
                book.PublishedDate,
                book.Language,
                book.GoogleBookInfoLink,
                book.GoogleBookPreviewLink));
        }

        return new LibraryEntrySearchResponse(libraryEntries.Count, result);
    }
}
