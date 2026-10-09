using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.InterfaceMethodBuilder;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.InvocationVerifierInterface;

internal static class MethodImposterInvocationVerifierInterfaceBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method)
    {
        var called = method.InvocationVerifierInterface.CalledMethod;

        return new InterfaceDeclarationBuilder(
            method.InvocationVerifierInterface.Name,
            method.InvocationVerifierInterface.TypeParameterList
        )
            .AddConstraintClauses(method.InvocationVerifierInterface.ConstraintClauses)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                InterfaceMethod(
                    WellKnownTypes.Int,
                    InvocationVerifierInterfaceMetadata.CallCountMethodName
                )
            )
            .AddMember(InterfaceMethod(called.ReturnType, called.Name, called.CountParameter))
            .Build();
    }
}
