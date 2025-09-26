using System.Collections.Generic;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetup
{
    private static FieldDeclarationSyntax NextMethodCallSetupFieldDeclaration = FieldDeclaration(
            VariableDeclaration(
                    NullableType(IdentifierName(MethodInvocationSetupTypeName))
                )
                .WithVariables(
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier("_nextSetup"))
                    )
                )
        )
        .AddModifiers(
            Token(SyntaxKind.PrivateKeyword)
        );

    private static MethodDeclarationSyntax GetMethodDeclarationSyntax =>
        MethodDeclaration(
                NullableType(IdentifierName("MethodInvocationSetup")),
                Identifier("GetnextSetup")
            )
            .WithModifiers(TokenList(Token(SyntaxKind.PrivateKeyword)))
            .WithBody(
                Block(
                    IfStatement(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("_callSetups"),
                                IdentifierName("TryDequeue")
                            ),
                            ArgumentList(
                                SingletonSeparatedList(
                                    Argument(
                                        DeclarationExpression(
                                            IdentifierName("var"),
                                            SingleVariableDesignation(Identifier("callSetup"))
                                        )
                                    ).WithRefKindKeyword(Token(SyntaxKind.OutKeyword))
                                )
                            )
                        ),
                        Block(
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    IdentifierName("_nextSetup"),
                                    IdentifierName("callSetup")
                                )
                            )
                        )
                    ),
                    ReturnStatement(
                        IdentifierName("_nextSetup")
                    )
                )
            );

    private static StatementSyntax InitializenextSetup = LocalDeclarationStatement(
        VariableDeclaration(
            IdentifierName("var"),
            SingletonSeparatedList(
                VariableDeclarator(Identifier("nextSetup"))
                    .WithInitializer(
                        EqualsValueClause(
                            BinaryExpression(
                                SyntaxKind.CoalesceExpression,
                                InvocationExpression(IdentifierName("GetnextSetup"))
                                    .WithArgumentList(ArgumentList()),
                                ThrowExpression(
                                    ObjectCreationExpression(IdentifierName("InvalidOperationException"))
                                        .WithArgumentList(
                                            ArgumentList(
                                                SingletonSeparatedList(
                                                    Argument(
                                                        LiteralExpression(
                                                            SyntaxKind.StringLiteralExpression,
                                                            Literal("Invalid Setup")
                                                        )
                                                    )
                                                )
                                            )
                                        )
                                )
                            )
                        )
                    )
            )
        )
    );

    private static StatementSyntax InvokeCallBefore(ArgumentListSyntax parameterArgumentList) =>
        IfStatement(
            IsPatternExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("nextSetup"),
                    IdentifierName("CallBefore")
                ),
                UnaryPattern(ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression)))
            ),
            Block(
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("nextSetup"),
                            IdentifierName("CallBefore")
                        ),
                        parameterArgumentList
                    )
                )
            )
        );

    private static StatementSyntax InvokeCallAfter(ArgumentListSyntax parameterArgumentList) =>
        IfStatement(
            IsPatternExpression(
                MemberAccessExpression(
                    SyntaxKind.SimpleMemberAccessExpression,
                    IdentifierName("nextSetup"),
                    IdentifierName("CallAfter")
                ),
                UnaryPattern(ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression)))
            ),
            Block(
                ExpressionStatement(
                    InvocationExpression(
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            IdentifierName("nextSetup"),
                            IdentifierName("CallAfter")
                        ),
                        parameterArgumentList
                    )
                )
            )
        );

    private static IEnumerable<StatementSyntax> InvokeResultGenerator(ImposterTargetMethod method)
    {
        var callInvokeMethodExpression = InvocationExpression(
            MemberAccessExpression(
                SyntaxKind.SimpleMemberAccessExpression,
                IdentifierName("nextSetup.ResultGenerator"),
                IdentifierName("Invoke")
            )
        ).WithArgumentList(
            SyntaxFactoryHelper.ArgumentSyntaxList(method.Symbol.Parameters)
        );

        yield return IfStatement(
            IsPatternExpression(
                IdentifierName("nextSetup.ResultGenerator"),
                ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression))
            ),
            Block(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        IdentifierName("nextSetup.ResultGenerator"),
                        IdentifierName("DefaultResultGenerator")
                    )
                )
            )
        );

        yield return method.Symbol.ReturnType.SpecialType is not SpecialType.System_Void
                ? LocalDeclarationStatement(
                    VariableDeclaration(
                        IdentifierName("var"),
                        SingletonSeparatedList(
                            VariableDeclarator(Identifier("result"))
                                .WithInitializer(
                                    EqualsValueClause(callInvokeMethodExpression)
                                )
                        )
                    )
                )
                : ExpressionStatement(callInvokeMethodExpression)
            ;
    }

    private static MethodDeclarationSyntax InvokeMethodDeclarationSyntax(ImposterTargetMethod method)
    {
        var parameterArgumentList = SyntaxFactoryHelper.ArgumentSyntaxList(method.Symbol.Parameters);
        return MethodDeclaration(
                SyntaxFactoryHelper.TypeSyntax(method.Symbol.ReturnType),
                Identifier("Invoke")
            ).WithModifiers(
                TokenList(Token(SyntaxKind.PublicKeyword))
            ).WithParameterList(SyntaxFactoryHelper.ParameterListSyntax(method.Symbol.Parameters))
            .WithBody(new BlockBuilder()
                .AddStatement(InitializenextSetup)
                .AddStatement(InvokeCallBefore(parameterArgumentList))
                .AddStatements(InvokeResultGenerator(method))
                .AddStatement(InvokeCallAfter(parameterArgumentList))
                .AddStatement(method.Symbol.ReturnType.SpecialType is SpecialType.System_Void
                    ? EmptyStatement()
                    : ReturnStatement(IdentifierName("result")))
                .Build());
    }

    public static ClassDeclarationSyntax NestedMethodInvocationSetupType(ImposterTargetMethod method)
    {
        return ClassDeclaration("MethodInvocationSetup")
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddMembers(PropertyDeclaration(
                IdentifierName(method.DelegateName),
                Identifier("ResultGenerator")
            ).AddModifiers(Token(SyntaxKind.InternalKeyword)).AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            ), PropertyDeclaration(
                IdentifierName(method.CallbackDelegateName),
                Identifier("CallBefore")
            ).AddModifiers(Token(SyntaxKind.InternalKeyword)).AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            ), PropertyDeclaration(
                IdentifierName(method.CallbackDelegateName),
                Identifier("CallAfter")
            ).AddModifiers(Token(SyntaxKind.InternalKeyword)).AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            ));
    }
}