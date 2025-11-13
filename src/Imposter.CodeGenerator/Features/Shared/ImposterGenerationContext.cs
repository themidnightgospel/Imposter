using System;
using System.Text;
using Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterGenerationContext
{
    internal readonly GenerateImposterDeclaration GenerateImposterDeclaration;

    internal INamedTypeSymbol TargetSymbol => GenerateImposterDeclaration.ImposterTarget;

    internal readonly ImposterTargetMetadata Imposter;

    internal readonly string ImposterComponentsNamespace;

    internal readonly string TargetNamespaceName;

    internal readonly string ImposterNamespaceName;

    internal readonly SupportedCSharpFeatures SupportedCSharpFeatures;

    internal ImposterGenerationContext(
        GenerateImposterDeclaration generateImposterDeclaration,
        in SupportedCSharpFeatures supportedCSharpFeatures)
    {
        GenerateImposterDeclaration = generateImposterDeclaration;
        Imposter = new ImposterTargetMetadata(generateImposterDeclaration.ImposterTarget, supportedCSharpFeatures);
        ImposterComponentsNamespace = BuildImposterComponentsNamespace(TargetSymbol);
        TargetNamespaceName = TargetSymbol.ContainingNamespace.ToDisplayString();
        ImposterNamespaceName = generateImposterDeclaration.PutInTheSameNamespace
            ? TargetNamespaceName
            : ImposterComponentsNamespace;
        SupportedCSharpFeatures = supportedCSharpFeatures;
    }

    private static string BuildImposterComponentsNamespace(INamedTypeSymbol targetSymbol)
    {
        var display = targetSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

        const string globalPrefix = "global::";
        if (display.StartsWith(globalPrefix, StringComparison.Ordinal))
        {
            display = display[globalPrefix.Length..];
        }

        return $"Imposters.{SanitizeForNamespace(display)}";
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
