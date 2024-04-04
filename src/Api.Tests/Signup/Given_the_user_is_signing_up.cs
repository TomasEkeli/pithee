using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Api.Tests.Signup;

public class Given_the_user_is_signing_up
{
    protected readonly WebApplicationFactory<Program> _application;
    protected readonly HttpClient _client;

    public Given_the_user_is_signing_up()
    {
        _application = new WebApplicationFactory<Program>();
        _client = _application.CreateClient();
    }
}