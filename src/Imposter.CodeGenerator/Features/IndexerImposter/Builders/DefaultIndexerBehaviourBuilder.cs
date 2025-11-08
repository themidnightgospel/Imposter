using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Builders;

internal static class DefaultIndexerBehaviourBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        return new ClassDeclarationBuilder(indexer.DefaultIndexerBehaviour.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(
                SyntaxFactoryHelper.SingleVariableField(indexer.DefaultIndexerBehaviour.IsOnBackingField.Type, indexer.DefaultIndexerBehaviour.IsOnBackingField.Name, TokenList(Token(SyntaxKind.PrivateKeyword)),
                    LiteralExpression(SyntaxKind.TrueLiteralExpression)))
            .AddMember(BuildIsOnProperty(indexer))
            .AddMember(
                SyntaxFactoryHelper.SingleVariableField(
                    indexer.DefaultIndexerBehaviour.BackingField,
                    SyntaxKind.InternalKeyword,
                    ObjectCreationExpression(indexer.DefaultIndexerBehaviour.BackingField.Type)
                        .WithArgumentList(ArgumentList())))
            .AddMember(BuildGetMethod(indexer))
            .AddMember(BuildSetMethod(indexer))
            .Build();
    }

    private static PropertyDeclarationSyntax BuildIsOnProperty(in ImposterIndexerMetadata indexer)
    {
        var getBody = Block(
            ReturnStatement(
                WellKnownTypes.System.Threading.Volatile
                    .Dot(IdentifierName("Read"))
                    .Call(
                        SyntaxFactoryHelper.ArgumentListSyntax([
                            Argument(null, Token(SyntaxKind.RefKeyword), IdentifierName(indexer.DefaultIndexerBehaviour.IsOnBackingField.Name))
                        ])
                    )
            )
        );

        var setBody = Block(
            ExpressionStatement(
                WellKnownTypes.System.Threading.Volatile
                    .Dot(IdentifierName("Write"))
                    .Call(
                        SyntaxFactoryHelper.ArgumentListSyntax([
                            Argument(null, Token(SyntaxKind.RefKeyword), IdentifierName(indexer.DefaultIndexerBehaviour.IsOnBackingField.Name)),
                            Argument(IdentifierName("value"))
                        ])
                    )
            )
        );

        return PropertyDeclaration(WellKnownTypes.Bool, Identifier(indexer.DefaultIndexerBehaviour.IsOnPropertyName))
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .WithAccessorList(
                AccessorList(
                    List<AccessorDeclarationSyntax>([
                        AccessorDeclaration(SyntaxKind.GetAccessorDeclaration).WithBody(getBody),
                        AccessorDeclaration(SyntaxKind.SetAccessorDeclaration).WithBody(setBody)
                    ])
                )
            );
    }

    private static MethodDeclarationSyntax BuildGetMethod(in ImposterIndexerMetadata indexer)
    {
        var argumentsParam = Parameter(Identifier("arguments")).WithType(indexer.Arguments.TypeSyntax);
        var valueIdentifier = IdentifierName("value");

        return MethodDeclaration(indexer.Core.TypeSyntax, Identifier("Get"))
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameterListParameters(argumentsParam)
            .WithBody(Block(
                LocalDeclarationStatement(
                    VariableDeclaration(indexer.Core.TypeSyntax)
                        .WithVariables(
                            SingletonSeparatedList(
                                VariableDeclarator(valueIdentifier.Identifier)
                                    .WithInitializer(EqualsValueClause(DefaultExpression(indexer.Core.TypeSyntax)))))),
                IfStatement(
                    IdentifierName(indexer.DefaultIndexerBehaviour.BackingField.Name)
                        .Dot(IdentifierName("TryGetValue"))
                        .Call(
                            SyntaxFactoryHelper.ArgumentListSyntax(
                            [
                                Argument(IdentifierName("arguments")),
                                    Argument(null, Token(SyntaxKind.OutKeyword), valueIdentifier)
                            ])),
                    ReturnStatement(valueIdentifier)),
                ReturnStatement(DefaultExpression(indexer.Core.TypeSyntax))));
    }

    private static MethodDeclarationSyntax BuildSetMethod(in ImposterIndexerMetadata indexer)
    {
        return MethodDeclaration(WellKnownTypes.Void, Identifier("Set"))
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameterListParameters(
                Parameter(Identifier("arguments")).WithType(indexer.Arguments.TypeSyntax),
                Parameter(Identifier("value")).WithType(indexer.Core.TypeSyntax))
            .WithBody(Block(
                ExpressionStatement(
                    AssignmentExpression(
                        SyntaxKind.SimpleAssignmentExpression,
                        ElementAccessExpression(IdentifierName(indexer.DefaultIndexerBehaviour.BackingField.Name))
                            .WithArgumentList(
                                BracketedArgumentList(
                                    SingletonSeparatedList(Argument(IdentifierName("arguments"))))),
                        IdentifierName("value")))));
    }
}



