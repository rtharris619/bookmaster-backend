namespace Bookmaster.Modules.Books.Domain;

public sealed class GoogleBookAuthor
{
    private GoogleBookAuthor()
    {
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; }

    public static GoogleBookAuthor Create(string name)
    {
        var googleBookAuthor = new GoogleBookAuthor
        {
            Id = Guid.NewGuid(),
            Name = name
        };

        return googleBookAuthor;
    }
}
