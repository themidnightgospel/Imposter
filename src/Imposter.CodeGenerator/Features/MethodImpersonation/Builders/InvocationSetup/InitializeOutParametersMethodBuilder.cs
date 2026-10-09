using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Models;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationSetup;

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
        IReadOnlyList<ParameterModel> outParameters
    ) =>
        IdentifierName(name)
            .Call(outParameters.Select(it => ArgumentSyntax(it)))
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
        IReadOnlyList<ParameterModel> outParameters
    ) =>
        new MethodDeclarationBuilder(WellKnownTypes.Void, name)
            .AddParameters(outParameters.Select(ParameterSyntax))
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .WithBody(Block(outParameters.Select(AssignDefaultValueStatementSyntax)))
            .Build();
}
