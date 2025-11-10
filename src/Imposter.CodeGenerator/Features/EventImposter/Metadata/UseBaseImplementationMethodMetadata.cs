namespace Imposter.CodeGenerator.Features.EventImposter.Metadata;

using Microsoft.CodeAnalysis.CSharp.Syntax;

internal readonly struct UseBaseImplementationMethodMetadata
{
    internal readonly string Name = "UseBaseImplementation";

    internal readonly TypeSyntax ReturnType;

    internal UseBaseImplementationMethodMetadata(TypeSyntax returnType)
    {
        ReturnType = returnType;
    }
}
