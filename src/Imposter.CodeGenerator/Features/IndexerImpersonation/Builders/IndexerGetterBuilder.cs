using System.Collections.Generic;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Builders.IndexerImposterBuilderCommon;
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

        if (
            indexer.Core.GetterSupportsBaseImplementation
            && getterBaseImplementationParameter is not null
        )
        {
            invocationArguments.Add(
                Argument(IdentifierName(getterBaseImplementationParameter.Identifier))
            );
        }

        return new MethodDeclarationBuilder(indexer.Core.NullableAwareValueTypeSyntax, "Get")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters(parameters.ToArray())
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(indexer.Builder.GetterImposterField.Name)
                            .Dot(IdentifierName("Get"))
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
            .AddMember(SinglePrivateReadonlyVariableField(getter.DefaultBehaviourField))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    getter.SetupsField,
                    getter.SetupsField.Type.New()
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    getter.InvocationHistoryField,
                    getter.InvocationHistoryField.Type.New()
                )
            )
            .AddMember(SinglePrivateReadonlyVariableField(getter.InvocationBehaviorField))
            .AddMember(SinglePrivateReadonlyVariableField(getter.PropertyDisplayNameField))
            .AddMember(
                SingleVariableField(getter.HasConfiguredReturnField, SyntaxKind.PrivateKeyword)
            )
            .AddMember(BuildGetterConstructor(indexer))
            .AddMember(BuildGetterGetMethod(indexer))
            .AddMember(BuildFindGetterInvocationImposterMethod(indexer))
            .AddMember(BuildGetOrCreateMethod(indexer))
            .AddMember(BuildGetterCalledMethod(indexer))
            .AddMember(BuildPropertyDisplayNameProperty(getter))
            .AddMember(BuildMarkReturnConfiguredMethod(getter))
            .AddMember(BuildEnsureGetterConfiguredMethod(getter))
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
        var invokeSetup = ReturnStatement(
            setup
                .Dot(IdentifierName("Invoke"))
                .Call(
                    ArgumentListSyntax([
                        .. HandlerArguments(indexer, indexer.Core.PassedThroughKeyNames),
                        Argument(IdentifierName(getter.BaseImplementationParameter.Name)),
                    ])
                )
        );
        var recordInvocation = FinallyClause(
            Block(
                IdentifierName(getter.InvocationHistoryField.Name)
                    .Dot(ConcurrentStackSyntaxHelper.Push)
                    .Call(Argument(arguments))
                    .ToStatementSyntax()
            )
        );

        return new MethodDeclarationBuilder(indexer.Core.NullableAwareValueTypeSyntax, "Get")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters([
                .. indexer.Core.ParameterSyntaxes,
                ParameterSyntax(getter.BaseImplementationParameter),
            ])
            .WithBody(
                Block(
                    CreateArgumentsDeclaration(indexer, getter.ArgumentsVariableName),
                    TryStatement(
                        Block(
                            findSetup,
                            IfStatement(
                                IsPatternExpression(setup, ConstantPattern(Null)),
                                WithoutMatchingSetup(indexer)
                            ),
                            invokeSetup
                        ),
                        default,
                        recordInvocation
                    )
                )
            )
            .Build();
    }

    // Without a matching setup, a getter returns the default behaviour's value while that's on, throws in explicit
    // mode and returns the default value otherwise.
    private static BlockSyntax WithoutMatchingSetup(in ImposterIndexerMetadata indexer)
    {
        var getter = indexer.GetterImplementation;
        var defaultBehaviour = IdentifierName(getter.DefaultBehaviourField.Name);

        return Block(
            IdentifierName(getter.EnsureGetterConfiguredMethodName).Call().ToStatementSyntax(),
            IfStatement(
                defaultBehaviour.Dot(
                    IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName)
                ),
                ReturnStatement(
                    defaultBehaviour
                        .Dot(IdentifierName("Get"))
                        .Call(
                            ArgumentListSyntax([
                                .. HandlerArguments(indexer, indexer.Core.PassedThroughKeyNames),
                                Argument(IdentifierName(getter.BaseImplementationParameter.Name)),
                            ])
                        )
                )
            ),
            IfStatement(
                IsExplicit(IdentifierName(getter.InvocationBehaviorField.Name)),
                Block(
                    ThrowMissingImposter(getter.PropertyDisplayNameField.Name, getter.GetterSuffix)
                ),
                ElseClause(Block(ReturnDefaultNonNullable))
            )
        );
    }

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
            .Dot(IdentifierName("Matches"))
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
                                        setup.Dot(
                                            IdentifierName(
                                                IndexerGetterImposterMetadata
                                                    .GetterInvocationMetadata
                                                    .HasConfiguredReturnFieldName
                                            )
                                        ),
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
        var existing = Identifier("existing");
        var winner = Identifier("winner");

        return new MethodDeclarationBuilder(invocationType, "GetOrCreate")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                ParameterSyntax(indexer.ArgumentsCriteria.TypeSyntax, criteria.Identifier.Text)
            )
            .WithBody(
                Block(
                    ReturnIfSetUp(VolatileRead(criteriaSetup), invocationType, existing),
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
                    ReturnIfSetUp(
                        WellKnownTypes
                            .System.Threading.Interlocked.Dot(IdentifierName("CompareExchange"))
                            .Call(
                                ArgumentListSyntax([
                                    RefArgument(criteriaSetup),
                                    Argument(setup),
                                    Argument(Null),
                                ])
                            ),
                        invocationType,
                        winner
                    ),
                    IdentifierName(getter.SetupsField.Name)
                        .Dot(SetupCollectionSyntaxHelper.Push)
                        .Call(Argument(setup))
                        .ToStatementSyntax(),
                    ReturnStatement(setup)
                )
            )
            .Build();
    }

    // Criteria are compared by reference, so their setup is kept on them and published with a compare-and-swap: a
    // thread that loses the race returns the winner's setup, and only the winner's is added to the setups.
    private static IfStatementSyntax ReturnIfSetUp(
        ExpressionSyntax criteriaSetup,
        TypeSyntax invocationType,
        SyntaxToken setUp
    ) =>
        IfStatement(
            IsPatternExpression(
                criteriaSetup,
                DeclarationPattern(invocationType, SingleVariableDesignation(setUp))
            ),
            ReturnStatement(IdentifierName(setUp))
        );

    private static MethodDeclarationSyntax BuildGetterCalledMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var invocationHistory = IdentifierName(getter.InvocationHistoryField.Name);
        var count = IdentifierName(getter.CountParameterName);
        var invocationCount = IdentifierName("invocationCount");

        var invocationCountDeclaration = LocalVariableDeclarationSyntax(
            WellKnownTypes.Int,
            invocationCount.Identifier.Text,
            invocationHistory
                .Dot(IdentifierName("Count"))
                .Call(
                    Argument(
                        IdentifierName(getter.CriteriaParameterName).Dot(IdentifierName("Matches"))
                    )
                )
        );

        var entry = IdentifierName("entry");
        var entryDescription = "get "
            .StringLiteral()
            .Add(IdentifierName(getter.PropertyDisplayNameField.Name))
            .Add(BuildIndices(indexer, entry));

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Called")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                ParameterSyntax(indexer.ArgumentsCriteria.TypeSyntax, getter.CriteriaParameterName)
            )
            .AddParameter(
                ParameterSyntax(
                    WellKnownTypes.Imposter.Abstractions.Count,
                    getter.CountParameterName
                )
            )
            .WithBody(
                Block(
                    invocationCountDeclaration,
                    ThrowIfCountDoesNotMatch(
                        count,
                        invocationCount,
                        new PerformedInvocations(invocationHistory, entry, entryDescription)
                    )
                )
            )
            .Build();
    }

    private static PropertyDeclarationSyntax BuildPropertyDisplayNameProperty(
        in IndexerGetterImposterMetadata getter
    ) =>
        new PropertyDeclarationBuilder(WellKnownTypes.String, "PropertyDisplayName")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .Build()
            .WithAccessorList(null)
            .WithExpressionBody(
                ArrowExpressionClause(IdentifierName(getter.PropertyDisplayNameField.Name))
            )
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

    private static MethodDeclarationSyntax BuildMarkReturnConfiguredMethod(
        in IndexerGetterImposterMetadata getter
    ) =>
        BuildMarkConfiguredMethod(
            getter.MarkReturnConfiguredMethod.Name,
            getter.HasConfiguredReturnField.Name
        );

    private static MethodDeclarationSyntax BuildEnsureGetterConfiguredMethod(
        in IndexerGetterImposterMetadata getter
    ) =>
        EnsureConfiguredMethod(
            getter.EnsureGetterConfiguredMethodName,
            getter.InvocationBehaviorField.Name,
            VolatileRead(getter.HasConfiguredReturnField.Name),
            Block(ThrowMissingImposter(getter.PropertyDisplayNameField.Name, getter.GetterSuffix))
        );
}
