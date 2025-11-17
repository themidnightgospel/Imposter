using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilder;

internal readonly struct LastReturnValueFieldMetadata
{
    internal readonly string Name = "_lastReturnValue";

    internal readonly TypeSyntax TypeSyntax;

    internal LastReturnValueFieldMetadata(TypeSyntax handlerType)
    {
        TypeSyntax = handlerType;
    }
}
