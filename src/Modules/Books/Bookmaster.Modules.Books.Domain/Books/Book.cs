using Bookmaster.Common.Domain;
using Bookmaster.Modules.Books.Domain.Library;

namespace Bookmaster.Modules.Books.Domain.Books;

public sealed class Book : Entity
{
    private readonly List<Author> _authors = [];
    private readonly List<BookCategory> _categories = [];
    private readonly List<LibraryEntry> _libraryEntries = [];

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

    public string? PublishedDate { get; private set; }

    public IReadOnlyCollection<Author> Authors => [.. _authors];
    public IReadOnlyCollection<BookCategory> Categories => [.. _categories];
    public IReadOnlyCollection<LibraryEntry> LibraryEntries => [.. _libraryEntries];

    public static Book Create(
        List<Author> authors,
        List<BookCategory>? categories,
        string googleBookId,
        string title,
        string? subTitle,
        string description,
        string? textSnippet,
        string? googleBookInfoLink,
        int pageCount,
        string? thumbnail,
        string? smallThumbnail,
        string? publisher,
        string? publishedDate)
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

        if (categories is not null)
        {
            book._categories.AddRange(categories);
        }

        return book;
    }
}
