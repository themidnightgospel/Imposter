using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Arguments;

public static class ArgumentsCriteriaBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Parameters.HasInputParameters)
        {
            return null;
        }

        var argumentsCriteriaClass = new ClassDeclarationBuilder(
            method.ArgumentsCriteria.Name,
            TypeParameterListSyntax(method.GenericTypeArguments)
        )
            .WithTypeParameterConstraintClauses(method.GenericTypeConstraintClauses)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMembers(method.Symbol.Parameters.Select(ParameterAsArgProperty))
            .AddMember(
                new ConstructorBuilder(method.ArgumentsCriteria.Name)
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        ParameterList(SeparatedList(method.Symbol.Parameters.Select(ArgParameter)))
                    )
                    .WithBody(
                        Block(
                            method.Parameters.AllParameterMetadata.Select(parameter =>
                                ThisExpression()
                                    .Dot(IdentifierName(parameter.Name))
                                    .Assign(IdentifierName(parameter.Name))
                                    .ToStatementSyntax()
                            )
                        )
                    )
                    .Build()
            )
            .AddMember(MatchesMethod(method));

        if (method.Symbol.IsGenericMethod)
        {
            argumentsCriteriaClass.AddMember(BuildAsMethod(method));
        }

        return argumentsCriteriaClass
            .Build()
#if DEBUG
            .WithLeadingTriviaComment(method.DisplayName)
#endif
            .WithTrailingTrivia(CarriageReturnLineFeed);
    }

    private static MethodDeclarationSyntax BuildAsMethod(in ImposterTargetMethodMetadata method)
    {
        var returnType = BuildReturnType(method);
        var typeParameterRenamer = new TypeParameterRenamer(
            method.Symbol.TypeParameters,
            method.ArgumentsCriteriaAsMethod.TargetTypeArguments
        );
        var constructorArgs = BuildConstructorArgs(method, typeParameterRenamer);

        return new MethodDeclarationBuilder(returnType, method.ArgumentsCriteria.AsMethod.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithTypeParameters(
                TypeParameterList(SeparatedList(method.ArgumentsCriteriaAsMethod.TypeParameters))
            )
            .AddConstraintClauses(method.TargetGenericTypeConstraintClauses)
            .WithBody(Block(ReturnStatement(returnType.New(ArgumentList(constructorArgs)))))
            .Build();

        static TypeSyntax BuildReturnType(in ImposterTargetMethodMetadata metadata) =>
            GenericName(metadata.ArgumentsCriteria.Name)
                .WithTypeArgumentList(
                    TypeArgumentList(
                        SeparatedList<TypeSyntax>(
                            metadata.ArgumentsCriteriaAsMethod.TargetTypeArguments
                        )
                    )
                );

        static SeparatedSyntaxList<ArgumentSyntax> BuildConstructorArgs(
            in ImposterTargetMethodMetadata metadata,
            TypeParameterRenamer renamer
        ) =>
            SeparatedList(
                metadata.Parameters.AllParameterMetadata.Select(parameter =>
                    BuildArgForParameter(parameter, renamer)
                )
            );

        static ArgumentSyntax BuildArgForParameter(
            MethodParameterMetadata parameter,
            TypeParameterRenamer renamer
        )
        {
            var targetType = (TypeSyntax)renamer.Visit(TypeSyntax(parameter.Symbol.Type));

            if (parameter.Symbol.RefKind is RefKind.Out)
            {
                return Argument(OutArgAny(targetType));
            }

            var sourceType = TypeSyntax(parameter.Symbol.Type);
            return BuildIsPredicateArg(parameter, targetType, sourceType);
        }

        static ArgumentSyntax BuildIsPredicateArg(
            MethodParameterMetadata parameter,
            TypeSyntax targetType,
            TypeSyntax sourceType
        )
        {
            var tryCastVarIdentifier = Identifier(parameter.Name + "Target");

            return Argument(
                WellKnownTypes
                    .Imposter.Abstractions.Arg(targetType)
                    .Dot(IdentifierName("Is"))
                    .Call(
                        ArgumentList(
                            SingletonSeparatedList(
                                Argument(
                                    BuildTryCastAndMatchLambda(
                                        parameter,
                                        targetType,
                                        sourceType,
                                        tryCastVarIdentifier
                                    )
                                )
                            )
                        )
                    )
            );
        }

        static SimpleLambdaExpressionSyntax BuildTryCastAndMatchLambda(
            MethodParameterMetadata parameter,
            TypeSyntax targetType,
            TypeSyntax sourceType,
            SyntaxToken tryCastVarIdentifier
        )
        {
            var tryCastInvocation = WellKnownTypes
                .Imposter.Abstractions.TypeCaster.Dot(
                    GenericName("TryCast")
                        .WithTypeArgumentList(
                            TypeArgumentList(SeparatedList<TypeSyntax>([targetType, sourceType]))
                        )
                )
                .Call(
                    ArgumentList(
                        SeparatedList([
                            Argument(It),
                            Argument(
                                    DeclarationExpression(
                                        sourceType,
                                        SingleVariableDesignation(tryCastVarIdentifier)
                                    )
                                )
                                .WithRefOrOutKeyword(Token(SyntaxKind.OutKeyword)),
                        ])
                    )
                );

            var matchesCall = IdentifierName(parameter.Name)
                .Dot(IdentifierName("Matches"))
                .Call(
                    ArgumentList(
                        SingletonSeparatedList(Argument(IdentifierName(tryCastVarIdentifier)))
                    )
                );

            return SimpleLambdaExpression(
                Parameter(It.Identifier),
                tryCastInvocation.And(matchesCall)
            );
        }
    }

    private static MethodDeclarationSyntax MatchesMethod(in ImposterTargetMethodMetadata method)
    {
        var matchesParameterName = method.ArgumentsCriteria.MatchesMethod.ParameterName;
        var matchesParameterIdentifier = Identifier(matchesParameterName);
        var matchesParameterExpression = IdentifierName(matchesParameterName);

        return new MethodDeclarationBuilder(
            WellKnownTypes.Bool,
            method.ArgumentsCriteria.MatchesMethod.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(
                ParameterList(
                    SingletonSeparatedList(
                        Parameter(matchesParameterIdentifier).WithType(method.Arguments.Syntax)
                    )
                )
            )
            .WithBody(
                Block(
                    ReturnStatement(
                        method.Parameters.InputParameterMetadata.Count switch
                        {
                            0 => True,
                            1 => InvokeMatches(method.Parameters.InputParameterMetadata[0]),
                            _ => method
                                .Parameters.InputParameterMetadata.Skip(1)
                                .Select(InvokeMatches)
                                .Aggregate(
                                    (ExpressionSyntax)InvokeMatches(
                                        method.Parameters.InputParameterMetadata[0]
                                    ),
                                    (left, right) => left.And(right)
                                ),
                        }
                    )
                )
            )
            .Build();

        InvocationExpressionSyntax InvokeMatches(MethodParameterMetadata p) =>
            IdentifierName(p.Name)
                .Dot(IdentifierName("Matches"))
                .Call(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(matchesParameterExpression.Dot(IdentifierName(p.Name)))
                        )
                    )
                );
    }
}
