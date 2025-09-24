using Imposter.CodeGenerator.Helpers.SyntaxBuilders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.Helpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.ImposterParts.InvocationSetupBuilder;

internal static partial class InvocationSetupBuilder
{
    private static MethodDeclarationSyntax ReturnsMethodDeclarationSyntax(ImposterTargetMethod method) =>
        MethodDeclaration(
                IdentifierName(method.InvocationsSetupBuilderInterface),
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
                        GetMethodCallSetupAccessExpressionSyntax,
                        IdentifierName("resultGenerator"))
                ),
                ReturnStatement(ThisExpression())
            ));

    private static MethodDeclarationSyntax ReturnsValueMethodDeclarationSyntax(ImposterTargetMethod method)
    {
        return MethodDeclaration(
                IdentifierName(method.InvocationsSetupBuilderInterface),
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
                        GetMethodCallSetupAccessExpressionSyntax,
                        Lambda(method.Symbol.Parameters,
                            new BlockBuilder()
                                .AddStatementsIf(method.HasOutParameters, () => InvokeInitializeOutParametersWithDefaultValues(method.Symbol.Parameters))
                                .AddStatement(ReturnStatement(IdentifierName("value")))
                                .Build())
                    )),
                ReturnStatement(ThisExpression())
            ));
    }

    private static readonly MemberAccessExpressionSyntax GetMethodCallSetupAccessExpressionSyntax =
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
                                        IdentifierName("ResultGenerator")
                                    ),
                                    LiteralExpression(SyntaxKind.NullLiteralExpression)
                                )
                            )
                        )
                    )
                )
            ),
            IdentifierName("ResultGenerator")
        );
}