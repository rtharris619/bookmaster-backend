using Bookmaster.Common.Features.Data;
using Bookmaster.Common.Features.Dates;
using Bookmaster.Common.Infrastructure.Authentication;
using Bookmaster.Common.Infrastructure.Data;
using Bookmaster.Common.Infrastructure.Dates;
using Bookmaster.Common.Infrastructure.Outbox;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Quartz;

namespace Bookmaster.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string databaseConnectionString)
    {
        services.AddAuthenticationInternal();

        services.AddServices(databaseConnectionString);

        services.AddQuartz();
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services, string databaseConnectionString)
    {
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.TryAddSingleton<InsertOutboxMessagesInterceptor>();

        NpgsqlDataSource npgsqlDataSource = new NpgsqlDataSourceBuilder(databaseConnectionString).Build();
        services.TryAddSingleton(npgsqlDataSource);

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        return services;
    }
}
