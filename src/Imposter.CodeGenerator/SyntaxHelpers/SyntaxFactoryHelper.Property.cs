using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers;

internal static partial class SyntaxFactoryHelper
{
    internal static PropertyDeclarationSyntax ParameterAsArgProperty(IParameterSymbol parameter)
    {
        return PropertyDeclaration(ArgType(parameter), Identifier(parameter.Name))
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddAccessorListAccessors(
                AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken)));
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

    internal static PropertyDeclarationSyntax PropertyDeclarationSyntax(
        TypeSyntax type,
        string name,
        BlockSyntax? getAccessor,
        BlockSyntax? setAccessor)
    {
        return PropertyDeclaration(
            attributeLists: default,
            modifiers: TokenList(Token(SyntaxKind.PublicKeyword)),
            type: type,
            explicitInterfaceSpecifier: null,
            identifier: Identifier(name),
            accessorList: AccessorList(List<AccessorDeclarationSyntax>(GetAccessors())),
            expressionBody: null,
            initializer: null,
            semicolonToken: default
        );

        IEnumerable<AccessorDeclarationSyntax> GetAccessors()
        {
            if (getAccessor is not null)
            {
                yield return AccessorDeclaration(SyntaxKind.GetAccessorDeclaration, getAccessor);
            }

            if (setAccessor is not null)
            {
                yield return AccessorDeclaration(SyntaxKind.SetAccessorDeclaration, setAccessor);
            }
        }
    }
}