using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal static class Defaults
{
    public static IReadOnlyList<AttributeListSyntax> DefaultTypeAttributes =
    [
        SyntaxFactoryHelper.GeneratedCodeAttribute()
    ];
}