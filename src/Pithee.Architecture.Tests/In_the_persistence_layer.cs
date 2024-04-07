using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;

namespace Pithee.ArchitectureTests;

public class In_the_persistence_layer : Given_the_layers
{
    [Fact]
    public void Does_not_use_the_api_layer() =>
        ArchRuleDefinition
            .Types()
            .That()
            .Are(PersistenceLayer)
            .Should()
            .NotDependOnAny(ApiLayer)
            .Check(PritheeArchitecture);
}
