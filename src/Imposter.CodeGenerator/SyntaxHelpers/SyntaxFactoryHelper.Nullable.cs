using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static NullableDirectiveTriviaSyntax EnableNullableTrivia() =>
        NullableDirectiveTrivia(Token(SyntaxKind.EnableKeyword), isActive: true);

    internal static PragmaWarningDirectiveTriviaSyntax DisableWarnings() =>
        PragmaWarningDirectiveTrivia(Token(SyntaxKind.DisableKeyword), isActive: true)
            .WithErrorCodes(default);

    internal static PragmaWarningDirectiveTriviaSyntax RestoreWarnings() =>
        PragmaWarningDirectiveTrivia(Token(SyntaxKind.RestoreKeyword), isActive: true)
            .WithErrorCodes(default);

    internal static NullableDirectiveTriviaSyntax RestoreNullableTrivia() =>
        NullableDirectiveTrivia(Token(SyntaxKind.RestoreKeyword), isActive: true);

    internal static TypeSyntax ToNullableType(this TypeSyntax typeSyntax) =>
        NullableType(typeSyntax);
}
