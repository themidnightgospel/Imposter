using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;

internal readonly struct IndexerGetterImposterMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly FieldMetadata DefaultBehaviourField;

    internal readonly FieldMetadata SetupsField;

    internal readonly FieldMetadata SetupLookupField;

    internal readonly FieldMetadata InvocationHistoryField;

    internal readonly FieldMetadata InvocationBehaviorField;

    internal readonly FieldMetadata PropertyDisplayNameField;

    internal readonly FieldMetadata HasConfiguredReturnField;

    internal readonly GetterBuilderMetadata Builder;

    internal readonly GetterInvocationMetadata Invocation;

    internal readonly string ArgumentsVariableName;

    internal readonly string SetupVariableName;

    internal readonly string CriteriaParameterName;

    internal readonly string CountParameterName;

    internal readonly string GetterSuffix;

    internal readonly string BaseImplementationParameterName;

    internal readonly TypeSyntax ReturnHandlerType;

    internal readonly string FindGetterInvocationImposterMethodName;

    internal readonly string EnsureGetterConfiguredMethodName;

    internal readonly MethodMetadata MarkReturnConfiguredMethod;

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
        BaseImplementationParameterName = names.Use("baseImplementation");
        FindGetterInvocationImposterMethodName = names.Use("FindGetterInvocationImposter");
        EnsureGetterConfiguredMethodName = names.Use("EnsureGetterConfigured");

        var returnGeneratorType = BuildReturnGeneratorType(indexer);
        ReturnHandlerType = BuildReturnHandlerType(indexer);
        Invocation = new GetterInvocationMetadata(indexer, TypeSyntax, ReturnHandlerType);

        DefaultBehaviourField = new FieldMetadata(
            names.Use("_defaultBehaviour"),
            indexer.DefaultIndexerBehaviour.TypeSyntax
        );
        SetupsField = new FieldMetadata(
            names.Use("_getterInvocationImposters"),
            WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(Invocation.TypeSyntax)
        );
        SetupLookupField = new FieldMetadata(
            names.Use("_setupLookup"),
            WellKnownTypes.System.Collections.Concurrent.ConcurrentDictionary(
                indexer.ArgumentsCriteria.TypeSyntax,
                Invocation.TypeSyntax
            )
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
        HasConfiguredReturnField = new FieldMetadata(
            names.Use("_hasConfiguredReturn"),
            WellKnownTypes.Bool
        );

        Builder = new GetterBuilderMetadata(returnGeneratorType);
        MarkReturnConfiguredMethod = new MethodMetadata(
            "MarkReturnConfigured",
            WellKnownTypes.Void
        );
    }

    private static QualifiedNameSyntax BuildReturnGeneratorType(
        in ImposterIndexerMetadata indexer
    ) =>
        QualifiedName(
            WellKnownTypes.System.Namespace,
            GenericName(
                Identifier("Func"),
                TypeArgumentList(
                    SeparatedList<TypeSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            indexer.Arguments.TypeSyntax,
                            Token(SyntaxKind.CommaToken),
                            indexer.Core.NullableAwareStoredTypeSyntax,
                        }
                    )
                )
            )
        );

    private static QualifiedNameSyntax BuildReturnHandlerType(in ImposterIndexerMetadata indexer) =>
        QualifiedName(
            WellKnownTypes.System.Namespace,
            GenericName(
                Identifier("Func"),
                TypeArgumentList(
                    SeparatedList<TypeSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            indexer.Arguments.TypeSyntax,
                            Token(SyntaxKind.CommaToken),
                            indexer.Core.AsSystemFuncType.ToNullableType(),
                            Token(SyntaxKind.CommaToken),
                            indexer.Core.NullableAwareStoredTypeSyntax,
                        }
                    )
                )
            )
        );

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
        internal readonly string Name;

        internal readonly NameSyntax TypeSyntax;

        internal readonly FieldMetadata ParentField;

        internal readonly FieldMetadata DefaultBehaviourField;

        internal readonly FieldMetadata ReturnValuesField;

        internal readonly FieldMetadata CallbacksField;

        internal readonly FieldMetadata LastReturnValueField;

        internal readonly FieldMetadata PropertyDisplayNameField;

        internal readonly FieldMetadata CriteriaField;

        internal readonly NextReturnValueMethodMetadata NextReturnValueMethod;

        internal GetterInvocationMetadata(
            in ImposterIndexerMetadata indexer,
            NameSyntax getterImposterType,
            TypeSyntax returnHandlerType
        )
        {
            Name = "GetterInvocationImposter";
            TypeSyntax = IdentifierName(Name);
            ParentField = new FieldMetadata("_parent", getterImposterType);
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
                WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(
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
