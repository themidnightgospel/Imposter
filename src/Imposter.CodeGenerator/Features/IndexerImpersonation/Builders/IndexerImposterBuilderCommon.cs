using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FormatValueMethodBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Imposter.CodeGenerator.SyntaxHelpers.VolatileSyntaxHelper;
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
        new ConstructorWithFieldInitializationBuilder(className)
            .WithModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameter(
                new ParameterMetadata(DefaultBehaviourParameterName, defaultBehaviourType),
                defaultBehaviourFieldName
            )
            .AddParameter(
                new ParameterMetadata(
                    InvocationBehaviorParameterName,
                    WellKnownTypes.Imposter.Abstractions.ImposterMode
                ),
                invocationBehaviorFieldName
            )
            .AddParameter(
                new ParameterMetadata(PropertyDisplayNameParameterName, WellKnownTypes.String),
                propertyDisplayNameFieldName
            )
            .Build();

    internal static MethodDeclarationSyntax BuildMarkConfiguredMethod(
        string methodName,
        string fieldName
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, methodName)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(Block(VolatileWrite(fieldName, True).ToStatementSyntax()))
            .Build();
}
