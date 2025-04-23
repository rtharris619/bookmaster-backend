namespace Bookmaster.Modules.Books.Domain.Books;

public interface IBookRepository
{
    Task<Book?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Book?> GetByGoogleBookIdAsync(string id, CancellationToken cancellationToken = default);

    void Insert(Book book);
}
