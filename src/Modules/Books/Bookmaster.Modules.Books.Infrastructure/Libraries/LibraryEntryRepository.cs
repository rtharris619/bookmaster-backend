using Bookmaster.Modules.Books.Domain.Libraries;
using Bookmaster.Modules.Books.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookmaster.Modules.Books.Infrastructure.Libraries;

internal sealed class LibraryEntryRepository(BooksDbContext context) : ILibraryEntryRepository
{
    public Task<LibraryEntry?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.LibraryEntries
            .Include(x => x.Book)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public Task<List<LibraryEntry>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        return context.LibraryEntries.Where(x => x.PersonId == personId).ToListAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid personId, Guid bookId, CancellationToken cancellationToken = default)
    {
        return context.LibraryEntries
            .AnyAsync(x => x.PersonId == personId && x.BookId == bookId, cancellationToken);
    }

    public void Insert(LibraryEntry libraryEntry)
    {
        context.LibraryEntries.Add(libraryEntry);
    }
}
