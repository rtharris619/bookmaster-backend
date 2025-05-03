using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Domain.Tags;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookmaster.Modules.Books.Infrastructure.Tags;

internal sealed class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasMany<LibraryEntry>()
            .WithMany(x => x.Tags)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("library_entry_tags");
            });
    }
}
