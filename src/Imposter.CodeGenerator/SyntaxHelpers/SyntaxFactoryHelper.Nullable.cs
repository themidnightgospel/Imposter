using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static PragmaWarningDirectiveTriviaSyntax EnableNullableTrivia()
    {
        return PragmaWarningDirectiveTrivia(
            Token(SyntaxKind.DisableKeyword),
            SingletonSeparatedList(ParseExpression("nullable")),
            true);
    }

    internal static PragmaWarningDirectiveTriviaSyntax RestoreNullableTrivia()
    {
        return PragmaWarningDirectiveTrivia(
            Token(SyntaxKind.RestoreKeyword),
            SeparatedList<ExpressionSyntax>().Add(ParseExpression("nullable")),
            true);
    }
}