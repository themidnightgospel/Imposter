using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Shared;

internal readonly struct FieldMetadata(
    string name,
    TypeSyntax type,
    SyntaxTokenList? modifiers = null,
    ExpressionSyntax? initializer = null
)
{
    internal readonly string Name = name;

    internal readonly TypeSyntax Type = type;

    internal readonly SyntaxTokenList? Modifiers = modifiers;

    internal readonly ExpressionSyntax? Initializer = initializer;

    internal static FieldMetadata PrivateField(
        string name,
        TypeSyntax type,
        ExpressionSyntax? initializer = null
    ) => new(name, type, TokenList(Token(SyntaxKind.PrivateKeyword)), initializer);

    internal static FieldMetadata PrivateReadonlyField(
        string name,
        TypeSyntax type,
        ExpressionSyntax? initializer = null
    ) =>
        new(
            name,
            type,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
            initializer
        );
}
