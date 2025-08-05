using Bookmaster.Common.Domain;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.Library;

namespace Bookmaster.Modules.Books.Domain.People;

public sealed class Person : Entity
{
    private readonly List<LibraryEntry> _libraryEntries = [];

    private Person() { }

    public Person(Guid id, string email, string firstName, string lastName, string identityId) 
    {
        Id = id;        
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        IdentityId = identityId;
    }

    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string IdentityId { get; private set; }

    public IReadOnlyCollection<LibraryEntry> LibraryEntries => [.. _libraryEntries];

    public static Person Create(Guid id, string email, string firstName, string lastName, string identityId)
    {
        return new Person
        {
            Id = id,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            IdentityId = identityId
        };
    }

    public void Update(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
