using System.Collections.Generic;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal partial class MethodImposterBuilder
{
    private static MethodDeclarationSyntax InvokeMethod(ImposterTargetMethodMetadata method)
    {
        return MethodDeclaration(
                SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType),
                Identifier("Invoke")
            )
            .WithParameterList(SyntaxFactoryHelper.ParameterListSyntax(method.Symbol.Parameters))
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)))
            .WithBody(new BlockBuilder()
                .AddStatementsIf(method.ParametersExceptOut.Count > 0, () => DeclareAndInitializeArgumentsVariable(method))
                .AddStatement(DeclareMatchingSetupVariable(method))
                .AddStatement(InvokeMatchingSetup(method))
                .Build()
            );
    }

    private static StatementSyntax DeclareAndInitializeArgumentsVariable(ImposterTargetMethodMetadata method) =>
        LocalDeclarationStatement(
            VariableDeclaration(SyntaxFactoryHelper.Var)
                .AddVariables(
                    VariableDeclarator(Identifier("arguments"))
                        .WithInitializer(
                            EqualsValueClause(
                                ObjectCreationExpression(
                                    method.ArgumentsType.Syntax,
                                    SyntaxFactoryHelper.ArgumentSyntaxList(method.ParametersExceptOut, includeRefKind: false),
                                    null
                                )
                            )
                        )
                )
        );

    // TODO Will need it later
    internal static InvocationExpressionSyntax CreateInvocationHistory(ImposterTargetMethodMetadata method, bool includeException)
    {
        return InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                IdentifierName("_invocationHistory"),
                IdentifierName("Add")
            ),
            ArgumentList(
                SingletonSeparatedList(
                    Argument(
                        ObjectCreationExpression(method.InvocationHistory.Syntax)
                            .WithArgumentList(
                                ArgumentList(
                                    SeparatedList(GetArguments())
                                )
                            )
                    )
                )
            )
        );

        IEnumerable<ArgumentSyntax> GetArguments()
        {
            if (method.ParametersExceptOut.Count > 0)
            {
                yield return Argument(IdentifierName("arguments"));
            }

            if (method.HasReturnValue)
            {
                yield return Argument(includeException ? LiteralExpression(SyntaxKind.NullLiteralExpression) : IdentifierName("result"));
            }

            if (includeException)
            {
                yield return Argument(IdentifierName("ex"));
            }
        }
    }

    private static StatementSyntax InvokeMatchingSetup(ImposterTargetMethodMetadata method)
    {
        var invokeExpression = InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                IdentifierName("matchingSetup"),
                IdentifierName("Invoke")
            )
        ).WithArgumentList(
            SyntaxFactoryHelper.ArgumentSyntaxList(method.Symbol.Parameters)
        );

        if (method.Symbol.ReturnsVoid)
        {
            return ExpressionStatement(invokeExpression);
        }

        return ReturnStatement(invokeExpression);
    }

    private static StatementSyntax DeclareMatchingSetupVariable(ImposterTargetMethodMetadata method)
    {
        return LocalDeclarationStatement(
            VariableDeclaration(
                    SyntaxFactoryHelper.Var
                )
                .AddVariables(
                    VariableDeclarator(Identifier("matchingSetup"))
                        .WithInitializer(
                            EqualsValueClause(
                                SyntaxFactoryHelper.CreateNullCoalescingOperator(
                                    InvocationExpression(
                                        IdentifierName("FindMatchingSetup"),
                                        method.ParametersExceptOut.Count == 0
                                            ? ArgumentList()
                                            : ArgumentList(SingletonSeparatedList(Argument(IdentifierName("arguments"))))
                                    ),
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        method.InvocationSetupType.Syntax,
                                        IdentifierName("DefaultInvocationSetup")
                                    )
                                )
                            )
                        )
                )
        );
    }
}