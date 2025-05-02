using Bookmaster.Modules.Books.Domain.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookmaster.Modules.Books.Infrastructure.Books;

internal sealed class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
{
    public void Configure(EntityTypeBuilder<BookCategory> builder)
    {
        builder.ToTable("book_categories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany<Book>().WithMany(x => x.Categories)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("book_book_categories");
            });
    }
}
