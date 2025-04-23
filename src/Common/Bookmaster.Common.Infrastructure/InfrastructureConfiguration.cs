using Bookmaster.Common.Features.Dates;
using Bookmaster.Common.Infrastructure.Dates;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Bookmaster.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddServices();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
