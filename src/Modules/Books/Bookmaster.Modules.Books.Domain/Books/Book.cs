using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Books.Domain.Books;

public sealed class Book : Entity
{
    private readonly List<Author> _authors = [];

    // For EF Core to Materialise
    private Book()
    {
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Subtitle { get; private set; }

    public string Description { get; private set; }

    public string GoogleBookId { get; private set; }

    public IReadOnlyCollection<Author> Authors => [.. _authors];

    public static Book Create(
        string googleBookId,
        string title,
        string subTitle,
        string description)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            GoogleBookId = googleBookId,
            Title = title,
            Subtitle = subTitle,
            Description = description
        };

        return book;
    }
}
