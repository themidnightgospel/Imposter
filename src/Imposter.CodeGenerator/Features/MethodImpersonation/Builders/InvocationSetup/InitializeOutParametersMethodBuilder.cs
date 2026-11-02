using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.InvocationSetup;

internal static class InitializeOutParametersMethodBuilder
{
    internal static ExpressionStatementSyntax? Invoke(in ImposterTargetMethodMetadata method) =>
        method.Parameters.HasOutputParameters
            ? IdentifierName(method.MethodInvocationImposter.InitializeOutParametersMethodName)
                .Call(method.Parameters.OutputParameters.Select(ArgumentSyntax))
                .ToStatementSyntax()
            : null;

    internal static MethodDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Parameters.HasOutputParameters)
        {
            return null;
        }

        var outParameters = method.Parameters.OutputParameters;
        return new MethodDeclarationBuilder(
            WellKnownTypes.Void,
            method.MethodInvocationImposter.InitializeOutParametersMethodName
        )
            .AddParameters(outParameters.Select(ParameterSyntax))
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .WithBody(Block(outParameters.Select(AssignDefaultValueStatementSyntax)))
            .Build();
    }
}
