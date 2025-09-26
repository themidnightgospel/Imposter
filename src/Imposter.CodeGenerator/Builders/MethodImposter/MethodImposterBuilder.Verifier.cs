using System;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal static partial class MethodImposterBuilder
{
    private static readonly Lazy<MethodDeclarationSyntax> CalledMethodDeclaration = new(() =>
        MethodDeclaration(
                PredefinedType(Token(SyntaxKind.VoidKeyword)),
                Identifier("Called"))
            .AddParameterListParameters(
                Parameter(Identifier("count"))
                    .WithType(IdentifierName("Count")))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));

    private static MemberDeclarationSyntax BuildMethodInvocationVerifierInterface(ImposterTargetMethod method)
    {
        return new InterfaceDeclarationBuilder(method.MethodInvocationVerifierInterfaceName)
            .AddMember(CalledMethodDeclaration.Value)
            .Build(modifiers: TokenList(Token(SyntaxKind.PublicKeyword)
                .WithLeadingTriviaComment(method.DisplayName)));
    }
}