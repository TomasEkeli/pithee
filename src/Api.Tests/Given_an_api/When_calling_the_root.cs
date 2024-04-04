using Microsoft.AspNetCore.Mvc.Testing;

namespace Api.Tests.Given_an_api;

public class When_calling_the_root
{
    [Fact]
    public async Task It_responds_with_a_200_ok()
    {
        var application = new WebApplicationFactory<Program>();
        var client = application.CreateClient();

        var response = await client.GetAsync("/");

        response.EnsureSuccessStatusCode();
    }
}