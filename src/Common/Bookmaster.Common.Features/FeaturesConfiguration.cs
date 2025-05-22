using System.Reflection;
using Bookmaster.Common.Domain;
using Bookmaster.Common.Features.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Bookmaster.Common.Features;

public static class FeaturesConfiguration
{
    public static IServiceCollection AddFeatures(
        this IServiceCollection services,
        Assembly[] moduleAssemblies)
    {
        services.Scan(scan => scan
            .FromAssemblies(moduleAssemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(moduleAssemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)), publicOnly: false)
                .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)), publicOnly: false)
                .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(scan => scan
            .FromAssemblies(moduleAssemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)), publicOnly: false)
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}
