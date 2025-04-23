using System.Reflection;
using Bookmaster.Api.Extensions;
using Bookmaster.Common.Features;
using Bookmaster.Common.Infrastructure;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Modules.Books.Features;
using Bookmaster.Modules.Books.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Assembly[] moduleFeatureAssemblies = [
    Bookmaster.Modules.Books.Features.AssemblyReference.Assembly
];

builder.Services.AddFeatures(moduleFeatureAssemblies);

builder.Services.AddInfrastructure();

builder.AddBooksModule(builder.Configuration);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
