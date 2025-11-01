using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilder;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.ImposterBuilderInterface;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposter;
using Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.Shared;
using Microsoft.CodeAnalysis;

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
    }
}