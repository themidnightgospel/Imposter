using Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.GenericInterface;

internal static class MethodImposterGenericInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax? Build(in ImposterTargetMethodMetadata method)
    {
        if (!method.Model.IsGenericMethod)
        {
            return null;
        }

        var invokeMethod = new MethodDeclarationBuilder(
            method.NullableAwareReturnTypeSyntax,
            MethodImposterInvokeMethodMetadata.Name
        )
            .WithParameterList(InvokeSignatureBuilder.MethodImposterParameters(method))
            .WithSemicolon()
            .Build();

        var hasMatchingGroupMethodBuilder = new MethodDeclarationBuilder(
            HasMatchingInvocationImposterGroupMethodMetadata.ReturnType,
            HasMatchingInvocationImposterGroupMethodMetadata.Name
        );

        hasMatchingGroupMethodBuilder.WithParameterList(
            CapturedArgumentsBuilder.InParameters(method)
        );

        var hasMatchingGroupMethod = hasMatchingGroupMethodBuilder.WithSemicolon().Build();

        return new InterfaceDeclarationBuilder(
            method.MethodImposter.Interface.Name,
            method.GenericTypeParameterListSyntax
        )
            .AddConstraintClauses(method.GenericTypeConstraintClauses)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(method.MethodImposter.Interface.Syntax))
            .AddMember(invokeMethod)
            .AddMember(
                method.CanReuseCapturedSpansAcrossAdapters
                    ? new MethodDeclarationBuilder(
                        method.NullableAwareReturnTypeSyntax,
                        method.MethodImposter.InvokeWithCapturedSpansMethodName
                    )
                        .WithParameterList(InvokeSignatureBuilder.WithCapturedSpans(method))
                        .WithSemicolon()
                        .Build()
                    : null
            )
            .AddMember(hasMatchingGroupMethod)
            .Build();
    }
}
