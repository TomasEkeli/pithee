namespace Pithee.Api.Users;

public interface IUsersHandler
{
        Task<UserResponse?> GetUser(string username);
}

public class UsersHandler(
    IUsersRepository _repository)
    : IUsersHandler
{
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
