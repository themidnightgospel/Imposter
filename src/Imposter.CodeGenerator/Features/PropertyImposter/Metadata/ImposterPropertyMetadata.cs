using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilder;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.ImposterBuilderInterface;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposter;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposterBuilderInterface;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata;

internal readonly ref struct ImposterPropertyMetadata
{
    internal readonly ImposterPropertyCoreMetadata Core;

    internal readonly PropertyImposterBuilderInterfaceMetadata ImposterBuilderInterface;

    internal readonly PropertyImposterBuilderMetadata ImposterBuilder;

    internal readonly PropertySetterImposterBuilderInterfaceMetadata SetterImposterBuilderInterface;

    internal readonly PropertyGetterImposterBuilderInterfaceMetadata GetterImposterBuilderInterface;

    internal readonly PropertyGetterImposterBuilderMetadata GetterImposterBuilder;

    internal readonly PropertySetterImposterMetadata SetterImposter;

    // TODO move to imoster instance metadata
    internal readonly FieldMetadata AsField;

    internal readonly DefaultPropertyBehaviourMetadata DefaultPropertyBehaviour;

    internal readonly FieldMetadata DefaultPropertyBehaviourField;

    internal readonly SyntaxTokenList ImposterInstanceModifiers;

    public ImposterPropertyMetadata(IPropertySymbol property, string uniqueName)
    {
        Core = new ImposterPropertyCoreMetadata(property, uniqueName);

        DefaultPropertyBehaviour = new DefaultPropertyBehaviourMetadata(Core);
        DefaultPropertyBehaviourField = new FieldMetadata("_defaultPropertyBehaviour", DefaultPropertyBehaviour.TypeSyntax);
        AsField = new FieldMetadata($"_{Core.UniqueName}", Core.TypeSyntax);

        GetterImposterBuilderInterface = new PropertyGetterImposterBuilderInterfaceMetadata(Core);
        GetterImposterBuilder = new PropertyGetterImposterBuilderMetadata(Core, DefaultPropertyBehaviourField);

        SetterImposterBuilderInterface = new PropertySetterImposterBuilderInterfaceMetadata(Core);
        SetterImposter = new PropertySetterImposterMetadata(Core, DefaultPropertyBehaviourField);

        ImposterBuilderInterface = new PropertyImposterBuilderInterfaceMetadata(Core, SetterImposterBuilderInterface, GetterImposterBuilderInterface);
        ImposterBuilder = new PropertyImposterBuilderMetadata(Core, DefaultPropertyBehaviourField, SetterImposter, GetterImposterBuilder);

        ImposterInstanceModifiers = BuildImposterInstanceModifiers(property);
    }

    private static SyntaxTokenList BuildImposterInstanceModifiers(IPropertySymbol propertySymbol)
    {
        if (propertySymbol.ContainingType?.TypeKind == TypeKind.Class)
        {
            return GetAccessibilityModifiers(propertySymbol.DeclaredAccessibility)
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
