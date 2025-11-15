using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Builders;

internal static class IndexerArgumentsCriteriaBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var classBuilder = new ClassDeclarationBuilder(indexer.ArgumentsCriteria.Name).AddModifier(
            Token(SyntaxKind.InternalKeyword)
        );

        foreach (var parameter in indexer.Core.Parameters)
        {
            classBuilder = classBuilder.AddMember(
                SingleVariableField(
                    new FieldMetadata(parameter.Name, parameter.ArgTypeSyntax),
                    SyntaxKind.PublicKeyword
                )
            );
        }

        classBuilder = classBuilder.AddMember(BuildConstructor(indexer));
        classBuilder = classBuilder.AddMember(BuildMatchesMethod(indexer));

        return classBuilder.Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterIndexerMetadata indexer)
    {
        var constructorBuilder = new ConstructorBuilder(
            indexer.ArgumentsCriteria.Name
        ).WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)));

        var bodyBuilder = new BlockBuilder();

        foreach (var parameter in indexer.Core.Parameters)
        {
            constructorBuilder = constructorBuilder.AddParameter(
                ParameterSyntax(parameter.ArgTypeSyntax, parameter.Name)
            );
            bodyBuilder.AddStatement(
                ThisExpression()
                    .Dot(IdentifierName(parameter.Name))
                    .Assign(IdentifierName(parameter.Name))
                    .ToStatementSyntax()
            );
        }

        return constructorBuilder.WithBody(bodyBuilder.Build()).Build();
    }

    private static MethodDeclarationSyntax BuildMatchesMethod(in ImposterIndexerMetadata indexer)
    {
        var argumentsParam = Parameter(Identifier("arguments"))
            .WithType(indexer.Arguments.TypeSyntax);
        ExpressionSyntax? comparison = null;

        foreach (var parameter in indexer.Core.Parameters)
        {
            var parameterComparison = IdentifierName(parameter.Name)
                .Dot(IdentifierName("Matches"))
                .Call(
                    ArgumentList(
                        SingletonSeparatedList(
                            Argument(
                                IdentifierName("arguments").Dot(IdentifierName(parameter.Name))
                            )
                        )
                    )
                );

            comparison = comparison is null
                ? parameterComparison
                : comparison.And(parameterComparison);
        }

        return new MethodDeclarationBuilder(WellKnownTypes.Bool, "Matches")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(argumentsParam)
            .WithBody(Block(ReturnStatement(comparison ?? True)))
            .Build();
    }
}
