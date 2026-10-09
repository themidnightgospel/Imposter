using System.Collections.Generic;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Builders.IndexerImposterBuilderCommon;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
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
                    indexer.Core.AsSystemFuncType.ToNullableType(),
                    indexer.GetterImplementation.BaseImplementationParameterName
                )
                .WithDefault(EqualsValueClause(Null));

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

        return new MethodDeclarationBuilder(indexer.Core.NullableAwareStoredTypeSyntax, "Get")
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

        return new ClassDeclarationBuilder("GetterImposter")
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
                    getter.SetupLookupField,
                    getter.SetupLookupField.Type.New()
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
            "GetterImposter",
            indexer.DefaultIndexerBehaviour.TypeSyntax,
            indexer.GetterImplementation.DefaultBehaviourField.Name,
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
                        Argument(arguments),
                        Argument(IdentifierName(getter.BaseImplementationParameterName)),
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

        return new MethodDeclarationBuilder(indexer.Core.NullableAwareStoredTypeSyntax, "Get")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameters([
                .. indexer.Core.ParameterSyntaxes,
                ParameterSyntax(
                        indexer.Core.AsSystemFuncType.ToNullableType(),
                        getter.BaseImplementationParameterName
                    )
                    .WithDefault(EqualsValueClause(Null)),
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
                                Argument(IdentifierName(getter.ArgumentsVariableName)),
                                Argument(IdentifierName(getter.BaseImplementationParameterName)),
                            ])
                        )
                )
            ),
            IfStatement(
                BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    IdentifierName(getter.InvocationBehaviorField.Name),
                    WellKnownTypes.Imposter.Abstractions.ImposterMode.Dot(
                        IdentifierName("Explicit")
                    )
                ),
                Block(
                    BuildMissingImposterThrow(
                        getter.PropertyDisplayNameField.Name,
                        getter.GetterSuffix
                    )
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
        var setupMatches = setup
            .Dot(IdentifierName("Criteria"))
            .Dot(IdentifierName("Matches"))
            .Call(
                ArgumentList(
                    SingletonSeparatedList(Argument(IdentifierName(getter.ArgumentsVariableName)))
                )
            );

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
                    ForEachStatement(
                        Var,
                        Identifier(getter.SetupVariableName),
                        IdentifierName(getter.SetupsField.Name),
                        Block(IfStatement(setupMatches, Block(ReturnStatement(setup))))
                    ),
                    ReturnStatement(Null)
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetOrCreateMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;

        return new MethodDeclarationBuilder(getter.Invocation.TypeSyntax, "GetOrCreate")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                ParameterSyntax(indexer.ArgumentsCriteria.TypeSyntax, getter.CriteriaParameterName)
            )
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(getter.SetupLookupField.Name)
                            .Dot(IdentifierName("GetOrAdd"))
                            .Call(
                                ArgumentListSyntax([
                                    Argument(IdentifierName(getter.CriteriaParameterName)),
                                    Argument(IdentifierName("CreateSetup")),
                                ])
                            )
                    ),
                    BuildCreateSetupFunction(indexer)
                )
            )
            .Build();
    }

    // GetOrCreate's local function, which adds a getter invocation imposter for criteria not seen before.
    private static LocalFunctionStatementSyntax BuildCreateSetupFunction(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var invocationType = getter.Invocation.TypeSyntax;
        var setup = IdentifierName(getter.SetupVariableName);

        return LocalFunctionStatement(invocationType, Identifier("CreateSetup"))
            .AddParameterListParameters(
                ParameterSyntax(indexer.ArgumentsCriteria.TypeSyntax, "key")
            )
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(
                        invocationType,
                        getter.SetupVariableName,
                        invocationType.New(
                            ArgumentListSyntax([
                                Argument(ThisExpression()),
                                Argument(IdentifierName(getter.DefaultBehaviourField.Name)),
                                Argument(IdentifierName("key")),
                            ])
                        )
                    ),
                    IdentifierName(getter.SetupsField.Name)
                        .Dot(ConcurrentStackSyntaxHelper.Push)
                        .Call(Argument(setup))
                        .ToStatementSyntax(),
                    ReturnStatement(setup)
                )
            );
    }

    private static MethodDeclarationSyntax BuildGetterCalledMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var invocationHistory = IdentifierName(getter.InvocationHistoryField.Name);
        var count = IdentifierName(getter.CountParameterName);

        var invocationCountDeclaration = LocalVariableDeclarationSyntax(
            WellKnownTypes.Int,
            "invocationCount",
            invocationHistory
                .Dot(IdentifierName("Count"))
                .Call(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(
                                IdentifierName(getter.CriteriaParameterName)
                                    .Dot(IdentifierName("Matches"))
                            )
                        )
                    )
                )
        );

        var condition = Not(
            count
                .Dot(IdentifierName("Matches"))
                .Call(
                    ArgumentList(
                        SingletonSeparatedList(Argument(IdentifierName("invocationCount")))
                    )
                )
        );

        var descriptionExpression = "get "
            .StringLiteral()
            .Add(IdentifierName(getter.PropertyDisplayNameField.Name))
            .Add(BuildIndices(indexer, IdentifierName("entry")));

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
                    BuildCalledVerificationBlock(
                        condition,
                        invocationHistory,
                        count,
                        descriptionExpression
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
    ) => BuildMarkConfiguredMethod("MarkReturnConfigured", getter.HasConfiguredReturnField.Name);

    private static MethodDeclarationSyntax BuildEnsureGetterConfiguredMethod(
        in IndexerGetterImposterMetadata getter
    ) =>
        BuildEnsureConfiguredMethod(
            getter.EnsureGetterConfiguredMethodName,
            getter.InvocationBehaviorField.Name,
            getter.HasConfiguredReturnField.Name,
            getter.PropertyDisplayNameField.Name,
            getter.GetterSuffix
        );
}
