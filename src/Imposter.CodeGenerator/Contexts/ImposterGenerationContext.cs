using Imposter.CodeGenerator.SyntaxProviders;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Contexts;

internal class ImposterGenerationContext
{
    internal readonly GenerateImposterDeclaration GenerateImposterDeclaration;

    internal INamedTypeSymbol TargetSymbol => GenerateImposterDeclaration.ImposterTarget;

    internal readonly ImposterTypeMetadata Imposter;

    internal readonly string ImposterComponentsNamespace;

    internal ImposterGenerationContext(GenerateImposterDeclaration generateImposterDeclaration)
    {
        GenerateImposterDeclaration = generateImposterDeclaration;
        Imposter = new ImposterTypeMetadata(generateImposterDeclaration.ImposterTarget);
        ImposterComponentsNamespace = $"Imposters.{TargetSymbol.ToDisplayString()}";
    }
}