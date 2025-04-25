using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Modules.Books.Features.Abstractions;
using Bookmaster.Modules.Books.Infrastructure.Database;
using Refit;
using Npgsql;
using Microsoft.AspNetCore.Builder;
using Bookmaster.Modules.Books.Features.Books;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Infrastructure.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Infrastructure.Library;
using Bookmaster.Modules.Books.Domain.People;
using Bookmaster.Modules.Books.Infrastructure.People;

namespace Bookmaster.Modules.Books.Infrastructure;

public static class BooksModule
{
    public static WebApplicationBuilder AddBooksModule(
        this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.AddFeatureServices();

        builder.Services.AddInfrastructure(configuration);

        builder.Services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return builder;
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<BooksDbContext>(options =>
        {
            options.UseNpgsql(
                databaseConnectionString,
                npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, "books"))
            .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<BooksDbContext>());

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ILibraryEntryRepository, LibraryEntryRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
    }

    private static void AddFeatureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddRefitClient<IGoogleBooksApi>(new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            })
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://www.googleapis.com/books/v1"));
    }
}
