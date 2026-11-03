using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static readonly IdentifierNameSyntax Var = IdentifierName("var");

    internal static readonly IdentifierNameSyntax It = IdentifierName("it");
}
