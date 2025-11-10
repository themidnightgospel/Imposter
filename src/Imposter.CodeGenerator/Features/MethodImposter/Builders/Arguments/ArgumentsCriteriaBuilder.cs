using System.Linq;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.Arguments;

public static class ArgumentsCriteriaBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Parameters.HasInputParameters)
        {
            return null;
        }

        var argumentsCriteriaClass = new ClassDeclarationBuilder(method.ArgumentsCriteria.Name, TypeParameterListSyntax(method.GenericTypeArguments))
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMembers(method.Symbol.Parameters.Select(ParameterAsArgProperty))
            .AddMember(
                new ConstructorBuilder(method.ArgumentsCriteria.Name)
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SeparatedList(
                                method.Symbol.Parameters.Select(ArgParameter)
                            )
                        )
                    )
                    .WithBody(Block(method.Symbol.Parameters
                            .Select(parameter =>
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
            // TODO Not optimal
            .WithLeadingTriviaComment(method.DisplayName)
            .WithTrailingTrivia(CarriageReturnLineFeed);
    }

    // TODO refactor
    private static MethodDeclarationSyntax BuildAsMethod(in ImposterTargetMethodMetadata method)
    {
        var typeParameters = method.Symbol.TypeParameters;
        var asMethodTypeParams = typeParameters.Select(p => TypeParameter(p.Name + "Target")).ToArray();
        var targetTypeArgs = typeParameters
            .Select(p => (TypeSyntax)IdentifierName(p.Name + "Target"))
            .ToArray();
        var returnType = GenericName(method.ArgumentsCriteria.Name)
            .WithTypeArgumentList(TypeArgumentList(SeparatedList<TypeSyntax>(targetTypeArgs)));

        var constructorArgs = method
            .Symbol
            .Parameters
            .Select(p =>
            {
                var renamer = new TypeParameterRenamer(typeParameters, "Target");
                var targetType = (TypeSyntax)renamer.Visit(TypeSyntax(p.Type));

                if (p.RefKind is RefKind.Out)
                {
                    return Argument(OutArgAny(targetType));
                }

                var sourceType = TypeSyntax(p.Type);

                var tryCastVarIdentifier = Identifier(p.Name + "Target");

                return Argument(
                    WellKnownTypes.Imposter.Abstractions.Arg(targetType)
                        .Dot(IdentifierName("Is"))
                        .Call(ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        SimpleLambdaExpression(
                                            Parameter(It.Identifier),
                                            WellKnownTypes.Imposter.Abstractions.TypeCaster
                                                .Dot(GenericName("TryCast")
                                                    .WithTypeArgumentList(TypeArgumentList(SeparatedList<TypeSyntax>([targetType, sourceType])))
                                                )
                                                .Call(ArgumentList(SeparatedList([
                                                    Argument(It),
                                                    Argument(DeclarationExpression(sourceType, SingleVariableDesignation(tryCastVarIdentifier))).WithRefOrOutKeyword(Token(SyntaxKind.OutKeyword))
                                                ])))
                                                .And(IdentifierName(p.Name)
                                                    .Dot(IdentifierName("Matches"))
                                                    .Call(ArgumentList(SingletonSeparatedList(Argument(IdentifierName(tryCastVarIdentifier))))))
                                        )
                                    )
                                )
                            )
                        )
                );
            });

        return new MethodDeclarationBuilder(returnType, "As")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithTypeParameters(TypeParameterList(SeparatedList(asMethodTypeParams)))
            .WithBody(Block(
                ReturnStatement(
                    returnType.New(ArgumentList(SeparatedList(constructorArgs)))
                )
            ))
            .Build();
    }

    private static MethodDeclarationSyntax MatchesMethod(in ImposterTargetMethodMetadata method)
    {
        return new MethodDeclarationBuilder(
                WellKnownTypes.Bool,
                "Matches")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(
                ParameterList(
                    SingletonSeparatedList(
                        Parameter(Identifier("arguments"))
                            .WithType(method.Arguments.Syntax)
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
                                .Parameters
                                .InputParameters
                                .Skip(1)
                                .Select(InvokeMatches)
                                .Aggregate(
                                    (ExpressionSyntax)InvokeMatches(method.Parameters.InputParameters[0]),
                                    (left, right) => BinaryExpression(
                                        SyntaxKind.LogicalAndExpression,
                                        left,
                                        right
                                    ))
                        }
                    )
                )
            )
            .Build();

        InvocationExpressionSyntax InvokeMatches(IParameterSymbol p) =>
            IdentifierName(p.Name)
                .Dot(IdentifierName("Matches"))
                .Call(ArgumentList(
                        SingletonSeparatedList(
                            Argument(
                                IdentifierName("arguments")
                                    .Dot(IdentifierName(p.Name))
                            )
                        )
                    )
                );
    }
}
