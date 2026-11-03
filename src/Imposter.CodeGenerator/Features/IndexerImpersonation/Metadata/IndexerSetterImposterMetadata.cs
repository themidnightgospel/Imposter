using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerSetterImposterMetadata
{
    internal const string SetMethodName = "Set";

    internal const string CallbackMethodName = "Callback";

    internal const string CalledMethodName = "Called";

    internal const string UseBaseImplementationMethodName = "UseBaseImplementation";

    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly FieldMetadata CallbacksField;

    internal readonly FieldMetadata InvocationHistoryField;

    // The setter keeps the values set in the default behaviour: none for a value passed through.
    internal readonly FieldMetadata? DefaultBehaviourField;

    internal readonly FieldMetadata InvocationBehaviorField;

    internal readonly FieldMetadata PropertyDisplayNameField;

    internal readonly FieldMetadata HasConfiguredSetterField;

    internal readonly IndexerAccessorBuilderMetadata Builder;

    internal readonly string ValueParameterName;

    internal readonly string CriteriaParameterName;

    internal readonly string MarkConfiguredMethodName;

    internal readonly string SetterSuffix;

    internal readonly FieldMetadata? BaseImplementationCriteriaField;

    internal readonly ParameterMetadata BaseImplementationParameter;

    internal readonly string ArgumentsVariableName;

    internal readonly string MatchedCallbackVariableName;

    internal readonly string RegistrationVariableName;

    internal readonly string InvokedBaseImplementationVariableName;

    internal readonly string EnsureSetterConfiguredMethodName;

    internal IndexerSetterImposterMetadata(in ImposterIndexerMetadata indexer)
    {
        // The setter's Set takes the indexer's parameters and refers to these locals and members by name.
        var names = indexer.Core.CreateParameterNameSet();
        Name = "SetterImposter";
        TypeSyntax = IdentifierName(Name);

        ValueParameterName = names.Use("value");
        CriteriaParameterName = names.Use("criteria");
        SetterSuffix = " (setter)";
        EnsureSetterConfiguredMethodName = names.Use("EnsureSetterConfigured");
        CallbacksField = new FieldMetadata(
            names.Use("_callbacks"),
            WellKnownTypes.Imposter.Abstractions.CallbackCollection(BuildRegistrationTuple(indexer))
        );
        // A value passed through can't be kept, so the history keeps the keys alone.
        TypeSyntax invocationHistoryEntryType = !indexer.Core.IsValuePassedThrough
            ? TupleType(
                SeparatedList<TupleElementSyntax>([
                    TupleElement(indexer.Arguments.TypeSyntax)
                        .WithIdentifier(Identifier(HistoryArgumentsElementName)),
                    TupleElement(indexer.Core.NullableAwareKeptTypeSyntax)
                        .WithIdentifier(Identifier(HistoryValueElementName)),
                ])
            )
            : indexer.Arguments.TypeSyntax;
        InvocationHistoryField = new FieldMetadata(
            names.Use("_invocationHistory"),
            WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(invocationHistoryEntryType)
        );
        DefaultBehaviourField = !indexer.Core.IsValuePassedThrough
            ? new FieldMetadata(
                names.Use("_defaultBehaviour"),
                indexer.DefaultIndexerBehaviour.TypeSyntax
            )
            : null;
        InvocationBehaviorField = new FieldMetadata(
            names.Use("_invocationBehavior"),
            WellKnownTypes.Imposter.Abstractions.ImposterMode
        );
        PropertyDisplayNameField = new FieldMetadata(
            names.Use("_propertyDisplayName"),
            WellKnownTypes.String
        );
        HasConfiguredSetterField = new FieldMetadata(
            names.Use("_hasConfiguredSetter"),
            WellKnownTypes.Bool
        );
        BaseImplementationCriteriaField = indexer.Core.SetterSupportsBaseImplementation
            ? new FieldMetadata(
                names.Use("_baseCriteria"),
                WellKnownTypes.Imposter.Abstractions.CallbackCollection(
                    indexer.ArgumentsCriteria.TypeSyntax
                )
            )
            : null;
        BaseImplementationParameter = indexer.Core.SetterBaseImplementationParameter(
            names.Use(ImposterIndexerCoreMetadata.BaseImplementationParameterName)
        );
        ArgumentsVariableName = names.Use("arguments");
        MatchedCallbackVariableName = names.Use("matchedCallback");
        RegistrationVariableName = names.Use("registration");
        InvokedBaseImplementationVariableName = names.Use("invokedBaseImplementation");

        Builder = new IndexerAccessorBuilderMetadata("_setterImposter");
        MarkConfiguredMethodName = "MarkConfigured";
    }

    // The elements of a history entry that keeps the value: the keys and the value.
    internal const string HistoryArgumentsElementName = "Arguments";

    internal const string HistoryValueElementName = "Value";

    // The elements of a callback registration: the criteria a set must match, and the callback.
    internal const string RegistrationCriteriaElementName = "Criteria";

    internal const string RegistrationCallbackElementName = "Callback";

    private static TupleTypeSyntax BuildRegistrationTuple(in ImposterIndexerMetadata indexer) =>
        TupleType(
            SeparatedList<TupleElementSyntax>([
                TupleElement(
                    indexer.ArgumentsCriteria.TypeSyntax,
                    Identifier(RegistrationCriteriaElementName)
                ),
                TupleElement(
                    indexer.Delegates.SetterCallbackDelegateType,
                    Identifier(RegistrationCallbackElementName)
                ),
            ])
        );
}
