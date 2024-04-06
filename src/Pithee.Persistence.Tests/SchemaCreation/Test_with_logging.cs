using MELT;

namespace Pithee.Persistence.Tests.SchemaCreation;

public abstract class Test_with_logging
{
    protected readonly ITestLoggerFactory _loggerFactory =
        TestLoggerFactory.Create();

    protected IEnumerable<LogEntry> Logs =>
        _loggerFactory.Sink.LogEntries;
}
