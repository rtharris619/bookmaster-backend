using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Books.Domain;

public sealed class GoogleBook : Entity
{
    private readonly List<GoogleBookAuthor> _authors = [];

    // For EF Core to Materialise
    private GoogleBook()
    {
    }

    public Guid Id { get; private set; }

    public string Title { get; private set; }

    public string Subtitle { get; private set; }

    public string Description { get; private set; }

    public string GoogleBookId { get; private set; }

    public IReadOnlyCollection<GoogleBookAuthor> Authors => [.. _authors];

    public static GoogleBook Create(
        string googleBookId,
        string title,
        string subTitle,
        string description)
    {
        var googleBook = new GoogleBook
        {
            Id = Guid.NewGuid(),
            GoogleBookId = googleBookId,
            Title = title,
            Subtitle = subTitle,
            Description = description
        };

        return googleBook;
    }
}
