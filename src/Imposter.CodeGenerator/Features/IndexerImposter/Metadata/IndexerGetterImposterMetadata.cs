using Microsoft.CodeAnalysis;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata;

internal readonly struct IndexerGetterImposterMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly string InvocationImposterName;

    internal readonly NameSyntax InvocationImposterTypeSyntax;

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

    internal IndexerGetterImposterMetadata(in ImposterIndexerMetadata indexer)
    {
        Name = "GetterImposter";
        TypeSyntax = IdentifierName(Name);
        InvocationImposterName = "GetterInvocationImposter";
        InvocationImposterTypeSyntax = IdentifierName(InvocationImposterName);
        ArgumentsVariableName = "arguments";
        SetupVariableName = "setup";
        CriteriaParameterName = "criteria";
        CountParameterName = "count";
        GetterSuffix = " (getter)";

        var returnGeneratorType = BuildReturnGeneratorType(indexer);

        DefaultBehaviourField = new FieldMetadata("_defaultBehaviour", indexer.DefaultIndexerBehaviour.TypeSyntax);
        SetupsField = new FieldMetadata(
            "_setups",
            WellKnownTypes.System.Collections.Concurrent.ConcurrentStack(InvocationImposterTypeSyntax));
        SetupLookupField = new FieldMetadata(
            "_setupLookup",
            WellKnownTypes.System.Collections.Concurrent.ConcurrentDictionary(
                indexer.ArgumentsCriteria.TypeSyntax,
                InvocationImposterTypeSyntax));
        InvocationHistoryField = new FieldMetadata(
            "_invocationHistory",
            WellKnownTypes.System.Collections.Concurrent.ConcurrentBag(indexer.Arguments.TypeSyntax));
        InvocationBehaviorField = new FieldMetadata(
            "_invocationBehavior",
            WellKnownTypes.Imposter.Abstractions.ImposterInvocationBehavior);
        PropertyDisplayNameField = new FieldMetadata(
            "_propertyDisplayName",
            PredefinedType(Token(SyntaxKind.StringKeyword)));
        HasConfiguredReturnField = new FieldMetadata("_hasConfiguredReturn", WellKnownTypes.Bool);

        Builder = new GetterBuilderMetadata(returnGeneratorType);
        Invocation = new GetterInvocationMetadata(indexer, returnGeneratorType);
    }

    private static QualifiedNameSyntax BuildReturnGeneratorType(in ImposterIndexerMetadata indexer)
        => QualifiedName(
            WellKnownTypes.System.Namespace,
            GenericName(
                Identifier("Func"),
                TypeArgumentList(
                    SeparatedList<TypeSyntax>(new SyntaxNodeOrToken[]
                    {
                        indexer.Arguments.TypeSyntax,
                        Token(SyntaxKind.CommaToken),
                        indexer.Core.TypeSyntax
                    }))));

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

        internal readonly FieldMetadata InvocationCountField;

        internal readonly FieldMetadata PropertyDisplayNameField;

        internal readonly FieldMetadata CriteriaField;

        internal GetterInvocationMetadata(in ImposterIndexerMetadata indexer, TypeSyntax returnGeneratorType)
        {
            Name = "GetterInvocationImposter";
            TypeSyntax = IdentifierName(Name);
            ParentField = new FieldMetadata("_parent", IdentifierName("GetterImposter"));
            DefaultBehaviourField = new FieldMetadata("_defaultBehaviour", indexer.DefaultIndexerBehaviour.TypeSyntax);
            ReturnValuesField = new FieldMetadata(
                "_returnValues",
                WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(returnGeneratorType));
            CallbacksField = new FieldMetadata(
                "_callbacks",
                WellKnownTypes.System.Collections.Concurrent.ConcurrentQueue(indexer.Delegates.GetterCallbackDelegateType));
            LastReturnValueField = new FieldMetadata("_lastReturnValue", NullableType(returnGeneratorType));
            InvocationCountField = new FieldMetadata("_invocationCount", PredefinedType(Token(SyntaxKind.IntKeyword)));
            PropertyDisplayNameField = new FieldMetadata(
                "_propertyDisplayName",
                PredefinedType(Token(SyntaxKind.StringKeyword)));
            CriteriaField = new FieldMetadata("Criteria", indexer.ArgumentsCriteria.TypeSyntax);
        }
    }
}
