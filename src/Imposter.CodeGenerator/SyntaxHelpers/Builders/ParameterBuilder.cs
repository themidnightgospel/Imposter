namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

internal sealed class ParameterBuilder(TypeSyntax type, string name)
{
    private readonly List<SyntaxToken> _modifiers = [];
    private EqualsValueClauseSyntax? _defaultValueClause;

    /// <summary>Adds a modifier (e.g., 'ref', 'in', 'out') to the parameter.</summary>
    public ParameterBuilder AddModifier(in SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    /// <summary>Sets the default value expression for the parameter.</summary>
    public ParameterBuilder WithDefaultValue(ExpressionSyntax? defaultValue)
    {
        if (defaultValue is not null)
        {
            // Roslyn factory requires the EqualsToken to be part of the EqualsValueClauseSyntax
            _defaultValueClause = EqualsValueClause(Token(SyntaxKind.EqualsToken), defaultValue);
        }

        return this;
    }

    public ParameterSyntax Build() =>
        Parameter(
            attributeLists: default,
            _modifiers.Count > 0 ? TokenList(_modifiers) : default,
            type,
            Identifier(name),
            _defaultValueClause
        );
}
