using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilder;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.ImposterBuilderInterface;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposter;
using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposterBuilderInterface;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
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

    internal readonly FieldMetadata BuilderField;

    internal readonly DefaultPropertyBehaviourMetadata DefaultPropertyBehaviour;

    internal readonly SyntaxTokenList ImposterInstanceModifiers;

    // The imposter property that sets this property up: named after it, or by its unique name when it's implemented
    // explicitly.
    internal readonly string SetupName;

    internal readonly ExplicitInterfaceSpecifierSyntax? ExplicitInterfaceSpecifier;

    public ImposterPropertyMetadata(
        PropertyModel property,
        string uniqueName,
        NameSet memberNameSet,
        bool requiresExplicitInterfaceImplementation
    )
    {
        Core = new ImposterPropertyCoreMetadata(property, uniqueName);

        DefaultPropertyBehaviour = new DefaultPropertyBehaviourMetadata(Core);
        var defaultPropertyBehaviourField = new FieldMetadata(
            "_defaultPropertyBehaviour",
            DefaultPropertyBehaviour.TypeSyntax
        );
        GetterImposterBuilderInterface = new PropertyGetterImposterBuilderInterfaceMetadata(Core);
        GetterImposterBuilder = new PropertyGetterImposterBuilderMetadata(
            Core,
            defaultPropertyBehaviourField
        );

        SetterImposterBuilderInterface = new PropertySetterImposterBuilderInterfaceMetadata(Core);
        SetterImposter = new PropertySetterImposterMetadata(Core, defaultPropertyBehaviourField);

        ImposterBuilderInterface = new PropertyImposterBuilderInterfaceMetadata(
            Core,
            SetterImposterBuilderInterface,
            GetterImposterBuilderInterface
        );
        ImposterBuilder = new PropertyImposterBuilderMetadata(
            Core,
            defaultPropertyBehaviourField,
            SetterImposter,
            GetterImposterBuilder
        );
        BuilderField = new FieldMetadata(
            memberNameSet.Use($"_{Core.UniqueName}PropertyBuilderField"),
            ImposterBuilder.Syntax
        );

        SetupName = requiresExplicitInterfaceImplementation ? Core.UniqueName : Core.Name;
        if (requiresExplicitInterfaceImplementation)
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
