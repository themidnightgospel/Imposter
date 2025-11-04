using Imposter.CodeGenerator.Helpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter.InvocationVerifierInterface;

internal static class MethodImposterInvocationVerifierInterfaceBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method) =>
        InterfaceDeclarationBuilderFactory
            .CreateForMethod(method.Symbol, method.InvocationVerifierInterface.Name)
            .AddMember(
                MethodDeclaration(
                        PredefinedType(Token(SyntaxKind.VoidKeyword)),
                        Identifier(CalledMethodMetadata.Name))
                    .AddParameterListParameters(
                        Parameter(Identifier("count"))
                            .WithType(IdentifierName("Count")))
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
            )
            .Build(modifiers: TokenList(Token(SyntaxKind.PublicKeyword)));
}
