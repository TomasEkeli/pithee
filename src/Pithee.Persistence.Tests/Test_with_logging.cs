using MELT;

namespace Pithee.Persistence.Tests;

public abstract class Test_with_logging
{
    protected readonly ITestLoggerFactory _loggerFactory =
        TestLoggerFactory.Create();

    protected IEnumerable<LogEntry> Logs =>
        _loggerFactory.Sink.LogEntries;
}
