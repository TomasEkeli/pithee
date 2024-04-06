using Microsoft.Extensions.Logging;
using Pithee.Persistence.Postgres;
using Dapper;

namespace Pithee.Persistence.Tests.SchemaCreation;

public class When_initializing : Given_a_database
{
    [Fact]
    public async Task It_creates_at_least_1_table()
    {
        var initializer = new SchemaCreator(
            _context,
            [ new OneTable() ],
            _loggerFactory.CreateLogger<SchemaCreator>()
        );

        await initializer.Initialize();

        using var connection = _context.CreateConnection();
        var number_of_tables = await connection
            .QuerySingleAsync<int>(
            """
                select count(*)
                from information_schema.tables
                where table_schema = 'public';
            """
            );

        number_of_tables.Should().BeGreaterThan(0);
    }

    class OneTable : IDefineSchema
    {
        public int Version => 1;

        public int Priority => 1;

        public string Name => nameof(OneTable);

        public string Script =>
            """
            create table OneTable (
                Id serial primary key,
                Name text not null
            );
            """;
    }
}