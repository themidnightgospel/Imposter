using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal struct MethodDeclarationBuilder(TypeSyntax returnType, string name)
{
    private readonly List<AttributeListSyntax> _attributes = [];
    private readonly List<SyntaxToken> _modifiers = [];
    private ParameterListSyntax? _parameterListSyntax;
    private readonly List<ParameterSyntax> _parameters = [];
    private readonly List<TypeParameterConstraintClauseSyntax> _constraintClauses = [];
    private readonly List<TypeParameterSyntax> _typeParameters = [];
    private TypeParameterListSyntax? _typeParameterList;
    private BlockSyntax? _body;
    private ArrowExpressionClauseSyntax? _expressionBody;
    private SyntaxToken _semicolonToken;
    private ExplicitInterfaceSpecifierSyntax? _explicitInterfaceSpecifier;

    public MethodDeclarationBuilder AddAttribute(AttributeListSyntax attribute)
    {
        _attributes.Add(attribute);
        return this;
    }
    
    public MethodDeclarationBuilder AddModifierIf(bool condition, Func<SyntaxToken> modifierFactory)
    {
        if (condition)
        {
            _modifiers.Add(modifierFactory());
        }
        return this;
    }

    public MethodDeclarationBuilder AddModifier(in SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    public MethodDeclarationBuilder AddModifiers(in SyntaxTokenList modifiers)
    {
        if (modifiers.Count == 0)
        {
            return this;
        }

        foreach (var modifier in modifiers)
        {
            _modifiers.Add(modifier);
        }

        return this;
    }

    public MethodDeclarationBuilder WithParameterList(ParameterListSyntax parameterListSyntax)
    {
        _parameterListSyntax = parameterListSyntax;
        return this;
    }

    public MethodDeclarationBuilder AddParameter(ParameterSyntax? parameter)
    {
        if (parameter is not null)
        {
            _parameters.Add(parameter);
        }

        return this;
    }

    public MethodDeclarationBuilder AddParameterIf(bool condition, Func<ParameterSyntax> parameter)
    {
        if (condition)
        {
            _parameters.Add(parameter());
        }

        return this;
    }

    // Those "if" fit better as extension methods
    public MethodDeclarationBuilder AddParametersIf(bool condition, Func<IEnumerable<ParameterSyntax>> parameters)
    {
        if (condition)
        {
            _parameters.AddRange(parameters());
        }

        return this;
    }

    public MethodDeclarationBuilder AddParameters(IEnumerable<ParameterSyntax> parameters)
    {
        _parameters.AddRange(parameters);
        return this;
    }

    public MethodDeclarationBuilder WithTypeParameters(TypeParameterListSyntax? typeParameterList)
    {
        _typeParameterList = typeParameterList;
        return this;
    }

    public MethodDeclarationBuilder AddTypeParameters(IEnumerable<TypeParameterSyntax>? typeParameters)
    {
        if (typeParameters is not null)
        {
            _typeParameters.AddRange(typeParameters);
        }

        return this;
    }

    public MethodDeclarationBuilder AddConstraintClause(TypeParameterConstraintClauseSyntax clause)
    {
        _constraintClauses.Add(clause);
        return this;
    }

    public MethodDeclarationBuilder WithBody(BlockSyntax body)
    {
        _body = body;
        _expressionBody = null;
        _semicolonToken = default;
        return this;
    }

    public MethodDeclarationBuilder WithExpressionBody(ArrowExpressionClauseSyntax expressionBody)
    {
        _expressionBody = expressionBody;
        _body = null;
        _semicolonToken = default;
        return this;
    }

    public MethodDeclarationBuilder WithSemicolon()
    {
        _semicolonToken = Token(SyntaxKind.SemicolonToken);
        _body = null;
        return this;
    }

    public MethodDeclarationBuilder WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax? specifier)
    {
        _explicitInterfaceSpecifier = specifier;
        return this;
    }

    public MethodDeclarationSyntax Build()
    {
        return MethodDeclaration(
            _attributes.Count > 0 ? List(_attributes) : default,
            _modifiers.Count > 0 ? TokenList(_modifiers) : default,
            returnType,
            _explicitInterfaceSpecifier,
            Identifier(name),
            _typeParameterList ?? (_typeParameters.Count > 0 ? TypeParameterList(SeparatedList(_typeParameters)) : null),
            _parameterListSyntax ?? ParameterList(SeparatedList(_parameters)),
            _constraintClauses.Count > 0 ? List(_constraintClauses) : default,
            _body,
            _expressionBody,
            _semicolonToken
        );
    }
}
