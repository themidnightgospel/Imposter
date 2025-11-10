using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static PropertyDeclarationSyntax ParameterAsArgProperty(IParameterSymbol parameter)
    {
        return new PropertyDeclarationBuilder(ArgType(parameter), parameter.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .WithGetter()
            .Build();
    }

    internal static FieldDeclarationSyntax ParameterAsReadonlyField(IParameterSymbol parameter) =>
        SingleVariableField(
            TypeSyntax(parameter.Type),
            parameter.Name,
            TokenList(Token(SyntaxKind.PublicKeyword)
            )
        );

    internal static PropertyDeclarationSyntax ReadOnlyPropertyDeclarationSyntax(
        TypeSyntax type,
        string name,
        ExpressionSyntax? initializer = null) =>
        PropertyDeclaration(
            attributeLists: default,
            modifiers: TokenList(Token(SyntaxKind.PublicKeyword)),
            type: type,
            explicitInterfaceSpecifier: null,
            identifier: Identifier(name),
            accessorList: null,
            expressionBody: initializer is null ? null : ArrowExpressionClause(initializer),
            initializer: null,
            semicolonToken: Token(SyntaxKind.SemicolonToken)
        );

}