using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    public static TypeArgumentListSyntax AsTypeArguments(this IEnumerable<NameSyntax> typeNames)
        => TypeArgumentList(SeparatedList<TypeSyntax>(typeNames));

    public static TypeArgumentListSyntax? TypeArgumentListSyntax(IReadOnlyList<NameSyntax> typeArguments)
        => typeArguments.Count == 0
            ? null
            : TypeArgumentList(SeparatedList<TypeSyntax>(typeArguments));
}