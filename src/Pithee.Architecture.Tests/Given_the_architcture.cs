using ArchUnitNET.Domain;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;

namespace Pithee.ArchitectureTests;

public abstract class Given_the_architcture : Given_the_assemblies
{
    protected static readonly Architecture PritheeArchitecture =
        new ArchLoader()
            .LoadAssemblies(
                DomainAssembly,
                DomainTestsAssembly,
                PersistenceAssembly,
                PersistenceTestsAssembly,
                ApiAssembly,
                ApiTestsAssembly,
                CodeGenAssembly
            )
            .Build();

    protected static readonly IObjectProvider<IType> DomainLayer =
        ArchRuleDefinition.Types()
            .That()
            .ResideInAssembly(DomainAssembly)
            .As("Domain layer");

    protected static readonly IObjectProvider<IType> DomainTestsLayer =
        ArchRuleDefinition.Types()
            .That()
            .ResideInAssembly(DomainTestsAssembly)
            .As("Domain tests layer");

    protected static readonly IObjectProvider<IType> PersistenceLayer =
        ArchRuleDefinition.Types()
            .That()
            .ResideInAssembly(PersistenceAssembly)
            .As("Persistence layer");

    protected static readonly IObjectProvider<IType> PersistenceTestsLayer =
        ArchRuleDefinition.Types()
            .That()
            .ResideInAssembly(PersistenceTestsAssembly)
            .As("Persistence tests layer");

    protected static readonly IObjectProvider<IType> ApiLayer =
        ArchRuleDefinition.Types()
            .That()
            .ResideInAssembly(ApiAssembly)
            .As("Api layer");

    protected static readonly IObjectProvider<IType> ApiTestsLayer =
        ArchRuleDefinition.Types()
            .That()
            .ResideInAssembly(ApiTestsAssembly)
            .As("Api tests layer");

    protected static readonly IObjectProvider<IType> CodeGenLayer =
        ArchRuleDefinition.Types()
            .That()
            .ResideInAssembly(CodeGenAssembly)
            .As("Code generation layer");
}