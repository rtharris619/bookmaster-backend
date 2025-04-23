namespace Bookmaster.Modules.Books.Domain.Person;

public interface IPersonRepository
{
    Task<Person?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Person person);
}
