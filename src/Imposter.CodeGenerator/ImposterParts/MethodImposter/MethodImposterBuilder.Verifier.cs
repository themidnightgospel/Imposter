using System;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.ImposterParts.MethodImposter;

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
        return InterfaceDeclaration(method.MethodInvocationVerifierInterfaceName)
            .AddMembers(CalledMethodDeclaration.Value)
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
            .WithLeadingTrivia(
                Comment(method.Comment),
                CarriageReturnLineFeed
            );
    }
}