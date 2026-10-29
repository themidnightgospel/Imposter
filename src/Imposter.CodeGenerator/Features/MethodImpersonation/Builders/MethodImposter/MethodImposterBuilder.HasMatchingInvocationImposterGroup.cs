using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    private static MethodDeclarationSyntax BuildHasMatchingInvocationImposterGroupMethod(
        in ImposterTargetMethodMetadata method
    )
    {
        var hasMatchingMethod = method.MethodImposter.HasMatchingInvocationImposterGroupMethod;

        return new MethodDeclarationBuilder(hasMatchingMethod.ReturnType, hasMatchingMethod.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(CapturedArgumentsBuilder.Parameters(method))
            .WithBody(
                Block(
                    ReturnStatement(
                        IdentifierName(
                                method.MethodImposter.FindMatchingInvocationImposterGroupMethod.Name
                            )
                            .Call(CapturedArgumentsBuilder.Arguments(method))
                            .IsNotNull()
                    )
                )
            )
            .Build();
    }
}
