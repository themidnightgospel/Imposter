using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static FieldDeclarationSyntax SingleVariableField(TypeSyntax typeSyntax, string name, SyntaxKind modifiers = SyntaxKind.PrivateKeyword) =>
        FieldDeclaration(
            default,
            TokenList(Token(modifiers)),
            VariableDeclaration(
                typeSyntax,
                SingletonSeparatedList(
                    VariableDeclarator(Identifier(name))
                )
            )
        );
}