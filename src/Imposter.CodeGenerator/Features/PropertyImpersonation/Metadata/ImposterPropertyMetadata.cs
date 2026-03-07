using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilder;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.ImposterBuilderInterface;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposter;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposterBuilderInterface;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata;

internal readonly ref struct ImposterPropertyMetadata
{
    internal readonly ImposterPropertyCoreMetadata Core;

    internal readonly PropertyImposterBuilderInterfaceMetadata ImposterBuilderInterface;

    internal readonly PropertyImposterBuilderMetadata ImposterBuilder;

    internal readonly PropertySetterImposterBuilderInterfaceMetadata SetterImposterBuilderInterface;

    internal readonly PropertyGetterImposterBuilderInterfaceMetadata GetterImposterBuilderInterface;

    internal readonly PropertyGetterImposterBuilderMetadata GetterImposterBuilder;

    internal readonly PropertySetterImposterMetadata SetterImposter;

    internal readonly FieldMetadata AsField;

    internal readonly DefaultPropertyBehaviourMetadata DefaultPropertyBehaviour;

    internal readonly FieldMetadata DefaultPropertyBehaviourField;

    internal readonly SyntaxTokenList ImposterInstanceModifiers;

    internal readonly bool RequiresExplicitInterfaceImplementation;

    internal readonly ExplicitInterfaceSpecifierSyntax? ExplicitInterfaceSpecifier;

    public ImposterPropertyMetadata(
        IPropertySymbol property,
        string uniqueName,
        NameSet memberNameSet,
        bool requiresExplicitInterfaceImplementation = false
    )
    {
        Core = new ImposterPropertyCoreMetadata(property, uniqueName);

        DefaultPropertyBehaviour = new DefaultPropertyBehaviourMetadata(Core);
        DefaultPropertyBehaviourField = new FieldMetadata(
            "_defaultPropertyBehaviour",
            DefaultPropertyBehaviour.TypeSyntax
        );
        var propertyFieldBaseName = $"_{Core.UniqueName}PropertyBuilderField";
        AsField = new FieldMetadata(memberNameSet.Use(propertyFieldBaseName), Core.TypeSyntax);

        GetterImposterBuilderInterface = new PropertyGetterImposterBuilderInterfaceMetadata(Core);
        GetterImposterBuilder = new PropertyGetterImposterBuilderMetadata(
            Core,
            DefaultPropertyBehaviourField
        );

        SetterImposterBuilderInterface = new PropertySetterImposterBuilderInterfaceMetadata(Core);
        SetterImposter = new PropertySetterImposterMetadata(Core, DefaultPropertyBehaviourField);

        ImposterBuilderInterface = new PropertyImposterBuilderInterfaceMetadata(
            Core,
            SetterImposterBuilderInterface,
            GetterImposterBuilderInterface
        );
        ImposterBuilder = new PropertyImposterBuilderMetadata(
            Core,
            DefaultPropertyBehaviourField,
            SetterImposter,
            GetterImposterBuilder
        );

        RequiresExplicitInterfaceImplementation = requiresExplicitInterfaceImplementation;
        if (requiresExplicitInterfaceImplementation && property.ContainingType is not null)
        {
            ExplicitInterfaceSpecifier = ExplicitInterfaceSpecifier(
                (NameSyntax)SyntaxFactoryHelper.TypeSyntax(property.ContainingType)
            );
            ImposterInstanceModifiers = default;
        }
        else
        {
            ExplicitInterfaceSpecifier = null;
            ImposterInstanceModifiers = ImposterInstanceModifierBuilder.For(property);
        }
    }
}
