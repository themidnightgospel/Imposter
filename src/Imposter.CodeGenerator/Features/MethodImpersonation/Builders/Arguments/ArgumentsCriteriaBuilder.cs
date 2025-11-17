using System.Linq;
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
                            method.Symbol.Parameters.Select(parameter =>
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
            .WithLeadingTriviaComment(method.DisplayName)
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
                metadata.Symbol.Parameters.Select(parameter =>
                    BuildArgForParameter(parameter, renamer)
                )
            );

        static ArgumentSyntax BuildArgForParameter(
            IParameterSymbol parameter,
            TypeParameterRenamer renamer
        )
        {
            var targetType = (TypeSyntax)renamer.Visit(TypeSyntax(parameter.Type));

            if (parameter.RefKind is RefKind.Out)
            {
                return BuildOutArgument(parameter, targetType);
            }

            var sourceType = TypeSyntax(parameter.Type);
            return BuildIsPredicateArg(parameter, targetType, sourceType);
        }

        static ArgumentSyntax BuildOutArgument(IParameterSymbol parameter, TypeSyntax targetType)
        {
            _ = parameter;
            return Argument(OutArgAny(targetType));
        }

        static ArgumentSyntax BuildIsPredicateArg(
            IParameterSymbol parameter,
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
            IParameterSymbol parameter,
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
                        method.Parameters.InputParameters.Count switch
                        {
                            0 => True,
                            1 => InvokeMatches(method.Parameters.InputParameters[0]),
                            _ => method
                                .Parameters.InputParameters.Skip(1)
                                .Select(InvokeMatches)
                                .Aggregate(
                                    (ExpressionSyntax)InvokeMatches(
                                        method.Parameters.InputParameters[0]
                                    ),
                                    (left, right) => left.And(right)
                                ),
                        }
                    )
                )
            )
            .Build();

        InvocationExpressionSyntax InvokeMatches(IParameterSymbol p) =>
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
