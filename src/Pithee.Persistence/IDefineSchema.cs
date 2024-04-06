namespace Pithee.Persistence;

public interface IDefineSchema
{
    int Version { get; }
    int Priority { get; }
    string Name { get; }
    string Script { get; }
}
