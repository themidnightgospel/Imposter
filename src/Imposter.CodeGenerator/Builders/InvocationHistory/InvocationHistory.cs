using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationHistory;

public static class InvocationHistory
{
    internal static ClassDeclarationSyntax Build(ImposterTargetMethodMetadata method)
    {
        var properties = GetProperties(method).ToArray();

        return ClassDeclaration(method.InvocationHistoryType.Name)
            .WithTypeParameterList(SyntaxFactoryHelper.TypeParameterList(method.Symbol))
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .AddMembers(properties)
            .AddMembers(GetConstructor(method.InvocationHistoryType.Name, properties))
            .WithLeadingTriviaComment(method.DisplayName);

        static IEnumerable<PropertyDeclarationSyntax> GetProperties(ImposterTargetMethodMetadata method)
        {
            if (method.ParametersExceptOut.Count > 0)
            {
                yield return PropertyDeclaration(method.ArgumentsType.Syntax, Identifier("Arguments"))
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
                .Select(it =>
                    Parameter(Identifier(it.Identifier.Text.ToCamelCase()))
                        .WithType(it.Type)
                        .WithDefault(it.Type is NullableTypeSyntax
                            ? EqualsValueClause(
                                DefaultExpression(it.Type)
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