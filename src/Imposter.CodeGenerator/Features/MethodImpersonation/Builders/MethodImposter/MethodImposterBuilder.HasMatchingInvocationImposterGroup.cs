using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
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
        return new MethodDeclarationBuilder(
            HasMatchingInvocationImposterGroupMethodMetadata.ReturnType,
            HasMatchingInvocationImposterGroupMethodMetadata.Name
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithParameterList(CapturedArgumentsBuilder.InParameters(method))
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
