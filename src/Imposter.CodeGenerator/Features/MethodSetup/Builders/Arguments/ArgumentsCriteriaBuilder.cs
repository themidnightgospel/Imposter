using System.Linq;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.Arguments;

public static class ArgumentsCriteriaBuilder
{
    internal static ClassDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Parameters.HasInputParameters)
        {
            return null;
        }

        var argumentsCriteriaClass = new ClassDeclarationBuilder(method.ArgumentsCriteria.Name, SyntaxFactoryHelper.TypeParameterListSyntax(method.GenericTypeArguments))
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMembers(method.Symbol.Parameters.Select(SyntaxFactoryHelper.ParameterAsArgProperty))
            .AddMember(
                ConstructorDeclaration(method.ArgumentsCriteria.Name)
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(
                        ParameterList(
                            SeparatedList(
                                method.Symbol.Parameters.Select(SyntaxFactoryHelper.ArgParameter)
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
    private static MemberDeclarationSyntax BuildAsMethod(in ImposterTargetMethodMetadata method)
    {
        var typeParameters = method.Symbol.TypeParameters;
        var asMethodTypeParams = typeParameters.Select(p => TypeParameter(p.Name + "Target")).ToArray();
        var targetTypeArgs = typeParameters.Select(p => IdentifierName(p.Name + "Target")).ToArray();
        var returnType = GenericName(method.ArgumentsCriteria.Name)
            .WithTypeArgumentList(TypeArgumentList(SeparatedList<TypeSyntax>(targetTypeArgs)));

        var constructorArgs = method
            .Symbol
            .Parameters
            .Select(p =>
            {
                var renamer = new TypeParameterRenamer(typeParameters, "Target");
                var targetType = renamer.Visit(SyntaxFactoryHelper.TypeSyntax(p.Type));

                if (p.RefKind is RefKind.Out)
                {
                    return Argument(SyntaxFactoryHelper.OutArgAny(targetType));
                }

                var sourceType = SyntaxFactoryHelper.TypeSyntax(p.Type);

                var tryCastVarIdentifier = Identifier(p.Name + "Target");

                return Argument(
                    WellKnownTypes.Imposter.Abstractions.Arg(targetType)
                        .Dot(IdentifierName("Is"))
                        .Call(ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        SimpleLambdaExpression(
                                            Parameter(SyntaxFactoryHelper.It.Identifier),
                                            WellKnownTypes.Imposter.Abstractions.TypeCaster
                                                .Dot(GenericName("TryCast")
                                                    .WithTypeArgumentList(TypeArgumentList(SeparatedList([targetType, sourceType])))
                                                )
                                                .Call(ArgumentList(SeparatedList([
                                                    Argument(SyntaxFactoryHelper.It),
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

        return MethodDeclaration(returnType, "As")
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithTypeParameterList(TypeParameterList(SeparatedList(asMethodTypeParams)))
            .WithBody(Block(
                ReturnStatement(
                    returnType.New(ArgumentList(SeparatedList(constructorArgs)))
                )
            ));
    }

    private static MethodDeclarationSyntax MatchesMethod(in ImposterTargetMethodMetadata method)
    {
        return MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.BoolKeyword)),
                    Identifier("Matches")
                )
                .WithModifiers(
                    TokenList(Token(SyntaxKind.PublicKeyword))
                )
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
                                0 => LiteralExpression(SyntaxKind.TrueLiteralExpression),
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
            ;

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