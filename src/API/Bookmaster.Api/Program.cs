using Bookmaster.Common.Features;
using Bookmaster.Common.Presentation.Endpoints;
using Bookmaster.Modules.Books.Features;
using Bookmaster.Modules.Books.Infrastructure;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFeatures([Bookmaster.Modules.Books.Features.AssemblyReference.Assembly]);

builder.AddBooksModule();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();
