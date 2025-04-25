using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookmaster.Modules.Books.Infrastructure.People;

internal sealed class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.LibraryEntries);

        builder.HasData(CreatePeople());
    }

    private static List<Person> CreatePeople()
    {
        return [
            new Person(Guid.Parse("2c356126-124e-4b99-b2b3-1c848dedf966"), "ryan@bookmaster.com", "Ryan", "Harris"),
            new Person(Guid.Parse("9ed784e0-6231-4bf8-9b98-b16716dede98"), "claudene@bookmaster.com", "Claudene", "Harris")
        ];
    }
}
