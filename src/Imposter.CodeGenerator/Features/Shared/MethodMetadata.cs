using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Shared;

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
