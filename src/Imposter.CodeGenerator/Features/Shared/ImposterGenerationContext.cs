using Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterGenerationContext
{
    internal readonly GenerateImposterDeclaration GenerateImposterDeclaration;

    internal INamedTypeSymbol TargetSymbol => GenerateImposterDeclaration.ImposterTarget;

    internal readonly ImposterTargetMetadata Imposter;

    internal readonly string ImposterComponentsNamespace;

    internal readonly SupportedCSharpFeatures SupportedCSharpFeatures;

    internal ImposterGenerationContext(
        GenerateImposterDeclaration generateImposterDeclaration,
        in SupportedCSharpFeatures supportedCSharpFeatures)
    {
        GenerateImposterDeclaration = generateImposterDeclaration;
        Imposter = new ImposterTargetMetadata(generateImposterDeclaration.ImposterTarget);
        ImposterComponentsNamespace = $"Imposters.{TargetSymbol.ToDisplayString()}";
        SupportedCSharpFeatures = supportedCSharpFeatures;
    }
}
