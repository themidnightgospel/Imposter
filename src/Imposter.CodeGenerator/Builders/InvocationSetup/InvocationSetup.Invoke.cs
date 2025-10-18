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
                    IdentifierName(MethodInvocationSetupTypeName)
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
                Identifier("GetNextSetup")
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
                                InvocationExpression(IdentifierName("GetNextSetup"))
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

    private static StatementSyntax InvokeCallBefore(in ArgumentListSyntax parameterArgumentList) =>
        IfStatement(
            IdentifierName("nextSetup").Dot(IdentifierName("CallBefore")).IsNotNull(),
            Block(
                IdentifierName("nextSetup")
                    .Dot(IdentifierName("CallBefore"))
                    .Call(parameterArgumentList)
                    .AsStatement()
            )
        );

    private static StatementSyntax InvokeCallAfter(in ArgumentListSyntax parameterArgumentList) =>
        IfStatement(IdentifierName("nextSetup")
                .Dot(IdentifierName("CallAfter"))
                .IsNotNull(),
            Block(
                IdentifierName("nextSetup")
                    .Dot(IdentifierName("CallAfter"))
                    .Call(parameterArgumentList)
                    .AsStatement()
            )
        );

    private static IEnumerable<StatementSyntax> InvokeResultGenerator(in ImposterTargetMethodMetadata method)
    {
        var callInvokeMethodExpression = InvocationExpression(
            IdentifierName("nextSetup")
                .Dot(IdentifierName("ResultGenerator"))
                .Dot(IdentifierName("Invoke"))
        ).WithArgumentList(
            SyntaxFactoryHelper.ArgumenstListSyntax(method.Symbol.Parameters)
        );

        yield return IfStatement(
            IdentifierName("nextSetup").Dot(IdentifierName("ResultGenerator")).IsNull(),
            Block(
                IdentifierName("nextSetup")
                    .Dot(IdentifierName("ResultGenerator"))
                    .Assign(IdentifierName("DefaultResultGenerator"))
                    .AsStatement()
            )
        );

        yield return method.HasReturnValue
                ? LocalDeclarationStatement(SyntaxFactoryHelper.VariableDeclarationSyntax(SyntaxFactoryHelper.Var, "result", EqualsValueClause(callInvokeMethodExpression)))
                : ExpressionStatement(callInvokeMethodExpression);
    }

    private static MethodDeclarationSyntax InvokeMethodDeclarationSyntax(in ImposterTargetMethodMetadata method)
    {
        var parameterArgumentList = SyntaxFactoryHelper.ArgumenstListSyntax(method.Symbol.Parameters);
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

    public static ClassDeclarationSyntax NestedMethodInvocationSetupType(in ImposterTargetMethodMetadata method)
    {
        return ClassDeclaration("MethodInvocationSetup")
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddMembers(PropertyDeclaration(
                NullableType(method.Delegate.Syntax),
                Identifier("ResultGenerator")
            ).AddModifiers(Token(SyntaxKind.InternalKeyword)).AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            ), PropertyDeclaration(
                NullableType(method.CallbackDelegate.Syntax),
                Identifier("CallBefore")
            ).AddModifiers(Token(SyntaxKind.InternalKeyword)).AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            ), PropertyDeclaration(
                NullableType(method.CallbackDelegate.Syntax),
                Identifier("CallAfter")
            ).AddModifiers(Token(SyntaxKind.InternalKeyword)).AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
                AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            ));
    }
}