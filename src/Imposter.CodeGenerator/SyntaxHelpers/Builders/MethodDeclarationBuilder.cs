using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal sealed class MethodDeclarationBuilder(TypeSyntax returnType, string name)
{
    private readonly List<SyntaxToken> _modifiers = [];
    private ParameterListSyntax? _parameterListSyntax;
    private readonly List<ParameterSyntax> _parameters = [];
    private readonly List<TypeParameterConstraintClauseSyntax> _constraintClauses = [];
    private TypeParameterListSyntax? _typeParameterList;
    private BlockSyntax? _body;
    private ArrowExpressionClauseSyntax? _expressionBody;
    private SyntaxToken _semicolonToken;
    private ExplicitInterfaceSpecifierSyntax? _explicitInterfaceSpecifier;

    public MethodDeclarationBuilder AddModifier(in SyntaxToken modifier)
    {
        _modifiers.Add(modifier);
        return this;
    }

    public MethodDeclarationBuilder AddModifiers(in SyntaxTokenList modifiers)
    {
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

    public MethodDeclarationBuilder AddConstraintClause(TypeParameterConstraintClauseSyntax clause)
    {
        _constraintClauses.Add(clause);
        return this;
    }

    public MethodDeclarationBuilder AddConstraintClauses(
        IEnumerable<TypeParameterConstraintClauseSyntax>? clauses
    )
    {
        if (clauses is not null)
        {
            _constraintClauses.AddRange(clauses);
        }
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

    public MethodDeclarationBuilder WithExplicitInterfaceSpecifier(
        ExplicitInterfaceSpecifierSyntax? specifier
    )
    {
        _explicitInterfaceSpecifier = specifier;
        return this;
    }

    public MethodDeclarationBuilder WithExplicitInterfaceSpecifier(NameSyntax interfaceName) =>
        WithExplicitInterfaceSpecifier(ExplicitInterfaceSpecifier(interfaceName));

    public MethodDeclarationSyntax Build()
    {
        return MethodDeclaration(
            attributeLists: default,
            _modifiers.Count > 0 ? TokenList(_modifiers) : default,
            returnType,
            _explicitInterfaceSpecifier,
            Identifier(name),
            _typeParameterList,
            _parameterListSyntax ?? ParameterList(SeparatedList(_parameters)),
            _constraintClauses.Count > 0 ? List(_constraintClauses) : default,
            _body,
            _expressionBody,
            _semicolonToken
        );
    }
}
