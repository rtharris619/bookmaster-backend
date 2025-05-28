using System.Reflection;
using Bookmaster.Api.Extensions;
using Bookmaster.Api.Settings;
using Bookmaster.Common.Features;
using Bookmaster.Common.Infrastructure;
using Bookmaster.Common.Infrastructure.Configuration;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Modules.Books.Features;
using Bookmaster.Modules.Books.Infrastructure;
using Bookmaster.Modules.Users.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.AddCorsPolicy();

builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Assembly[] moduleFeatureAssemblies = [
    Bookmaster.Modules.Books.Features.AssemblyReference.Assembly
];

builder.Services.AddFeatures(moduleFeatureAssemblies);

string databaseConnectionString = builder.Configuration.GetConnectionStringOrThrow("Database");

builder.Services.AddInfrastructure(databaseConnectionString);

builder.Configuration.AddModuleConfiguration(["users"]);

builder.AddBooksModule(builder.Configuration);

builder.AddUsersModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseCors(CorsOptions.PolicyName);

app.MapEndpoints();

app.Run();
