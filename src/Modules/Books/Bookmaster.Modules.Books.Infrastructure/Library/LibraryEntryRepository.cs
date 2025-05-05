using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookmaster.Modules.Books.Infrastructure.Library;

internal sealed class LibraryEntryRepository(BooksDbContext context) : ILibraryEntryRepository
{
    public Task<LibraryEntry?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.LibraryEntries
            .Include(x => x.Book).ThenInclude(x => x.Authors)
            .Include(x => x.Book).ThenInclude(x => x.Categories)
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public Task<List<LibraryEntry>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default)
    {
        return context.LibraryEntries.Where(x => x.PersonId == personId)
            .Include(x => x.Book).ThenInclude(x => x.Authors)
            .Include(x => x.Book).ThenInclude(x => x.Categories)
            .Include(x => x.Tags)
            .ToListAsync(cancellationToken);
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

    public void Delete(LibraryEntry libraryEntry)
    {
        context.LibraryEntries.Remove(libraryEntry);
    }
}
