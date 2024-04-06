using System.Net;
using System.Net.Http.Json;

namespace Pithee.Api.Tests.Users;

public class When_the_user_does_not_exist : Given_an_api
{
    [Fact]
    public async Task It_responds_with_a_404_not_found()
    {
        var response = await _client.GetAsync("/users/" + Guid.NewGuid());

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}

public class When_the_user_exists : Given_an_api
{
    readonly string _username = "testuser-" + Guid.NewGuid();

    async Task<HttpResponseMessage> SignupUser(
        string username,
        string password)
    {
        return await _client.PostAsJsonAsync(
            "/signup",
            new { username, password }
        );
    }

    [Fact]
    public async Task It_responds_with_a_200_ok()
    {
        var signup_response = await SignupUser(
            username: _username,
            password: Guid.NewGuid().ToString()
        );

        var response = await _client.GetAsync(
            signup_response.Headers.Location!.ToString()
        );

        response
            .StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }
}