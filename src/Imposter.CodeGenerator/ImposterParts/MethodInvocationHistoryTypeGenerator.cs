using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.ImposterParts;

public static class MethodInvocationHistoryTypeGenerator
{
    internal static ClassDeclarationSyntax GetMethodInvocationHistoryClass(ImposterTargetMethod method)
    {
        var properties = GetProperties(method).ToArray();

        return SyntaxFactory
            .ClassDeclaration(method.MethodInvocationHistoryClassName)
            .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
            .AddMembers(properties)
            .AddMembers(GetConstructor(method.MethodInvocationHistoryClassName, properties));

        static IEnumerable<PropertyDeclarationSyntax> GetProperties(ImposterTargetMethod method)
        {
            if (method.ParametersExceptOut.Count > 0)
            {
                yield return SyntaxFactory
                    .PropertyDeclaration(SyntaxFactory.ParseTypeName(method.ArgumentsClassName), SyntaxFactory.Identifier("Arguments"))
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .WithAccessorList(SyntaxFactory.AccessorList(
                        SyntaxFactory.SingletonList(
                            SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                        )
                    ));
            }

            if (method.HasReturnValue)
            {
                yield return SyntaxFactory
                    .PropertyDeclaration(SyntaxFactory.ParseTypeName(method.Symbol.ReturnType.ToFullyQualifiedName()), SyntaxFactory.Identifier("Result"))
                    .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                    .WithAccessorList(SyntaxFactory.AccessorList(
                        SyntaxFactory.SingletonList(
                            SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                        )
                    ));
            }

            yield return SyntaxFactory
                .PropertyDeclaration(SyntaxFactory.ParseTypeName("System.Exception"), SyntaxFactory.Identifier("Exception"))
                .WithModifiers(SyntaxFactory.TokenList(SyntaxFactory.Token(SyntaxKind.PublicKeyword)))
                .WithAccessorList(SyntaxFactory.AccessorList(
                    SyntaxFactory.SingletonList(
                        SyntaxFactory.AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(SyntaxFactory.Token(SyntaxKind.SemicolonToken))
                    )
                ));
        }

        static ConstructorDeclarationSyntax GetConstructor(string className, PropertyDeclarationSyntax[] properties)
        {
            var constructorParameters = properties
                .Select(p =>
                    SyntaxFactory
                        .Parameter(SyntaxFactory.Identifier(p.Identifier.Text.ToCamelCase()))
                        .WithType(p.Type)
                ).ToArray();

            var constructorAssignments = properties
                .Select(p =>
                    SyntaxFactory.ExpressionStatement(
                        SyntaxFactory.AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            SyntaxFactory.IdentifierName(p.Identifier.Text),
                            SyntaxFactory.IdentifierName(p.Identifier.Text.ToCamelCase())
                        )
                    )
                ).ToArray();

            return SyntaxFactory
                .ConstructorDeclaration(className)
                .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(constructorParameters)
                .WithBody(SyntaxFactory.Block(SyntaxFactory.List(constructorAssignments)));
        }
    }
}