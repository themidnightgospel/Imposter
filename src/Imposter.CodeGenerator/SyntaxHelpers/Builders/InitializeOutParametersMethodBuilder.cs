using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal static class InitializeOutParametersMethodBuilder
{
    internal static ExpressionStatementSyntax? Invoke(in ImposterTargetMethodMetadata method) =>
        method.Parameters.HasOutputParameters
            ? Invoke(
                method.MethodInvocationImposter.InitializeOutParametersMethodName,
                method.Parameters.OutputParameters
            )
            : null;

    private static ExpressionStatementSyntax Invoke(
        string name,
        IReadOnlyList<ParameterModel> parameters
    ) =>
        IdentifierName(name)
            .Call(
                parameters.Where(it => it.RefKind is RefKind.Out).Select(it => ArgumentSyntax(it))
            )
            .ToStatementSyntax();

    internal static MethodDeclarationSyntax? Build(in ImposterTargetMethodMetadata method) =>
        method.Parameters.HasOutputParameters
            ? Build(
                method.MethodInvocationImposter.InitializeOutParametersMethodName,
                method.Parameters.OutputParameters
            )
            : null;

    private static MethodDeclarationSyntax Build(
        string name,
        IReadOnlyList<ParameterModel> parameters
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, name)
            .AddParameters(parameters.Select(ParameterSyntax))
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .WithBody(
                Block(
                    parameters
                        .Where(it => it.RefKind is RefKind.Out)
                        .Select(AssignDefaultValueStatementSyntax)
                )
            )
            .Build();
}
