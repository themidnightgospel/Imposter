using Imposter.CodeGenerator.Contexts;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.ImposterTargetExtensions;

internal static class ImposterTargetExtensionsBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterGenerationContext imposterGenerationContext)
    {
        // TODO optimize
        var extensionClassName = imposterGenerationContext.TargetSymbol.ToDisplayString().Replace(":", "").Replace(".", "");
        var imposterType = ParseTypeName(imposterGenerationContext.ImposterComponentsNamespace + "." + imposterGenerationContext.Imposter.Name);
        
        return new ClassDeclarationBuilder(extensionClassName)
            .AddMember(
                MethodDeclaration(imposterType, "Imposter")
                    .AddModifiers(
                        Token(SyntaxKind.PublicKeyword),
                        Token(SyntaxKind.StaticKeyword)
                    )
                    .AddParameterListParameters(
                        Parameter(
                                Identifier("target")
                            )
                            .AddModifiers(Token(SyntaxKind.ThisKeyword))
                            .WithType(SyntaxFactoryHelper.TypeSyntax(imposterGenerationContext.TargetSymbol))
                    )
                    .WithExpressionBody(
                        ArrowExpressionClause(
                            ObjectCreationExpression(imposterType)
                        )
                    )
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)))
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.StaticKeyword))
            .Build();
    }
}