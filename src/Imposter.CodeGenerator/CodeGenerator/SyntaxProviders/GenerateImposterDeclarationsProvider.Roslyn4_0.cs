#if !ROSLYN4_4_OR_GREATER
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

/// <summary>
/// Discover GenerateImposterAttribute usages and produce GenerateImposterDeclaration values for the generator pipeline.
/// </summary>
internal static class GenerateImposterDeclarationsProvider
{
    internal static IncrementalValuesProvider<GenerateImposterDeclaration> GetGenerateImposterDeclarations(
        this in IncrementalGeneratorInitializationContext context
    )
    {
        // Roslyn 4.0 does not expose ForAttributeWithMetadataName. GenerateImposterAttribute only targets the
        // assembly, so its usages are read from the assembly's bound attributes once per compilation: binding
        // resolves aliases, and the cost is linear in the number of attributes. No caching is lost, because the
        // generated output is combined with the compilation anyway.
        return context.CompilationProvider.SelectMany(
            static (compilation, cancellationToken) =>
                GetImposterDeclarations(compilation, cancellationToken)
        );
    }

    private static ImmutableArray<GenerateImposterDeclaration> GetImposterDeclarations(
        Compilation compilation,
        CancellationToken cancellationToken
    )
    {
        var declarations = ImmutableArray.CreateBuilder<GenerateImposterDeclaration>();
        var seenDeclarations = new HashSet<GenerateImposterDeclaration>();

        foreach (var attribute in compilation.Assembly.GetAttributes())
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (
                IsGenerateImposterAttribute(attribute.AttributeClass)
                && attribute.ConstructorArguments.Length > 0
                && attribute.ConstructorArguments[0].Value
                    is INamedTypeSymbol { TypeKind: not TypeKind.Error } imposterType
            )
            {
                var declaration = new GenerateImposterDeclaration(
                    NormalizeImposterTarget(imposterType),
                    GetPutInTheSameNamespaceValue(attribute)
                );

                if (seenDeclarations.Add(declaration))
                {
                    declarations.Add(declaration);
                }
            }
        }

        return ImposterTypeCollisions.Mark(declarations.ToImmutable()).ToImmutableArray();
    }

    // Compares names instead of rendering the attribute class with ToDisplayString, which allocates.
    private static bool IsGenerateImposterAttribute(INamedTypeSymbol? attributeClass) =>
        attributeClass
            is {
                MetadataName: nameof(GenerateImposterAttribute),
                ContainingNamespace:
                {
                    Name: "Abstractions",
                    ContainingNamespace:
                    { Name: "Imposter", ContainingNamespace.IsGlobalNamespace: true },
                },
            };

    private static bool GetPutInTheSameNamespaceValue(AttributeData attributeData) =>
        attributeData.ConstructorArguments.Length != 2
        || (bool)attributeData.ConstructorArguments[1].Value!;

    private static INamedTypeSymbol NormalizeImposterTarget(INamedTypeSymbol imposterType)
    {
        return imposterType.IsUnboundGenericType
            ? (INamedTypeSymbol)imposterType.OriginalDefinition
            : imposterType;
    }
}
#endif
