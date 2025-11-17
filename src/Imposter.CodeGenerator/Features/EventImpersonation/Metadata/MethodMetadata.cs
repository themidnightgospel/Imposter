using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.EventImpersonation.Metadata;

internal readonly struct MethodMetadata
{
    internal readonly string Name;

    internal readonly TypeSyntax ReturnType;

    internal MethodMetadata(string name, TypeSyntax returnType)
    {
        Name = name;
        ReturnType = returnType;
    }
}
