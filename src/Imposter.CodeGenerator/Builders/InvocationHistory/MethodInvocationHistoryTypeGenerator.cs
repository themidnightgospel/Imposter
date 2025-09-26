using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationHistory;

public static class MethodInvocationHistoryBuilder
{
    internal static ClassDeclarationSyntax Build(ImposterTargetMethod method)
    {
        var properties = GetProperties(method).ToArray();

        return ClassDeclaration(method.MethodInvocationHistoryClassName)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .AddMembers(properties)
            .AddMembers(GetConstructor(method.MethodInvocationHistoryClassName, properties))
            .WithLeadingTriviaComment(method.DisplayName);

        static IEnumerable<PropertyDeclarationSyntax> GetProperties(ImposterTargetMethod method)
        {
            if (method.ParametersExceptOut.Count > 0)
            {
                yield return PropertyDeclaration(ParseTypeName(method.ArgumentsClassName), Identifier("Arguments"))
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithAccessorList(AccessorList(
                        SingletonList(
                            AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                        )
                    ));
            }

            if (method.HasReturnValue)
            {
                var resultTypeSyntax = SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType);

                yield return PropertyDeclaration(
                        resultTypeSyntax is NullableTypeSyntax
                            ? resultTypeSyntax
                            : NullableType(resultTypeSyntax),
                        Identifier("Result"))
                    .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                    .WithAccessorList(AccessorList(
                        SingletonList(
                            AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                                .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                        )
                    ));
            }

            yield return PropertyDeclaration(
                    NullableType(WellKnownTypes.System.Exception), Identifier("Exception"))
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                .WithAccessorList(AccessorList(
                    SingletonList(
                        AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
                    )
                ));
        }

        static ConstructorDeclarationSyntax GetConstructor(string className, PropertyDeclarationSyntax[] properties)
        {
            var constructorParameters = properties
                .Select(p =>
                    Parameter(Identifier(p.Identifier.Text.ToCamelCase()))
                        .WithType(p.Type)
                        .WithDefault(p.Type is NullableTypeSyntax
                            ? EqualsValueClause(
                                LiteralExpression(
                                    SyntaxKind.NullLiteralExpression
                                )
                            )
                            : null)
                ).ToArray();

            var constructorAssignments = properties
                .Select(p =>
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName(p.Identifier.Text),
                            IdentifierName(p.Identifier.Text.ToCamelCase())
                        )
                    )
                ).ToArray();

            return ConstructorDeclaration(className)
                .AddModifiers(Token(SyntaxKind.PublicKeyword))
                .AddParameterListParameters(constructorParameters)
                .WithBody(Block(List(constructorAssignments)));
        }
    }
}