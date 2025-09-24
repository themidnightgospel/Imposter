using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Helpers.SyntaxBuilders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts;

internal static class ImposterBuilder
{
    internal static ClassDeclarationSyntax Build(ImposterTarget imposterTarget)
    {
        return new ClassDeclarationBuilder(imposterTarget.ImposterClass.Name)
            .AddMembers(MethodImposterFields(imposterTarget))
            .AddMembers(Constructor(imposterTarget))
            .AddMembers(ImposterMethods(imposterTarget))
            .Build(modifiers: TokenList(Token(SyntaxKind.PublicKeyword)));
    }

    private static IEnumerable<MethodDeclarationSyntax> ImposterMethods(ImposterTarget imposterTarget)
    {
        foreach (var imposterMethod in imposterTarget.Methods)
        {
            yield return MethodDeclaration(
                    IdentifierName(imposterMethod.MethodImposter.BuilderInterfaceName),
                    Identifier(imposterMethod.Symbol.Name)
                )
                .WithParameterList(SyntaxFactoryHelper.ArgParameters(imposterMethod.Symbol.Parameters))
                .WithBody(Block(ReturnStatement(
                            ObjectCreationExpression(
                                IdentifierName($"{imposterMethod.MethodImposter.Name}.Builder"),
                                new ArgumentListBuilder()
                                    .AddArgument(Argument(IdentifierName(imposterMethod.MethodImposter.DeclaredAsFieldName)))
                                    .AddArgumentIf(imposterMethod.ParametersExceptOut.Count > 0, () => Argument(SyntaxFactoryHelper.ArgumentCriteriaCreationExpression(imposterMethod)))
                                    .Build(),
                                null
                            )
                        )
                    )
                )
                .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)));
        }
    }

    private static IEnumerable<ConstructorDeclarationSyntax> Constructor(ImposterTarget imposterTarget)
    {
        yield return ConstructorDeclaration(imposterTarget.ImposterClass.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithBody(new BlockBuilder()
                .AddStatements(
                    imposterTarget
                        .Methods
                        .Select(method => ExpressionStatement(
                            AssignmentExpression(
                                SyntaxKind.SimpleAssignmentExpression,
                                MemberAccessExpression(
                                    SyntaxKind.SimpleMemberAccessExpression,
                                    ThisExpression(),
                                    IdentifierName(method.MethodImposter.DeclaredAsFieldName)
                                ),
                                ObjectCreationExpression(IdentifierName(method.MethodImposter.Name))
                                    .WithArgumentList(ArgumentList())
                            )
                        ))
                )
                .Build());
    }

    private static IEnumerable<FieldDeclarationSyntax> MethodImposterFields(ImposterTarget imposterTarget)
    {
        foreach (var method in imposterTarget.Methods)
        {
            yield return FieldDeclaration(
                    VariableDeclaration(IdentifierName(method.MethodImposter.Name))
                        .WithVariables(
                            SingletonSeparatedList(
                                VariableDeclarator(Identifier(method.MethodImposter.DeclaredAsFieldName))
                            ))
                )
                .AddModifiers(
                    Token(SyntaxKind.PrivateKeyword)
                );
        }
    }
}