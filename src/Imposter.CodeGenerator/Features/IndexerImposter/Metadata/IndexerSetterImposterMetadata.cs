using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata;

internal readonly struct IndexerSetterImposterMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly FieldMetadata CallbacksField;

    internal readonly FieldMetadata InvocationHistoryField;

    internal readonly FieldMetadata DefaultBehaviourField;

    internal readonly FieldMetadata InvocationBehaviorField;

    internal readonly FieldMetadata PropertyDisplayNameField;

    internal readonly FieldMetadata HasConfiguredSetterField;

    internal readonly SetterBuilderMetadata Builder;

    internal readonly string ValueParameterName;

    internal readonly string CriteriaParameterName;

    internal readonly string SetterSuffix;

    internal readonly FieldMetadata? BaseImplementationCriteriaField;

    internal readonly string BaseImplementationParameterName;

    internal IndexerSetterImposterMetadata(in ImposterIndexerMetadata indexer)
    {
        Name = "SetterImposter";
        TypeSyntax = IdentifierName(Name);

        ValueParameterName = indexer.Core.ParameterNameSet.Use("value");
        CriteriaParameterName = indexer.Core.ParameterNameSet.Use("criteria");
        SetterSuffix = " (setter)";
        CallbacksField = new FieldMetadata(
            "_callbacks",
            WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(BuildRegistrationTuple(indexer)));
        InvocationHistoryField = new FieldMetadata(
            "_invocationHistory",
            WellKnownTypes.System.Collections.Concurrent.ConcurrentBag(indexer.Arguments.TypeSyntax));
        DefaultBehaviourField = new FieldMetadata("_defaultBehaviour", indexer.DefaultIndexerBehaviour.TypeSyntax);
        InvocationBehaviorField = new FieldMetadata(
            "_invocationBehavior",
            WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior);
        PropertyDisplayNameField = new FieldMetadata(
            "_propertyDisplayName",
            PredefinedType(Token(SyntaxKind.StringKeyword)));
        HasConfiguredSetterField = new FieldMetadata("_hasConfiguredSetter", WellKnownTypes.Bool);
        BaseImplementationCriteriaField = indexer.Core.SetterSupportsBaseImplementation
            ? new FieldMetadata(
                "_baseCriteria",
                WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(indexer.ArgumentsCriteria.TypeSyntax))
            : null;
        BaseImplementationParameterName = indexer.Core.ParameterNameSet.Use("baseImplementation");

        Builder = new SetterBuilderMetadata();
    }

    private static TupleTypeSyntax BuildRegistrationTuple(in ImposterIndexerMetadata indexer)
        => TupleType(
            SeparatedList<TupleElementSyntax>(new SyntaxNodeOrToken[]
            {
                TupleElement(indexer.ArgumentsCriteria.TypeSyntax, Identifier("Criteria")),
                Token(SyntaxKind.CommaToken),
                TupleElement(indexer.Delegates.SetterCallbackDelegateType, Identifier("Callback"))
            }));

    internal readonly struct SetterBuilderMetadata
    {
        internal readonly string Name;

        internal readonly string ImposterFieldName;

        internal readonly string CriteriaFieldName;

        public SetterBuilderMetadata()
        {
            Name = "Builder";
            ImposterFieldName = "_setterImposter";
            CriteriaFieldName = "_criteria";
        }
    }
}
