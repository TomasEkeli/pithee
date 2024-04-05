namespace Api.Tests.WebFinger;

public class When_the_user_has_been_registered : Given_an_api
{
    readonly string _username;

    public When_the_user_has_been_registered()
    {
        _username = $"alice-{Guid.NewGuid():N}";
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
}