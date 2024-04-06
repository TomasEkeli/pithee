using Dapper;
using Pithee.Domain.Users;

namespace Pithee.Persistence.Users;

public interface IUsersRepository
{
    Task<User?> Get(string username);
    Task<User> Set(User user);
}

public partial class UsersRepository(
    IDataContext context,
    ILogger<UsersRepository> _logger
) : IUsersRepository
{
    readonly Dictionary<string, User> _users = new();

    const string SelectUserByUsername = """
        select
            id,
            preferred_username as PreferredUsername,
            password_hash as PasswordHash,
            private_key as PrivateKey,
            public_key as PublicKey
        from users
        where preferred_username = @username
    """;

    const string InsertUser = """
        insert into users (
            id,
            preferred_username,
            password_hash,
            private_key,
            public_key
        ) values (
            @Id,
            @PreferredUsername,
            @PasswordHash,
            @PrivateKey,
            @PublicKey
        )
    """;


    public async Task<User?> Get(string username)
    {
        LogGettingUser(_logger, username);
        try
        {
            using var connection = context.CreateConnection();

            var user = await connection.QueryFirstOrDefaultAsync<User>(
                SelectUserByUsername,
                new { username }
            );

            return user;
        }
        catch (Exception ex)
        {
            LogErrorGettingUser(_logger, username, ex);

            throw;
        }
    }

    public async Task<User> Set(User user)
    {
        LogSettingUser(_logger, user);
        try
        {
            using var connection = context.CreateConnection();

            var rowsChanged = await connection.ExecuteAsync(
                InsertUser,
                user
            );

            return user;
        }
        catch (Exception ex)
        {
            LogErrorSettingUser(_logger, user, ex);
            throw;
        }
    }

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Debug,
        Message = "Getting user {Username}"
    )]
    static partial void LogGettingUser(
        ILogger logger,
        string userName
    );

    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Error,
        Message = "Error getting user {Username}"
    )]
    static partial void LogErrorGettingUser(
        ILogger logger,
        string username,
        Exception ex
    );

    [LoggerMessage(
        EventId = 3,
        Level = LogLevel.Debug,
        Message = "Setting user {User}"
    )]
    static partial void LogSettingUser(
        ILogger logger,
        User user
    );

    [LoggerMessage(
        EventId = 4,
        Level = LogLevel.Error,
        Message = "Error setting user {User}"
    )]
    static partial void LogErrorSettingUser(
        ILogger logger,
        User user,
        Exception ex
    );
}
