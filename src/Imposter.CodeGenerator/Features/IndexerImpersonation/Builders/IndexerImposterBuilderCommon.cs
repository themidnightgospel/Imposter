using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FormatValueMethodBuilder;
using static Imposter.CodeGenerator.Features.Shared.Builders.MissingImposterBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerImposterBuilderCommon
{
    internal const string DefaultBehaviourParameterName = "defaultBehaviour";
    internal const string InvocationBehaviorParameterName = "invocationBehavior";
    internal const string PropertyDisplayNameParameterName = "propertyDisplayName";

    internal static ArgumentListSyntax BuildIndexerArgumentsArgumentList(
        in ImposterIndexerMetadata indexer
    ) =>
        ArgumentList(
            SeparatedList(
                indexer.Core.Parameters.Select(parameter => parameter.ConstructorArgument)
            )
        );

    internal static LocalDeclarationStatementSyntax CreateArgumentsDeclaration(
        in ImposterIndexerMetadata indexer,
        string variableName
    ) =>
        LocalVariableDeclarationSyntax(
            indexer.Arguments.TypeSyntax,
            variableName,
            indexer.Arguments.TypeSyntax.New(BuildIndexerArgumentsArgumentList(indexer))
        );

    internal static ArgumentListSyntax BuildDelegateInvocationArguments(
        ExpressionSyntax source,
        in ImposterIndexerMetadata indexer
    )
    {
        var arguments = indexer.Core.Parameters.Select(parameter =>
            Argument(source.Dot(IdentifierName(parameter.FieldName)))
        );

        return ArgumentList(SeparatedList(arguments));
    }

    internal static ArgumentListSyntax BuildDelegateInvocationArgumentsWithValue(
        ExpressionSyntax source,
        in ImposterIndexerMetadata indexer,
        ExpressionSyntax valueExpression
    )
    {
        var arguments = BuildDelegateInvocationArguments(source, indexer)
            .Arguments.Add(Argument(valueExpression));

        return ArgumentList(arguments);
    }

    internal static ExpressionSyntax BuildIndices(
        in ImposterIndexerMetadata indexer,
        ExpressionSyntax source
    )
    {
        var formattedValues = IdentifierName("string")
            .Dot(IdentifierName("Join"))
            .Call(
                ArgumentList(
                    SeparatedList<ArgumentSyntax>(
                        new SyntaxNodeOrToken[]
                        {
                            Argument(", ".StringLiteral()),
                            Token(SyntaxKind.CommaToken),
                            Argument(
                                ImplicitArrayCreationExpression(
                                    InitializerExpression(
                                        SyntaxKind.ArrayInitializerExpression,
                                        SeparatedList(
                                            indexer.Core.Parameters.Select(
                                                ExpressionSyntax (parameter) =>
                                                    Invocation(
                                                        source.Dot(
                                                            IdentifierName(parameter.FieldName)
                                                        )
                                                    )
                                            )
                                        )
                                    )
                                )
                            ),
                        }
                    )
                )
            );

        return "[".StringLiteral().Add(formattedValues.Add("]".StringLiteral()));
    }

    internal static ConstructorDeclarationSyntax BuildImposterConstructor(
        string className,
        TypeSyntax defaultBehaviourType,
        string defaultBehaviourFieldName,
        string invocationBehaviorFieldName,
        string propertyDisplayNameFieldName
    ) =>
        new ConstructorBuilder(className)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .AddParameter(ParameterSyntax(defaultBehaviourType, DefaultBehaviourParameterName))
            .AddParameter(
                ParameterSyntax(
                    WellKnownTypes.Imposter.Abstractions.ImposterMode,
                    InvocationBehaviorParameterName
                )
            )
            .AddParameter(ParameterSyntax(WellKnownTypes.String, PropertyDisplayNameParameterName))
            .WithBody(
                new BlockBuilder()
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(defaultBehaviourFieldName))
                            .Assign(IdentifierName(DefaultBehaviourParameterName))
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(invocationBehaviorFieldName))
                            .Assign(IdentifierName(InvocationBehaviorParameterName))
                            .ToStatementSyntax()
                    )
                    .AddStatement(
                        ThisExpression()
                            .Dot(IdentifierName(propertyDisplayNameFieldName))
                            .Assign(IdentifierName(PropertyDisplayNameParameterName))
                            .ToStatementSyntax()
                    )
                    .Build()
            )
            .Build();

    internal static MethodDeclarationSyntax BuildMarkConfiguredMethod(
        string methodName,
        string fieldName
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, methodName)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(
                Block(
                    WellKnownTypes
                        .System.Threading.Volatile.Dot(IdentifierName("Write"))
                        .Call(
                            ArgumentList(
                                SeparatedList<ArgumentSyntax>(
                                    new SyntaxNodeOrToken[]
                                    {
                                        Argument(
                                            null,
                                            Token(SyntaxKind.RefKeyword),
                                            IdentifierName(fieldName)
                                        ),
                                        Token(SyntaxKind.CommaToken),
                                        Argument(True),
                                    }
                                )
                            )
                        )
                        .ToStatementSyntax()
                )
            )
            .Build();

    internal static MethodDeclarationSyntax BuildEnsureConfiguredMethod(
        string methodName,
        string invocationBehaviorFieldName,
        string hasConfiguredFieldName,
        string propertyDisplayNameFieldName,
        string suffix
    )
    {
        var explicitCheck = IsExplicit(IdentifierName(invocationBehaviorFieldName));

        var configuredCheck = WellKnownTypes
            .System.Threading.Volatile.Dot(IdentifierName("Read"))
            .Call(
                Argument(null, Token(SyntaxKind.RefKeyword), IdentifierName(hasConfiguredFieldName))
            );

        var condition = explicitCheck.And(Not(configuredCheck));

        return new MethodDeclarationBuilder(WellKnownTypes.Void, methodName)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                Block(
                    IfStatement(
                        condition,
                        Block(ThrowMissingImposter(propertyDisplayNameFieldName, suffix))
                    )
                )
            )
            .Build();
    }

    internal static IfStatementSyntax BuildCalledVerificationBlock(
        ExpressionSyntax condition,
        ExpressionSyntax invocationHistoryIdentifier,
        ExpressionSyntax countParameterIdentifier,
        ExpressionSyntax entryDescriptionExpression
    )
    {
        var stringListType = WellKnownTypes.System.Collections.Generic.List(WellKnownTypes.String);

        return IfStatement(
            condition,
            Block(
                LocalVariableDeclarationSyntax(Var, "performedInvocations", stringListType.New()),
                ForEachStatement(
                    Var,
                    Identifier("entry"),
                    invocationHistoryIdentifier,
                    Block(
                        IdentifierName("performedInvocations")
                            .Dot(IdentifierName("Add"))
                            .Call(Argument(entryDescriptionExpression))
                            .ToStatementSyntax()
                    )
                ),
                ThrowStatement(
                    ObjectCreationExpression(
                            WellKnownTypes.Imposter.Abstractions.VerificationFailedException
                        )
                        .WithArgumentList(
                            ArgumentList(
                                SeparatedList<ArgumentSyntax>(
                                    new SyntaxNodeOrToken[]
                                    {
                                        Argument(countParameterIdentifier),
                                        Token(SyntaxKind.CommaToken),
                                        Argument(IdentifierName("invocationCount")),
                                        Token(SyntaxKind.CommaToken),
                                        Argument(
                                            JoinWithNewLines(IdentifierName("performedInvocations"))
                                        ),
                                    }
                                )
                            )
                        )
                )
            )
        );
    }
}
