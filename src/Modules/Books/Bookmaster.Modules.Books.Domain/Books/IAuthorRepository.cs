namespace Bookmaster.Modules.Books.Domain.Books;

public interface IAuthorRepository
{
    Task<Author?> GetByNameAsync(string author, CancellationToken cancellationToken = default);

    void Insert(Author author);
}
