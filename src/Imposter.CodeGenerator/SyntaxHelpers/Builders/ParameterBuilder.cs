namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

internal struct ParameterBuilder(TypeSyntax type, string name)
{
    private readonly List<AttributeListSyntax> _attributes = [];
    private readonly List<SyntaxToken> _modifiers = [];
    private EqualsValueClauseSyntax? _defaultValueClause;

    /// <summary>Adds an attribute list to the parameter.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterBuilder AddAttribute(AttributeListSyntax attribute)
    {
        _attributes.Add(attribute);
        return this;
    }

    /// <summary>Adds a modifier (e.g., 'ref', 'in', 'out') to the parameter.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterBuilder AddModifier(in SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    /// <summary>Sets the default value expression for the parameter.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ParameterBuilder WithDefaultValue(ExpressionSyntax? defaultValue)
    {
        if (defaultValue is not null)
        {
            // Roslyn factory requires the EqualsToken to be part of the EqualsValueClauseSyntax
            _defaultValueClause = EqualsValueClause(
                Token(SyntaxKind.EqualsToken),
                defaultValue);
        }

        return this;
    }

    public ParameterSyntax Build() =>
        Parameter(
            _attributes.Count > 0 ? List(_attributes) : default,
            _modifiers.Count > 0 ? TokenList(_modifiers) : default,
            type,
            Identifier(name),
            _defaultValueClause
        );
}
