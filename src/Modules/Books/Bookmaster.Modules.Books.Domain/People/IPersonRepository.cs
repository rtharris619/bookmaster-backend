namespace Bookmaster.Modules.Books.Domain.People;

public interface IPersonRepository
{
    Task<Person?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Person?> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken = default);

    void Insert(Person person);
}
