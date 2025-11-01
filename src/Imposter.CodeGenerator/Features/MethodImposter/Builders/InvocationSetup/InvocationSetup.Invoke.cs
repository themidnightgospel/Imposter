using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
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

    private static MethodDeclarationSyntax BuildGetNextSetupMethod(in ImposterTargetMethodMetadata method)
    {
        return MethodDeclaration(
                NullableType(IdentifierName("MethodInvocationSetup")),
                Identifier(method.InvocationSetup.GetNextSetupMethod.Name)
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
                                            Var,
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
    }

    private static LocalDeclarationStatementSyntax InitializeNextSetup(in ImposterTargetMethodMetadata method)
    {
        return LocalDeclarationStatement(
            VariableDeclaration(
                Var,
                SingletonSeparatedList(
                    VariableDeclarator(Identifier(method.InvocationSetup.InvokeMethod.NextSetupVariableName))
                        .WithInitializer(
                            EqualsValueClause(
                                BinaryExpression(
                                    SyntaxKind.CoalesceExpression,
                                    InvocationExpression(IdentifierName(method.InvocationSetup.GetNextSetupMethod.Name))
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
    }

    private static IfStatementSyntax InvokeCallBefore(in ImposterTargetMethodMetadata method)
    {
        return IfStatement(
            IdentifierName(method.InvocationSetup.InvokeMethod.NextSetupVariableName).Dot(IdentifierName("CallBefore")).IsNotNull(),
            Block(
                IdentifierName(method.InvocationSetup.InvokeMethod.NextSetupVariableName)
                    .Dot(IdentifierName("CallBefore"))
                    .Call(method.Parameters.ParametersAsArgumentListSyntaxWithRef)
                    .AwaitIf(method.IsAsync)
                    .ToStatementSyntax()
            )
        );
    }

    private static IfStatementSyntax InvokeCallAfter(in ImposterTargetMethodMetadata method) =>
        IfStatement(IdentifierName(method.InvocationSetup.InvokeMethod.NextSetupVariableName)
                .Dot(IdentifierName("CallAfter"))
                .IsNotNull(),
            Block(
                IdentifierName(method.InvocationSetup.InvokeMethod.NextSetupVariableName)
                    .Dot(IdentifierName("CallAfter"))
                    .Call(method.Parameters.ParametersAsArgumentListSyntaxWithRef)
                    .AwaitIf(method.IsAsync)
                    .ToStatementSyntax()
            )
        );


    private static IfStatementSyntax SetDefaultGeneratorIfNull(in ImposterTargetMethodMetadata method)
    {
        return IfStatement(
            IdentifierName(method.InvocationSetup.InvokeMethod.NextSetupVariableName).Dot(IdentifierName("ResultGenerator")).IsNull(),
            Block(
                IdentifierName(method.InvocationSetup.InvokeMethod.NextSetupVariableName)
                    .Dot(IdentifierName("ResultGenerator"))
                    .Assign(IdentifierName("DefaultResultGenerator"))
                    .ToStatementSyntax()
            )
        );
    }

    private static StatementSyntax InvokeResultGenerator(in ImposterTargetMethodMetadata method)
    {
        var callInvokeMethodExpression = IdentifierName(method.InvocationSetup.InvokeMethod.NextSetupVariableName)
            .Dot(IdentifierName("ResultGenerator"))
            .Dot(IdentifierName("Invoke"))
            .Call(ArgumentListSyntax(method.Symbol.Parameters))
            .AwaitIf(method.IsAsync);

        return method.HasReturnValue
            ? LocalDeclarationStatement(VariableDeclarationSyntax(Var, method.InvocationSetup.InvokeMethod.ResultVariableName, callInvokeMethodExpression))
            : ExpressionStatement(callInvokeMethodExpression);
    }

    private static MethodDeclarationSyntax InvokeMethodDeclarationSyntax(in ImposterTargetMethodMetadata method) =>
        new MethodDeclarationBuilder(method.ReturnTypeSyntax, method.InvocationSetup.InvokeMethod.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifierIf(method.IsAsync, () => Token(SyntaxKind.AsyncKeyword))
            .WithParameterList(method.Parameters.ParameterListSyntax)
            .WithBody(new BlockBuilder()
                .AddStatement(InitializeNextSetup(method))
                .AddStatement(InvokeCallBefore(method))
                .AddStatement(SetDefaultGeneratorIfNull(method))
                .AddStatement(InvokeResultGenerator(method))
                .AddStatement(InvokeCallAfter(method))
                .AddStatement(method.Symbol.ReturnType.SpecialType is SpecialType.System_Void
                    ? EmptyStatement()
                    : ReturnStatement(IdentifierName(method.InvocationSetup.InvokeMethod.ResultVariableName)))
                .Build())
            .Build();

    private static ClassDeclarationSyntax NestedMethodInvocationSetupType(in ImposterTargetMethodMetadata method)
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