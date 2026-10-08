using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Helpers;

internal static class ImposterInstanceModifierBuilder
{
    internal static SyntaxTokenList For(ISymbol symbol, MemberAccess memberAccess) =>
        symbol?.ContainingType?.TypeKind == TypeKind.Class
            ? Override(memberAccess.GetOverrideAccessibility(symbol))
            : InterfaceImplementation();

    internal static SyntaxTokenList For(MethodModel method) =>
        method.IsClassMember ? Override(method.OverrideAccessibility) : InterfaceImplementation();

    private static SyntaxTokenList Override(Accessibility accessibility) =>
        GetAccessibilityModifiers(accessibility)
            .Add(SyntaxFactory.Token(SyntaxKind.OverrideKeyword));

    private static SyntaxTokenList InterfaceImplementation() =>
        SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword));

    private static SyntaxTokenList GetAccessibilityModifiers(Accessibility accessibility)
    {
        return accessibility switch
        {
            Accessibility.Public => SyntaxFactory.TokenList(
                SyntaxFactory.Token(SyntaxKind.PublicKeyword)
            ),
            Accessibility.Internal => SyntaxFactory.TokenList(
                SyntaxFactory.Token(SyntaxKind.InternalKeyword)
            ),
            Accessibility.Protected => SyntaxFactory.TokenList(
                SyntaxFactory.Token(SyntaxKind.ProtectedKeyword)
            ),
            Accessibility.ProtectedOrInternal => SyntaxFactory.TokenList(
                SyntaxFactory.Token(SyntaxKind.ProtectedKeyword),
                SyntaxFactory.Token(SyntaxKind.InternalKeyword)
            ),
            Accessibility.ProtectedAndInternal => SyntaxFactory.TokenList(
                SyntaxFactory.Token(SyntaxKind.PrivateKeyword),
                SyntaxFactory.Token(SyntaxKind.ProtectedKeyword)
            ),
            _ => SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)),
        };
    }
}
