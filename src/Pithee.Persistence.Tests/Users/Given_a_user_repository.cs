using Pithee.Persistence.Users;

namespace Pithee.Persistence.Tests.Users;

public class Given_a_user_repository
{
    protected readonly IUsersRepository _repository;

    public Given_a_user_repository()
    {
        _repository = new UsersRepository();
    }
}
