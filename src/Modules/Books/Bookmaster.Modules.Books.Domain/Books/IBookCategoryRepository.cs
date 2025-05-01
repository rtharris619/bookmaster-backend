namespace Bookmaster.Modules.Books.Domain.Books;

public interface IBookCategoryRepository
{
    Task<BookCategory?> GetByNameAsync(string category, CancellationToken cancellationToken = default);

    void Insert(BookCategory category);
}
