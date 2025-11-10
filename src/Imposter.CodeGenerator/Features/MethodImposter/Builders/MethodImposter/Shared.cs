using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImposter.Builders.MethodImposter;

internal static class Shared
{
    // TODO remove
    internal static readonly MethodDeclarationSyntax CalledMethodDeclaration =
        new MethodDeclarationBuilder(
                PredefinedType(Token(SyntaxKind.VoidKeyword)),
                "Called")
            .AddParameter(
                Parameter(Identifier("count"))
                    .WithType(IdentifierName("Count")))
            .Build();
}
