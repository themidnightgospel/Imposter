using System;
using System.Globalization;
using System.Text;
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

    internal ImposterGenerationContext(
        GenerateImposterDeclaration generateImposterDeclaration,
        in SupportedCSharpFeatures supportedCSharpFeatures
    )
    {
        GenerateImposterDeclaration = generateImposterDeclaration;
        Imposter = new ImposterTargetMetadata(
            generateImposterDeclaration.ImposterTarget,
            supportedCSharpFeatures
        );

        var targetName = GetTargetName(TargetSymbol);
        var sanitizedTargetName = SanitizeForNamespace(targetName);
        var hintNameSuffix = GetHintNameSuffix(targetName, sanitizedTargetName);

        if (generateImposterDeclaration.PutInTheSameNamespace)
        {
            ImposterNamespaceName = TargetSymbol.ContainingNamespace.IsGlobalNamespace
                ? null
                : TargetSymbol.ContainingNamespace.ToDisplayString();
            HintName = $"{sanitizedTargetName}{hintNameSuffix}";
        }
        else
        {
            ImposterNamespaceName = $"{DedicatedNamespacePrefix}.{sanitizedTargetName}";
            HintName = $"{DedicatedNamespacePrefix}.{sanitizedTargetName}{hintNameSuffix}";
        }

        SupportedCSharpFeatures = supportedCSharpFeatures;
    }

    private const string DedicatedNamespacePrefix = "Imposters";

    // The target's fully qualified name without `global::`, e.g. `Sample.IPair<int, string>`.
    private static string GetTargetName(INamedTypeSymbol targetSymbol)
    {
        var display = targetSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        const string globalPrefix = "global::";
        return display.StartsWith(globalPrefix, StringComparison.Ordinal)
            ? display[globalPrefix.Length..]
            : display;
    }

    // Sanitizing is lossy (`IFoo<int>` and `IFoo_int_` both become `IFoo_int_`), and duplicate hint names make
    // Roslyn drop every source of the generator. A sanitized name without `_` lost nothing, so it is used as is
    // and plain names cannot collide. Any other name gets a hash of the original name, so it differs from every
    // plain name and only collides with another hashed name on a hash collision.
    private static string GetHintNameSuffix(string targetName, string sanitizedTargetName) =>
        sanitizedTargetName.IndexOf('_') < 0
            ? "Imposter.g.cs"
            : $"Imposter.{Fnv1aHash(targetName).ToString("x8", CultureInfo.InvariantCulture)}.g.cs";

    // FNV-1a over UTF-16 code units: unlike string.GetHashCode, it is stable across processes and platforms.
    private static uint Fnv1aHash(string value)
    {
        const uint offsetBasis = 2166136261;
        const uint prime = 16777619;

        var hash = offsetBasis;
        foreach (var ch in value)
        {
            hash = unchecked((hash ^ ch) * prime);
        }

        return hash;
    }

    // Reduces a name to characters valid in a namespace and a hint name, e.g. `Sample.IPair<int, string>`
    // becomes `Sample.IPair_int__string_`.
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
