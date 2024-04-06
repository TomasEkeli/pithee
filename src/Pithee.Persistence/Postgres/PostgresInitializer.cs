namespace Pithee.Persistence.Postgres;

public partial class PostgresInitializer(
    IDatabaseCreator databaseCreator,
    ISchemaCreator schemaInitializer,
    ILogger<PostgresInitializer> logger)
    : IPersistenceInitializer
{
    public async Task Initialize()
    {
        LogInitializingPersistence(logger);

        try
        {
            await databaseCreator.Create();
            await schemaInitializer.Initialize();
        }
        catch (Exception ex)
        {
            LogErrorInitializingPersistence(logger, ex);
            throw;
        }
    }

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Debug,
        Message = "Initializing persistence"
    )]
    static partial void LogInitializingPersistence(
        ILogger logger
    );

    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Error,
        Message = "Error initializing persistence"
    )]
    static partial void LogErrorInitializingPersistence(
        ILogger logger,
        Exception ex
    );
}