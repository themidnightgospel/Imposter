using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerArgumentsCriteriaBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var classBuilder = new ClassDeclarationBuilder(indexer.ArgumentsCriteria.Name).AddModifier(
            Token(SyntaxKind.InternalKeyword)
        );

        foreach (var parameter in indexer.Core.MatchedParameters)
        {
            classBuilder = classBuilder.AddMember(
                SingleVariableField(
                    new FieldMetadata(parameter.FieldName, parameter.ArgTypeSyntax),
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
        var constructor = new ConstructorWithFieldInitializationBuilder(
            indexer.ArgumentsCriteria.Name
        ).WithModifiers(Token(SyntaxKind.InternalKeyword));

        foreach (var parameter in indexer.Core.MatchedParameters)
        {
            constructor.AddParameter(
                new ParameterMetadata(parameter.Name, parameter.ArgTypeSyntax),
                parameter.FieldName
            );
        }

        return constructor.Build();
    }

    private static MethodDeclarationSyntax BuildMatchesMethod(in ImposterIndexerMetadata indexer)
    {
        var argumentsParam = ParameterSyntax(indexer.Arguments.TypeSyntax, "arguments");
        ExpressionSyntax? comparison = null;

        foreach (var parameter in indexer.Core.MatchedParameters)
        {
            var parameterComparison = ThisExpression()
                .Dot(IdentifierName(parameter.FieldName))
                .Dot(IdentifierName("Matches"))
                .Call(
                    Argument(IdentifierName("arguments").Dot(IdentifierName(parameter.FieldName)))
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
