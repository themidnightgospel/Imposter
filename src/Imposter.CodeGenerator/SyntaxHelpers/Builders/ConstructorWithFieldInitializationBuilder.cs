using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal class ConstructorWithFieldInitializationBuilder
{
    private ConstructorBuilder _constructorBuilder;
    private readonly BlockBuilder _bodyBuilder = new BlockBuilder();

    internal ConstructorWithFieldInitializationBuilder(string className)
    {
        _constructorBuilder = new ConstructorBuilder(className);
    }
    
    internal ConstructorWithFieldInitializationBuilder AddParameter(FieldMetadata fieldMetadata)
    {
        _constructorBuilder.AddParameter(ParameterSyntax(fieldMetadata.Type, fieldMetadata.Name));;
        _bodyBuilder.AddStatement(
            ThisExpression()
                .Dot(IdentifierName(fieldMetadata.Name))
                .Assign(IdentifierName(fieldMetadata.Name))
                .ToStatementSyntax()
        );
        return this;
    }

internal ConstructorWithFieldInitializationBuilder WithModifiers(in SyntaxToken modifier)
    {
        _constructorBuilder.WithModifiers(TokenList(modifier));
        return this;
    }

    internal ConstructorDeclarationSyntax Build()
    {
        return _constructorBuilder
            .WithBody(_bodyBuilder.Build())
            .Build();
    }
}
