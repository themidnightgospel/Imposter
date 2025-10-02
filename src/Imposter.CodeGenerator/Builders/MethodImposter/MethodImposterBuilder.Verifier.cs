using System;
using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
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

    private static MemberDeclarationSyntax BuildMethodInvocationVerifierInterface(ImposterTargetMethodMetadata method) =>
        SyntaxFactoryHelper
            .InterfaceDeclarationBuilder(method.Symbol, method.InvocationVerifierInterface)
            .AddMember(CalledMethodDeclaration.Value)
            .Build(modifiers: TokenList(Token(SyntaxKind.PublicKeyword)
                .WithLeadingTriviaComment(method.DisplayName)));
}