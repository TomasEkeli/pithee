namespace Pithee.Persistence.Tests.SchemaCreation;

public class When_getting_a_db_connection : Given_a_database
{
    [Fact]
    public void It_logs_the_connection_string()
    {
        _context.CreateConnection();

        var log = Logs.Single();

        log.Message
            .Should()
            .StartWith(
                "Making connection to Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=pithee_test"
            );
    }

}
