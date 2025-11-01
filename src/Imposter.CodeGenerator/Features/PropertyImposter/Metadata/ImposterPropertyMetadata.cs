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
    internal readonly AsFieldMetadata AsField;

    internal readonly DefaultPropertyBehaviourMetadata DefaultPropertyBehaviour;

    internal readonly FieldMetadata DefaultPropertyBehaviourField;

    public ImposterPropertyMetadata(IPropertySymbol property, string uniqueName)
    {
        Core = new ImposterPropertyCoreMetadata(property, uniqueName);

        DefaultPropertyBehaviour = new DefaultPropertyBehaviourMetadata(Core);
        DefaultPropertyBehaviourField = new FieldMetadata("_defaultPropertyBehaviour", DefaultPropertyBehaviour.TypeSyntax);

        GetterImposterBuilderInterface = new PropertyGetterImposterBuilderInterfaceMetadata(Core);
        GetterImposterBuilder = new PropertyGetterImposterBuilderMetadata(Core, DefaultPropertyBehaviourField);

        SetterImposterBuilderInterface = new PropertySetterImposterBuilderInterfaceMetadata(Core);
        SetterImposter = new PropertySetterImposterMetadata(Core, DefaultPropertyBehaviourField);

        ImposterBuilderInterface = new PropertyImposterBuilderInterfaceMetadata(Core, SetterImposterBuilderInterface, GetterImposterBuilderInterface);
        ;
        ImposterBuilder = new PropertyImposterBuilderMetadata(Core, DefaultPropertyBehaviourField, SetterImposter, GetterImposterBuilder);

        AsField = new AsFieldMetadata(Core);
    }

    internal readonly struct AsFieldMetadata
    {
        internal readonly string Name;

        public AsFieldMetadata(in ImposterPropertyCoreMetadata property)
        {
            Name = $"_{property.UniqueName}";
        }
    }
}