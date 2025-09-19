using System;
using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts;

internal static class ArgumentsTypeGenerator
{
    internal static void AddArgumentsType(ImposterGenerationContext imposterGenerationContext, ImposterTargetMethod method)
    {
        if (method.Parameters.Count == 0)
        {
            return;
        }

        var argumentsClassDeclarationSyntax = CreateParameterType(
            method.ArgumentsClassName,
            method.Symbol.Parameters,
            SyntaxFactoryHelper.ParameterAsProperty,
            parameter => SyntaxFactoryHelper.ParameterSyntax(parameter, false));

        // TODO To .ToFullString once
        imposterGenerationContext.SourceBuilder.AppendLine(argumentsClassDeclarationSyntax.NormalizeWhitespace().ToFullString());
        imposterGenerationContext.SourceBuilder.AppendLine();
    }

    internal static void AddArgArgumentsType(ImposterGenerationContext imposterGenerationContext, ImposterTargetMethod method)
    {
        if (method.Parameters.Count == 0)
        {
            return;
        }

        var argumentsClassDeclarationSyntax = CreateParameterType(
                method.ArgArgumentsClassName,
                method.Symbol.Parameters,
                SyntaxFactoryHelper.ParameterAsArgProperty,
                SyntaxFactoryHelper.ArgParameter)
            .AddMembers(MatchesMethod(method.Symbol.Parameters));


        // TODO To .ToFullString once
        imposterGenerationContext.SourceBuilder.AppendLine(argumentsClassDeclarationSyntax.NormalizeWhitespace().ToFullString());
        imposterGenerationContext.SourceBuilder.AppendLine();
    }

    private static MethodDeclarationSyntax MatchesMethod(IReadOnlyList<IParameterSymbol> parameters)
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
                        .WithType(IdentifierName("MethodWithOutParameterArguments"))
                ))
            )
            .WithBody(
                Block(
                    ReturnStatement(
                        parameters.Count switch
                        {
                            0 => LiteralExpression(SyntaxKind.TrueLiteralExpression),
                            1 => InvokeMatches(parameters[0]),
                            _ => parameters
                                .Skip(1)
                                .Select(InvokeMatches)
                                .Aggregate(
                                    (ExpressionSyntax)InvokeMatches(parameters[0]),
                                    (left, right) => BinaryExpression(
                                        SyntaxKind.LogicalAndExpression,
                                        left,
                                        right
                                    ))
                        }
                    )
                )
            );

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

    private static ClassDeclarationSyntax CreateParameterType(
        string typeName,
        IReadOnlyList<IParameterSymbol> parameters,
        Func<IParameterSymbol, PropertyDeclarationSyntax> parameterToPropery,
        Func<IParameterSymbol, ParameterSyntax> parameterToConstructorParameter
    ) =>
        ClassDeclaration(typeName)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithMembers(List<MemberDeclarationSyntax>(parameters.Select(parameterToPropery)))
            .AddMembers(ConstructorDeclaration(typeName)
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithParameterList(ParameterList(SeparatedList(parameters.Select(parameterToConstructorParameter))))
                .WithBody(Block(parameters
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