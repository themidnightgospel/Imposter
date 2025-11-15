using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct FieldMetadata(string name, TypeSyntax type)
{
    internal readonly string Name = name;

    internal readonly TypeSyntax Type = type;
}
