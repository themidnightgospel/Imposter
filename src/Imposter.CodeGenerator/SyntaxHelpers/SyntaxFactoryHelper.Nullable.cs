using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static NullableDirectiveTriviaSyntax EnableNullableTrivia() =>
        NullableDirectiveTrivia(Token(SyntaxKind.EnableKeyword), isActive: true);

    internal static PragmaWarningDirectiveTriviaSyntax DisableCS8608() =>
        PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), true)
            .WithErrorCodes(SingletonSeparatedList<ExpressionSyntax>(IdentifierName("CS8608")));

    internal static PragmaWarningDirectiveTriviaSyntax RestoreCS8608() =>
        PragmaWarningDirectiveTrivia(Token(SyntaxKind.RestoreKeyword), isActive: true)
            .WithErrorCodes(SingletonSeparatedList<ExpressionSyntax>(IdentifierName("CS8608")));

    internal static NullableDirectiveTriviaSyntax RestoreNullableTrivia() =>
        NullableDirectiveTrivia(Token(SyntaxKind.RestoreKeyword), isActive: true);

    internal static TypeSyntax ToNullableType(this TypeSyntax typeSyntax) =>
        NullableType(typeSyntax);
}
