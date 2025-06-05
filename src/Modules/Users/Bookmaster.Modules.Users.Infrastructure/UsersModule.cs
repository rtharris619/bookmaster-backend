using Bookmaster.Modules.Users.Domain.Users;
using Bookmaster.Modules.Users.Features.Abstractions.Data;
using Bookmaster.Modules.Users.Features.Abstractions.Identity;
using Bookmaster.Modules.Users.Infrastructure.Database;
using Bookmaster.Modules.Users.Infrastructure.Identity;
using Bookmaster.Modules.Users.Infrastructure.Users;
using Bookmaster.Common.Presentation.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Bookmaster.Common.Infrastructure.Outbox;
using Bookmaster.Common.Domain;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Bookmaster.Modules.Users.Infrastructure.Outbox;

namespace Bookmaster.Modules.Users.Infrastructure;

public static class UsersModule
{
    public static WebApplicationBuilder AddUsersModule(
        this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        builder.Services.AddDomainEventHandlers();

        builder.Services.AddInfrastructure(configuration);

        builder.Services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return builder;
    }

    private static void AddDomainEventHandlers(this IServiceCollection services)
    {
        Type[] domainEventHandlers = [.. Features.AssemblyReference.Assembly
            .GetTypes()
            .Where(t => t.IsAssignableTo(typeof(IDomainEventHandler)))];

        foreach (Type domainEventHandler in domainEventHandlers)
        {
            services.TryAddScoped(domainEventHandler);

            Type domainEvent = domainEventHandler
                .GetInterfaces()
                .Single(i => i.IsGenericType)
                .GetGenericArguments()
                .Single();

            Type closedIdempotentHandler = typeof(IdempotentDomainEventHandler<>).MakeGenericType(domainEvent);

            services.Decorate(domainEventHandler, closedIdempotentHandler);
        }
    }

    private static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // KeyCloak / Identity

        services.Configure<KeyCloakOptions>(configuration.GetSection("Users:KeyCloak"));

        services.AddTransient<KeyCloakAuthDelegatingHandler>();

        services.AddHttpClient<KeyCloakClient>((serviceProvider, httpClient) =>
        {
            KeyCloakOptions keyCloakOptions = serviceProvider.GetRequiredService<IOptions<KeyCloakOptions>>().Value;

            httpClient.BaseAddress = new Uri(keyCloakOptions.AdminUrl);
        })
        .AddHttpMessageHandler<KeyCloakAuthDelegatingHandler>();

        services.AddTransient<IIdentityProviderService, IdentityProviderService>();

        // Database

        string databaseConnectionString = configuration.GetConnectionString("Database")!;
        services.AddDbContext<UsersDbContext>((sp, options) =>
        {
            options.UseNpgsql(
                databaseConnectionString,
                npgsqlOptions => npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Users))
            .AddInterceptors(sp.GetRequiredService<InsertOutboxMessagesInterceptor>())
            .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        services.Configure<OutboxOptions>(configuration.GetSection("Users:Outbox"));
        services.ConfigureOptions<ConfigureProcessOutboxJob>();
    }
}
