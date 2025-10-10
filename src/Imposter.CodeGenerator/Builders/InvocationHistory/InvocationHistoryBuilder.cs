using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationHistory;

public static partial class InvocationHistoryBuilder
{
    internal static IEnumerable<MemberDeclarationSyntax> Build(ImposterTargetMethodMetadata method)
    {
        var properties = GetProperties(method).ToArray();

        yield return BuildHistoryInterface(method);

        var historyClass = ClassDeclaration(method.InvocationHistory.Name)
            .WithTypeParameterList(SyntaxFactoryHelper.TypeParameterList(method.Symbol))
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .AddMembers(properties)
            .AddMembers(GetConstructor(method.InvocationHistory.Name, properties))
            .WithLeadingTriviaComment(method.DisplayName)
            .WithBaseList(BaseList(SingletonSeparatedList<BaseTypeSyntax>(SimpleBaseType(method.InvocationHistory.Interface.Syntax))))
            .AddMembers(BuildMatchesMethodForHistoryClass(method));

        yield return historyClass;

        yield return BuildInvocationHistoryCollectionClass(method);

        static MemberDeclarationSyntax BuildMatchesMethodForHistoryClass(ImposterTargetMethodMetadata method)
        {
            // TODO optimize
            var criteriaTypeParameters = method
                .TargetGenericTypeArguments
                .Select(it => TypeParameter(it.Identifier));

            return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), "Matches")
                .AddModifier(Token(SyntaxKind.PublicKeyword))
                .AddTypeParameters(criteriaTypeParameters)
                .AddParameterIf(method.HasInputParameters, () => SyntaxFactoryHelper.ParameterSyntax(method.ArgumentsCriteriaType.Syntax, "criteria"))
                .WithBody(Block(
                    ReturnStatement(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                method.Symbol.IsGenericMethod
                                    ? InvocationExpression(
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("criteria"),
                                            GenericName(Identifier("As"), method.GenericTypeArguments.AsTypeArguments())
                                        )
                                    )
                                    : IdentifierName("criteria")
                                , IdentifierName("Matches")
                            ),
                            ArgumentList(SingletonSeparatedList(Argument(IdentifierName("Arguments"))))
                        )
                    )
                ))
                .Build();
        }

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