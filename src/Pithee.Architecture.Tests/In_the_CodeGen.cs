using ArchUnitNET.Fluent;
using ArchUnitNET.xUnit;

namespace Pithee.ArchitectureTests;

public class In_the_CodeGen : Given_the_layers
{
    [Fact]
    public void Does_not_use_the_api_layer() =>
        ArchRuleDefinition
            .Types()
            .That()
            .Are(CodeGenLayer)
            .Should()
            .NotDependOnAny(ApiLayer)
            .Check(PritheeArchitecture);

    [Fact]
    public void Does_not_use_the_persistence_layer() =>
        ArchRuleDefinition
            .Types()
            .That()
            .Are(CodeGenLayer)
            .Should()
            .NotDependOnAny(PersistenceLayer)
            .Check(PritheeArchitecture);

    [Fact]
    public void Does_not_use_the_domain_layer() =>
        ArchRuleDefinition
            .Types()
            .That()
            .Are(CodeGenLayer)
            .Should()
            .NotDependOnAny(DomainLayer)
            .Check(PritheeArchitecture);

    [Fact]
    public void Does_not_use_any_tests()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .Are(CodeGenLayer)
            .Should()
            .NotDependOnAny(".*.Tests.*", true)
            .Check(PritheeArchitecture);
    }
}