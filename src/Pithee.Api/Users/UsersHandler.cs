using Pithee.Api.Users.Signup;
using Pithee.Persistence.Users;

namespace Pithee.Api.Users;

public interface IUsersHandler
{
    Task<SignupResponse> Signup(
        SignupRequest request);
    Task<UserResponse?> GetUser(
        string username);
}

public class UsersHandler(
    IUsersRepository _repository)
    : IUsersHandler
{
    public async Task<SignupResponse> Signup(
        SignupRequest request)
    {
        var user = await _repository.Set(
            new(
                Id: "https://pithee.example.net/users/" + request.Username,
                PreferredUsername: request.Username,
                PasswordHash: request.Password,
                PrivateKey: Guid.NewGuid().ToString(),
                PublicKey: Guid.NewGuid().ToString()
            )
        );

        return new(user.PreferredUsername);
    }

    public async Task<UserResponse?> GetUser(
        string username)
    {
        var user = await _repository.Get(
            username);

        if (user is null)
        {
            return null;
        }
        return new(
            user.Id,
            user.PreferredUsername,
            PublicKey: new(
                Id: user.PublicKey,
                Owner: user.Id,
                PublicKeyPem: user.PublicKey
            )
        );
    }
}
