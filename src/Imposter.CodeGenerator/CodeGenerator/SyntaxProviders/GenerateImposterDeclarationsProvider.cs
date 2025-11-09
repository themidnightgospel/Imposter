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
    private static readonly string GenerateImposterAttribute = typeof(GenerateImposterAttribute).FullName!;

    internal static IncrementalValuesProvider<GenerateImposterDeclaration> GetGenerateImposterDeclarations(this IncrementalGeneratorInitializationContext context)
    {
        return context
            .SyntaxProvider
            .ForAttributeWithMetadataName(
                GenerateImposterAttribute,
                predicate: static (node, _) => node is AttributeSyntax { ArgumentList.Arguments.Count: > 0 },
                transform: static (ctx, token) => GetImposterTargetTypeSymbol(ctx, token))
            .SelectMany((symbols, _) => symbols)
            .Collect()
            .SelectMany((targetSymbols, _) => targetSymbols.Distinct())
            .WithTrackingName("GenerateImposterDeclarations");
    }
    private static IEnumerable<GenerateImposterDeclaration> GetImposterTargetTypeSymbol(GeneratorAttributeSyntaxContext context, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();

        return context
            .Attributes
            .Select(it =>
            {
                if (it.ConstructorArguments.Length > 0
                    && it.ConstructorArguments[0].Value is INamedTypeSymbol { TypeKind: not TypeKind.Error } imposterType)
                {
                    return new GenerateImposterDeclaration(imposterType, GetPutInTheSameNamespaceValue(it));
                }

                return default;
            })
            .Where(it => it != default)
            .Distinct()
            .Select(it => it!);
    }

    private static bool GetPutInTheSameNamespaceValue(AttributeData attributeData) =>
        attributeData.ConstructorArguments.Length != 2 || (bool)attributeData.ConstructorArguments[1].Value!;
}
