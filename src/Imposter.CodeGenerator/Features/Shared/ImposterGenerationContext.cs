using System;
using System.Text;
using Imposter.CodeGenerator.CodeGenerator.Logging;
using Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterGenerationContext
{
    internal readonly GenerateImposterDeclaration GenerateImposterDeclaration;

    internal INamedTypeSymbol TargetSymbol => GenerateImposterDeclaration.ImposterTarget;

    internal readonly ImposterTargetMetadata Imposter;

    internal readonly string? ImposterNamespaceName;

    // Derived from the target alone, so it does not depend on the order in which targets are processed.
    internal readonly string HintName;

    internal readonly SupportedCSharpFeatures SupportedCSharpFeatures;

    internal readonly IGeneratorLogger Logger;

    internal ImposterGenerationContext(
        GenerateImposterDeclaration generateImposterDeclaration,
        in SupportedCSharpFeatures supportedCSharpFeatures,
        IGeneratorLogger logger
    )
    {
        GenerateImposterDeclaration = generateImposterDeclaration;
        Imposter = new ImposterTargetMetadata(
            generateImposterDeclaration.ImposterTarget,
            supportedCSharpFeatures
        );

        var sanitizedTargetName = GetSanitizedTargetName(TargetSymbol);

        if (generateImposterDeclaration.PutInTheSameNamespace)
        {
            ImposterNamespaceName = TargetSymbol.ContainingNamespace.IsGlobalNamespace
                ? null
                : TargetSymbol.ContainingNamespace.ToDisplayString();
            HintName = $"{sanitizedTargetName}Imposter.g.cs";
        }
        else
        {
            ImposterNamespaceName = $"{DedicatedNamespacePrefix}.{sanitizedTargetName}";
            HintName = $"{DedicatedNamespacePrefix}.{sanitizedTargetName}Imposter.g.cs";
        }

        SupportedCSharpFeatures = supportedCSharpFeatures;
        Logger = logger;
    }

    private const string DedicatedNamespacePrefix = "Imposters";

    // The target's fully qualified name without `global::`, reduced to characters valid in a namespace and a
    // hint name, e.g. `Sample.IPair<int, string>` becomes `Sample.IPair_int__string_`.
    private static string GetSanitizedTargetName(INamedTypeSymbol targetSymbol)
    {
        var display = targetSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        const string globalPrefix = "global::";
        if (display.StartsWith(globalPrefix, StringComparison.Ordinal))
        {
            display = display[globalPrefix.Length..];
        }

        return SanitizeForNamespace(display);
    }

    private static string SanitizeForNamespace(string value)
    {
        var builder = new StringBuilder(value.Length);

        foreach (var ch in value)
        {
            if (char.IsLetterOrDigit(ch) || ch is '_' or '.')
            {
                builder.Append(ch);
            }
            else
            {
                builder.Append('_');
            }
        }

        return builder.ToString();
    }
}
