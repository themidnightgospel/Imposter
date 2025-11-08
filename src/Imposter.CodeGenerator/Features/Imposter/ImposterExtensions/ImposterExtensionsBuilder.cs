using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.ImposterExtensions;

internal static class ImposterExtensionsBuilder
{
    private const string ExtensionParameterName = "imposter";
    private const string MethodName = "Imposter";

    internal static ClassDeclarationSyntax Build(
        in ImposterGenerationContext imposterGenerationContext,
        string imposterNamespaceName)
    {
        var extensionClassName = $"{imposterGenerationContext.TargetSymbol.Name}{MethodName}Extensions";
        var interfaceType = SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol);
        var imposterType = ParseTypeName(GetImposterTypeName(imposterNamespaceName, imposterGenerationContext.Imposter.Name));

        var method = MethodDeclaration(imposterType, Identifier(MethodName))
            .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.StaticKeyword)))
            .WithExpressionBody(
                ArrowExpressionClause(
                    ObjectCreationExpression(imposterType)
                        .WithArgumentList(ArgumentList())))
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));

        var extensionDeclaration = ExtensionDeclaration()
            .WithKeyword(Token(SyntaxKind.ExtensionKeyword))
            .WithParameterList(
                ParameterList(
                    SingletonSeparatedList(
                        Parameter(Identifier(ExtensionParameterName))
                            .WithType(interfaceType))))
            .WithMembers(List<MemberDeclarationSyntax>([method]))
            .WithOpenBraceToken(Token(SyntaxKind.OpenBraceToken))
            .WithCloseBraceToken(Token(SyntaxKind.CloseBraceToken));

        return new ClassDeclarationBuilder(extensionClassName)
            .AddPublicModifier()
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .AddMember(extensionDeclaration)
            .Build();
    }

    private static string GetImposterTypeName(string namespaceName, string imposterName)
    {
        if (string.IsNullOrWhiteSpace(namespaceName))
        {
            return $"global::{imposterName}";
        }

        return $"global::{namespaceName}.{imposterName}";
    }
}
