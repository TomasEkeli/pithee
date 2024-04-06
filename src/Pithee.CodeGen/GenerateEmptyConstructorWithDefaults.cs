using Microsoft.CodeAnalysis;

namespace Pithee.CodeGen;

/**
 * <summary>
 * <para>
 * Generates a parameter-less constructor for any partial type that is
 * annotated with the "GenerateCtor" attribute. This is handy when using
 * Dapper as it does not play very well with record-style constructors.
 * </para>
 *
 * <para>
 * To use this this project must be referenced as an analyzer in the
 * project file. This is done by adding the following to the
 * .csproj file:
 * </para>
 * <para>
 * <c>
 * &lt;ItemGroup&gt;
 *    &lt;ProjectReference
 *      Include="../Pithee.CodeGen/Pithee.CodeGen.csproj"
 *      OutputItemType="Analyzer"
 *      ReferenceOutputAssembly="false" /&gt;
 * &lt;/ItemGroup&gt;
 * </c>
 * </para>
 *
 * <para>
 * The attribute is generated into the project that references this one,
 * placed in the <c>Pithee.CodeGen</c> namespace. The attribute is named
 * <c>GenerateCtor</c>.
 * Usage is as follows:
 * </para>
 * <para>
 * <code>
 * using Pithee.CodeGen;
 *
 * namespace Whatever;
 *
 * [GenerateCtor]
 *
 * public partial record MyRecord(string Name, int Age);
 * </code>
 * </para>
 *
 * </summary>
 */
[Generator]
public class GenerateEmptyConstructorWithDefaults
    : IIncrementalGenerator
{
    const string Namespace = "Pithee.CodeGen";
    const string Name = "GenerateCtor";
    const string GenerateName = $"{Namespace}.{Name}";

    public void Initialize(
        IncrementalGeneratorInitializationContext context)
    {
        var attributeInstance = context
            .SyntaxProvider
            .ForAttributeWithMetadataName(
                fullyQualifiedMetadataName: GenerateName,
                predicate: (_, _) => true,
                transform: (syntaxContext, _) => syntaxContext
            )
            .Combine(context.CompilationProvider);


        context.RegisterSourceOutput(
            attributeInstance,
            GeneratePartialWithCtor
        );

        // give the project an attribute to decorate with
        context.RegisterPostInitializationOutput(
            callback: CreateAttribue);
    }

    static void GeneratePartialWithCtor(
        SourceProductionContext source_context,
        (GeneratorAttributeSyntaxContext, Compilation) current)
    {
        var (syntaxContext, _) = current;

        if (syntaxContext.TargetSymbol is not INamedTypeSymbol namedType)
        {
            return;
        }

        // correct number of defaults (one for each parameter in the ctor)
        var correctNumberOfDefaults = string.Join(
            ", ",
            namedType
                .Constructors
                .First()
                .Parameters
                .Select(p => "default!")
        );

        // generate the partial class with the empty ctor
        string recordName = namedType.ToDisplayString(
                        SymbolDisplayFormat
                            .FullyQualifiedFormat.WithGlobalNamespaceStyle(
                                SymbolDisplayGlobalNamespaceStyle.Omitted
                            )
                        );
        source_context.AddSource(
            hintName: $"{recordName}_{GenerateName}.g.cs",
            source: $$"""
            namespace {{namedType.ContainingNamespace}};
            public partial record {{namedType.Name}}
            {
                {{namedType.Name}}() : this({{correctNumberOfDefaults}}){}
            }
            """
        );
    }

    static void CreateAttribue(
        IncrementalGeneratorPostInitializationContext afterGenerate)
    {
        // todo: the namespace could be the default namespace of the project
        afterGenerate.AddSource(
            hintName: $"{GenerateName}.g.cs",
            source: $$"""
            namespace {{Namespace}};
            [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
            sealed class {{Name}} : System.Attribute
            {
                public {{Name}}() { }
            }
            """
        );
    }
}