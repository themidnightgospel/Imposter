using System.Linq;
using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.InvocationSetup;

internal static partial class InvocationSetupBuilder
{
    private static ExpressionStatementSyntax InvokeInitializeOutParametersWithDefaultValuesMethod(ImposterTargetMethod method)
    {
        return ExpressionStatement(InvocationExpression(
            IdentifierName("InitializeOutParametersWithDefaultValues"),
            ArgumentList(
                SeparatedList(method.Symbol.Parameters.Where(it => it.RefKind is RefKind.Out).Select(SyntaxFactoryHelper.ArgumentSyntax))
            )
        ));
    }

    private static MethodDeclarationSyntax InitializeOutParametersWithDefaultsMethod(ImposterTargetMethod method)
    {
        var outParameters = method.Symbol.Parameters.Where(it => it.RefKind is RefKind.Out).ToArray();

        return MethodDeclaration(
                PredefinedType(Token(SyntaxKind.VoidKeyword)),
                "InitializeOutParametersWithDefaultValues"
            )
            .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.StaticKeyword))
            .AddParameterListParameters(outParameters.Select(SyntaxFactoryHelper.ParameterSyntax).ToArray())
            .WithBody(Block(outParameters.Select(SyntaxFactoryHelper.AssignDefaultValueStatementSyntax).ToArray()));
    }
}