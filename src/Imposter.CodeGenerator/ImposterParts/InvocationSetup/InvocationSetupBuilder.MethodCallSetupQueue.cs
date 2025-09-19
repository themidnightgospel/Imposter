using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    private static FieldDeclarationSyntax CallSetupsFieldDeclaration = GetCallSetupsFieldDeclaration();

    private static FieldDeclarationSyntax CurrentlySetupCallFieldDeclaration = FieldDeclaration(
            VariableDeclaration(
                    NullableType(IdentifierName("MethodInvocationSetup"))
                )
                .WithVariables(
                    SingletonSeparatedList(
                        VariableDeclarator(Identifier("_currentlySetupCall"))
                    )
                )
        )
        .AddModifiers(
            Token(SyntaxKind.PrivateKeyword)
        );

    private static FieldDeclarationSyntax GetCallSetupsFieldDeclaration()
    {
        var concurrentQueueTypeName = GenericName(Identifier("Queue"))
            .WithTypeArgumentList(
                TypeArgumentList(
                    SingletonSeparatedList<TypeSyntax>(
                        IdentifierName("MethodInvocationSetup")
                    )
                )
            );

        return FieldDeclaration(
                VariableDeclaration(concurrentQueueTypeName)
                    .WithVariables(
                        SingletonSeparatedList(
                            VariableDeclarator(Identifier("_callSetups"))
                                .WithInitializer(
                                    EqualsValueClause(
                                        ObjectCreationExpression(concurrentQueueTypeName)
                                            .WithArgumentList(ArgumentList())
                                    )
                                )
                        )
                    )
            )
            .AddModifiers(
                Token(SyntaxKind.PrivateKeyword),
                Token(SyntaxKind.ReadOnlyKeyword)
            );
    }

    private static InvocationExpressionSyntax GetMethodCallSetupForCallbackSyntax =
        InvocationExpression(
            IdentifierName("GetMethodCallSetup"),
            ArgumentList(
                SingletonSeparatedList(
                    Argument(
                        SimpleLambdaExpression(
                            Parameter(Identifier("it")),
                            LiteralExpression(
                                SyntaxKind.FalseLiteralExpression,
                                Token(SyntaxKind.FalseKeyword)
                            )
                        )
                    )
                )
            )
        );

    private static MethodDeclarationSyntax GetMethodCallSetupDeclarationSyntax =
        MethodDeclaration(
                IdentifierName("MethodInvocationSetup"), // return type
                Identifier("GetMethodCallSetup") // method name
            )
            .AddModifiers(Token(SyntaxKind.PrivateKeyword))
            .AddParameterListParameters(
                Parameter(Identifier("addNew"))
                    .WithType(
                        GenericName(Identifier("Func"))
                            .WithTypeArgumentList(
                                TypeArgumentList(
                                    SeparatedList<TypeSyntax>(new SyntaxNodeOrToken[]
                                    {
                                        IdentifierName("MethodInvocationSetup"),
                                        Token(SyntaxKind.CommaToken),
                                        PredefinedType(Token(SyntaxKind.BoolKeyword))
                                    })
                                )
                            )
                    )
            )
            .WithBody(
                Block(
                    IfStatement(
                        BinaryExpression(
                            SyntaxKind.LogicalOrExpression,
                            BinaryExpression(
                                SyntaxKind.IsExpression,
                                IdentifierName("_currentlySetupCall"),
                                LiteralExpression(SyntaxKind.NullLiteralExpression)
                            ),
                            InvocationExpression(
                                IdentifierName("addNew"),
                                ArgumentList(
                                    SingletonSeparatedList(Argument(IdentifierName("_currentlySetupCall")))
                                )
                            )
                        ),
                        Block(
                            ExpressionStatement(
                                AssignmentExpression(
                                    SyntaxKind.SimpleAssignmentExpression,
                                    IdentifierName("_currentlySetupCall"),
                                    ObjectCreationExpression(IdentifierName("MethodInvocationSetup"))
                                        .WithArgumentList(ArgumentList())
                                )
                            ),
                            ExpressionStatement(
                                InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("_callSetups"),
                                        IdentifierName("Enqueue")
                                    ),
                                    ArgumentList(
                                        SingletonSeparatedList(
                                            Argument(IdentifierName("_currentlySetupCall"))
                                        )
                                    )
                                )
                            )
                        )
                    ),
                    ReturnStatement(IdentifierName("_currentlySetupCall"))
                )
            );
}