using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Components;

internal static class ArgumentsTypeGenerator
{
    internal static void AddArgumentsType(ImposterGenerationContext imposterGenerationContext, ImposterTargetMethod method)
    {
        if (method.Parameters.Count == 0)
        {
            return;
        }

        var argumentsClassDeclarationSyntax = CreateArgumentSyntax(method.ArgumentsClassName, method.Symbol.Parameters);
        // TODO To .ToFullString once
        imposterGenerationContext.SourceBuilder.AppendLine(argumentsClassDeclarationSyntax.NormalizeWhitespace().ToFullString());
        imposterGenerationContext.SourceBuilder.AppendLine();
    }

    public static ClassDeclarationSyntax CreateArgumentSyntax(
        string className,
        IReadOnlyList<IParameterSymbol> parameters)
    {
        var properties = parameters.Select(CreatePropertyFromParameter).ToArray();
        var constructorParameters = parameters.Select(it => ImposterSyntaxFactory.CreateParameterSyntax(it, false)).ToArray();
        var constructor = CreateConstructor(className, constructorParameters, parameters);

        return SyntaxFactory.ClassDeclaration(className)
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
            .WithMembers(
                SyntaxFactory.List<MemberDeclarationSyntax>(
                    properties
                        .Select(prop => prop.WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed))
                        .Concat(new MemberDeclarationSyntax[]
                        {
                            constructor.WithLeadingTrivia(SyntaxFactory.CarriageReturnLineFeed)
                        })
                )
            )
            .WithTrailingTrivia(SyntaxFactory.CarriageReturnLineFeed);
    }

    private static PropertyDeclarationSyntax CreatePropertyFromParameter(IParameterSymbol parameter)
    {
        var type = SyntaxFactory.ParseTypeName(parameter.Type.ToFullyQualifiedName());

        return SyntaxFactory
            .PropertyDeclaration(type, parameter.Name)
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
            .WithAccessorList(
                SyntaxFactory.AccessorList(
                    SyntaxFactory
                        .SingletonList(
                            SyntaxFactory
                                .AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                        )
                )
            );
    }

    private static ConstructorDeclarationSyntax CreateConstructor(
        string className,
        IEnumerable<ParameterSyntax> parameters,
        IEnumerable<IParameterSymbol> parameterSymbols)
    {
        var assignmentStatements = parameterSymbols.Select(parameter =>
            SyntaxFactory.ExpressionStatement(
                SyntaxFactory.AssignmentExpression(
                    SyntaxKind.SimpleAssignmentExpression,
                    SyntaxFactory.MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        SyntaxFactory.ThisExpression(),
                        SyntaxFactory.IdentifierName(parameter.Name)
                    ),
                    SyntaxFactory.IdentifierName(parameter.Name)
                )
            )
        );

        return SyntaxFactory
            .ConstructorDeclaration(className)
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(SyntaxFactory.ParameterList(SyntaxFactory.SeparatedList(parameters)))
            .WithBody(SyntaxFactory.Block(assignmentStatements));
    }
}