using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Helpers;

internal static class ImposterInstanceModifierBuilder
{
    internal static SyntaxTokenList For(MethodModel method) =>
        For(method.IsClassMember, method.OverrideAccessibility);

    internal static SyntaxTokenList For(PropertyModel property) =>
        For(property.IsClassMember, property.OverrideAccessibility);

    internal static SyntaxTokenList For(EventModel @event) =>
        For(@event.IsClassMember, @event.OverrideAccessibility);

    // An overriding accessor restates its own accessibility when it differs from the property's.
    internal static SyntaxTokenList ForAccessor(
        PropertyAccessorModel? accessor,
        PropertyModel property
    ) =>
        accessor is null
        || !property.IsClassMember
        || accessor.OverrideAccessibility == property.OverrideAccessibility
            ? default
            : GetAccessibilityModifiers(accessor.OverrideAccessibility);

    private static SyntaxTokenList For(bool isClassMember, Accessibility overrideAccessibility) =>
        isClassMember ? Override(overrideAccessibility) : InterfaceImplementation();

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
