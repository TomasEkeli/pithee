using System.Net;
using System.Net.Http.Json;

namespace Pithee.Api.Tests.Signup;

public class When_the_user_provides_a_valid_username_and_password
    : Given_an_api
{
    readonly Credentials _payload;
    readonly JsonContent _content;

    public When_the_user_provides_a_valid_username_and_password()
    {
        _payload = new("testuser", "password");
        _content = JsonContent.Create(_payload);
    }


    [Fact]
    public async Task It_responds_with_a_201_created()
    {
        var response = await _client.PostAsync("/signup", _content);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task It_responds_with_the_location_of_the_new_user()
    {
        var response = await _client.PostAsync("/signup", _content);

        response.Headers.Location.Should().Be("/users/testuser");
    }

    [Fact]
    public async Task It_responds_with_the_new_user()
    {
        var response = await _client.PostAsync("/signup", _content);
        var content = await response.Content.ReadFromJsonAsync<Credentials>();

        content.Should().Be(_payload);
    }

    record Credentials(string Username, string Password);
}