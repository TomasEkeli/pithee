using Dapper;

namespace Pithee.Persistence.Postgres;

public partial class SchemaCreator(
    IDataContext context,
    IEnumerable<IDefineSchema> schemas,
    ILogger<SchemaCreator> logger)
    : ISchemaCreator
{
    public async Task Initialize()
    {
        LogInitializing(
            logger,
            schemas.Count()
        );

        var ordered = schemas
            .OrderBy(s => s.Priority)
            .ThenBy(s => s.Version)
            .ToList();

        try
        {
            using var connection = context.CreateConnection();

            foreach (var schema in ordered)
            {
                await connection.ExecuteAsync(schema.Script);
            }
        }
        catch (System.Exception ex)
        {
            LogErrorInitializing(logger, ex);
            throw;
        }
    }

    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Debug,
        Message = "Initializing schema with {numberOfSchemas} schemas"
    )]
    static partial void LogInitializing(
        ILogger logger,
        int numberofSchemas
    );

    [LoggerMessage(
        EventId = 2,
        Level = LogLevel.Critical,
        Message = "Error initializing schema"
    )]
    static partial void LogErrorInitializing(
        ILogger logger,
        Exception ex
    );
}