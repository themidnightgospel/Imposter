using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Builders.MethodImposter;

internal static class Shared
{
    // TODO remove
    internal static readonly MethodDeclarationSyntax CalledMethodDeclaration =
        MethodDeclaration(
                PredefinedType(Token(SyntaxKind.VoidKeyword)),
                Identifier("Called"))
            .AddParameterListParameters(
                Parameter(Identifier("count"))
                    .WithType(IdentifierName("Count")));
}