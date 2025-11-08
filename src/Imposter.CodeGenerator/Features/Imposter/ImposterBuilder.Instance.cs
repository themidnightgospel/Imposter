using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter;

internal readonly partial struct ImposterBuilder
{
    private static MemberDeclarationSyntax[] InstanceMethods(
        in ImposterGenerationContext imposterGenerationContext,
        string imposterInstanceFieldName) =>
    [
        MethodDeclaration(
                SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol),
                Identifier("Instance"))
            .WithModifiers(GetAccessibilityModifiers(imposterGenerationContext.TargetSymbol))
            .WithExpressionBody(
                ArrowExpressionClause(IdentifierName(imposterInstanceFieldName)))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)),
        MethodDeclaration(
                SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol),
                Identifier("Instance"))
            .WithExplicitInterfaceSpecifier(
                ExplicitInterfaceSpecifier(
                    WellKnownTypes.Imposter.Abstractions.IHaveImposterInstance(SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol))))
            .WithBody(Block(
                ReturnStatement(IdentifierName(imposterInstanceFieldName))))
    ];

    private static SyntaxTokenList GetAccessibilityModifiers(INamedTypeSymbol targetSymbol) =>
        targetSymbol.DeclaredAccessibility switch
        {
            Accessibility.Public => TokenList(Token(SyntaxKind.PublicKeyword)),
            Accessibility.Private => TokenList(Token(SyntaxKind.PrivateKeyword)),
            Accessibility.Protected => TokenList(Token(SyntaxKind.ProtectedKeyword)),
            Accessibility.Internal => TokenList(Token(SyntaxKind.InternalKeyword)),
            Accessibility.ProtectedOrInternal => TokenList(Token(SyntaxKind.ProtectedKeyword), Token(SyntaxKind.InternalKeyword)),
            Accessibility.ProtectedAndInternal => TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ProtectedKeyword)),
            _ => TokenList(Token(SyntaxKind.InternalKeyword))
        };
}
