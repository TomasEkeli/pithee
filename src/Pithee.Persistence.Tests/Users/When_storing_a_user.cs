using Pithee.Domain.Users;

namespace Pithee.Persistence.Tests.Users;

public class When_storing_a_user : Given_a_user_repository
{
    [Fact]
    public async Task It_stores_the_user_so_it_can_be_retrieved()
    {
        var user = new User(
            Id: "https://pithee.example.net/users/testuser",
            PreferredUsername: "testuser",
            PasswordHash: "password",
            PrivateKey: "private-key",
            PublicKey: "public-key"
        );

        await _repository.Set(user);

        var stored_user = await _repository.Get("testuser");

        stored_user.Should().NotBeNull();

        stored_user
            !.Id
            .Should()
            .Be(
                "https://pithee.example.net/users/testuser"
            );

        stored_user
            .PreferredUsername
            .Should()
            .Be("testuser");
        stored_user
            .PasswordHash
            .Should()
            .Be("password");
        stored_user
            .PrivateKey
            .Should()
            .Be("private-key");
        stored_user
            .PublicKey
            .Should()
            .Be("public-key");
    }
}