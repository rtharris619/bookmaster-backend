using Bookmaster.Modules.Books.Domain.People;
using Bookmaster.Modules.Books.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookmaster.Modules.Books.Infrastructure.People;

internal sealed class PersonRepository(BooksDbContext context) : IPersonRepository
{
    public Task<Person?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.People
            .Include(p => p.LibraryEntries)
            .SingleOrDefaultAsync(p => p.Id == id,
                cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.People
            .AnyAsync(x => x.Id == id, cancellationToken);
    }

    public void Insert(Person person)
    {
        context.People.Add(person);
    }
}
