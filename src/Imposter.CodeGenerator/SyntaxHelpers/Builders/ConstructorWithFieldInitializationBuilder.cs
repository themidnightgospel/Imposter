using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.SyntaxHelpers.Builders;

internal class ConstructorWithFieldInitializationBuilder
{
    private readonly ConstructorBuilder _constructorBuilder;
    private readonly BlockBuilder _bodyBuilder = new BlockBuilder();

    internal ConstructorWithFieldInitializationBuilder(string className)
    {
        _constructorBuilder = new ConstructorBuilder(className);
    }

    internal ConstructorWithFieldInitializationBuilder AddParameter(
        in FieldMetadata fieldMetadata
    ) => AddParameter(ParameterSyntax(fieldMetadata.Type, fieldMetadata.Name), fieldMetadata.Name);

    // For a parameter named apart from the field it's stored in.
    internal ConstructorWithFieldInitializationBuilder AddParameter(
        in ParameterMetadata parameter,
        string fieldName
    ) => AddParameter(ParameterSyntax(parameter), fieldName);

    private ConstructorWithFieldInitializationBuilder AddParameter(
        ParameterSyntax parameter,
        string fieldName
    )
    {
        _constructorBuilder.AddParameter(parameter);
        _bodyBuilder.AddStatement(
            ThisExpression()
                .Dot(IdentifierName(fieldName))
                .Assign(IdentifierName(parameter.Identifier.Text))
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
        return _constructorBuilder.WithBody(_bodyBuilder.Build()).Build();
    }
}
