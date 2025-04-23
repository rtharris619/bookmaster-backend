using Bookmaster.Modules.Books.Domain.Person;
using Bookmaster.Modules.Books.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookmaster.Modules.Books.Infrastructure.People;

internal sealed class PersonRepository(BooksDbContext context) : IPersonRepository
{
    public Task<Person?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return context.People
            .Include(p => p.Books)
            .SingleOrDefaultAsync(p => p.Id == id,
                cancellationToken);
    }

    public void Insert(Person person)
    {
        context.People.Add(person);
    }
}
