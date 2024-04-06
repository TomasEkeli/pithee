using System.Net;

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
