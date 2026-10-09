#if !ROSLYN4_4_OR_GREATER
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.CodeGenerator.Helpers;
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
        // resolves aliases, and the cost is linear in the number of attributes. The declarations compare by value,
        // so the outputs of unchanged ones are reused.
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
        var memberAccess = new MemberAccess(compilation.Assembly);
        var declarations = ImmutableArray.CreateBuilder<GenerateImposterDeclaration>();
        var seenRegistrations = new HashSet<(INamedTypeSymbol, bool)>();

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
                var target = NormalizeImposterTarget(imposterType);
                var putInTheSameNamespace = GetPutInTheSameNamespaceValue(attribute);

                if (seenRegistrations.Add((target, putInTheSameNamespace)))
                {
                    declarations.Add(
                        GenerateImposterDeclaration.From(
                            target,
                            putInTheSameNamespace,
                            memberAccess
                        )
                    );
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
