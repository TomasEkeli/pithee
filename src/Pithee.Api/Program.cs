using Pithee.Api.Signup;
using Pithee.Api.Webfinger;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IWebFingerHandler, WebFingerHandler>();
builder.Services.AddTransient<ISignupHandler, SignupHandler>();
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

app.Run();

// make the program available to the test project
public partial class Program { }

public record Credentials(string Username, string Password);