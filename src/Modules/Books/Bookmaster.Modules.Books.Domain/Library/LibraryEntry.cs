using Bookmaster.Common.Domain;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.People;
using Bookmaster.Modules.Books.Domain.Tags;

namespace Bookmaster.Modules.Books.Domain.Library;

public sealed class LibraryEntry : Entity
{
    private readonly List<Tag> _tags = [];

    private LibraryEntry() { }

    public Guid Id { get; private set; }
    public Guid BookId { get; private set; }
    public Guid PersonId { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }

    public Book Book { get; set; }
    public Person Person { get; set; }

    public IReadOnlyCollection<Tag> Tags => [.. _tags];

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

    public void AddTags(
        List<Tag> tags)
    {
        _tags.Clear();
        _tags.AddRange(tags);
    }
}
