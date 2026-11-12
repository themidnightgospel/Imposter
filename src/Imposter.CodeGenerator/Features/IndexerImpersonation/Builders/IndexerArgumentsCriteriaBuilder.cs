using System.Linq;
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

        classBuilder = classBuilder.AddMember(
            indexer.Core.HasGetter
                ? SingleVariableField(
                    new FieldMetadata(
                        indexer.ArgumentsCriteria.GetterSetupFieldName,
                        WellKnownTypes.Object.ToNullableType()
                    ),
                    SyntaxKind.InternalKeyword
                )
                : null
        );
        classBuilder = classBuilder.AddMember(BuildConstructor(indexer));
        if (indexer.Core.HasSetter)
        {
            classBuilder = classBuilder.AddMember(
                SingleVariableField(
                    WellKnownTypes.Bool,
                    indexer.ArgumentsCriteria.SetterLenientFieldName,
                    TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.VolatileKeyword))
                )
            );
            classBuilder = classBuilder.AddMember(
                SingleVariableField(
                    WellKnownTypes.Bool,
                    indexer.ArgumentsCriteria.SetterUsedFieldName,
                    TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.VolatileKeyword))
                )
            );
        }
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
        var argumentsName = indexer.Locals["arguments"];
        var argumentsParam = ParameterSyntax(indexer.Arguments.TypeSyntax, argumentsName);
        var comparison = indexer
            .Core.MatchedParameters.Select(parameter =>
                ParameterComparison(parameter, argumentsName)
            )
            .DefaultIfEmpty<ExpressionSyntax>(True)
            .Aggregate((all, next) => all.And(next));

        return new MethodDeclarationBuilder(
            WellKnownTypes.Bool,
            IndexerArgumentsCriteriaMetadata.MatchesMethodName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(argumentsParam)
            .WithBody(Block(ReturnStatement(comparison)))
            .Build();
    }

    private static InvocationExpressionSyntax ParameterComparison(
        IndexerParameterMetadata parameter,
        string argumentsName
    ) =>
        ThisExpression()
            .Dot(IdentifierName(parameter.FieldName))
            .Dot(IdentifierName("Matches"))
            .Call(Argument(IdentifierName(argumentsName).Dot(IdentifierName(parameter.FieldName))));
}
