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

    public string? Description { get; private set; }

    public string GoogleBookId { get; private set; }

    public int PageCount { get; private set; }

    public string PrintType { get; private set; }

    public string? Thumbnail { get; private set; }

    public string? Publisher { get; private set; }

    public string? PublishedDate { get; private set; }

    public string Language { get; private set; }

    public string? GoogleBookInfoLink { get; private set; }

    public string? GoogleBookPreviewLink { get; private set; }

    public IReadOnlyCollection<Author> Authors => [.. _authors];
    public IReadOnlyCollection<BookCategory> Categories => [.. _categories];
    public IReadOnlyCollection<LibraryEntry> LibraryEntries => [.. _libraryEntries];

    public static Book Create(
        List<Author> authors,
        List<BookCategory>? categories,
        string googleBookId,
        string title,
        string? subTitle,
        string? description,
        int pageCount,
        string printType,
        string? thumbnail,
        string? publisher,
        string? publishedDate,
        string language,
        string? googleBookInfoLink,
        string? googleBookPreviewLink)
    {
        var book = new Book
        {
            Id = Guid.NewGuid(),
            GoogleBookId = googleBookId,
            Title = title,
            Subtitle = subTitle,
            Description = description,            
            PageCount = pageCount,
            PrintType = printType,
            Thumbnail = thumbnail,
            Publisher = publisher,
            PublishedDate = publishedDate,
            Language = language,
            GoogleBookInfoLink = googleBookInfoLink,
            GoogleBookPreviewLink = googleBookPreviewLink
        };

        book._authors.AddRange(authors);

        if (categories is not null)
        {
            book._categories.AddRange(categories);
        }

        return book;
    }

    public void Update(
        List<Author> authors,
        List<BookCategory>? categories,
        string googleBookId,
        string title,
        string? subTitle,
        string? description,
        int pageCount,
        string printType,
        string? thumbnail,
        string? publisher,
        string? publishedDate,
        string language,
        string? googleBookInfoLink,
        string? googleBookPreviewLink)
    {
        UpdateAuthors(authors);

        if (categories is not null)
        {
            UpdateCategories(categories);
        }

        GoogleBookId = googleBookId;
        Title = title;
        Subtitle = subTitle;
        Description = description;
        PageCount = pageCount;
        PrintType = printType;
        Thumbnail = thumbnail;
        Publisher = publisher;
        PublishedDate = publishedDate;
        Language = language;
        GoogleBookInfoLink = googleBookInfoLink;
        GoogleBookPreviewLink = googleBookPreviewLink;
    }

    private void UpdateAuthors(List<Author> authors)
    {
        _authors.Clear();
        _authors.AddRange(authors);
    }

    private void UpdateCategories(List<BookCategory> categories)
    {
        _categories.Clear();
        _categories.AddRange(categories);
    }
}
