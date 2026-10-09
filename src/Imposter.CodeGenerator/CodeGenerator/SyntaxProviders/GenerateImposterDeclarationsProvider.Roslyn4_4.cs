#if ROSLYN4_4_OR_GREATER
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

/// <summary>
/// Discover GenerateImposterAttribute usages and produce GenerateImposterDeclaration values for the generator pipeline.
/// </summary>
internal static class GenerateImposterDeclarationsProvider
{
    private static readonly string GenerateImposterAttribute =
        typeof(GenerateImposterAttribute).FullName!;

    // The transform runs again after every edit, but it produces symbol-free declarations that compare by value, so
    // the outputs of unchanged declarations are reused.
    internal static IncrementalValuesProvider<GenerateImposterDeclaration> GetGenerateImposterDeclarations(
        this in IncrementalGeneratorInitializationContext context
    )
    {
        return context
            .SyntaxProvider.ForAttributeWithMetadataName(
                GenerateImposterAttribute,
                predicate: static (_, _) => true,
                transform: static (ctx, token) => GetDeclarations(ctx, token)
            )
            .SelectMany((declarations, _) => declarations)
            .Collect()
            .SelectMany(
                (declarations, _) => ImposterTypeCollisions.Mark(declarations.Distinct().ToArray())
            )
            .WithTrackingName("GenerateImposterDeclarations");
    }

    private static EquatableArray<GenerateImposterDeclaration> GetDeclarations(
        in GeneratorAttributeSyntaxContext context,
        in CancellationToken token
    )
    {
        token.ThrowIfCancellationRequested();

        var memberAccess = new MemberAccess(context.SemanticModel.Compilation.Assembly);

        return context
            .Attributes.Where(attribute =>
                attribute.ConstructorArguments.Length > 0
                && attribute.ConstructorArguments[0].Value
                    is INamedTypeSymbol { TypeKind: not TypeKind.Error }
            )
            .Select(attribute =>
                GenerateImposterDeclaration.From(
                    NormalizeImposterTarget(
                        (INamedTypeSymbol)attribute.ConstructorArguments[0].Value!
                    ),
                    GetPutInTheSameNamespaceValue(attribute),
                    memberAccess
                )
            )
            .Distinct()
            .ToEquatableArray();
    }

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
