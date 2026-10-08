using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.ImposterBuilderInterface;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.SetterImposterBuilderInterface;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly ref struct ImposterIndexerMetadata
{
    internal readonly ImposterIndexerCoreMetadata Core;

    internal readonly IndexerArgumentsMetadata Arguments;

    internal readonly IndexerArgumentsCriteriaMetadata ArgumentsCriteria;

    internal readonly DefaultIndexerBehaviourMetadata DefaultIndexerBehaviour;

    internal readonly IndexerDelegateMetadata Delegates;

    internal readonly IndexerGetterImposterMetadata GetterImplementation;

    internal readonly IndexerSetterImposterMetadata SetterImplementation;

    internal readonly IndexerGetterImposterBuilderInterfaceMetadata GetterBuilderInterface;

    internal readonly IndexerSetterImposterBuilderInterfaceMetadata SetterBuilderInterface;

    internal readonly IndexerImposterBuilderInterfaceMetadata BuilderInterface;

    internal readonly IndexerImposterBuilderMetadata Builder;

    internal readonly FieldMetadata BuilderField;

    internal readonly SyntaxTokenList ImposterInstanceModifiers;

    internal readonly bool RequiresExplicitInterfaceImplementation;

    internal readonly ExplicitInterfaceSpecifierSyntax? ExplicitInterfaceSpecifier;

    internal ImposterIndexerMetadata(
        IPropertySymbol propertySymbol,
        string uniqueName,
        MemberAccess memberAccess,
        bool requiresExplicitInterfaceImplementation
    )
    {
        Core = new ImposterIndexerCoreMetadata(propertySymbol, uniqueName, memberAccess);
        Arguments = new IndexerArgumentsMetadata(Core);
        ArgumentsCriteria = new IndexerArgumentsCriteriaMetadata(Core);
        DefaultIndexerBehaviour = new DefaultIndexerBehaviourMetadata(Core, Arguments);
        var defaultIndexerBehaviourField = new FieldMetadata(
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
        Builder = new IndexerImposterBuilderMetadata(Core, defaultIndexerBehaviourField);
        BuilderField = new FieldMetadata($"_{Core.UniqueName}Indexer", Builder.TypeSyntax);
        RequiresExplicitInterfaceImplementation = requiresExplicitInterfaceImplementation;
        if (requiresExplicitInterfaceImplementation)
        {
            ExplicitInterfaceSpecifier = SyntaxFactory.ExplicitInterfaceSpecifier(
                (NameSyntax)SyntaxFactoryHelper.TypeSyntax(propertySymbol.ContainingType)
            );
            ImposterInstanceModifiers = default;
        }
        else
        {
            ExplicitInterfaceSpecifier = null;
            ImposterInstanceModifiers = ImposterInstanceModifierBuilder.For(
                propertySymbol,
                memberAccess
            );
        }
    }
}
