using System.Data;
using Npgsql;

namespace Pithee.Persistence.Postgres;

public partial class PostgresAdminDataContext(
    PostgresAdminDataContext.AdminDbConfig _config,
    ILogger<PostgresAdminDataContext> _logger)
    : IAdminDataContext
{
    public IDbConnection CreateAdminConnection()
    {
        LogMakingAdminConnection(
            _logger,
            _config
        );

        return new NpgsqlConnection(_config.ConnectionString);
    }

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Debug,
        Message = "Making admin connection"
    )]
    static partial void LogMakingAdminConnection(
        ILogger logger,
        [LogProperties] AdminDbConfig config
    );

    public sealed record AdminDbConfig(
        string Host,
        ushort Port,
        string Username,
        string Password
    )
    {
        public string ConnectionString =>
            $@"Host={Host
            };Port={Port
            };Username={Username
            };Password={Password
            };Database=postgres";
    }
}