using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct DefaultResultGeneratorMethodMetadata
{
    internal readonly string Name = "DefaultResultGenerator";

    internal readonly TypeSyntax ReturnType;

    internal DefaultResultGeneratorMethodMetadata(in ReturnTypeMetadata methodReturnType)
    {
        ReturnType = methodReturnType.TypeSymbolMetadata.TypeSyntax;
    }
}
