using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Builders.InvocationSetup;

internal static partial class InvocationSetup
{
    private static MethodDeclarationSyntax ThrowsTExceptionMethodDeclarationSyntax(ImposterTargetMethod method)
    {
        return MethodDeclaration(
                IdentifierName(method.InvocationsSetupInterface),
                Identifier("Throws")
            )
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithTypeParameterList(TypeParameterList(SingletonSeparatedList(TypeParameter("TException"))))
            .AddConstraintClauses(
                TypeParameterConstraintClause("TException")
                    .AddConstraints(TypeConstraint(IdentifierName("Exception")), ConstructorConstraint()))
            .WithBody(Block(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        GetMethodCallSetupAccessExpressionSyntax("ResultGenerator"),
                        Lambda(method.Symbol.Parameters,
                            new BlockBuilder()
                                .AddStatementsIf(method.HasOutParameters, () => InvokeInitializeOutParametersWithDefaultValues(method.Symbol.Parameters))
                                .AddStatement(ThrowStatement(ObjectCreationExpression(IdentifierName("TException"), ArgumentList(), default)))
                                .Build())
                    )),
                ReturnStatement(ThisExpression())
            ));
    }

    private static MethodDeclarationSyntax ThrowsExceptionInstanceMethodDeclarationSyntax(ImposterTargetMethod method)
    {
        var exceptionParameter = Parameter(
            Identifier("ex")
        ).WithType(WellKnownTypes.System.Exception);

        return MethodDeclaration(
                IdentifierName(method.InvocationsSetupInterface),
                Identifier("Throws")
            )
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(
                ParameterList(
                    SingletonSeparatedList(exceptionParameter)
                )
            )
            .WithBody(Block(ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        GetMethodCallSetupAccessExpressionSyntax("ResultGenerator"),
                        Lambda(method.Symbol.Parameters,
                            new BlockBuilder()
                                .AddStatementsIf(method.HasOutParameters, () => InvokeInitializeOutParametersWithDefaultValues(method.Symbol.Parameters))
                                .AddStatement(ThrowStatement(IdentifierName(exceptionParameter.Identifier)))
                                .Build())
                    )),
                ReturnStatement(ThisExpression())
            ));
    }

    private static MethodDeclarationSyntax ThrowsExceptionWithGeneratorMethodDeclarationSyntax(ImposterTargetMethod method)
    {
        return MethodDeclaration(
                IdentifierName(method.InvocationsSetupInterface),
                Identifier("Throws")
            )
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .AddParameterListParameters(
                Parameter(Identifier("exceptionGenerator"))
                    .WithType(IdentifierName(method.ExceptionGeneratorDelegateName))
            )
            .WithBody(
                Block(ExpressionStatement(
                        AssignmentExpression(
                            SyntaxKind.SimpleAssignmentExpression,
                            GetMethodCallSetupAccessExpressionSyntax("ResultGenerator"),
                            Lambda(
                                method.Symbol.Parameters,
                                Block(
                                    ThrowStatement(
                                        InvocationExpression(
                                            IdentifierName("exceptionGenerator"),
                                            ArgumentSyntaxList(method.Symbol.Parameters)
                                        )
                                    ))
                            )
                        )),
                    ReturnStatement(ThisExpression())
                ));
    }
}