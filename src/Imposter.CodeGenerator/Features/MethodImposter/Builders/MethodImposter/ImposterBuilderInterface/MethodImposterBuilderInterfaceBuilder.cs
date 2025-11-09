using Imposter.CodeGenerator.Helpers;
#if DEBUG
using Imposter.CodeGenerator.SyntaxHelpers;
#endif
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.ImposterBuilderInterface;

internal static class MethodImposterBuilderInterfaceBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method) =>
        InterfaceDeclarationBuilderFactory
            .CreateForMethod(method.Symbol, method.MethodImposter.BuilderInterface.Name)
            .AddBaseType(SimpleBaseType(method.MethodInvocationImposterGroup.Interface.Syntax))
            .AddBaseType(SimpleBaseType(method.MethodInvocationImposterGroup.CallbackInterface.Syntax))
            .AddBaseType(SimpleBaseType(method.InvocationVerifierInterface.Syntax))
            .AddModifier(Token(SyntaxKind.PublicKeyword)
#if DEBUG
                    .WithLeadingTriviaComment(method.DisplayName)
#endif
            )
            .Build();
}
