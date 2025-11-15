using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal struct ConstructorBuilder(string name)
{
    private readonly List<AttributeListSyntax> _attributes = [];
    private readonly List<ParameterSyntax> _parameters = [];
    private ConstructorInitializerSyntax? _initializers;
    private BlockSyntax? _body;
    private SyntaxTokenList _modifiers = default;
    private ParameterListSyntax? _parameterListSyntax;

    internal ConstructorBuilder AddAttribute(AttributeListSyntax attribute)
    {
        _attributes.Add(attribute);
        return this;
    }

    internal ConstructorBuilder AddParameter(ParameterSyntax parameter)
    {
        _parameters.Add(parameter);
        return this;
    }

    internal ConstructorBuilder AddParameters(IEnumerable<ParameterSyntax> parameters)
    {
        _parameters.AddRange(parameters);
        return this;
    }

    internal ConstructorBuilder WithParameterList(ParameterListSyntax parameterListSyntax)
    {
        _parameterListSyntax = parameterListSyntax;
        return this;
    }


    internal ConstructorBuilder WithBody(BlockSyntax body)
    {
        _body = body;
        return this;
    }

    internal ConstructorBuilder AddInitializer(ConstructorInitializerSyntax initializer)
    {
        _initializers = initializer;
        return this;
    }

    internal ConstructorBuilder WithModifiers(in SyntaxTokenList modifiers)
    {
        _modifiers = modifiers;
        return this;
    }

    public ConstructorDeclarationSyntax Build()
    {
        return ConstructorDeclaration(
            attributeLists: _attributes.Count > 0 ? List(_attributes) : default,
            modifiers: _modifiers,
            identifier: Identifier(name),
            parameterList: _parameterListSyntax ?? ParameterList(SeparatedList(_parameters)),
            initializer: _initializers!,
            body: _body ?? Block()
        );
    }
}
