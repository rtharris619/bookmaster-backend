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

    public string? Subtitle { get; private set; }

    public string Description { get; private set; }

    public string? TextSnippet { get; private set; }

    public string GoogleBookId { get; private set; }

    public string? GoogleBookInfoLink { get; set; }

    public int PageCount { get; private set; }

    public string? Thumbnail { get; private set; }

    public string? SmallThumbnail { get; private set; }

    public string? Publisher { get; private set; }

    public DateTime? PublishedDate { get; private set; }

    public IReadOnlyCollection<Author> Authors => [.. _authors];

    public static Book Create(
        List<Author> authors,
        string googleBookId,
        string title,
        string subTitle,
        string description,
        string? textSnippet,
        string googleBookInfoLink,
        int pageCount,
        string thumbnail,
        string smallThumbnail,
        string publisher,
        DateTime publishedDate)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            GoogleBookId = googleBookId,
            Title = title,
            Subtitle = subTitle,
            Description = description,
            TextSnippet = textSnippet,
            GoogleBookInfoLink = googleBookInfoLink,
            PageCount = pageCount,
            Thumbnail = thumbnail,
            SmallThumbnail = smallThumbnail,
            Publisher = publisher,
            PublishedDate = publishedDate
        };

        book._authors.AddRange(authors);

        return book;
    }
}
