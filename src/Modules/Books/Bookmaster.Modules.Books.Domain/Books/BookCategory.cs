namespace Bookmaster.Modules.Books.Domain.Books;

public sealed class BookCategory
{
    private BookCategory()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public static BookCategory Create(string name)
    {
        var bookCategory = new BookCategory
        {
            Id = Guid.NewGuid(),
            Name = name
        };
        return bookCategory;
    }
}
