using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter.InvocationVerifierInterface;

internal static class MethodImposterInvocationVerifierInterfaceBuilder
{
    internal static MemberDeclarationSyntax Build(in ImposterTargetMethodMetadata method) =>
        SyntaxFactoryHelper
            .InterfaceDeclarationBuilder(method.Symbol, method.InvocationVerifierInterface.Name)
            .AddMember(MethodDeclaration(
                    PredefinedType(Token(SyntaxKind.VoidKeyword)),
                    Identifier(InvocationVerifierInterfaceMetadata.CalledMethodMetadata.Name))
                .AddParameterListParameters(
                    Parameter(Identifier("count"))
                        .WithType(IdentifierName("Count"))))
            .Build(modifiers: TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
}