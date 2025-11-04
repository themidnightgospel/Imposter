using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal static class InitializeOutParametersMethodBuilder
{
    private const string Name = "InitializeOutParametersWithDefaultValues";

    internal static ExpressionStatementSyntax? Invoke(in ImposterTargetMethodMetadata method) =>
        method.Parameters.HasOutputParameters
            ? Invoke(method.Parameters.OutputParameters)
            : null;

    private static ExpressionStatementSyntax Invoke(IReadOnlyList<IParameterSymbol> parameters) =>
        IdentifierName(Name)
            .Call(parameters
                .Where(it => it.RefKind is RefKind.Out)
                .Select(it => ArgumentSyntax(it)))
            .ToStatementSyntax();

    internal static MethodDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
        => method.Parameters.HasOutputParameters ? Build(method.Parameters.OutputParameters) : null;

    private static MethodDeclarationSyntax Build(IReadOnlyList<IParameterSymbol> parameters) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, Name)
            .AddParameters(parameters.Select(ParameterSyntax))
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .WithBody(Block(parameters
                .Where(it => it.RefKind is RefKind.Out)
                .Select(AssignDefaultValueStatementSyntax)))
            .Build();
}