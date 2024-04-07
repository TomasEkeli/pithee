using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using ArchUnitNET.Fluent;
using ArchUnitNET.Fluent.Slices;
using ArchUnitNET.xUnit;

namespace Pithee.ArchitectureTests;

public class Given_the_domain_layer : Given_the_architcture
{
    [Fact]
    public void Classes_have_a_parameterless_constructor()
    {
        var classes_in_domain = ArchRuleDefinition
            .Classes()
            .That()
            .ResideInAssembly(DomainAssembly)
            .And()
            .AreNotAbstract()
            .And()
            .AreNotNested()
            .GetObjects(PritheeArchitecture);

        List<Class> lack_parameterless_ctor = [];

        foreach(Class type in classes_in_domain)
        {
            var constructors = type.GetConstructors();

            if (!constructors.Any(c => !c.Parameters.Any()))
            {
                lack_parameterless_ctor.Add(type);
            }
        }

        lack_parameterless_ctor.Should().BeEmpty();
    }

    [Fact]
    public void Does_not_use_the_api_layer()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .Are(DomainLayer)
            .Should()
            .NotDependOnAny(ApiLayer)
            .Check(PritheeArchitecture);
    }

    [Fact]
    public void Domain_does_not_use_persistence()
    {
        ArchRuleDefinition
            .Types()
            .That()
            .Are(DomainLayer)
            .Should()
            .NotDependOnAny(PersistenceLayer)
            .Check(PritheeArchitecture);
    }

    [Fact(Skip = "there are some cycles")]
    public void No_circular_dependencies()
    {
        var no_cycles = SliceRuleDefinition
            .Slices()
            .Matching("Pithee.(*)")
            .Should()
            .BeFreeOfCycles();

        no_cycles.Check(PritheeArchitecture);

    }
}