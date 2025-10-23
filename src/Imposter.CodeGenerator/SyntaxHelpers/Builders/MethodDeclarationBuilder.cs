using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal struct MethodDeclarationBuilder(TypeSyntax returnType, string name)
{
    private readonly List<AttributeListSyntax> _attributes = new();
    private readonly List<SyntaxToken> _modifiers = new();
    private ParameterListSyntax? _parameterListSyntax;
    private readonly List<ParameterSyntax> _parameters = new();
    private readonly List<TypeParameterConstraintClauseSyntax> _constraintClauses = new();
    private readonly List<TypeParameterSyntax> _typeParameters = new();
    private TypeParameterListSyntax? _typeParameterList;
    private BlockSyntax? _body;
    private ArrowExpressionClauseSyntax? _expressionBody;
    private SyntaxToken _semicolonToken;
    private ExplicitInterfaceSpecifierSyntax? _explicitInterfaceSpecifier;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder AddAttribute(AttributeListSyntax attribute)
    {
        _attributes.Add(attribute);
        return this;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder AddModifierIf(bool condition, Func<SyntaxToken> modifierFactory)
    {
        if (condition)
        {
            _modifiers.Add(modifierFactory());
        }
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder AddModifier(SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder WithParameterList(ParameterListSyntax parameterListSyntax)
    {
        _parameterListSyntax = parameterListSyntax;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder AddParameter(ParameterSyntax? parameter)
    {
        if (parameter is not null)
        {
            _parameters.Add(parameter);
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder AddParameterIf(bool condition, Func<ParameterSyntax> parameter)
    {
        if (condition)
        {
            _parameters.Add(parameter());
        }

        return this;
    }

    // Those "if" fit better as extension methods
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder AddParametersIf(bool condition, Func<IEnumerable<ParameterSyntax>> parameters)
    {
        if (condition)
        {
            _parameters.AddRange(parameters());
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder AddParameters(IEnumerable<ParameterSyntax> parameters)
    {
        _parameters.AddRange(parameters);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder WithTypeParameters(TypeParameterListSyntax? typeParameterList)
    {
        _typeParameterList = typeParameterList;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder AddTypeParameters(IEnumerable<TypeParameterSyntax>? typeParameters)
    {
        if (typeParameters is not null)
        {
            _typeParameters.AddRange(typeParameters);
        }

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder AddConstraintClause(TypeParameterConstraintClauseSyntax clause)
    {
        _constraintClauses.Add(clause);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder WithBody(BlockSyntax body)
    {
        _body = body;
        _expressionBody = null;
        _semicolonToken = default;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder WithExpressionBody(ArrowExpressionClauseSyntax expressionBody)
    {
        _expressionBody = expressionBody;
        _body = null;
        _semicolonToken = default;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public MethodDeclarationBuilder WithSemicolon()
    {
        _semicolonToken = Token(SyntaxKind.SemicolonToken);
        _body = null;
        _expressionBody = null;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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