using Bookmaster.Modules.Books.Domain.Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bookmaster.Modules.Books.Infrastructure.Library;

internal sealed class LibraryEntryConfiguration : IEntityTypeConfiguration<LibraryEntry>
{
    public void Configure(EntityTypeBuilder<LibraryEntry> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
