using Dapper;
using Microsoft.Extensions.Logging;
using Npgsql;
using Pithee.Persistence.Postgres;

namespace Pithee.Persistence.Tests.SchemaCreation;

public abstract class Given_a_database
    : Test_with_logging, IAsyncLifetime
{
    const string AdminDb =
        "Host=localhost;Port=5432;Username=postgres;Password=postgres";
    protected IDataContext _context;
    readonly Guid _id = Guid.NewGuid();
    string _database_name => $"pithee_test_{_id:N}";

    public Given_a_database()
    {
        _context = new PostgresDataContext(
            $"Host=localhost;Port=5432;Username=postgres;Password=postgres;Database={_database_name}",
            _loggerFactory.CreateLogger<PostgresDataContext>()
        );
    }

    public async Task InitializeAsync()
    {
        await CreateDatabase();
    }

    public async Task DisposeAsync()
    {
        await DropDatabase();
    }

    async Task CreateDatabase()
    {
        using var connection = new NpgsqlConnection(AdminDb);

        connection.Open();

        await connection.ExecuteAsync(
            $"create database {_database_name}"
        );
    }

    async Task DropDatabase()
    {
        using var connection = new NpgsqlConnection(AdminDb);

        connection.Open();

        await connection.ExecuteAsync(
            $"drop database if exists {_database_name} with (force);"
        );
    }

}
