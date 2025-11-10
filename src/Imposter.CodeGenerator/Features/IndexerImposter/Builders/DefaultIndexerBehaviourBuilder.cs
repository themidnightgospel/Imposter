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

        return new PropertyDeclarationBuilder(WellKnownTypes.Bool, indexer.DefaultIndexerBehaviour.IsOnPropertyName)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithGetterBody(getBody)
            .WithSetterBody(setBody)
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetMethod(in ImposterIndexerMetadata indexer)
    {
        var argumentsParam = Parameter(Identifier("arguments")).WithType(indexer.Arguments.TypeSyntax);
        var baseImplementationParam = Parameter(Identifier("baseImplementation"))
            .WithType(indexer.Core.AsSystemFuncType)
            .WithDefault(EqualsValueClause(LiteralExpression(SyntaxKind.NullLiteralExpression)));
        var valueIdentifier = IdentifierName("value");

        return new MethodDeclarationBuilder(indexer.Core.TypeSyntax, "Get")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(argumentsParam)
            .AddParameter(baseImplementationParam)
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
                IfStatement(
                    BinaryExpression(
                        SyntaxKind.NotEqualsExpression,
                        IdentifierName(baseImplementationParam.Identifier),
                        LiteralExpression(SyntaxKind.NullLiteralExpression)),
                    ReturnStatement(
                        InvocationExpression(IdentifierName(baseImplementationParam.Identifier))
                            .WithArgumentList(ArgumentList()))),
                ReturnStatement(DefaultExpression(indexer.Core.TypeSyntax))))
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetMethod(in ImposterIndexerMetadata indexer)
    {
        var baseImplementationParam = Parameter(Identifier("baseImplementation"))
            .WithType(indexer.Core.AsSystemActionType)
            .WithDefault(EqualsValueClause(LiteralExpression(SyntaxKind.NullLiteralExpression)));

        var argumentsParameter = Parameter(Identifier("arguments")).WithType(indexer.Arguments.TypeSyntax);

        var assignment = ExpressionStatement(
            AssignmentExpression(
                SyntaxKind.SimpleAssignmentExpression,
                ElementAccessExpression(IdentifierName(indexer.DefaultIndexerBehaviour.BackingField.Name))
                    .WithArgumentList(
                        BracketedArgumentList(
                            SingletonSeparatedList(Argument(IdentifierName("arguments"))))),
                IdentifierName("value")));

        var baseInvocation = IfStatement(
            BinaryExpression(
                SyntaxKind.NotEqualsExpression,
                IdentifierName(baseImplementationParam.Identifier),
                LiteralExpression(SyntaxKind.NullLiteralExpression)),
            Block(
                ExpressionStatement(
                    InvocationExpression(IdentifierName(baseImplementationParam.Identifier))
                        .WithArgumentList(ArgumentList())),
                ReturnStatement()));

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Set")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(argumentsParameter)
            .AddParameter(Parameter(Identifier("value")).WithType(indexer.Core.TypeSyntax))
            .AddParameter(baseImplementationParam)
            .WithBody(Block(
                baseInvocation,
                assignment))
            .Build();
    }
}



