using System.Collections.Generic;
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
                indexer.Core.MatchedParameters.Select(parameter => parameter.ConstructorArgument)
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

    // The keys a delegate gets: the matched keys from source, an arguments class, and the keys passed through under the
    // names they're in scope by.
    internal static ArgumentListSyntax BuildDelegateInvocationArguments(
        ExpressionSyntax source,
        in ImposterIndexerMetadata indexer,
        IReadOnlyList<string> passedThroughKeyNames
    )
    {
        var arguments = new List<ArgumentSyntax>();
        var passedThroughKeyNameIndex = 0;
        foreach (var parameter in indexer.Core.Parameters)
        {
            arguments.Add(
                parameter.IsPassedThrough
                    ? Argument(IdentifierName(passedThroughKeyNames[passedThroughKeyNameIndex++]))
                    : Argument(source.Dot(IdentifierName(parameter.FieldName)))
            );
        }

        return ArgumentList(SeparatedList(arguments));
    }

    // The keys passed through, which members and handlers take beside the arguments class under the given names, as
    // parameters...
    internal static IEnumerable<ParameterSyntax> PassedThroughKeyParameters(
        in ImposterIndexerMetadata indexer,
        IReadOnlyList<string> names
    ) =>
        indexer.Core.PassedThroughParameters.Select(
            (parameter, index) => ParameterSyntax(parameter.TypeSyntax, names[index])
        );

    // ...as the parameters of a lambda, whose delegate gives their types...
    internal static IEnumerable<ParameterSyntax> PassedThroughKeyLambdaParameters(
        IReadOnlyList<string> names
    ) => names.Select(name => Parameter(Identifier(name)));

    // ...and as the arguments that pass them on.
    internal static IEnumerable<ArgumentSyntax> PassedThroughKeyArguments(
        IReadOnlyList<string> names
    ) => names.Select(name => Argument(IdentifierName(name)));

    internal static ArgumentListSyntax BuildDelegateInvocationArgumentsWithValue(
        ExpressionSyntax source,
        in ImposterIndexerMetadata indexer,
        IReadOnlyList<string> passedThroughKeyNames,
        ExpressionSyntax valueExpression
    )
    {
        var arguments = BuildDelegateInvocationArguments(source, indexer, passedThroughKeyNames)
            .Arguments.Add(Argument(valueExpression));

        return ArgumentList(arguments);
    }

    // The keys a setup matches, formatted as an index list. An indexer whose keys all pass through has none.
    internal static ExpressionSyntax BuildIndices(
        in ImposterIndexerMetadata indexer,
        ExpressionSyntax source
    )
    {
        if (indexer.Core.MatchedParameters.Length == 0)
        {
            return "[]".StringLiteral();
        }

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
                                            indexer.Core.MatchedParameters.Select(
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
        in FieldMetadata? defaultBehaviourField,
        string invocationBehaviorFieldName,
        string propertyDisplayNameFieldName
    )
    {
        var constructor = new ConstructorWithFieldInitializationBuilder(className).WithModifiers(
            Token(SyntaxKind.InternalKeyword)
        );
        if (defaultBehaviourField is { } field)
        {
            constructor.AddParameter(
                new ParameterMetadata(DefaultBehaviourParameterName, field.Type),
                field.Name
            );
        }

        return constructor
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
    }

    internal static MethodDeclarationSyntax BuildMarkConfiguredMethod(
        string methodName,
        string fieldName
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, methodName)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithBody(Block(VolatileWrite(fieldName, True).ToStatementSyntax()))
            .Build();
}
