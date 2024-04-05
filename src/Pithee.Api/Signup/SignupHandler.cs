namespace Pithee.Api.Signup;

public interface ISignupHandler
{
    Task<SignupResponse> Signup(SignupRequest request);
}

public class SignupHandler : ISignupHandler
{
    public Task<SignupResponse> Signup(SignupRequest request)
    {
        SignupResponse response = new(request.Username);

        return Task.FromResult(response);
    }
}
