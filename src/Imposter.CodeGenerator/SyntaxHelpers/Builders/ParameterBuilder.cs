using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal sealed class ParameterBuilder(TypeSyntax type, string name)
{
    private readonly List<SyntaxToken> _modifiers = [];
    private EqualsValueClauseSyntax? _defaultValueClause;

    public ParameterBuilder AddModifier(in SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    public ParameterBuilder WithDefaultValue(ExpressionSyntax? defaultValue)
    {
        if (defaultValue is not null)
        {
            _defaultValueClause = EqualsValueClause(defaultValue);
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
