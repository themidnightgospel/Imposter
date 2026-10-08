using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Shared;

/// <summary>
/// The getter method that takes the next configured return value, or repeats the last one once all are taken.
/// </summary>
internal readonly struct NextReturnValueMethodMetadata
{
    internal readonly string Name = "NextReturnValue";

    internal readonly TypeSyntax ReturnType;

    internal NextReturnValueMethodMetadata(TypeSyntax returnType)
    {
        ReturnType = returnType;
    }
}
