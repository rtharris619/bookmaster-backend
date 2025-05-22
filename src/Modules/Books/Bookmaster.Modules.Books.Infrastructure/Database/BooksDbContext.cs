using System.Data.Common;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Domain.People;
using Bookmaster.Modules.Books.Domain.Tags;
using Bookmaster.Modules.Books.Features.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bookmaster.Modules.Books.Infrastructure.Database;

public sealed class BooksDbContext(DbContextOptions<BooksDbContext> options)
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Author> Authors { get; set; }
    internal DbSet<Book> Books { get; set; }
    internal DbSet<BookCategory> BookCategories { get; set; }
    internal DbSet<LibraryEntry> LibraryEntries { get; set; }
    internal DbSet<Person> People { get; set; }
    internal DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Books);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }

    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }

        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }
}
