using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct DefaultResultGeneratorMethodMetadata
{
    internal readonly string Name;

    internal readonly TypeSyntax ReturnType;

    internal DefaultResultGeneratorMethodMetadata(
        in ReturnTypeMetadata methodReturnType,
        NameSet memberNames
    )
    {
        Name = memberNames.Use("DefaultResultGenerator");
        ReturnType = methodReturnType.TypeSymbolMetadata.TypeSyntax;
    }
}
