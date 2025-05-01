using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookmaster.Modules.Books.Infrastructure.Books;

internal sealed class BookCategoryRepository(BooksDbContext context) : IBookCategoryRepository
{
    public async Task<BookCategory?> GetByNameAsync(string category, CancellationToken cancellationToken = default)
    {
        return await context.BookCategories
            .FirstOrDefaultAsync(c => c.Name.ToLower().Trim() == category.ToLower().Trim(),
            cancellationToken);
    }

    public void Insert(BookCategory category)
    {
        context.BookCategories.Add(category);
    }
}
