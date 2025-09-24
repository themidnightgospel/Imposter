using System.Collections.Generic;
using Imposter.CodeGenerator.Helpers;
using Imposter.CodeGenerator.Helpers.SyntaxBuilders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.MethodImposter;

internal partial class MethodImposterBuilder
{
    internal static MethodDeclarationSyntax InvokeMethod(ImposterTargetMethod method)
    {
        return MethodDeclaration(
                SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType),
                Identifier("Invoke")
            )
            .WithParameterList(SyntaxFactoryHelper.ParameterListSyntax(method.Symbol.Parameters))
            .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword)))
            .WithBody(new BlockBuilder()
                .AddStatementsIf(method.ParametersExceptOut.Count > 0, () => DeclareAndInitializeArgumentsVariable(method))
                .AddStatement(DeclareMatchingSetupVariable(method))
                .AddStatementsIf(method.ParametersExceptOut.Count > 0, FindMatchingSetupSyntax)
                .AddStatement(IfMatchingSetupIsNullAssignDefault(method))
                .AddStatement(InvokeMatchingSetupAndRecordHistory(method))
                .Build()
            );
    }

    private static StatementSyntax DeclareAndInitializeArgumentsVariable(ImposterTargetMethod method) =>
        LocalDeclarationStatement(
            VariableDeclaration(IdentifierName(method.ArgumentsClassName))
                .AddVariables(
                    VariableDeclarator(Identifier("arguments"))
                        .WithInitializer(
                            EqualsValueClause(
                                ObjectCreationExpression(
                                    IdentifierName(method.ArgumentsClassName),
                                    SyntaxFactoryHelper.ArgumentSyntaxList(method.ParametersExceptOut, includeRefKind: false),
                                    null
                                )
                            )
                        )
                )
        );

    internal static InvocationExpressionSyntax CreateInvocationHistory(ImposterTargetMethod method, bool includeException)
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
                        ObjectCreationExpression(IdentifierName(method.MethodInvocationHistoryClassName))
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

    internal static TryStatementSyntax InvokeMatchingSetupAndRecordHistory(ImposterTargetMethod method)
    {
        return TryStatement(
            new BlockBuilder()
                .AddStatement(InvokeMatchingSetup(method))
                .AddStatement(ExpressionStatement(CreateInvocationHistory(method, false)))
                .AddStatementsIf(method.HasReturnValue, () => ReturnStatement(IdentifierName("result")))
                .Build(),
            SingletonList(
                CatchClause(
                    CatchDeclaration(IdentifierName("Exception"), Identifier("ex")),
                    null,
                    Block(
                        ExpressionStatement(CreateInvocationHistory(method, true)),
                        ThrowStatement()
                    )
                )
            ),
            null
        );
    }

    internal static StatementSyntax InvokeMatchingSetup(ImposterTargetMethod method)
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

        return LocalDeclarationStatement(
            VariableDeclaration(IdentifierName("var"))
                .AddVariables(
                    VariableDeclarator(Identifier("result"))
                        .WithInitializer(EqualsValueClause(invokeExpression))
                )
        );
    }

    internal static StatementSyntax IfMatchingSetupIsNullAssignDefault(ImposterTargetMethod method) =>
        IfStatement(
            BinaryExpression(
                SyntaxKind.EqualsExpression,
                IdentifierName("matchingSetup"),
                LiteralExpression(SyntaxKind.NullLiteralExpression)
            ),
            Block(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("matchingSetup"),
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName(method.InvocationsSetupBuilder),
                            IdentifierName("DefaultInvocationSetup")
                        )
                    )
                )
            )
        );

    private static StatementSyntax DeclareMatchingSetupVariable(ImposterTargetMethod method)
    {
        return LocalDeclarationStatement(
            VariableDeclaration(NullableType(IdentifierName(method.InvocationsSetupBuilder)))
                .AddVariables(
                    VariableDeclarator(Identifier("matchingSetup"))
                        .WithInitializer(EqualsValueClause(
                            method.ParametersExceptOut.Count == 0
                                ? ConditionalExpression(
                                    BinaryExpression(
                                        SyntaxKind.EqualsExpression,
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("_invocationSetups"),
                                            IdentifierName("Count")
                                        ),
                                        LiteralExpression(
                                            SyntaxKind.NumericLiteralExpression,
                                            Literal(0)
                                        )
                                    ),
                                    LiteralExpression(
                                        SyntaxKind.NullLiteralExpression
                                    ),
                                    ElementAccessExpression(
                                        IdentifierName("_invocationSetups"),
                                        BracketedArgumentList(
                                            SingletonSeparatedList(
                                                Argument(
                                                    BinaryExpression(
                                                        SyntaxKind.SubtractExpression,
                                                        MemberAccessExpression(
                                                            SyntaxKind.SimpleMemberAccessExpression,
                                                            IdentifierName("_invocationSetups"),
                                                            IdentifierName("Count")
                                                        ),
                                                        LiteralExpression(
                                                            SyntaxKind.NumericLiteralExpression,
                                                            Literal(1)
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                    )
                                )
                                : LiteralExpression(SyntaxKind.NullLiteralExpression)))
                )
        );
    }

    // TODO cache
    internal static StatementSyntax FindMatchingSetupSyntax()
    {
        // The for loop's body
        var forLoopBody = Block(
            // var setup = _invocationSetups[i];
            LocalDeclarationStatement(
                VariableDeclaration(IdentifierName("var"))
                    .AddVariables(
                        VariableDeclarator(Identifier("setup"))
                            .WithInitializer(
                                EqualsValueClause(
                                    ElementAccessExpression(
                                        IdentifierName("_invocationSetups")
                                    ).WithArgumentList(
                                        BracketedArgumentList(
                                            SingletonSeparatedList(
                                                Argument(IdentifierName("i"))
                                            )
                                        )
                                    )
                                )
                            )
                    )
            ),
            // if (setup.ArgArguments.Matches(arguments)) { ... }
            IfStatement(
                InvocationExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("setup"),
                            IdentifierName("ArgumentsCriteria")
                        ),
                        IdentifierName("Matches")
                    )
                ).WithArgumentList(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(IdentifierName("arguments"))
                        )
                    )
                ),
                Block(
                    ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            IdentifierName("matchingSetup"),
                            IdentifierName("setup")
                        )
                    ),
                    BreakStatement()
                )
            )
        );

        // The full method body, including the for loop and final return null
        return ForStatement(
            VariableDeclaration(PredefinedType(Token(SyntaxKind.IntKeyword)))
                .AddVariables(
                    VariableDeclarator(Identifier("i"))
                        .WithInitializer(
                            EqualsValueClause(
                                BinaryExpression(
                                    SyntaxKind.SubtractExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("_invocationSetups"),
                                        IdentifierName("Count")
                                    ),
                                    LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(1))
                                )
                            )
                        )
                ),
            default,
            BinaryExpression(
                SyntaxKind.GreaterThanOrEqualExpression,
                IdentifierName("i"),
                LiteralExpression(SyntaxKind.NumericLiteralExpression, Literal(0))
            ),
            SingletonSeparatedList<ExpressionSyntax>(
                PostfixUnaryExpression(
                    SyntaxKind.PostDecrementExpression,
                    IdentifierName("i")
                )
            ),
            forLoopBody
        );
    }
}