using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal struct PropertyDeclarationBuilder(TypeSyntax typeSyntax, string name)
{
    private readonly List<AttributeListSyntax> _attributes = [];
    private readonly List<SyntaxToken> _modifiers = [];
    private AccessorDeclarationSyntax? _getter;
    private AccessorDeclarationSyntax? _setter;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PropertyDeclarationBuilder AddAttribute(AttributeListSyntax attribute)
    {
        _attributes.Add(attribute);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PropertyDeclarationBuilder AddModifier(SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PropertyDeclarationBuilder AddModifiers(SyntaxTokenList modifiers)
    {
        foreach (var modifier in modifiers)
        {
            _modifiers.Add(modifier);
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PropertyDeclarationBuilder WithGetterBody(BlockSyntax body)
    {
        _getter = AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithBody(body);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public PropertyDeclarationBuilder WithSetterBody(BlockSyntax body)
    {
        _setter = AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithBody(body);
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
                _attributes.Count > 0 ? List(_attributes) : default,
                _modifiers.Count > 0 ? TokenList(_modifiers) : default,
                typeSyntax,
                explicitInterfaceSpecifier: null,
                Identifier(name),
                accessorList)
            .NormalizeWhitespace(elasticTrivia: true);
    }
}
