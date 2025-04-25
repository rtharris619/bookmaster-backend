namespace Bookmaster.Modules.Books.Domain.Library;

public interface ILibraryEntryRepository
{
    Task<LibraryEntry?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<LibraryEntry>> GetByPersonIdAsync(Guid personId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid personId, Guid bookId, CancellationToken cancellationToken = default);
    void Insert(LibraryEntry libraryEntry);
}
