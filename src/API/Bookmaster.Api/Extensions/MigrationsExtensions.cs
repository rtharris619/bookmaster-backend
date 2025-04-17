using Bookmaster.Modules.Books.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Bookmaster.Api.Extensions;

internal static class MigrationsExtensions
{
    internal static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        ApplyMigration<BooksDbContext>(scope);
    }

    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using DbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();
        context.Database.Migrate();
    }
}
