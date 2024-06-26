using System.Net;
using System.Net.Http.Json;

namespace Pithee.Api.Tests.Users.Signup;

public class With_valid_username_and_password
    : Given_an_api
{
    const string Path = "/users/signup";
    readonly string _username = $"testuser-{Guid.NewGuid()}";
    readonly string _password = Guid.NewGuid().ToString();
    readonly Credentials _payload;
    readonly JsonContent _content;

    public With_valid_username_and_password()
    {
        _payload = new(_username, _password);
        _content = JsonContent.Create(_payload);
    }


    [Fact]
    public async Task It_responds_with_a_201_created()
    {
        var response = await _client.PostAsync(Path, _content);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task It_responds_with_the_location_of_the_new_user()
    {
        var response = await _client.PostAsync(Path, _content);

        response
            .Headers
            .Location
            .Should()
            .Be($"/users/{_username}");
    }

    [Fact]
    public async Task It_responds_with_the_new_user()
    {
        var response = await _client.PostAsync(Path, _content);
        var content = await response.Content.ReadFromJsonAsync<Response>();

        content
            !.PreferredUsername
            .Should()
            .Be(_payload.Username);
    }

    record Credentials(string Username, string Password);

    record Response(string PreferredUsername);
}