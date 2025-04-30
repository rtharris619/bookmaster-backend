using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.Library;

namespace Bookmaster.Modules.Books.Features.Library.LibraryEntrySearch;

internal sealed class LibraryEntrySearchQueryHandler(ILibraryEntryRepository libraryEntryRepository)
    : IQueryHandler<LibraryEntrySearchQuery, LibraryEntrySearchResponse>
{
    public async Task<Result<LibraryEntrySearchResponse>> Handle(LibraryEntrySearchQuery request, CancellationToken cancellationToken)
    {
        var result = new List<LibraryEntriesResponse>();

        Result<List<LibraryEntry>> libraryEntryResults = await libraryEntryRepository.GetByPersonIdAsync(request.PersonId, cancellationToken);

        List<LibraryEntry> libraryEntries = libraryEntryResults.Value;

        foreach (LibraryEntry libraryEntry in libraryEntries)
        {   
            Book book = libraryEntry.Book;
            IReadOnlyCollection<Author> authors = book.Authors;
            string[] authorNames = [.. authors.Select(x => x.Name)];

            result.Add(new LibraryEntriesResponse(
                libraryEntry.Id,
                libraryEntry.BookId,
                libraryEntry.PersonId,
                book.GoogleBookId,
                book.Title,
                book.Subtitle,
                book.Description, 
                authorNames,
                book.PageCount,
                book.Thumbnail));
        }

        return new LibraryEntrySearchResponse(libraryEntries.Count, result);
    }
}
