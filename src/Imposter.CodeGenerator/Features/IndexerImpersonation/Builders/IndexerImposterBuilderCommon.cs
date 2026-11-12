using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
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
    internal static ClassDeclarationBuilder AccessorBuilder(
        in IndexerAccessorBuilderMetadata builder,
        TypeSyntax imposterType,
        TypeSyntax criteriaType,
        NameSyntax builderInterface,
        NameSyntax fluentInterface
    )
    {
        var imposterField = new FieldMetadata(builder.ImposterFieldName, imposterType);
        var criteriaField = new FieldMetadata(builder.CriteriaFieldName, criteriaType);

        return new ClassDeclarationBuilder(builder.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(builderInterface))
            .AddBaseType(SimpleBaseType(fluentInterface))
            .AddMember(SinglePrivateReadonlyVariableField(imposterField))
            .AddMember(SinglePrivateReadonlyVariableField(criteriaField))
            .AddMember(
                new ConstructorWithFieldInitializationBuilder(builder.Name)
                    .WithModifiers(Token(SyntaxKind.InternalKeyword))
                    .AddParameter(imposterField)
                    .AddParameter(criteriaField)
                    .Build()
            );
    }

    internal static MethodDeclarationSyntax ForwardCalled(
        in IndexerAccessorBuilderMetadata builder,
        NameSyntax verificationInterface,
        in CalledMethodMetadata called,
        string imposterMethodName
    )
    {
        var method = new MethodDeclarationBuilder(called.ReturnType, called.Name)
            .WithExplicitInterfaceSpecifier(verificationInterface)
            .AddParameter(ParameterSyntax(called.CountParameter));
        return method
            .WithBody(
                Block(
                    IdentifierName(builder.ImposterFieldName)
                        .Dot(IdentifierName(imposterMethodName))
                        .Call([
                            Argument(IdentifierName(builder.CriteriaFieldName)),
                            Argument(IdentifierName(called.CountParameter.Name)),
                        ])
                        .ToStatementSyntax()
                )
            )
            .Build();
    }

    internal static BinaryExpressionSyntax ExpectedIndices(
        in ImposterIndexerMetadata indexer,
        ExpressionSyntax criteria
    )
    {
        ExpressionSyntax description = "[".StringLiteral();
        var first = true;
        foreach (var parameter in indexer.Core.MatchedParameters)
        {
            description = description
                .Add(((first ? "" : ", ") + parameter.Name + ": ").StringLiteral())
                .Add(
                    WellKnownTypes
                        .Imposter.Abstractions.MatcherDescription.Dot(IdentifierName("Format"))
                        .Call(Argument(criteria.Dot(IdentifierName(parameter.FieldName))))
                );
            first = false;
        }
        return description.Add("]".StringLiteral());
    }

    private static ArgumentListSyntax BuildIndexerArgumentsArgumentList(
        in ImposterIndexerMetadata indexer
    ) =>
        ArgumentListSyntax(
            indexer.Core.MatchedParameters.Select(parameter => parameter.ConstructorArgument)
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

        return ArgumentListSyntax(arguments);
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
            .Call([
                Argument(", ".StringLiteral()),
                Argument(
                    ImplicitArrayCreationExpression(
                        InitializerExpression(
                            SyntaxKind.ArrayInitializerExpression,
                            SeparatedList(
                                indexer.Core.MatchedParameters.Select(
                                    ExpressionSyntax (parameter) =>
                                        Invocation(source.Dot(IdentifierName(parameter.FieldName)))
                                )
                            )
                        )
                    )
                ),
            ]);

        return "[".StringLiteral().Add(formattedValues.Add("]".StringLiteral()));
    }

    internal static ConstructorDeclarationSyntax BuildImposterConstructor(
        in ImposterIndexerMetadata indexer,
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
                new ParameterMetadata(indexer.Locals["defaultBehaviour"], field.Type),
                field.Name
            );
        }

        return constructor
            .AddParameter(
                new ParameterMetadata(
                    indexer.Locals["invocationBehavior"],
                    WellKnownTypes.Imposter.Abstractions.ImposterMode
                ),
                invocationBehaviorFieldName
            )
            .AddParameter(
                new ParameterMetadata(indexer.Locals["propertyDisplayName"], WellKnownTypes.String),
                propertyDisplayNameFieldName
            )
            .Build();
    }
}
