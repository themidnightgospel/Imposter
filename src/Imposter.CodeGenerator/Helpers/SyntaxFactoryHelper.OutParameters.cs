using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Helpers;

internal static partial class SyntaxFactoryHelper
{
    private const string InitializeOutParametersWithDefaultValuesMethodName = "InitializeOutParametersWithDefaultValues";

    internal static ExpressionStatementSyntax InvokeInitializeOutParametersWithDefaultValues(IReadOnlyList<IParameterSymbol> parameters)
    {
        return ExpressionStatement(InvocationExpression(
            IdentifierName(InitializeOutParametersWithDefaultValuesMethodName),
            ArgumentList(
                SeparatedList(parameters.Where(it => it.RefKind is RefKind.Out).Select(ArgumentSyntax))
            )
        ));
    }

    internal static MethodDeclarationSyntax InitializeOutParametersWithDefaultsMethod(IReadOnlyList<IParameterSymbol> parameters)
    {
        var outParameters = parameters.Where(it => it.RefKind is RefKind.Out).ToArray();

        return MethodDeclaration(
                PredefinedType(Token(SyntaxKind.VoidKeyword)),
                InitializeOutParametersWithDefaultValuesMethodName
            )
            .AddModifiers(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.StaticKeyword))
            .AddParameterListParameters(outParameters.Select(ParameterSyntax).ToArray())
            .WithBody(Block(outParameters.Select(AssignDefaultValueStatementSyntax).ToArray()));
    }
}