using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookmaster.Modules.Books.Infrastructure.Books;

internal sealed class AuthorRepository(BooksDbContext context) : IAuthorRepository
{
    public async Task<Author?> GetByNameAsync(string author, CancellationToken cancellationToken = default)
    {
        return await context.Authors
            .FirstOrDefaultAsync(a => a.Name.ToLower().Trim() == author.ToLower().Trim(),
            cancellationToken);
    }

    public void Insert(Author author)
    {
        context.Authors.Add(author);
    }
}
