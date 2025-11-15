#if !ROSLYN4_4_OR_GREATER
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;

/// <summary>
/// Discover GenerateImposterAttribute usages and produce GenerateImposterDeclaration values for the generator pipeline.
/// </summary>
internal static class GenerateImposterDeclarationsProvider
{
    private static readonly string GenerateImposterAttribute =
        typeof(GenerateImposterAttribute).FullName!;

    internal static IncrementalValuesProvider<GenerateImposterDeclaration> GetGenerateImposterDeclarations(
        this in IncrementalGeneratorInitializationContext context
    )
    {
        // Roslyn 4.0 does not expose GeneratorAttributeSyntaxContext/ForAttributeWithMetadataName.
        // Fall back to a syntax predicate for AttributeSyntax and resolve via semantic model.
        return context
            .SyntaxProvider.CreateSyntaxProvider(
                predicate: static (node, _) =>
                    node is AttributeSyntax attr && IsCandidateAttributeName(attr),
                transform: static (ctx, token) => GetImposterDeclarations(ctx, token)
            )
            .Where(static list => list is not null)
            .SelectMany(static (list, _) => list!)
            .Collect()
            .SelectMany(static (targetSymbols, _) => targetSymbols.Distinct());
    }

    private static IEnumerable<GenerateImposterDeclaration>? GetImposterDeclarations(
        in GeneratorSyntaxContext context,
        in CancellationToken token
    )
    {
        token.ThrowIfCancellationRequested();

        // We are only interested in assembly-level attributes; however, walking the assembly attributes
        // is inexpensive and avoids brittle syntax-to-symbol correlation across Roslyn versions.
        var assembly = context.SemanticModel.Compilation.Assembly;
        var attrs = assembly.GetAttributes();

        if (attrs.Length == 0)
        {
            return [];
        }

        return attrs
            .Where(static a => a.AttributeClass is not null)
            .Where(static a => a.AttributeClass!.ToDisplayString() == GenerateImposterAttribute)
            .Where(static a => a.ConstructorArguments.Length > 0)
            .Select(static a =>
            {
                if (
                    a.ConstructorArguments[0].Value is INamedTypeSymbol
                    {
                        TypeKind: not TypeKind.Error
                    } imposterType
                )
                {
                    return new GenerateImposterDeclaration(
                        imposterType,
                        GetPutInTheSameNamespaceValue(a)
                    );
                }

                return default;
            })
            .Where(static it => it != default)
            .Select(static it => it!);
    }

    private static bool IsCandidateAttributeName(AttributeSyntax attribute)
    {
        // Quick syntactic filter to reduce semantic model work.
        // Matches short and fully-qualified names e.g., GenerateImposter, GenerateImposterAttribute, Imposter.Abstractions.GenerateImposterAttribute
        var name = attribute.Name.ToString();
        return name.EndsWith("GenerateImposter")
            || name.EndsWith("GenerateImposterAttribute")
            || name.Contains("GenerateImposterAttribute");
    }

    private static bool GetPutInTheSameNamespaceValue(AttributeData attributeData) =>
        attributeData.ConstructorArguments.Length != 2
        || (bool)attributeData.ConstructorArguments[1].Value!;
}
#endif
