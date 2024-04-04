var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Pithee");
// change to get from post body instead of query-string
app.MapPost(
    "/signup",
    (Credentials user) => Results.Created(
        uri: $"/users/{user.Username}",
        value: user)
);

app.Run();

// make the program available to the test project
public partial class Program { }

public record Credentials(string Username, string Password);