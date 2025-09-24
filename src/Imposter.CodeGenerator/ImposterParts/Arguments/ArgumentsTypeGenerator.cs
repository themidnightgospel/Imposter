using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.Arguments;

internal static class ArgumentsTypeGenerator
{
    internal static IEnumerable<ClassDeclarationSyntax> GetArgumentsType(ImposterTargetMethod method)
    {
        var parametersExceptOut = method.ParametersExceptOut;

        if (parametersExceptOut.Count > 0)
        {
            yield return ClassDeclaration(method.ArgumentsClassName)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithMembers(List<MemberDeclarationSyntax>(parametersExceptOut.Select(SyntaxFactoryHelper.ParameterAsProperty)))
                .AddMembers(ConstructorDeclaration(method.ArgumentsClassName)
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(ParameterList(SeparatedList(parametersExceptOut.Select(parameter => SyntaxFactoryHelper.ParameterSyntax(parameter, false)))))
                    .WithBody(Block(parametersExceptOut
                        .Select(parameter =>
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        ThisExpression(),
                                        IdentifierName(parameter.Name)
                                    ),
                                    IdentifierName(parameter.Name)
                                )
                            )
                        ))))
                .WithTrailingTrivia(CarriageReturnLineFeed);
        }

        if (parametersExceptOut.Count > 0)
        {
            yield return ClassDeclaration(method.ArgumentsCriteriaClassName)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithMembers(List<MemberDeclarationSyntax>(parametersExceptOut.Select(SyntaxFactoryHelper.ParameterAsArgProperty)))
                .AddMembers(ConstructorDeclaration(method.ArgumentsCriteriaClassName)
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithParameterList(ParameterList(SeparatedList(parametersExceptOut.Select(SyntaxFactoryHelper.ArgParameter))))
                    .WithBody(Block(parametersExceptOut
                        .Select(parameter =>
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        ThisExpression(),
                                        IdentifierName(parameter.Name)
                                    ),
                                    IdentifierName(parameter.Name)
                                )
                            )
                        ))))
                .AddMembers(MatchesMethod(method))
                .WithLeadingTrivia(
                    Comment(method.Comment),
                    CarriageReturnLineFeed
                )
                .WithTrailingTrivia(CarriageReturnLineFeed);
        }
    }

    private static MethodDeclarationSyntax MatchesMethod(ImposterTargetMethod method)
    {
        return MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.BoolKeyword)),
                    Identifier("Matches")
                )
                .WithModifiers(
                    TokenList(Token(SyntaxKind.PublicKeyword))
                )
                .WithParameterList(
                    ParameterList(SingletonSeparatedList(
                        Parameter(Identifier("arguments"))
                            .WithType(IdentifierName(method.ArgumentsClassName))
                    ))
                )
                .WithBody(
                    Block(
                        ReturnStatement(
                            method.Symbol.Parameters.Length switch
                            {
                                0 => LiteralExpression(SyntaxKind.TrueLiteralExpression),
                                1 => InvokeMatches(method.Symbol.Parameters[0]),
                                _ => method.Symbol.Parameters
                                    .Skip(1)
                                    .Select(InvokeMatches)
                                    .Aggregate(
                                        (ExpressionSyntax)InvokeMatches(method.Symbol.Parameters[0]),
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

        InvocationExpressionSyntax InvokeMatches(IParameterSymbol p)
        {
            return InvocationExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName(p.Name),
                    IdentifierName("Matches")
                ),
                ArgumentList(SingletonSeparatedList(
                    Argument(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("arguments"),
                            IdentifierName(p.Name)
                        )
                    )
                ))
            );
        }
    }
}