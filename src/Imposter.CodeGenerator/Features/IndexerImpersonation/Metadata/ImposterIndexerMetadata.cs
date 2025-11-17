using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.ImposterBuilderInterface;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.SetterImposterBuilderInterface;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly ref struct ImposterIndexerMetadata
{
    internal readonly ImposterIndexerCoreMetadata Core;

    internal readonly IndexerArgumentsMetadata Arguments;

    internal readonly IndexerArgumentsCriteriaMetadata ArgumentsCriteria;

    internal readonly DefaultIndexerBehaviourMetadata DefaultIndexerBehaviour;

    internal readonly FieldMetadata DefaultIndexerBehaviourField;

    internal readonly IndexerDelegateMetadata Delegates;

    internal readonly IndexerGetterImposterMetadata GetterImplementation;

    internal readonly IndexerSetterImposterMetadata SetterImplementation;

    internal readonly IndexerGetterImposterBuilderInterfaceMetadata GetterBuilderInterface;

    internal readonly IndexerSetterImposterBuilderInterfaceMetadata SetterBuilderInterface;

    internal readonly IndexerImposterBuilderInterfaceMetadata BuilderInterface;

    internal readonly IndexerImposterBuilderMetadata Builder;

    internal readonly FieldMetadata BuilderField;

    internal readonly SyntaxTokenList ImposterInstanceModifiers;

    internal ImposterIndexerMetadata(IPropertySymbol propertySymbol, string uniqueName)
    {
        Core = new ImposterIndexerCoreMetadata(propertySymbol, uniqueName);
        Arguments = new IndexerArgumentsMetadata(Core);
        ArgumentsCriteria = new IndexerArgumentsCriteriaMetadata(Core);
        DefaultIndexerBehaviour = new DefaultIndexerBehaviourMetadata(Core, Arguments);
        DefaultIndexerBehaviourField = new FieldMetadata(
            $"_{Core.UniqueName}DefaultIndexerBehaviour",
            DefaultIndexerBehaviour.TypeSyntax
        );
        Delegates = new IndexerDelegateMetadata(Core);
        GetterImplementation = new IndexerGetterImposterMetadata(this);
        SetterImplementation = new IndexerSetterImposterMetadata(this);
        GetterBuilderInterface = new IndexerGetterImposterBuilderInterfaceMetadata(Core, Delegates);
        SetterBuilderInterface = new IndexerSetterImposterBuilderInterfaceMetadata(Core, Delegates);
        BuilderInterface = new IndexerImposterBuilderInterfaceMetadata(
            Core,
            SetterBuilderInterface,
            GetterBuilderInterface
        );
        Builder = new IndexerImposterBuilderMetadata(Core, DefaultIndexerBehaviourField);
        BuilderField = new FieldMetadata($"_{Core.UniqueName}Indexer", Builder.TypeSyntax);
        ImposterInstanceModifiers = ImposterInstanceModifierBuilder.For(propertySymbol);
    }
}
