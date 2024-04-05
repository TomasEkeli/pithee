namespace Pithee.Api.Tests.Root;

public class When_calling_the_root : Given_an_api
{
    [Fact]
    public async Task It_responds_with_a_200_ok()
    {
        var response = await _client.GetAsync("/");

        response.EnsureSuccessStatusCode();
    }
}