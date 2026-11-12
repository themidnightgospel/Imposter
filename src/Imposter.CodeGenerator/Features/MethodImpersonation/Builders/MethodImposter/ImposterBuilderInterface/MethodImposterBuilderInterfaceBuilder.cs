using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.ImposterBuilderInterface;

internal static class MethodImposterBuilderInterfaceBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method) =>
        new InterfaceDeclarationBuilder(
            method.MethodImposter.BuilderInterface.Name,
            method.GenericTypeParameterListSyntax
        )
            .AddConstraintClauses(method.GenericTypeConstraintClauses)
            .AddBaseType(SimpleBaseType(method.MethodInvocationImposterGroup.Interface.Syntax))
            .AddBaseType(
                SimpleBaseType(method.MethodInvocationImposterGroup.CallbackInterface.Syntax)
            )
            .AddBaseType(SimpleBaseType(method.InvocationVerifierInterface.Syntax))
            .AddModifier(
                Token(SyntaxKind.PublicKeyword)
#if DEBUG
                .WithLeadingTriviaComment(method.DisplayName)
#endif
            )
            .Build();
}
