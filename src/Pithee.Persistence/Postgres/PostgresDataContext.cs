using System.Data;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Pithee.Persistence.Postgres;

public partial class PostgresDataContext(
    string _connectionString,
    ILogger<PostgresDataContext> _logger)
    : IDataContext
{
    public IDbConnection CreateConnection()
    {
        LogMakingConnection(
            _logger,
            _connectionString
        );

        return new NpgsqlConnection(_connectionString);
    }

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Debug,
        Message = "Making connection to {connectionString}"
    )]
    static partial void LogMakingConnection(
        ILogger logger,
        string connectionString
    );
}