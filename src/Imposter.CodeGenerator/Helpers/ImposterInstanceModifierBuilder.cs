using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Helpers;

internal static class ImposterInstanceModifierBuilder
{
    internal static SyntaxTokenList For(MethodModel method) =>
        For(method.IsClassMember, method.OverrideAccessibility);

    // An override of a required property must be required too. Only the Roslyn 4.4+ builds see required members.
    internal static SyntaxTokenList For(PropertyModel property) =>
#if ROSLYN4_4_OR_GREATER
        property.IsRequired
            ? For(property.IsClassMember, property.OverrideAccessibility)
                .Add(SyntaxFactory.Token(SyntaxKind.RequiredKeyword))
            :
#endif
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
