using Pithee.Domain.Users;

namespace Pithee.Domain.Tests.Users;

public class When_creating
{
    [Fact]
    public void It_creates_the_user()
    {
        var user = new User(
            Id: "https://pithee.example.net/users/testuser",
            PreferredUsername: "testuser",
            PasswordHash: "password",
            PrivateKey: "private-key",
            PublicKey: "public-key");

        user
            .Id
            .Should()
            .Be(
                "https://pithee.example.net/users/testuser"
            );

        user
            .PreferredUsername
            .Should()
            .Be("testuser");

        user
            .PasswordHash
            .Should()
            .Be("password");

        user
            .PrivateKey
            .Should()
            .Be("private-key");

        user
            .PublicKey
            .Should()
            .Be("public-key");
    }
}