using System.Collections.Generic;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Shared.Builders;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Builders.IndexerImposterBuilderCommon;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.IndexerGetterImposterMetadata.GetterInvocationMetadata;
using static Imposter.CodeGenerator.Features.Shared.Builders.MissingImposterBuilder;
using static Imposter.CodeGenerator.Features.Shared.Builders.VerificationFailedBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Imposter.CodeGenerator.SyntaxHelpers.VolatileSyntaxHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

// The getter's GetterImposter class. Its nested Builder and GetterInvocationImposter classes are built in
// IndexerGetterBuilder.Builder.cs and IndexerGetterBuilder.InvocationImposter.cs.
internal static partial class IndexerGetterBuilder
{
    internal static MethodDeclarationSyntax BuildGetForwarder(in ImposterIndexerMetadata indexer)
    {
        var parameters = new List<ParameterSyntax>(indexer.Core.ParameterSyntaxes);

        ParameterSyntax? getterBaseImplementationParameter = null;
        if (indexer.Core.GetterSupportsBaseImplementation)
        {
            getterBaseImplementationParameter = ParameterSyntax(
                indexer.GetterImplementation.BaseImplementationParameter
            );

            parameters.Add(getterBaseImplementationParameter);
        }

        var invocationArguments = new List<ArgumentSyntax>(indexer.Core.ParameterArguments);

        if (getterBaseImplementationParameter is not null)
        {
            invocationArguments.Add(
                Argument(IdentifierName(getterBaseImplementationParameter.Identifier))
            );
        }

        return new MethodDeclarationBuilder(
            indexer.Core.NullableAwareValueTypeSyntax,
            IndexerImposterBuilderMetadata.GetMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters(parameters.ToArray())
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(indexer.Builder.GetterImposterField.Name)
                            .Dot(IdentifierName(IndexerGetterImposterMetadata.GetMethodName))
                            .Call(ArgumentListSyntax(invocationArguments))
                    )
                )
            )
            .Build();
    }

    internal static ClassDeclarationSyntax BuildGetterImposter(in ImposterIndexerMetadata indexer)
    {
        var getter = indexer.GetterImplementation;

        return new ClassDeclarationBuilder(getter.Name)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddMember(
                InvocationLifecycleBuilder.Clear([
                    InvocationLifecycleBuilder.ClearIfInitialized(
                        getter.InvocationHistoryField.Name
                    ),
                ])
            )
            .AddMember(SinglePrivateReadonlyVariableField(getter.DefaultBehaviourField))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    getter.SetupsField,
                    getter.SetupsField.Type.New()
                )
            )
            .AddMember(LazyCollectionSyntaxHelper.Field(getter.InvocationHistoryField))
            .AddMember(SinglePrivateReadonlyVariableField(getter.InvocationBehaviorField))
            .AddMember(SinglePrivateReadonlyVariableField(getter.PropertyDisplayNameField))
            .AddMember(BuildGetterConstructor(indexer))
            .AddMember(BuildGetterGetMethod(indexer))
            .AddMember(IndexerHistoryBuilder.Snapshot(indexer, true))
            .AddMember(IndexerHistoryBuilder.Matching(indexer, true))
            .AddMember(BuildFindGetterInvocationImposterMethod(indexer))
            .AddMember(BuildGetOrCreateMethod(indexer))
            .AddMember(BuildGetterCalledMethod(indexer))
            .AddMember(BuildPropertyDisplayNameProperty(getter))
            .AddMember(BuildGetterBuilder(indexer))
            .AddMember(BuildGetterInvocationImposter(indexer))
            .Build();
    }

    private static ConstructorDeclarationSyntax BuildGetterConstructor(
        in ImposterIndexerMetadata indexer
    ) =>
        BuildImposterConstructor(
            indexer.GetterImplementation.Name,
            indexer.GetterImplementation.DefaultBehaviourField,
            indexer.GetterImplementation.InvocationBehaviorField.Name,
            indexer.GetterImplementation.PropertyDisplayNameField.Name
        );

    private static MethodDeclarationSyntax BuildGetterGetMethod(in ImposterIndexerMetadata indexer)
    {
        var getter = indexer.GetterImplementation;
        var arguments = IdentifierName(getter.ArgumentsVariableName);
        var setup = IdentifierName(getter.SetupVariableName);
        var findSetup = LocalVariableDeclarationSyntax(
            Var,
            getter.SetupVariableName,
            IdentifierName(getter.FindGetterInvocationImposterMethodName).Call(Argument(arguments))
        );
        var invokeSetup = IndexerHistoryBuilder.Return(
            indexer,
            setup
                .Dot(IdentifierName(InvokeMethodName))
                .Call(
                    ArgumentListSyntax([
                        .. HandlerArguments(indexer, indexer.Core.PassedThroughKeyNames),
                        Argument(IdentifierName(getter.BaseImplementationParameter.Name)),
                    ])
                )
        );

        return new MethodDeclarationBuilder(
            indexer.Core.NullableAwareValueTypeSyntax,
            IndexerGetterImposterMetadata.GetMethodName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters([
                .. indexer.Core.ParameterSyntaxes,
                ParameterSyntax(getter.BaseImplementationParameter),
            ])
            .WithBody(
                IndexerHistoryBuilder.BuildBody(
                    indexer,
                    true,
                    Block(
                        List<StatementSyntax>([
                            findSetup,
                            IfStatement(
                                setup
                                    .IsNotNull()
                                    .And(setup.Dot(IdentifierName(HasConfiguredReturnFieldName))),
                                invokeSetup
                            ),
                            .. WithoutConfiguredReturn(indexer),
                        ])
                    )
                )
            )
            .Build();
    }

    // Without a configured return, a getter throws in explicit mode, as a method or a property getter does, before
    // any callback runs. Otherwise a callback-only setup that matched runs its callbacks, and the getter returns the
    // default behaviour's value while that's on and the default value after.
    private static IEnumerable<StatementSyntax> WithoutConfiguredReturn(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var setup = IdentifierName(getter.SetupVariableName);
        var defaultBehaviour = IdentifierName(getter.DefaultBehaviourField.Name);

        return
        [
            IfStatement(
                IsExplicit(IdentifierName(getter.InvocationBehaviorField.Name)),
                Block(
                    ThrowStatement(
                        WellKnownTypes.Imposter.Abstractions.MissingImposterException.New(
                            IdentifierName(getter.PropertyDisplayNameField.Name)
                                .Add(getter.GetterSuffix.StringLiteral())
                                .ToSingleArgumentList()
                        )
                    )
                )
            ),
            IfStatement(
                setup.IsNotNull(),
                setup
                    .Dot(IdentifierName(getter.Invocation.InvokeCallbacksMethodName))
                    .Call(
                        ArgumentListSyntax(
                            HandlerArguments(indexer, indexer.Core.PassedThroughKeyNames)
                        )
                    )
                    .ToStatementSyntax()
            ),
            IfStatement(
                defaultBehaviour.Dot(
                    IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName)
                ),
                IndexerHistoryBuilder.Return(
                    indexer,
                    defaultBehaviour
                        .Dot(IdentifierName(DefaultIndexerBehaviourMetadata.GetMethodName))
                        .Call(
                            ArgumentListSyntax([
                                .. HandlerArguments(indexer, indexer.Core.PassedThroughKeyNames),
                                Argument(IdentifierName(getter.BaseImplementationParameter.Name)),
                            ])
                        )
                )
            ),
            ReturnDefaultNonNullable,
        ];
    }

    // The newest matching setup with a configured return, or else the first matching callback-only setup, which
    // doesn't answer the call but still runs its callbacks.
    private static MethodDeclarationSyntax BuildFindGetterInvocationImposterMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var setup = IdentifierName(getter.SetupVariableName);
        var bareMatch = IdentifierName(
            IndexerGetterImposterMetadata.GetterInvocationMetadata.BareMatchVariableName
        );
        var setupMatches = setup
            .Dot(IdentifierName(getter.Invocation.CriteriaField.Name))
            .Dot(IdentifierName(IndexerArgumentsCriteriaMetadata.MatchesMethodName))
            .Call(Argument(IdentifierName(getter.ArgumentsVariableName)));

        return new MethodDeclarationBuilder(
            NullableType(getter.Invocation.TypeSyntax),
            getter.FindGetterInvocationImposterMethodName
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                ParameterSyntax(indexer.Arguments.TypeSyntax, getter.ArgumentsVariableName)
            )
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        NullableType(getter.Invocation.TypeSyntax),
                        bareMatch.Identifier.Text,
                        Null
                    ),
                    ForEachStatement(
                        Var,
                        Identifier(getter.SetupVariableName),
                        IdentifierName(getter.SetupsField.Name),
                        Block(
                            IfStatement(
                                setupMatches,
                                Block(
                                    IfStatement(
                                        setup.Dot(IdentifierName(HasConfiguredReturnFieldName)),
                                        ReturnStatement(setup)
                                    ),
                                    IfStatement(
                                        bareMatch.IsNull(),
                                        bareMatch.Assign(setup).ToStatementSyntax()
                                    )
                                )
                            )
                        )
                    ),
                    ReturnStatement(bareMatch)
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetOrCreateMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var invocationType = getter.Invocation.TypeSyntax;
        var criteria = IdentifierName(getter.CriteriaParameterName);
        var criteriaSetup = criteria.Dot(
            IdentifierName(indexer.ArgumentsCriteria.GetterSetupFieldName)
        );
        var setup = IdentifierName(getter.SetupVariableName);

        return new MethodDeclarationBuilder(
            invocationType,
            IndexerGetterImposterMetadata.GetOrCreateMethodName
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                ParameterSyntax(indexer.ArgumentsCriteria.TypeSyntax, criteria.Identifier.Text)
            )
            .WithBody(
                Block(
                    ReturnIfSetUp(VolatileRead(criteriaSetup), invocationType, "existing"),
                    LockStatement(
                        criteria,
                        Block(
                            ReturnIfSetUp(criteriaSetup, invocationType, "current"),
                            LocalVariableDeclarationSyntax(
                                invocationType,
                                setup.Identifier.Text,
                                invocationType.New(
                                    ArgumentListSyntax([
                                        Argument(ThisExpression()),
                                        Argument(IdentifierName(getter.DefaultBehaviourField.Name)),
                                        Argument(criteria),
                                    ])
                                )
                            ),
                            IdentifierName(getter.SetupsField.Name)
                                .Dot(SetupCollectionSyntaxHelper.Push)
                                .Call(Argument(setup))
                                .ToStatementSyntax(),
                            VolatileWrite(criteriaSetup, setup).ToStatementSyntax(),
                            ReturnStatement(setup)
                        )
                    )
                )
            )
            .Build();
    }

    // Criteria are compared by reference, so their setup is kept on them. It is created under a lock on the criteria
    // and published only after it has been added to the setups, so a setup another thread reads is already matchable.
    private static IfStatementSyntax ReturnIfSetUp(
        ExpressionSyntax criteriaSetup,
        TypeSyntax invocationType,
        string setupName
    ) =>
        IfStatement(
            IsPatternExpression(
                criteriaSetup,
                DeclarationPattern(invocationType, SingleVariableDesignation(Identifier(setupName)))
            ),
            ReturnStatement(IdentifierName(setupName))
        );

    private static MethodDeclarationSyntax BuildGetterCalledMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var invocationHistory = IdentifierName(getter.InvocationHistoryField.Name);
        var countParameter = indexer.GetterBuilderInterface.CalledMethod.CountParameter;
        var count = IdentifierName(countParameter.Name);
        var invocationCount = IdentifierName(indexer.Locals["invocationCount"]);

        var invocationCountDeclaration = LocalVariableDeclarationSyntax(
            WellKnownTypes.Int,
            invocationCount.Identifier.Text,
            NumericLiteral(0)
        );
        var countEntry = IdentifierName(indexer.Locals["entry"]);
        var countLoop = LazyCollectionSyntaxHelper.UnlessNull(
            ForEachStatement(
                Var,
                countEntry.Identifier,
                invocationHistory,
                Block(
                    IfStatement(
                        IdentifierName(getter.CriteriaParameterName)
                            .Dot(IdentifierName(IndexerArgumentsCriteriaMetadata.MatchesMethodName))
                            .Call(Argument(countEntry.Dot(IdentifierName("Arguments")))),
                        PostfixUnaryExpression(SyntaxKind.PostIncrementExpression, invocationCount)
                            .ToStatementSyntax()
                    )
                )
            )
        );

        var entry = IdentifierName(indexer.Locals["entry"]);
        var entryDescription = "get "
            .StringLiteral()
            .Add(IdentifierName(getter.PropertyDisplayNameField.Name))
            .Add(BuildIndices(indexer, entry.Dot(IdentifierName("Arguments"))));

        return new MethodDeclarationBuilder(
            WellKnownTypes.Void,
            IndexerGetterImposterMetadata.CalledMethodName
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                ParameterSyntax(indexer.ArgumentsCriteria.TypeSyntax, getter.CriteriaParameterName)
            )
            .AddParameter(ParameterSyntax(countParameter))
            .WithBody(
                Block(
                    invocationCountDeclaration,
                    countLoop,
                    ThrowIfCountDoesNotMatch(
                        count,
                        invocationCount,
                        new PerformedInvocations(
                            invocationHistory,
                            entry,
                            entryDescription,
                            IsHistoryNullable: true
                        )
                    )
                )
            )
            .Build();
    }

    private static PropertyDeclarationSyntax BuildPropertyDisplayNameProperty(
        in IndexerGetterImposterMetadata getter
    ) =>
        new PropertyDeclarationBuilder(
            WellKnownTypes.String,
            IndexerGetterImposterMetadata.PropertyDisplayNamePropertyName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .Build()
            .WithAccessorList(null)
            .WithExpressionBody(
                ArrowExpressionClause(IdentifierName(getter.PropertyDisplayNameField.Name))
            )
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
}
