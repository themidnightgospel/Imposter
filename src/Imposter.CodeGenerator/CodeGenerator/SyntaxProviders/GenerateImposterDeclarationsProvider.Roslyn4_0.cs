#if !ROSLYN4_4_OR_GREATER
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

internal static partial class GenerateImposterDeclarationsProvider
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
                && RegisteredTarget(attribute) is { } target
            )
            {
                var putInTheSameNamespace = PutInTheSameNamespace(attribute);

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
}
#endif
