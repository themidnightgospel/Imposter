using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.MethodSetup.Builders.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    private static MethodDeclarationSyntax? ReturnsMethodDeclarationSyntax(in ImposterTargetMethodMetadata method)
    {
        if (!method.HasReturnValue)
        {
            return null;
        }

        return MethodDeclaration(
                method.InvocationSetup.Interface.Syntax,
                Identifier(method.InvocationSetup.ReturnsMethod.Name)
            )
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.InvocationSetup.Interface.Syntax))
            .AddParameterListParameters(ParameterSyntax(method.InvocationSetup.ReturnsMethod.ResultGeneratorParameter))
            .WithBody(Block(
                GetMethodCallSetupAccessExpressionSyntax("ResultGenerator")
                    .Assign(IdentifierName(method.InvocationSetup.ReturnsMethod.ResultGeneratorParameter.Name))
                    .ToStatementSyntax(),
                ReturnStatement(ThisExpression())
            ));
    }

    private static MethodDeclarationSyntax? ReturnsValueMethodDeclarationSyntax(in ImposterTargetMethodMetadata method)
    {
        if (!method.HasReturnValue)
        {
            return null;
        }

        return MethodDeclaration(
                method.InvocationSetup.Interface.Syntax,
                Identifier(method.InvocationSetup.ReturnsMethod.Name)
            )
            .WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(method.InvocationSetup.Interface.Syntax))
            .AddParameterListParameters(ParameterSyntax(method.InvocationSetup.ReturnsMethod.ValueParameter))
            .WithBody(Block(
                    GetMethodCallSetupAccessExpressionSyntax("ResultGenerator")
                        .Assign(Lambda(method.Symbol.Parameters,
                            new BlockBuilder()
                                .AddStatement(InvokeInitializeOutParametersWithDefaultValues(method))
                                .AddStatement(ReturnStatement(IdentifierName(method.InvocationSetup.ReturnsMethod.ValueParameter.Name)))
                                .Build()))
                        .ToStatementSyntax(),
                    ReturnStatement(ThisExpression())
                )
            );

        static StatementSyntax? InvokeInitializeOutParametersWithDefaultValues(in ImposterTargetMethodMetadata method) =>
            method.Parameters.HasOutputParameters
                ? SyntaxFactoryHelper.InvokeInitializeOutParametersWithDefaultValues(method.Symbol.Parameters)
                : null;
    }

    private static MemberAccessExpressionSyntax GetMethodCallSetupAccessExpressionSyntax(in string callSetupPropertyName) =>
        IdentifierName("GetOrAddMethodSetup")
            .Call(ArgumentList(
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
            ))
            .Dot(IdentifierName(callSetupPropertyName));
}