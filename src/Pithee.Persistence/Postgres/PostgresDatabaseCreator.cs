using Dapper;

namespace Pithee.Persistence.Postgres;

public partial class PostgresDatabaseCreator(
    IAdminDataContext _context,
    PostgresDataContext.DbConfig _config,
    ILogger<PostgresDatabaseCreator> _logger)
    : IDatabaseCreator
{
    public async Task Create()
    {
        LogCreatingDatabase(_logger, _config.Database);

        try
        {
            await TryToConnectAndCreate();
        }
        catch (Exception ex)
        {
            LogGaveUpCreatingDatabase(
                _logger,
                _config.Database,
                ex
            );
            throw;
        }
    }

    async Task TryToConnectAndCreate()
    {
        var maxRetries = 100;
        var delay = TimeSpan.FromSeconds(1);

        var retries = 0;

        while (retries < maxRetries)
        {
            try
            {
                using var connection = _context
                    .CreateAdminConnection();

                var databaseExists = await connection
                    .ExecuteScalarAsync<int>(
                        $$"""
                            select 1
                            from pg_database
                            where datname = '{{_config.Database}}'
                        """
                    );

                if (databaseExists == 1)
                {
                    LogDatabaseExists(
                        _logger,
                        _config.Database);
                    return;
                }

                LogTryingToCreateDatabase(
                    _logger,
                    _config.Database);
                await connection.ExecuteAsync(
                    $@"create database {_config.Database}"
                );
            }
            catch (Exception ex)
            {
                retries++;
                LogRetrying(
                    _logger,
                    _config.Database,
                    retries,
                    maxRetries,
                    delay,
                    ex
                );
                await Task.Delay(delay);
            }
        }
        LogFailedToCreateDatabase(
            _logger,
            _config.Database,
            maxRetries,
            delay
        );
    }

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Debug,
        Message = "Creating database {DatabaseName}"
    )]
    static partial void LogCreatingDatabase(
        ILogger logger,
        string databaseName
    );

    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Information,
        Message = "Will retry creating database {DatabaseName}. Atttempt {Retries} of {MaxRetries} with {Delay} delay"
    )]
    static partial void LogRetrying(
        ILogger logger,
        string databaseName,
        int retries,
        int maxRetries,
        TimeSpan delay,
        Exception ex
    );

    [LoggerMessage(
        EventId = 3,
        Level = LogLevel.Debug,
        Message = "Database {DatabaseName} exists"
    )]
    static partial void LogDatabaseExists(
        ILogger logger,
        string databaseName
    );

    [LoggerMessage(
        EventId = 4,
        Level = LogLevel.Error,
        Message = "Failed to create database {DatabaseName} after {MaxRetries} retries with {Delay} delay"
    )]
    static partial void LogFailedToCreateDatabase(
        ILogger logger,
        string databaseName,
        int maxRetries,
        TimeSpan delay
    );

    [LoggerMessage(
        EventId = 5,
        Level = LogLevel.Debug,
        Message = "Trying to create database {DatabaseName}"
    )]
    static partial void LogTryingToCreateDatabase(
        ILogger logger,
        string databaseName
    );

    [LoggerMessage(
        EventId = 6,
        Level = LogLevel.Critical,
        Message = "Gave up creating database {DatabaseName}"
    )]
    static partial void LogGaveUpCreatingDatabase(
        ILogger logger,
        string databaseName,
        Exception ex
    );
}