using Microsoft.AspNetCore.Mvc.Testing;

namespace Api.Tests;

public abstract class Given_an_api
{
    protected readonly WebApplicationFactory<Program> _application;
    protected readonly HttpClient _client;

    public Given_an_api()
    {
        _application = new WebApplicationFactory<Program>();
        _client = _application.CreateClient();
    }
}
