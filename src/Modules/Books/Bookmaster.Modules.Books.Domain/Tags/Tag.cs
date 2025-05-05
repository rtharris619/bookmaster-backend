using Bookmaster.Common.Domain;

namespace Bookmaster.Modules.Books.Domain.Tags;

public sealed class Tag : Entity
{
    private Tag() { }

    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public static Tag Create(string name)
    {
        return new Tag
        {
            Id = Guid.NewGuid(),
            Name = name
        };
    }
}
