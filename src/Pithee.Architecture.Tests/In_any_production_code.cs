using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;

namespace Pithee.ArchitectureTests;

public class In_any_production_code : Given_the_layers
{

    [Fact]
    public void Do_not_depend_on_tests() =>
        ArchRuleDefinition
            .Types().That().Are(PersistenceLayer)
            .Or().Are(DomainLayer)
            .Or().Are(ApiLayer)
            .Should().NotDependOnAny(".*.Tests..*", true)
            .Because("production code does not depend on tests")
            .Check(PritheeArchitecture);
}