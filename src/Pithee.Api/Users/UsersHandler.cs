using Pithee.Api.Users.Signup;

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
                request.Username,
                request.Password
            )
        );

        return new(user.Username);
    }

    public async Task<UserResponse?> GetUser(string username)
    {
        var user = await _repository.Get(username);
        if (user is null)
        {
            return null;
        }
        return new(user.Username);
    }
}
