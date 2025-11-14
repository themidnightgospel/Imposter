using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.InvocationSetup;

internal readonly struct DefaultResultGeneratorMethodMetadata
{
    internal readonly string Name = "DefaultResultGenerator";

    internal readonly TypeSyntax ReturnType;

    internal DefaultResultGeneratorMethodMetadata(ReturnTypeMetadata methodReturnType)
    {
        ReturnType = methodReturnType.TypeSymbolMetadata.TypeSyntax;
    }
}
