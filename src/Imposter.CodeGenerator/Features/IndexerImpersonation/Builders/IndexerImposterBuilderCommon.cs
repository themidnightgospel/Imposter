using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FormatValueMethodBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerImposterBuilderCommon
{
    internal const string DefaultBehaviourParameterName = "defaultBehaviour";
    internal const string InvocationBehaviorParameterName = "invocationBehavior";
    internal const string PropertyDisplayNameParameterName = "propertyDisplayName";
    internal const string BaseImplementationParameterName = "baseImplementation";

    internal static BlockSyntax BuildFluentBodyReturningThis(params StatementSyntax[] statements)
    {
        var fluentStatements = statements.Concat([ReturnStatement(ThisExpression())]).ToArray();
        return Block(fluentStatements);
    }

    internal static ArgumentSyntax BuildArgument(
        IParameterSymbol parameter,
        ExpressionSyntax expression
    )
    {
        var modifier = parameter.RefKind switch
        {
            RefKind.Ref => Token(SyntaxKind.RefKeyword),
            RefKind.Out => Token(SyntaxKind.OutKeyword),
            RefKind.In => Token(SyntaxKind.InKeyword),
            _ => default(SyntaxToken),
        };

        return Argument(null, modifier, expression);
    }

    internal static ArgumentListSyntax BuildIndexerArgumentsArgumentList(
        in ImposterIndexerMetadata indexer
    ) =>
        ArgumentList(
            SeparatedList(
                indexer.Core.Parameters.Select(parameter => ArgumentSyntax(parameter.Symbol))
            )
        );

    internal static LocalDeclarationStatementSyntax CreateArgumentsDeclaration(
        in ImposterIndexerMetadata indexer
    ) =>
        LocalVariableDeclarationSyntax(
            indexer.Arguments.TypeSyntax,
            indexer.GetterImplementation.ArgumentsVariableName,
            indexer.Arguments.TypeSyntax.New(BuildIndexerArgumentsArgumentList(indexer))
        );

    internal static ArgumentListSyntax BuildDelegateInvocationArguments(
        ExpressionSyntax source,
        in ImposterIndexerMetadata indexer,
        bool fromArguments
    )
    {
        var arguments = indexer.Core.Parameters.Select(parameter =>
        {
            ExpressionSyntax argumentExpression = fromArguments
                ? source.Dot(IdentifierName(parameter.Name))
                : IdentifierName(parameter.Name);

            return BuildArgument(parameter.Symbol, argumentExpression);
        });

        return ArgumentList(SeparatedList(arguments));
    }

    internal static ArgumentListSyntax BuildDelegateInvocationArgumentsWithValue(
        ExpressionSyntax source,
        in ImposterIndexerMetadata indexer,
        bool fromArguments,
        ExpressionSyntax valueExpression
    )
    {
        var arguments = BuildDelegateInvocationArguments(source, indexer, fromArguments)
            .Arguments.Add(Argument(valueExpression));

        return ArgumentList(arguments);
    }

    internal static ThrowStatementSyntax BuildMissingImposterThrow(
        string propertyDisplayNameFieldName,
        string suffix
    ) =>
        ThrowStatement(
            ObjectCreationExpression(WellKnownTypes.Imposter.Abstractions.MissingImposterException)
                .WithArgumentList(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(
                                IdentifierName(propertyDisplayNameFieldName)
                                    .Add(suffix.StringLiteral())
                            )
                        )
                    )
                )
        );

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
                                                    IdentifierName("FormatValue")
                                                        .Call(
                                                            Argument(
                                                                source.Dot(
                                                                    IdentifierName(parameter.Name)
                                                                )
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

        return AddStrings("[".StringLiteral(), AddStrings(formattedValues, "]".StringLiteral()));
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
            .AddParameter(
                Parameter(Identifier(DefaultBehaviourParameterName)).WithType(defaultBehaviourType)
            )
            .AddParameter(
                Parameter(Identifier(InvocationBehaviorParameterName))
                    .WithType(WellKnownTypes.Imposter.Abstractions.ImposterMode)
            )
            .AddParameter(
                Parameter(Identifier(PropertyDisplayNameParameterName))
                    .WithType(PredefinedType(Token(SyntaxKind.StringKeyword)))
            )
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
        SyntaxKind visibility,
        string fieldName
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, methodName)
            .AddModifier(Token(visibility))
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
        var explicitCheck = BinaryExpression(
            SyntaxKind.EqualsExpression,
            IdentifierName(invocationBehaviorFieldName),
            WellKnownTypes.Imposter.Abstractions.ImposterMode.Dot(IdentifierName("Explicit"))
        );

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
                        Block(BuildMissingImposterThrow(propertyDisplayNameFieldName, suffix))
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
        var stringListType = WellKnownTypes.System.Collections.Generic.List(
            PredefinedType(Token(SyntaxKind.StringKeyword))
        );

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
                                            IdentifierName("string")
                                                .Dot(IdentifierName("Join"))
                                                .Call(
                                                    ArgumentList(
                                                        SeparatedList<ArgumentSyntax>(
                                                            new SyntaxNodeOrToken[]
                                                            {
                                                                Argument(
                                                                    IdentifierName("Environment")
                                                                        .Dot(
                                                                            IdentifierName(
                                                                                "NewLine"
                                                                            )
                                                                        )
                                                                ),
                                                                Token(SyntaxKind.CommaToken),
                                                                Argument(
                                                                    IdentifierName(
                                                                        "performedInvocations"
                                                                    )
                                                                ),
                                                            }
                                                        )
                                                    )
                                                )
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
