using Bookmaster.Modules.Books.Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookmaster.Modules.Books.Infrastructure.Books;

internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany<Book>().WithMany(x => x.Authors)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("book_authors");
            });
    }
}
