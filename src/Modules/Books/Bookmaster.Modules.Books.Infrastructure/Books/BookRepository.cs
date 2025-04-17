using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookmaster.Modules.Books.Infrastructure.Books;

internal sealed class BookRepository(BooksDbContext context) : IBookRepository
{
    public async Task<Book?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Books
            .Include(b => b.Authors)
            .SingleOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public void Insert(Book book)
    {
        context.Books.Add(book);
    }
}
