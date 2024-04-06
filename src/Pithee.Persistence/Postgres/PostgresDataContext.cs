using System.Data;
using Npgsql;

namespace Pithee.Persistence.Postgres;

public partial class PostgresDataContext(
    PostgresDataContext.DbConfig _config,
    ILogger<PostgresDataContext> _logger)
    : IDataContext
{
    public IDbConnection CreateConnection()
    {
        LogMakingConnection(
            _logger,
            _config
        );

        return new NpgsqlConnection(
            _config.ConnectionString
        );
    }

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Debug,
        Message = "Making connection"
    )]
    static partial void LogMakingConnection(
        ILogger logger,
        [LogProperties] DbConfig connectionString
    );

    public sealed record DbConfig(
        string Host,
        ushort Port,
        string Database,
        string Username,
        string Password
    )
    {
        public string ConnectionString =>
            $@"Host={Host
            };Port={Port
            };Database={Database
            };Username={Username
            };Password={Password}";
    }
}