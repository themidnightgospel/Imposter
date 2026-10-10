using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.ImposterBuilderInterface;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.SetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Models;
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

    // An indexer without keys to match, or whose keys to match another indexer's, can't have a this[...] of its own
    // on the imposter or its interface's setup view, so it's set up by a method named after its unique name.
    internal readonly bool HasSetupMethod;

    // An indexer the imposter sets up by a method: one with a setup method, or one that collides with another
    // interface's indexer and is set up through its interface's view, which calls a private method.
    internal readonly bool IsSetUpByMethod;

    internal readonly ExplicitInterfaceSpecifierSyntax? ExplicitInterfaceSpecifier;

    // The keys passed through, as the getter's handlers and setup methods take them: under names that don't hide the
    // parameters and locals those declare.
    internal readonly string[] GetterPassedThroughKeyNames;

    internal ImposterIndexerMetadata(
        PropertyModel indexer,
        string uniqueName,
        NameSet memberNameSet,
        bool requiresExplicitInterfaceImplementation,
        ExceptionTypeParameterMetadata exceptionTypeParameter
    )
    {
        Core = new ImposterIndexerCoreMetadata(indexer, uniqueName);
        Arguments = new IndexerArgumentsMetadata(Core);
        ArgumentsCriteria = new IndexerArgumentsCriteriaMetadata(Core);
        DefaultIndexerBehaviour = new DefaultIndexerBehaviourMetadata(Core, Arguments);
        var defaultIndexerBehaviourField = new FieldMetadata(
            $"_{Core.UniqueName}DefaultIndexerBehaviour",
            DefaultIndexerBehaviour.TypeSyntax
        );
        Delegates = Core.Delegates;
        GetterImplementation = new IndexerGetterImposterMetadata(this);
        SetterImplementation = new IndexerSetterImposterMetadata(this);
        GetterBuilderInterface = new IndexerGetterImposterBuilderInterfaceMetadata(
            Core,
            Delegates,
            exceptionTypeParameter
        );
        SetterBuilderInterface = new IndexerSetterImposterBuilderInterfaceMetadata(Core, Delegates);
        BuilderInterface = new IndexerImposterBuilderInterfaceMetadata(
            Core,
            SetterBuilderInterface,
            GetterBuilderInterface
        );
        Builder = new IndexerImposterBuilderMetadata(this, defaultIndexerBehaviourField);
        ParameterMetadata?[] returnsParameters =
        [
            GetterBuilderInterface.ReturnsMethod.ValueParameter,
            GetterBuilderInterface.ReturnsMethod.FuncParameter,
            GetterBuilderInterface.ReturnsMethod.DelegateParameter,
        ];
        var getterNames = new NameSet([
            GetterImplementation.ArgumentsVariableName,
            GetterImplementation.BaseImplementationParameter.Name,
            IndexerGetterImposterMetadata.GeneratorVariableName,
            IndexerGetterImposterMetadata.CallbackVariableName,
            GetterBuilderInterface.ThrowsMethod.ExceptionParameter.Name,
            GetterBuilderInterface.ThrowsMethod.DelegateParameter.Name,
            GetterBuilderInterface.ThrowsMethod.ExceptionTypeParameter.Name,
            .. returnsParameters.OfType<ParameterMetadata>().Select(parameter => parameter.Name),
        ]);
        GetterPassedThroughKeyNames = Core
            .PassedThroughParameters.Select(parameter => getterNames.Use(parameter.Name))
            .ToArray();
        // The setup indexer uses the field by its bare name, so the name avoids the indexer's parameter names, as well
        // as the imposter's other members.
        BuilderField = new FieldMetadata(
            memberNameSet.Use(Core.CreateParameterNameSet().Use($"_{Core.UniqueName}Indexer")),
            Builder.TypeSyntax
        );
        RequiresExplicitInterfaceImplementation = requiresExplicitInterfaceImplementation;
        HasSetupMethod = Core.MatchedParameters.Length == 0 || indexer.HasIndexerWithTheSameSetup;
        IsSetUpByMethod = HasSetupMethod || requiresExplicitInterfaceImplementation;
        if (requiresExplicitInterfaceImplementation)
        {
            ExplicitInterfaceSpecifier = SyntaxFactory.ExplicitInterfaceSpecifier(
                (NameSyntax)SyntaxFactoryHelper.TypeSyntax(indexer.ContainingType)
            );
            ImposterInstanceModifiers = default;
        }
        else
        {
            ExplicitInterfaceSpecifier = null;
            ImposterInstanceModifiers = ImposterInstanceModifierBuilder.For(indexer);
        }
    }
}
