using Microsoft.Extensions.Logging;
using Pithee.Persistence.Postgres;
using Pithee.Persistence.Users;

namespace Pithee.Persistence.Tests;

public abstract class When_the_database_has_been_initialized
    : Given_a_database, IAsyncLifetime
{
    readonly SchemaCreator _initializer;

    public When_the_database_has_been_initialized()
    {
        _initializer = new SchemaCreator(
            _context,
            [ new UserSchema() ],
            _loggerFactory.CreateLogger<SchemaCreator>()
        );
    }

    public new async Task InitializeAsync()
    {
        await base.InitializeAsync();
        await _initializer.Initialize();
    }
}
