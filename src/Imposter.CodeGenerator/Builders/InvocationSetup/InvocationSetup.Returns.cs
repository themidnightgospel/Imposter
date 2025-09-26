using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetup
{
    private static MethodDeclarationSyntax ReturnsMethodDeclarationSyntax(ImposterTargetMethod method) =>
        MethodDeclaration(
                IdentifierName(method.InvocationsSetupInterface),
                Identifier("Returns")
            )
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(
                Parameter(Identifier("resultGenerator"))
                    .WithType(IdentifierName(method.DelegateName))
            )
            .WithBody(Block(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        GetMethodCallSetupAccessExpressionSyntax("ResultGenerator"),
                        IdentifierName("resultGenerator"))
                ),
                ReturnStatement(ThisExpression())
            ));

    private static MethodDeclarationSyntax ReturnsValueMethodDeclarationSyntax(ImposterTargetMethod method)
    {
        return MethodDeclaration(
                IdentifierName(method.InvocationsSetupInterface),
                Identifier("Returns")
            )
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(
                Parameter(Identifier("value"))
                    .WithType(TypeSyntax(method.Symbol.ReturnType))
            )
            .WithBody(Block(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        GetMethodCallSetupAccessExpressionSyntax("ResultGenerator"),
                        Lambda(method.Symbol.Parameters,
                            new BlockBuilder()
                                .AddStatementsIf(method.HasOutParameters, () => InvokeInitializeOutParametersWithDefaultValues(method.Symbol.Parameters))
                                .AddStatement(ReturnStatement(IdentifierName("value")))
                                .Build())
                    )),
                ReturnStatement(ThisExpression())
            ));
    }

    private static MemberAccessExpressionSyntax GetMethodCallSetupAccessExpressionSyntax(string callSetupPropertyName) =>
        MemberAccessExpression(
            SyntaxKind.SimpleMemberAccessExpression,
            InvocationExpression(
                IdentifierName("GetMethodCallSetup"),
                ArgumentList(
                    SingletonSeparatedList(
                        Argument(
                            SimpleLambdaExpression(
                                Parameter(Identifier("it")),
                                BinaryExpression(
                                    SyntaxKind.NotEqualsExpression,
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("it"),
                                        IdentifierName(callSetupPropertyName)
                                    ),
                                    LiteralExpression(SyntaxKind.NullLiteralExpression)
                                )
                            )
                        )
                    )
                )
            ),
            IdentifierName(callSetupPropertyName)
        );
}