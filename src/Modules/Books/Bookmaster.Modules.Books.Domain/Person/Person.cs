using Bookmaster.Common.Domain;
using Bookmaster.Modules.Books.Domain.Books;

namespace Bookmaster.Modules.Books.Domain.Person;

public sealed class Person : Entity
{
    private readonly List<Book> _books = [];

    private Person() { }

    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public IReadOnlyCollection<Book> Books => [.. _books];

    public static Person Create(Guid id, string email, string firstName, string lastName)
    {
        return new Person
        {
            Id = id,
            Email = email,
            FirstName = firstName,
            LastName = lastName
        };
    }

    public static void AddBookToPerson(Person person, Book book)
    {
        person._books.Add(book);
    }
}
