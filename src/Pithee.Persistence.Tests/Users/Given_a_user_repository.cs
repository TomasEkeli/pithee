using Microsoft.Extensions.Logging;
using Pithee.Persistence.Users;

namespace Pithee.Persistence.Tests.Users;

public class Given_a_user_repository
    : When_the_database_has_been_initialized
{
    protected readonly IUsersRepository _repository;

    public Given_a_user_repository()
    {
        _repository = new UsersRepository(
            _context,
            _loggerFactory.CreateLogger<UsersRepository>()
        );
    }
}
