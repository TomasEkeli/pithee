using System.Net;
using System.Net.Http.Json;

namespace Pithee.Api.Tests.Users;

public class When_the_user_exists : Given_an_api
{
    const string Domain = "pithee.example.net";
    readonly string _username = "testuser-" + Guid.NewGuid();

    async Task<HttpResponseMessage> Signup(
        string username,
        string password)
    => await _client.PostAsJsonAsync(
            "/users/signup",
            new { username, password }
        );

    async Task<UserResponse> SignupAndGetUser(
        string username,
        string password)
    {
        var response = await Signup(username, password);

        return await _client
            .GetFromJsonAsync<UserResponse>(
                response!.Headers.Location!.ToString()
            );
    }

    [Fact]
    public async Task It_responds_with_a_200_ok()
    {
        var signup_response = await Signup(
            username: _username,
            password: Guid.NewGuid().ToString()
        );

        var user_response = await _client
            .GetAsync(
                signup_response!.Headers.Location!.ToString()
            );

        user_response
            !.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task It_responds_with_a_user()
    {
        var user = await SignupAndGetUser(
            username: _username,
            password: Guid.NewGuid().ToString()
        );

        user.Should().NotBeNull();
    }

    [Fact]
    public async Task It_responds_with_the_expected_username()
    {
        var user = await SignupAndGetUser(
            username: _username,
            password: Guid.NewGuid().ToString()
        );

        user!.PreferredUsername.Should().Be(_username);
    }

    [Fact]
    public async Task It_responds_with_a_public_key()
    {
        var user = await SignupAndGetUser(
            username: _username,
            password: Guid.NewGuid().ToString()
        );

        user!.PublicKey.Should().NotBeNull();
    }

    [Fact]
    public async Task It_responds_with_an_id()
    {
        var expected_id = $"https://{Domain}/users/{_username}";

        var user = await SignupAndGetUser(
            username: _username,
            password: Guid.NewGuid().ToString()
        );

        user!.Id.Should().Be(expected_id);
    }

    record UserResponse(
        string Id,
        string PreferredUsername,
        Key PublicKey
    );

    record Key(
        string Id,
        string Owner,
        string PublicKeyPem
    );
}