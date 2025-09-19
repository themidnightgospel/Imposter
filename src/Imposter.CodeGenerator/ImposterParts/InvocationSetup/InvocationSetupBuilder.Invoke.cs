using System.Collections.Generic;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    private static FieldDeclarationSyntax NextMethodCallSetupFieldDeclaration = FieldDeclaration(
            VariableDeclaration(
                    NullableType(IdentifierName(MethodInvocationSetupTypeName))
                )
                .WithVariables(
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier("_nextMethodCallSetupToInvoke"))
                    )
                )
        )
        .AddModifiers(
            Token(SyntaxKind.PrivateKeyword)
        );

    private static MethodDeclarationSyntax GetMethodDeclarationSyntax =>
        MethodDeclaration(
                NullableType(IdentifierName("MethodInvocationSetup")),
                Identifier("GetNextMethodCallSetupToInvoke")
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
                                    IdentifierName("_nextMethodCallSetupToInvoke"),
                                    IdentifierName("callSetup")
                                )
                            )
                        )
                    ),
                    ReturnStatement(
                        IdentifierName("_nextMethodCallSetupToInvoke")
                    )
                )
            );


    private static MethodDeclarationSyntax InvokeMethodDeclarationSyntax(
        IEnumerable<IParameterSymbol> parameters,
        ITypeSymbol returnType)
    {
        var parameterArgumentList = SyntaxFactoryHelper.ArgumentSyntaxList(parameters);

        var returnTypeSyntax = ParseTypeName(returnType.ToDisplayString());
        var qualifiedReturnTypeSyntax = ParseTypeName(returnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));

        // The method body statements
        var statements = new List<StatementSyntax>();

        // var nextMethodCallSetupToInvoke = GetNextMethodCallSetupToInvoke();
        statements.Add(
            LocalDeclarationStatement(
                VariableDeclaration(
                    IdentifierName("var"),
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier("nextMethodCallSetupToInvoke"))
                            .WithInitializer(
                                EqualsValueClause(
                                    InvocationExpression(IdentifierName("GetNextMethodCallSetupToInvoke"))
                                        .WithArgumentList(ArgumentList())
                                )
                            )
                    )
                )
            )
        );

        // if (nextMethodCallSetupToInvoke is null) { return default(int); }
        statements.Add(
            IfStatement(
                IsPatternExpression(
                    IdentifierName("nextMethodCallSetupToInvoke"),
                    ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression))
                ),
                Block(
                    ReturnStatement(DefaultExpression(returnTypeSyntax))
                )
            )
        );

        // if (nextMethodCallSetupToInvoke.CallBefore is not null) { ... }
        statements.Add(
            IfStatement(
                IsPatternExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("nextMethodCallSetupToInvoke"),
                        IdentifierName("CallBefore")
                    ),
                    UnaryPattern(ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression)))
                ),
                Block(
                    ExpressionStatement(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("nextMethodCallSetupToInvoke"),
                                IdentifierName("CallBefore")
                            ),
                            parameterArgumentList
                        )
                    )
                )
            )
        );

        // var result = nextMethodCallSetupToInvoke.ResultGenerator?.Invoke(a, b) ?? default(global::System.Int32);
        statements.Add(
            LocalDeclarationStatement(
                VariableDeclaration(
                    IdentifierName("var"),
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier("result"))
                            .WithInitializer(
                                EqualsValueClause(
                                    BinaryExpression(
                                        SyntaxKind.CoalesceExpression,
                                        ConditionalAccessExpression(MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("nextMethodCallSetupToInvoke"),
                                            IdentifierName("ResultGenerator")
                                        ), InvocationExpression(MemberBindingExpression(IdentifierName("Invoke")), parameterArgumentList)),
                                        DefaultExpression(qualifiedReturnTypeSyntax)
                                    )
                                )
                            )
                    )
                )
            )
        );

        // if (nextMethodCallSetupToInvoke.CallAfter is not null) { ... }
        statements.Add(
            IfStatement(
                IsPatternExpression(
                    MemberAccessExpression(
                        SyntaxKind.SimpleMemberAccessExpression,
                        IdentifierName("nextMethodCallSetupToInvoke"),
                        IdentifierName("CallAfter")
                    ),
                    UnaryPattern(ConstantPattern(LiteralExpression(SyntaxKind.NullLiteralExpression)))
                ),
                Block(
                    ExpressionStatement(
                        InvocationExpression(
                            MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression,
                                IdentifierName("nextMethodCallSetupToInvoke"),
                                IdentifierName("CallAfter")
                            ),
                            parameterArgumentList
                        )
                    )
                )
            )
        );

        // return result;
        statements.Add(ReturnStatement(IdentifierName("result")));

        // Build the final method declaration
        return MethodDeclaration(
                returnTypeSyntax,
                Identifier("Invoke")
            ).WithModifiers(
                TokenList(Token(SyntaxKind.PublicKeyword))
            ).WithParameterList(SyntaxFactoryHelper.ParameterListSyntax(parameters))
            .WithBody(Block(statements));
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