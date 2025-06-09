using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Modules.Books.Infrastructure.Database;
using Refit;
using Npgsql;
using Microsoft.AspNetCore.Builder;
using Bookmaster.Modules.Books.Domain.Books;
using Bookmaster.Modules.Books.Infrastructure.Books;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Bookmaster.Modules.Books.Domain.Library;
using Bookmaster.Modules.Books.Infrastructure.Library;
using Bookmaster.Modules.Books.Domain.People;
using Bookmaster.Modules.Books.Infrastructure.People;
using Bookmaster.Modules.Books.Features.Services;
using Bookmaster.Modules.Books.Domain.Tags;
using Bookmaster.Modules.Books.Infrastructure.Tags;
using Bookmaster.Modules.Books.Features.OpenLibrary;
using Bookmaster.Modules.Books.Features.GoogleBooks;
using Newtonsoft.Json;
using Bookmaster.Modules.Books.Features.Abstractions.Data;
using MassTransit;
using Bookmaster.Modules.Books.Infrastructure.Inbox;
using Bookmaster.Modules.Users.IntegrationEvents;
using Bookmaster.Common.Features.EventBus;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bookmaster.Modules.Books.Infrastructure;

public static class BooksModule
{
    public static WebApplicationBuilder AddBooksModule(
        this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.AddIntegrationEventHandlers();

        builder.AddFeatureServices();

        builder.Services.AddInfrastructure(configuration);

        builder.Services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return builder;
    }

    public static void ConfigureConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>();
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string databaseConnectionString = configuration.GetConnectionString("Database")!;

        services.AddDbContext<BooksDbContext>(options =>
        {
            options.UseNpgsql(
                databaseConnectionString,
                npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Books))
            .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<BooksDbContext>());

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<IBookCategoryRepository, BookCategoryRepository>();
        services.AddScoped<ILibraryEntryRepository, LibraryEntryRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        services.AddScoped<IBookService, BookService>();

        services.Configure<InboxOptions>(configuration.GetSection("Books:Inbox"));

        services.ConfigureOptions<ConfigureProcessInboxJob>();
    }

    private static void AddFeatureServices(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddRefitClient<IGoogleBooksApi>(new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            })
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://www.googleapis.com/books/v1"));

        builder.Services
            .AddRefitClient<IOpenLibraryApi>(new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer()
            })
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://openlibrary.org"));
    }

    private static void AddIntegrationEventHandlers(this IServiceCollection services)
    {
        Type[] integrationEventHandlers = Presentation.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler)))
            .ToArray();

        foreach (Type integrationEventHandler in integrationEventHandlers)
        {
            services.TryAddScoped(integrationEventHandler);

            Type integrationEvent = integrationEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler =
                typeof(IdempotentIntegrationEventHandler<>).MakeGenericType(integrationEvent);

            services.Decorate(integrationEventHandler, closedIdempotentHandler);
        }
    }
}
