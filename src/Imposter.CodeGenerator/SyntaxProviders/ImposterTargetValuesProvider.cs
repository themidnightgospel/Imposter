using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Imposter.Abstractions;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.SyntaxProviders;

internal static class GenerateImposterDeclarationsProvider
{
    private static readonly string GenerateImposterAttribute = typeof(GenerateImposterAttribute).FullName!;

    internal static IncrementalValuesProvider<GenerateImposterDeclaration> GetGenerateImposterDeclarations(this IncrementalGeneratorInitializationContext context)
    {
        return context
            .SyntaxProvider
            .ForAttributeWithMetadataName(
                GenerateImposterAttribute,
                predicate: static (_, _) => true,
                transform: static (ctx, token) => GetImposterTargetTypeSymbol(ctx, token))
            .SelectMany((symbols, _) => symbols)
            .Collect()
            .SelectMany((targetSymbols, _) => targetSymbols.Distinct());
    }

    private static IEnumerable<GenerateImposterDeclaration> GetImposterTargetTypeSymbol(GeneratorAttributeSyntaxContext context, CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            yield break;
        }

        foreach (var imposterTargetType in context
                     .Attributes
                     .Select(it =>
                     {
                         if (it.ConstructorArguments.Length > 0 && it.ConstructorArguments[0].Value is ITypeSymbol imposterType)
                         {
                             return imposterType;
                         }

                         return null;
                     })
                     .Where(it => it is not null)
                     .Distinct<ITypeSymbol?>(SymbolEqualityComparer.Default)
                     .Select(symbol => new GenerateImposterDeclaration(symbol!))
                )
        {
            yield return imposterTargetType;
        }
    }
}