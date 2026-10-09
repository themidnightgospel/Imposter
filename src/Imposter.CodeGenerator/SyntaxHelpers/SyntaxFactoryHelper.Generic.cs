using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static TypeArgumentListSyntax TypeArguments(IEnumerable<TypeSyntax> types) =>
        TypeArgumentList(SeparatedList(types));
}
