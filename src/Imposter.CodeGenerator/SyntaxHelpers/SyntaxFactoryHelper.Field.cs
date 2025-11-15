using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static FieldDeclarationSyntax SinglePrivateReadonlyVariableField(
        in FieldMetadata fieldMetadata,
        ExpressionSyntax? initializer = null
    ) =>
        SingleVariableField(
            fieldMetadata.Type,
            fieldMetadata.Name,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
            initializer
        );

    internal static FieldDeclarationSyntax SinglePrivateReadonlyVariableField(
        TypeSyntax typeSyntax,
        string name,
        ExpressionSyntax? initializer = null
    ) =>
        SingleVariableField(
            typeSyntax,
            name,
            TokenList(Token(SyntaxKind.PrivateKeyword), Token(SyntaxKind.ReadOnlyKeyword)),
            initializer
        );

    internal static FieldDeclarationSyntax SingleVariableField(
        in FieldMetadata fieldMetadata,
        SyntaxKind modifier,
        ExpressionSyntax? initializer = null
    ) =>
        SingleVariableField(
            fieldMetadata.Type,
            fieldMetadata.Name,
            TokenList(Token(modifier)),
            initializer
        );

    internal static FieldDeclarationSyntax SingleVariableField(
        TypeSyntax typeSyntax,
        string name,
        SyntaxKind modifier
    ) => SingleVariableField(typeSyntax, name, TokenList(Token(modifier)));

    internal static FieldDeclarationSyntax SingleVariableField(
        TypeSyntax typeSyntax,
        string name,
        in SyntaxTokenList? modifiers = null,
        ExpressionSyntax? initializer = null
    ) =>
        FieldDeclaration(
            default,
            modifiers ?? TokenList(),
            VariableDeclarationSyntax(typeSyntax, name, initializer)
        );

    internal static VariableDeclarationSyntax VariableDeclarationSyntax(
        TypeSyntax typeSyntax,
        string name,
        ExpressionSyntax? initializer = null
    ) =>
        VariableDeclaration(
            typeSyntax,
            SingletonSeparatedList(
                VariableDeclarator(
                    Identifier(name),
                    null,
                    initializer is null ? null : EqualsValueClause(initializer)
                )
            )
        );

    internal static LocalDeclarationStatementSyntax LocalVariableDeclarationSyntax(
        TypeSyntax typeSyntax,
        string name,
        ExpressionSyntax? initializer = null
    ) =>
        LocalDeclarationStatement(
            VariableDeclarationSyntax(typeSyntax: typeSyntax, name: name, initializer: initializer)
        );

    internal static FieldDeclarationSyntax ParameterAsReadonlyField(IParameterSymbol parameter) =>
        SingleVariableField(
            TypeSyntax(parameter.Type),
            parameter.Name,
            TokenList(Token(SyntaxKind.PublicKeyword))
        );
}
