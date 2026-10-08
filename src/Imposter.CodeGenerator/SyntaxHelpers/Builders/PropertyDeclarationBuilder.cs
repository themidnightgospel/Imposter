using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal sealed class PropertyDeclarationBuilder(TypeSyntax typeSyntax, string name)
{
    private readonly List<SyntaxToken> _modifiers = [];
    private AccessorDeclarationSyntax? _getter;
    private AccessorDeclarationSyntax? _setter;
    private ExplicitInterfaceSpecifierSyntax? _explicitInterfaceSpecifier;

    public PropertyDeclarationBuilder AddModifier(in SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    public PropertyDeclarationBuilder AddModifiers(in SyntaxTokenList modifiers)
    {
        foreach (var modifier in modifiers)
        {
            _modifiers.Add(modifier);
        }

        return this;
    }

    public PropertyDeclarationBuilder WithGetter()
    {
        _getter = AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
            .WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
        return this;
    }

    public PropertyDeclarationBuilder WithGetterBody(
        BlockSyntax body,
        SyntaxTokenList modifiers = default
    )
    {
        _getter = AccessorDeclaration(SyntaxKind.GetAccessorDeclaration)
            .WithModifiers(modifiers)
            .WithBody(body);
        return this;
    }

    public PropertyDeclarationBuilder WithSetterBody(
        BlockSyntax body,
        SyntaxTokenList modifiers = default
    )
    {
        _setter = AccessorDeclaration(SyntaxKind.SetAccessorDeclaration)
            .WithModifiers(modifiers)
            .WithBody(body);
        return this;
    }

    public PropertyDeclarationBuilder WithInitBody(
        BlockSyntax body,
        SyntaxTokenList modifiers = default
    )
    {
        _setter = AccessorDeclaration(SyntaxKind.InitAccessorDeclaration)
            .WithModifiers(modifiers)
            .WithBody(body);
        return this;
    }

    public PropertyDeclarationBuilder WithExplicitInterfaceSpecifier(
        ExplicitInterfaceSpecifierSyntax? specifier
    )
    {
        _explicitInterfaceSpecifier = specifier;
        return this;
    }

    public PropertyDeclarationSyntax Build()
    {
        var accessors = new List<AccessorDeclarationSyntax>();

        if (_getter is not null)
        {
            accessors.Add(_getter);
        }

        if (_setter is not null)
        {
            accessors.Add(_setter);
        }

        var accessorList = AccessorList(List(accessors));

        return PropertyDeclaration(
            attributeLists: default,
            _modifiers.Count > 0 ? TokenList(_modifiers) : default,
            typeSyntax,
            explicitInterfaceSpecifier: _explicitInterfaceSpecifier!,
            Identifier(name),
            accessorList
        );
    }
}
