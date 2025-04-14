namespace Bookmaster.Modules.Books.Domain.Books;

public sealed class Author
{
    private Author()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public static Author Create(string name)
    {
        var author = new Author
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        return author;
    }
}
