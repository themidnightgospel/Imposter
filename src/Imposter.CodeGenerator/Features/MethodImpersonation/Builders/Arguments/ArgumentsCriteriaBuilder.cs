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
            .AddMembers(
                method.Parameters.AllParameterMetadata.Select(parameter =>
                    ParameterAsArgProperty(parameter)
                )
            )
            .AddMember(
                new ConstructorBuilder(method.ArgumentsCriteria.Name)
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(method.Parameters.ArgParameterListSyntax)
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

        if (method.Model.IsGenericMethod)
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
            method.Model.TypeParameters,
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
        )
        {
            var matcherLambdaParameter = metadata.ArgumentsCriteriaAsMethod.MatcherLambdaParameter;

            return SeparatedList(
                metadata.Parameters.AllParameterMetadata.Select(parameter =>
                    BuildArgForParameter(parameter, renamer, matcherLambdaParameter)
                )
            );
        }

        static ArgumentSyntax BuildArgForParameter(
            MethodParameterMetadata parameter,
            TypeParameterRenamer renamer,
            IdentifierNameSyntax matcherLambdaParameter
        )
        {
            var targetMatcherType = (TypeSyntax)renamer.Visit(parameter.ArgTypeSyntax);

            if (parameter.Model.RefKind is RefKind.Out)
            {
                return Argument(targetMatcherType.Dot(IdentifierName("Any")).Call());
            }

            var sourceType = parameter.NullableAwareStoredTypeSyntax;
            var targetType = (TypeSyntax)renamer.Visit(sourceType);
            return Argument(
                targetMatcherType
                    .Dot(IdentifierName("Is"))
                    .Call(
                        ArgumentList(
                            SingletonSeparatedList(
                                Argument(
                                    BuildTryCastAndMatchLambda(
                                        parameter,
                                        targetType,
                                        sourceType,
                                        matcherLambdaParameter
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
            IdentifierNameSyntax matcherLambdaParameter
        )
        {
            var tryCastVarIdentifier = Identifier(parameter.Name + "Target");
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
                            Argument(matcherLambdaParameter),
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
                Parameter(matcherLambdaParameter.Identifier),
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
                        method
                            .Parameters.InputParameterMetadata.Select(it =>
                                (ExpressionSyntax)InvokeMatches(it)
                            )
                            .Aggregate((left, right) => left.And(right))
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
