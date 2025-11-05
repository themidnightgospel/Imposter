using System.Collections.Generic;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.Features.Shared;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Builders;

internal static class IndexerArgumentsBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var equatableType = QualifiedName(
            ParseName("global::System"),
            GenericName(
                Identifier("IEquatable"),
                TypeArgumentList(SingletonSeparatedList<TypeSyntax>(indexer.Arguments.TypeSyntax))));

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
                AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        MemberAccessExpression(
                            SyntaxKind.SimpleMemberAccessExpression,
                            ThisExpression(),
                            IdentifierName(parameter.Name)),
                        IdentifierName(parameter.Name))
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

        return MethodDeclaration(PredefinedType(Token(SyntaxKind.BoolKeyword)), Identifier("Equals"))
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(otherParameter)
            .WithBody(Block(ReturnStatement(comparison)));
    }

    private static MethodDeclarationSyntax BuildObjectEqualsMethod(in ImposterIndexerMetadata indexer)
    {
        return MethodDeclaration(PredefinedType(Token(SyntaxKind.BoolKeyword)), Identifier("Equals"))
            .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.OverrideKeyword))
            .AddParameterListParameters(Parameter(Identifier("obj")).WithType(PredefinedType(Token(SyntaxKind.ObjectKeyword))))
            .WithBody(Block(
                ReturnStatement(
                    BinaryExpression(
                        SyntaxKind.LogicalAndExpression,
                        IsPatternExpression(
                            IdentifierName("obj"),
                            DeclarationPattern(indexer.Arguments.TypeSyntax, SingleVariableDesignation(Identifier("other")))
                        ),
                        InvocationExpression(
                            IdentifierName("Equals"),
                            ArgumentList(SingletonSeparatedList(Argument(IdentifierName("other")))))
                    ))));
    }

    private static MethodDeclarationSyntax BuildGetHashCodeMethod(in ImposterIndexerMetadata indexer)
    {
        var statements = new List<StatementSyntax>
        {
            LocalDeclarationStatement(
                VariableDeclaration(IdentifierName("global::System.HashCode"))
                    .WithVariables(
                        SingletonSeparatedList(
                            VariableDeclarator(Identifier("hash"))
                                .WithInitializer(EqualsValueClause(ObjectCreationExpression(IdentifierName("global::System.HashCode")).WithArgumentList(ArgumentList()))))))
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

        return MethodDeclaration(PredefinedType(Token(SyntaxKind.IntKeyword)), Identifier("GetHashCode"))
            .AddModifiers(Token(SyntaxKind.PublicKeyword), Token(SyntaxKind.OverrideKeyword))
            .WithBody(Block(statements));
    }
}
