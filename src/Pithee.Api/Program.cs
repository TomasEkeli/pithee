using Pithee.Api;
using Pithee.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapGet("/", () => "Pithee");

var initDb =  app
    .Services
    .GetRequiredService<IPersistenceInitializer>();
await initDb.Initialize();

app.Run();

// make the program available to the test project
public partial class Program { }
