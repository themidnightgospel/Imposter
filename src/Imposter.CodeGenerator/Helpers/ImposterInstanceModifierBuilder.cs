using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Helpers;

internal static class ImposterInstanceModifierBuilder
{
    internal static SyntaxTokenList For(ISymbol symbol)
    {
        if (symbol?.ContainingType?.TypeKind == TypeKind.Class)
        {
            return GetAccessibilityModifiers(symbol.DeclaredAccessibility)
                .Add(SyntaxFactory.Token(SyntaxKind.OverrideKeyword));
        }

        return SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword));
    }

    private static SyntaxTokenList GetAccessibilityModifiers(Accessibility accessibility)
    {
        return accessibility switch
        {
            Accessibility.Public => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
            Accessibility.Internal => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.InternalKeyword)),
            Accessibility.Protected => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword)),
            Accessibility.ProtectedOrInternal => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.ProtectedKeyword), SyntaxFactory.Token(SyntaxKind.InternalKeyword)),
            Accessibility.ProtectedAndInternal => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PrivateKeyword), SyntaxFactory.Token(SyntaxKind.ProtectedKeyword)),
            _ => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
        };
    }
}
