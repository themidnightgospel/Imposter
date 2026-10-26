using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerGetterImposterMetadata
{
    // The getter invocation imposter's AddReturnValue takes the generator, and Invoke loops over the callbacks, under
    // these names.
    internal const string GeneratorVariableName = "generator";

    internal const string CallbackVariableName = "callback";

    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly FieldMetadata DefaultBehaviourField;

    internal readonly FieldMetadata SetupsField;

    internal readonly FieldMetadata InvocationHistoryField;

    internal readonly FieldMetadata InvocationBehaviorField;

    internal readonly FieldMetadata PropertyDisplayNameField;

    internal readonly GetterBuilderMetadata Builder;

    internal readonly GetterInvocationMetadata Invocation;

    internal readonly string ArgumentsVariableName;

    internal readonly string SetupVariableName;

    internal readonly string CriteriaParameterName;

    internal readonly string CountParameterName;

    internal readonly string GetterSuffix;

    internal readonly ParameterMetadata BaseImplementationParameter;

    internal readonly TypeSyntax ReturnHandlerType;

    internal readonly string FindGetterInvocationImposterMethodName;

    internal IndexerGetterImposterMetadata(in ImposterIndexerMetadata indexer)
    {
        // The getter's Get takes the indexer's parameters and refers to these locals and members by name.
        var names = indexer.Core.CreateParameterNameSet();
        Name = "GetterImposter";
        TypeSyntax = IdentifierName(Name);
        ArgumentsVariableName = names.Use("arguments");
        SetupVariableName = names.Use("getterInvocationImposter");
        CriteriaParameterName = "criteria";
        CountParameterName = "count";
        GetterSuffix = " (getter)";
        BaseImplementationParameter = indexer.Core.GetterBaseImplementationParameter(
            names.Use(ImposterIndexerCoreMetadata.BaseImplementationParameterName)
        );
        FindGetterInvocationImposterMethodName = names.Use("FindGetterInvocationImposter");

        var returnGeneratorType = indexer.Core.HasGeneratedValueDelegates
            ? indexer.Delegates.ReturnGeneratorDelegateType
            : WellKnownTypes.System.Func(
                indexer.Arguments.TypeSyntax,
                indexer.Core.NullableAwareValueTypeSyntax
            );
        ReturnHandlerType = indexer.Core.HasGeneratedValueDelegates
            ? indexer.Delegates.ReturnHandlerDelegateType
            : WellKnownTypes.System.Func(
                indexer.Arguments.TypeSyntax,
                indexer.Core.BaseGetterType.ToNullableType(),
                indexer.Core.NullableAwareValueTypeSyntax
            );
        Invocation = new GetterInvocationMetadata(indexer, ReturnHandlerType);

        DefaultBehaviourField = new FieldMetadata(
            names.Use("_defaultBehaviour"),
            indexer.DefaultIndexerBehaviour.TypeSyntax
        );
        SetupsField = new FieldMetadata(
            names.Use("_getterInvocationImposters"),
            WellKnownTypes.Imposter.Abstractions.SetupCollection(Invocation.TypeSyntax)
        );
        InvocationHistoryField = new FieldMetadata(
            names.Use("_invocationHistory"),
            WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(
                indexer.Arguments.TypeSyntax
            )
        );
        InvocationBehaviorField = new FieldMetadata(
            names.Use("_invocationBehavior"),
            WellKnownTypes.Imposter.Abstractions.ImposterMode
        );
        PropertyDisplayNameField = new FieldMetadata(
            names.Use("_propertyDisplayName"),
            WellKnownTypes.String
        );
        Builder = new GetterBuilderMetadata(returnGeneratorType);
    }

    internal readonly struct GetterBuilderMetadata
    {
        internal readonly string Name;

        internal readonly string ImposterFieldName;

        internal readonly string CriteriaFieldName;

        internal readonly string InvocationImposterPropertyName;

        internal readonly TypeSyntax ReturnGeneratorType;

        internal GetterBuilderMetadata(TypeSyntax returnGeneratorType)
        {
            Name = "Builder";
            ImposterFieldName = "_imposter";
            CriteriaFieldName = "_criteria";
            InvocationImposterPropertyName = "InvocationImposter";
            ReturnGeneratorType = returnGeneratorType;
        }
    }

    internal readonly struct GetterInvocationMetadata
    {
        internal const string HasConfiguredReturnFieldName = "HasConfiguredReturn";

        internal const string BareMatchVariableName = "bareMatch";

        internal readonly string Name;

        internal readonly NameSyntax TypeSyntax;

        internal readonly string InvokeCallbacksMethodName;

        internal readonly FieldMetadata DefaultBehaviourField;

        internal readonly FieldMetadata ReturnValuesField;

        internal readonly FieldMetadata CallbacksField;

        internal readonly FieldMetadata LastReturnValueField;

        internal readonly FieldMetadata PropertyDisplayNameField;

        internal readonly FieldMetadata CriteriaField;

        internal readonly NextReturnValueMethodMetadata NextReturnValueMethod;

        internal GetterInvocationMetadata(
            in ImposterIndexerMetadata indexer,
            TypeSyntax returnHandlerType
        )
        {
            Name = "GetterInvocationImposter";
            TypeSyntax = IdentifierName(Name);
            InvokeCallbacksMethodName = "InvokeCallbacks";
            DefaultBehaviourField = new FieldMetadata(
                "_defaultBehaviour",
                indexer.DefaultIndexerBehaviour.TypeSyntax
            );
            ReturnValuesField = new FieldMetadata(
                "_returnValues",
                WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(returnHandlerType)
            );
            CallbacksField = new FieldMetadata(
                "_callbacks",
                WellKnownTypes.Imposter.Abstractions.CallbackCollection(
                    indexer.Delegates.GetterCallbackDelegateType
                )
            );
            LastReturnValueField = new FieldMetadata(
                "_lastReturnValue",
                NullableType(returnHandlerType)
            );
            PropertyDisplayNameField = new FieldMetadata(
                "_propertyDisplayName",
                WellKnownTypes.String
            );
            CriteriaField = new FieldMetadata("Criteria", indexer.ArgumentsCriteria.TypeSyntax);
            NextReturnValueMethod = new NextReturnValueMethodMetadata(LastReturnValueField.Type);
        }
    }
}
