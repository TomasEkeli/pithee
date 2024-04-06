using Pithee.Api.Users;

namespace Pithee.Api.Signup;

public interface ISignupHandler
{
    Task<SignupResponse> Signup(
        SignupRequest request);
}

public class SignupHandler(
    IUsersRepository _repository)
    : ISignupHandler
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
}
