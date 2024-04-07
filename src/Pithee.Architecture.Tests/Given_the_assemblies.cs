using System.Reflection;

namespace Pithee.ArchitectureTests;

public abstract class Given_the_assemblies
{
    protected static readonly Assembly DomainAssembly =
        typeof(Domain.Users.User)
            .Assembly;

    protected static readonly Assembly DomainTestsAssembly =
        typeof(Domain.Tests.Users.When_creating)
            .Assembly;

    protected static readonly Assembly PersistenceAssembly =
        typeof(Persistence.IPersistenceInitializer)
            .Assembly;

    protected static readonly Assembly PersistenceTestsAssembly =
        typeof(Persistence.Tests.When_the_database_has_been_initialized)
            .Assembly;

    protected static readonly Assembly ApiAssembly =
        typeof(Program)
            .Assembly;

    protected static readonly Assembly ApiTestsAssembly =
        typeof(Api.Tests.Given_an_api)
            .Assembly;

    protected static readonly Assembly CodeGenAssembly =
        typeof(CodeGen.GenerateEmptyConstructorWithDefaults)
            .Assembly;

}
