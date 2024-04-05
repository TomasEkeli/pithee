using System.Net.Http.Json;

namespace Api.Tests.WebFinger;

public class When_the_user_has_been_registered : Given_an_api
{
    readonly string _username;

    public When_the_user_has_been_registered()
    {
        _username = $"alice@example.com";
    }

    [Fact]
    public async Task It_responds_with_a_200_ok()
    {
        var response = await _client.GetAsync(
            $"/.well-known/webfinger?resource={_username}"
        );

        response.EnsureSuccessStatusCode();
    }

    [Fact]
    public async Task It_responds_with_application_jrd_json()
    {
        var response = await _client.GetAsync(
            $"/.well-known/webfinger?resource={_username}"
        );

        response
            .Content
            .Headers
            .ContentType
            ?.MediaType
            .Should()
            .Be("application/jrd+json");
    }

    [Fact]
    public async Task It_responds_with_a_webfinger_response_with_expected_subject()
    {
        var webfinger = await _client.GetFromJsonAsync<WebFingerResponse>(
            $"/.well-known/webfinger?resource={_username}"
        );

        webfinger!.Subject.Should().Be(_username);
    }

    [Fact]
    public async Task It_responds_with_a_webfinger_response_with_a_self_link()
    {
        var webfinger = await _client.GetFromJsonAsync<WebFingerResponse>(
            $"/.well-known/webfinger?resource={_username}"
        );

        webfinger
            !.Links
            .First(l => l.Rel == "self")
            .Href
            .Should()
            .Be($"/users/{_username}");
    }

    record Link(string Rel, string Href, string Type);

    record WebFingerResponse(string Subject, Link[] Links);
}