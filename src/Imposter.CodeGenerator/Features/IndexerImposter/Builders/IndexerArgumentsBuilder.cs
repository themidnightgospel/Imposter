using System.Collections.Generic;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Builders;

internal static class IndexerArgumentsBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var equatableType = WellKnownTypes.System.IEquatable(indexer.Arguments.TypeSyntax);

        var classBuilder = new ClassDeclarationBuilder(indexer.Arguments.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddBaseType(SimpleBaseType(equatableType));

        foreach (var parameter in indexer.Core.Parameters)
        {
            classBuilder = classBuilder.AddMember(
                SyntaxFactoryHelper.SingleVariableField(
                    new FieldMetadata(parameter.Name, parameter.TypeSyntax),
                    SyntaxKind.PublicKeyword));
        }

        classBuilder = classBuilder.AddMember(BuildConstructor(indexer));
        classBuilder = classBuilder.AddMember(BuildEqualsMethod(indexer));
        classBuilder = classBuilder.AddMember(BuildObjectEqualsMethod(indexer));
        classBuilder = classBuilder.AddMember(BuildGetHashCodeMethod(indexer));

        return classBuilder.Build();
    }

    private static ConstructorDeclarationSyntax BuildConstructor(in ImposterIndexerMetadata indexer)
    {
        var constructorBuilder = new ConstructorBuilder(indexer.Arguments.Name)
            .WithModifiers(TokenList(Token(SyntaxKind.InternalKeyword)));

        var bodyBuilder = new BlockBuilder();

        foreach (var parameter in indexer.Core.Parameters)
        {
            constructorBuilder = constructorBuilder.AddParameter(parameter.ParameterSyntax);
            bodyBuilder.AddStatement(
                ThisExpression()
                    .Dot(IdentifierName(parameter.Name))
                    .Assign(IdentifierName(parameter.Name))
                    .ToStatementSyntax());
        }

        return constructorBuilder.WithBody(bodyBuilder.Build()).Build();
    }

    private static MethodDeclarationSyntax BuildEqualsMethod(in ImposterIndexerMetadata indexer)
    {
        var otherIdentifier = Identifier("other");
        var otherParameter = Parameter(otherIdentifier).WithType(indexer.Arguments.TypeSyntax);

        ExpressionSyntax comparison = LiteralExpression(SyntaxKind.TrueLiteralExpression);
        foreach (var parameter in indexer.Core.Parameters)
        {
            var equalsExpression = BinaryExpression(
                SyntaxKind.EqualsExpression,
                IdentifierName(parameter.Name),
                IdentifierName(otherIdentifier.Text).Dot(IdentifierName(parameter.Name)));

            comparison = BinaryExpression(SyntaxKind.LogicalAndExpression, comparison, equalsExpression);
        }

        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), "Equals")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddParameter(otherParameter)
            .WithBody(Block(ReturnStatement(comparison)))
            .Build();
    }

    private static MethodDeclarationSyntax BuildObjectEqualsMethod(in ImposterIndexerMetadata indexer)
    {
        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.BoolKeyword)), "Equals")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.OverrideKeyword))
            .AddParameter(Parameter(Identifier("obj")).WithType(PredefinedType(Token(SyntaxKind.ObjectKeyword))))
            .WithBody(Block(
                ReturnStatement(
                    BinaryExpression(
                        SyntaxKind.LogicalAndExpression,
                        IsPatternExpression(
                            IdentifierName("obj"),
                            DeclarationPattern(indexer.Arguments.TypeSyntax, SingleVariableDesignation(Identifier("other")))
                        ),
                        IdentifierName("Equals")
                            .Call(ArgumentList(SingletonSeparatedList(Argument(IdentifierName("other")))))
                    ))))
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetHashCodeMethod(in ImposterIndexerMetadata indexer)
    {
        var statements = new List<StatementSyntax>
        {
            LocalVariableDeclarationSyntax(
                WellKnownTypes.System.HashCode,
                "hash",
                ObjectCreationExpression(WellKnownTypes.System.HashCode)
                    .WithArgumentList(EmptyArgumentListSyntax))
        };

        foreach (var parameter in indexer.Core.Parameters)
        {
            statements.Add(
                ExpressionStatement(
                    IdentifierName("hash")
                        .Dot(IdentifierName("Add"))
                        .Call(Argument(IdentifierName(parameter.Name)))));
        }

        statements.Add(ReturnStatement(IdentifierName("hash").Dot(IdentifierName("ToHashCode")).Call()));

        return new MethodDeclarationBuilder(PredefinedType(Token(SyntaxKind.IntKeyword)), "GetHashCode")
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddModifier(Token(SyntaxKind.OverrideKeyword))
            .WithBody(Block(statements))
            .Build();
    }
}
