using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Bookmaster.Common.Features;

public static class FeaturesConfiguration
{
    public static IServiceCollection AddFeatures(
        this IServiceCollection services,
        Assembly[] moduleAssemblies)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(moduleAssemblies);
        });

        return services;
    }
}
