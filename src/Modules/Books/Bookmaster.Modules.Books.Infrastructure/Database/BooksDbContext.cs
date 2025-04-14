using System.Data.Common;
using Bookmaster.Modules.Books.Application.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bookmaster.Modules.Books.Infrastructure.Database;

public sealed class BooksDbContext(DbContextOptions<BooksDbContext> options)
    : DbContext(options), IUnitOfWork
{
    public async Task<DbTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (Database.CurrentTransaction is not null)
        {
            await Database.CurrentTransaction.DisposeAsync();
        }

        return (await Database.BeginTransactionAsync(cancellationToken)).GetDbTransaction();
    }
}
