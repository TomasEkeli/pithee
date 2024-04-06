using Pithee.Domain.Users;

namespace Pithee.Persistence.Users;

public interface IUsersRepository
{
    Task<User?> Get(string username);
    Task<User> Set(User user);
}

public class UsersRepository : IUsersRepository
{
    readonly Dictionary<string, User> _users = new();
    public Task<User?> Get(string username)
    {
        if (_users.TryGetValue(username, out User? value))
        {
            return Task.FromResult<User?>(value);
        }
        return Task.FromResult<User?>(null);
    }

    public Task<User> Set(User user)
    {
        _users[user.Username] = user;
        return Task.FromResult(user);
    }
}
