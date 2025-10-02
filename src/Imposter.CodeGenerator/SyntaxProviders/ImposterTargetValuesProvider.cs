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
            return [];
        }

        return context
            .Attributes
            .Select(it =>
            {
                if (it.ConstructorArguments.Length > 0 && it.ConstructorArguments[0].Value is INamedTypeSymbol imposterType)
                {
                    return new GenerateImposterDeclaration(imposterType, GetPutInTheSameNamespaceValue(it));
                }

                return null;
            })
            .Where(it => it is not null)
            .Distinct()
            .Select(it => it!);
    }

    // TODO refactor
    static bool GetPutInTheSameNamespaceValue(AttributeData attributeData)
    {
        // The boolean value is the second constructor argument (index 1).
        // The first argument (index 0) is the 'Type type'.

        // Check if the argument list is long enough (it should be at least 2)
        if (attributeData.ConstructorArguments.Length > 1)
        {
            var arg = attributeData.ConstructorArguments[1];

            // Ensure the argument is not null and is a boolean value
            if (arg.Kind == TypedConstantKind.Primitive && arg.Type.SpecialType == SpecialType.System_Boolean)
            {
                // Safely cast the Value to bool
                return (bool)arg.Value;
            }
        }

        // If the argument was omitted (using the default value in C# 9+), 
        // the ConstructorArguments array will only contain the 'Type' argument.
        // In this case, we return the default value defined in the constructor: 'true'.
        return true;
    }
}