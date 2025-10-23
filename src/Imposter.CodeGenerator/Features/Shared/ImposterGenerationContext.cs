using Imposter.CodeGenerator.CodeGenerator.SyntaxProviders;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct ImposterGenerationContext
{
    internal readonly GenerateImposterDeclaration GenerateImposterDeclaration;

    internal INamedTypeSymbol TargetSymbol => GenerateImposterDeclaration.ImposterTarget;

    internal readonly ImposterTargetMetadata Imposter;

    internal readonly string ImposterComponentsNamespace;

    internal ImposterGenerationContext(GenerateImposterDeclaration generateImposterDeclaration)
    {
        GenerateImposterDeclaration = generateImposterDeclaration;
        Imposter = new ImposterTargetMetadata(generateImposterDeclaration.ImposterTarget);
        ImposterComponentsNamespace = $"Imposters.{TargetSymbol.ToDisplayString()}";
    }
}