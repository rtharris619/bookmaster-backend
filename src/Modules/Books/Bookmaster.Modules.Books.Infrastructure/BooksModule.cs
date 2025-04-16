using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Modules.Books.Features.Abstractions;
using Bookmaster.Modules.Books.Infrastructure.Database;
using Refit;
using Microsoft.AspNetCore.Builder;
using Bookmaster.Modules.Books.Features.Books;

namespace Bookmaster.Modules.Books.Infrastructure;

public static class BooksModule
{
    public static WebApplicationBuilder AddBooksModule(
        this WebApplicationBuilder builder)
    {
        builder.AddFeatureServices();

        //builder.Services.AddInfrastructure();

        builder.Services.AddEndpoints(Presentation.AssemblyReference.Assembly);

        return builder;
    }

    private static void AddInfrastructure(this IServiceCollection services)
    {
        //services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<BooksDbContext>());
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
