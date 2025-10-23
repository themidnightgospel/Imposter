using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodSetup.Metadata;

internal readonly struct ParameterMetadata(string name, TypeSyntax type, ExpressionSyntax? defaultValue = null)
{
    internal readonly string Name = name;

    internal readonly TypeSyntax Type = type;

    internal readonly ExpressionSyntax? DefaultValue = defaultValue;
}