using System.Collections.Generic;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Builders.IndexerImposterBuilderCommon;
using static Imposter.CodeGenerator.Features.Shared.Builders.MissingImposterBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

// The GetterImposter's nested GetterInvocationImposter class, which produces the getter's results for some criteria.
internal static partial class IndexerGetterBuilder
{
    private static ClassDeclarationSyntax BuildGetterInvocationImposter(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocation = indexer.GetterImplementation.Invocation;

        return new ClassDeclarationBuilder(invocation.Name)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.SealedKeyword))
            .AddMember(SinglePrivateReadonlyVariableField(invocation.ParentField))
            .AddMember(SinglePrivateReadonlyVariableField(invocation.DefaultBehaviourField))
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    invocation.ReturnValuesField,
                    invocation.ReturnValuesField.Type.New()
                )
            )
            .AddMember(
                SinglePrivateReadonlyVariableField(
                    invocation.CallbacksField,
                    invocation.CallbacksField.Type.New()
                )
            )
            .AddMember(
                SingleVariableField(
                    invocation.LastReturnValueField.Type,
                    invocation.LastReturnValueField.Name,
                    TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.VolatileKeyword))
                )
            )
            .AddMember(
                SingleVariableField(invocation.PropertyDisplayNameField, SyntaxKind.PrivateKeyword)
            )
            .AddMember(BuildGetterInvocationCriteriaProperty(invocation))
            .AddMember(BuildGetterInvocationConstructor(indexer))
            .AddMember(BuildGetterInvocationAddReturnValueMethod(indexer))
            .AddMember(BuildGetterInvocationAddCallbackMethod(indexer))
            .AddMember(BuildGetterInvocationInvokeMethod(indexer))
            .AddMember(BuildGetterInvocationResolveNextGeneratorMethod(indexer))
            .AddMember(BuildGetterInvocationNextReturnValueMethod(invocation))
            .AddMember(
                indexer.GetterBuilderInterface.UseBaseImplementationMethod is not null
                    ? BuildGetterInvocationUseBaseImplementationMethod(indexer)
                    : null
            )
            .Build();
    }

    private static PropertyDeclarationSyntax BuildGetterInvocationCriteriaProperty(
        in IndexerGetterImposterMetadata.GetterInvocationMetadata invocation
    ) =>
        new PropertyDeclarationBuilder(invocation.CriteriaField.Type, invocation.CriteriaField.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .Build()
            .WithAccessorList(
                AccessorList(
                    List([
                        AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                        AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
                            .AddModifiers(Token(SyntaxKind.PrivateKeyword))
                            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                    ])
                )
            );

    private static ConstructorDeclarationSyntax BuildGetterInvocationConstructor(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var invocation = getter.Invocation;
        var parent = IdentifierName("parent");

        return new ConstructorBuilder(invocation.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(ParameterSyntax(getter.TypeSyntax, "parent"))
            .AddParameter(
                ParameterSyntax(
                    indexer.DefaultIndexerBehaviour.TypeSyntax,
                    DefaultBehaviourParameterName
                )
            )
            .AddParameter(
                ParameterSyntax(indexer.ArgumentsCriteria.TypeSyntax, getter.CriteriaParameterName)
            )
            .WithBody(
                new BlockBuilder()
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(invocation.ParentField.Name))
                            .Assign(parent)
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(invocation.DefaultBehaviourField.Name))
                            .Assign(IdentifierName(DefaultBehaviourParameterName))
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(invocation.PropertyDisplayNameField.Name))
                            .Assign(parent.Dot(IdentifierName("PropertyDisplayName")))
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(invocation.CriteriaField.Name))
                            .Assign(IdentifierName(getter.CriteriaParameterName))
                            .ToStatementSyntax()
                    )
                    .Build()
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationAddReturnValueMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var invocation = getter.Invocation;
        var generator = ParameterSyntax(
            getter.Builder.ReturnGeneratorType,
            IndexerGetterImposterMetadata.GeneratorVariableName
        );
        var handler = ReturnHandler(indexer)
            .WithExpressionBody(
                IdentifierName(generator.Identifier)
                    .Call(
                        ArgumentListSyntax(
                            HandlerArguments(indexer, indexer.GetterPassedThroughKeyNames)
                        )
                    )
            );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "AddReturnValue")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(generator)
            .WithBody(
                Block(
                    TurnDefaultBehaviourOff(indexer),
                    IdentifierName(invocation.ReturnValuesField.Name)
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(Argument(handler))
                        .ToStatementSyntax(),
                    IdentifierName(invocation.ParentField.Name)
                        .Dot(IdentifierName(getter.MarkReturnConfiguredMethod.Name))
                        .Call()
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationAddCallbackMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var callback = indexer.GetterBuilderInterface.CallbackMethod.CallbackParameter;

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "AddCallback")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(ParameterSyntax(callback))
            .WithBody(
                Block(
                    IdentifierName(indexer.GetterImplementation.Invocation.CallbacksField.Name)
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(Argument(IdentifierName(callback.Name)))
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationInvokeMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var arguments = IdentifierName(getter.ArgumentsVariableName);

        var invokeCallbacks = ForEachStatement(
            Var,
            Identifier(IndexerGetterImposterMetadata.CallbackVariableName),
            IdentifierName(getter.Invocation.CallbacksField.Name),
            Block(
                IdentifierName(IndexerGetterImposterMetadata.CallbackVariableName)
                    .Call(
                        BuildDelegateInvocationArguments(
                            arguments,
                            indexer,
                            indexer.GetterPassedThroughKeyNames
                        )
                    )
                    .ToStatementSyntax()
            )
        );

        var generatorDeclaration = LocalVariableDeclarationSyntax(
            getter.ReturnHandlerType,
            IndexerGetterImposterMetadata.GeneratorVariableName,
            IdentifierName("ResolveNextGenerator").Call(Argument(arguments))
        );

        var returnGenerated = ReturnStatement(
            IdentifierName(IndexerGetterImposterMetadata.GeneratorVariableName)
                .Call(
                    ArgumentListSyntax([
                        .. HandlerArguments(indexer, indexer.GetterPassedThroughKeyNames),
                        Argument(IdentifierName(getter.BaseImplementationParameter.Name)),
                    ])
                )
        );

        return new MethodDeclarationBuilder(indexer.Core.NullableAwareStoredTypeSyntax, "Invoke")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                ParameterSyntax(indexer.Arguments.TypeSyntax, getter.ArgumentsVariableName)
            )
            .AddParameters(PassedThroughKeyParameters(indexer, indexer.GetterPassedThroughKeyNames))
            .AddParameter(ParameterSyntax(getter.BaseImplementationParameter))
            .WithBody(Block(invokeCallbacks, generatorDeclaration, returnGenerated))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetterInvocationResolveNextGeneratorMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var invocation = getter.Invocation;
        var nextReturnValue = IdentifierName("nextReturnValue");

        var returnDefaultBehaviourWhileOn = IfStatement(
            IdentifierName(invocation.DefaultBehaviourField.Name)
                .Dot(IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName)),
            Block(ReturnStatement(DefaultBehaviourHandler(indexer)))
        );

        var declareNextReturnValue = LocalVariableDeclarationSyntax(
            Var,
            nextReturnValue.Identifier.Text,
            IdentifierName(invocation.NextReturnValueMethod.Name).Call()
        );

        var throwIfMissing = IfStatement(
            nextReturnValue.IsNull(),
            Block(
                ThrowMissingImposter(invocation.PropertyDisplayNameField.Name, getter.GetterSuffix)
            )
        );

        var returnNext = ReturnStatement(
            PostfixUnaryExpression(SyntaxKind.SuppressNullableWarningExpression, nextReturnValue)
        );

        return new MethodDeclarationBuilder(getter.ReturnHandlerType, "ResolveNextGenerator")
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddParameter(
                ParameterSyntax(indexer.Arguments.TypeSyntax, getter.ArgumentsVariableName)
            )
            .WithBody(
                Block(
                    returnDefaultBehaviourWhileOn,
                    declareNextReturnValue,
                    throwIfMissing,
                    returnNext
                )
            )
            .Build();
    }

    // A handler that gets the value from the default behaviour.
    private static ParenthesizedLambdaExpressionSyntax DefaultBehaviourHandler(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;

        return ReturnHandler(indexer)
            .WithExpressionBody(
                IdentifierName(getter.Invocation.DefaultBehaviourField.Name)
                    .Dot(IdentifierName("Get"))
                    .Call(
                        ArgumentListSyntax([
                            .. HandlerArguments(indexer, indexer.GetterPassedThroughKeyNames),
                            Argument(IdentifierName(getter.BaseImplementationParameter.Name)),
                        ])
                    )
            );
    }

    private static MethodDeclarationSyntax BuildGetterInvocationNextReturnValueMethod(
        in IndexerGetterImposterMetadata.GetterInvocationMetadata invocation
    ) =>
        new MethodDeclarationBuilder(
            invocation.NextReturnValueMethod.ReturnType,
            invocation.NextReturnValueMethod.Name
        )
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                NextOutcomeSyntaxHelper.TakeNextOutcomeBody(
                    IdentifierName(invocation.ReturnValuesField.Name),
                    IdentifierName(invocation.LastReturnValueField.Name),
                    IdentifierName("returnValue")
                )
            )
            .Build();

    private static MethodDeclarationSyntax BuildGetterInvocationUseBaseImplementationMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var invocation = indexer.GetterImplementation.Invocation;

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "UseBaseImplementation")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    IdentifierName(invocation.ParentField.Name)
                        .Dot(
                            IdentifierName(
                                indexer.GetterImplementation.MarkReturnConfiguredMethod.Name
                            )
                        )
                        .Call()
                        .ToStatementSyntax(),
                    TurnDefaultBehaviourOff(indexer),
                    IdentifierName(invocation.ReturnValuesField.Name)
                        .Dot(ConcurrentQueueSyntaxHelper.Enqueue)
                        .Call(Argument(BaseImplementationHandler(indexer)))
                        .ToStatementSyntax(),
                    IdentifierName(invocation.LastReturnValueField.Name)
                        .Assign(Null)
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    // A handler that calls the base implementation with the keys passed through, and throws a missing imposter when
    // there's none.
    private static ParenthesizedLambdaExpressionSyntax BaseImplementationHandler(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var baseImplementation = IdentifierName(getter.BaseImplementationParameter.Name);

        return ReturnHandler(indexer)
            .WithBlock(
                Block(
                    IfStatement(
                        baseImplementation.IsNull(),
                        Block(
                            ThrowMissingImposter(
                                getter.Invocation.PropertyDisplayNameField.Name,
                                getter.GetterSuffix
                            )
                        )
                    ),
                    ReturnStatement(
                        baseImplementation.Call(
                            ArgumentListSyntax(
                                PassedThroughKeyArguments(indexer.GetterPassedThroughKeyNames)
                            )
                        )
                    )
                )
            );
    }

    // A lambda with the return handlers' (arguments, the keys passed through, baseImplementation) parameters.
    private static ParenthesizedLambdaExpressionSyntax ReturnHandler(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;

        return ParenthesizedLambdaExpression()
            .WithParameterList(
                ParameterList(
                    SeparatedList([
                        Parameter(Identifier(getter.ArgumentsVariableName)),
                        .. PassedThroughKeyLambdaParameters(indexer.GetterPassedThroughKeyNames),
                        Parameter(Identifier(getter.BaseImplementationParameter.Name)),
                    ])
                )
            );
    }

    // The arguments a return handler passes on: the arguments class, and the keys passed through.
    private static IEnumerable<ArgumentSyntax> HandlerArguments(
        in ImposterIndexerMetadata indexer,
        IReadOnlyList<string> passedThroughKeyNames
    ) =>
        [
            Argument(IdentifierName(indexer.GetterImplementation.ArgumentsVariableName)),
            .. PassedThroughKeyArguments(passedThroughKeyNames),
        ];

    private static ExpressionStatementSyntax TurnDefaultBehaviourOff(
        in ImposterIndexerMetadata indexer
    ) =>
        IdentifierName(indexer.GetterImplementation.Invocation.DefaultBehaviourField.Name)
            .Dot(IdentifierName(indexer.DefaultIndexerBehaviour.IsOnPropertyName))
            .Assign(False)
            .ToStatementSyntax();
}
