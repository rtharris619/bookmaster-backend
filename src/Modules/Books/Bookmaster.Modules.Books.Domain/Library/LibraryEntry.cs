using Bookmaster.Common.Domain;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.People;

namespace Bookmaster.Modules.Books.Domain.Library;

public sealed class LibraryEntry : Entity
{
    private LibraryEntry() { }

    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public Guid PersonId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public Book Book { get; set; }
    public Person Person { get; set; }

    public static LibraryEntry Create(Book book, Person person, DateTime createdOnUtc)
    {
        return new LibraryEntry
        {
            Id = Guid.NewGuid(),
            BookId = book.Id,
            PersonId = person.Id,
            CreatedOnUtc = createdOnUtc
        };
    }
}
