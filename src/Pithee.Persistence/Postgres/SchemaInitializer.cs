using Dapper;
using Microsoft.Extensions.Logging;

namespace Pithee.Persistence.Postgres;

public partial class SchemaInitializer(
    IDataContext context,
    IEnumerable<IDefineSchema> schemas,
    ILogger<SchemaInitializer> logger)
    : ISchemaInitializer
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

        using var connection = context.CreateConnection();

        foreach (var schema in ordered)
        {
            await connection.ExecuteAsync(schema.Script);
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
}