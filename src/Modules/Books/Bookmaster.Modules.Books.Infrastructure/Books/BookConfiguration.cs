using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookmaster.Modules.Books.Infrastructure.Books;

internal sealed class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany<Person>()
            .WithMany(p => p.Books)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("person_books");
            });
    }
}
