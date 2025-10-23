using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodSetup.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

// TODO doesn't seem to belong here
internal static partial class SyntaxFactoryHelper
{
    private const string InitializeOutParametersWithDefaultValuesMethodName = "InitializeOutParametersWithDefaultValues";

    internal static ExpressionStatementSyntax? InvokeInitializeOutParametersWithDefaultValues(in ImposterTargetMethodMetadata method)
    {
        return method.Parameters.HasOutputParameters ? InvokeInitializeOutParametersWithDefaultValues(method.Symbol.Parameters) : null;
    }

    internal static ExpressionStatementSyntax InvokeInitializeOutParametersWithDefaultValues(IReadOnlyList<IParameterSymbol> parameters)
    {
        return ExpressionStatement(InvocationExpression(
            IdentifierName(InitializeOutParametersWithDefaultValuesMethodName),
            ArgumentListSyntax(
                SeparatedList(parameters.Where(it => it.RefKind is RefKind.Out).Select(it => ArgumentSyntax(it)))
            )
        ));
    }

    internal static MethodDeclarationSyntax? DeclareInitializeOutParametersWithDefaultsMethod(in ImposterTargetMethodMetadata method)
        => method.Parameters.HasOutputParameters ? DeclareInitializeOutParametersWithDefaultsMethod(method.Symbol.Parameters) : null;

    internal static MethodDeclarationSyntax DeclareInitializeOutParametersWithDefaultsMethod(IReadOnlyList<IParameterSymbol> parameters)
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