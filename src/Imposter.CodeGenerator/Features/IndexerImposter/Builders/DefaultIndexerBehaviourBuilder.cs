using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Builders;

internal static class DefaultIndexerBehaviourBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        return new ClassDeclarationBuilder(indexer.DefaultIndexerBehaviour.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(
                SingleVariableField(indexer.DefaultIndexerBehaviour.IsOnBackingField.Type, indexer.DefaultIndexerBehaviour.IsOnBackingField.Name, TokenList(Token(SyntaxKind.PrivateKeyword)),
                    True))
            .AddMember(BuildIsOnProperty(indexer))
            .AddMember(
                SingleVariableField(
                    indexer.DefaultIndexerBehaviour.BackingField,
                    SyntaxKind.InternalKeyword,
                    indexer.DefaultIndexerBehaviour.BackingField.Type.New(EmptyArgumentListSyntax)))
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
                        ArgumentListSyntax([
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
                        ArgumentListSyntax([
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
                LocalVariableDeclarationSyntax(
                    indexer.Core.TypeSyntax,
                    valueIdentifier.Identifier.Text,
                    DefaultExpression(indexer.Core.TypeSyntax)),
                IfStatement(
                    IdentifierName(indexer.DefaultIndexerBehaviour.BackingField.Name)
                        .Dot(IdentifierName("TryGetValue"))
                        .Call(
                            ArgumentListSyntax(
                            [
                                Argument(IdentifierName("arguments")),
                                Argument(null, Token(SyntaxKind.OutKeyword), valueIdentifier)
                            ])),
                    ReturnStatement(valueIdentifier)),
                IfStatement(
                    IdentifierName(baseImplementationParam.Identifier).IsNotNull(),
                    ReturnStatement(
                        IdentifierName(baseImplementationParam.Identifier)
                            .Call(EmptyArgumentListSyntax))),
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
            ElementAccessExpression(IdentifierName(indexer.DefaultIndexerBehaviour.BackingField.Name))
                .WithArgumentList(
                    BracketedArgumentList(
                        SingletonSeparatedList(Argument(IdentifierName("arguments")))))
                .Assign(IdentifierName("value")));

        var baseInvocation = IfStatement(
            IdentifierName(baseImplementationParam.Identifier).IsNotNull(),
            Block(
                ExpressionStatement(
                    IdentifierName(baseImplementationParam.Identifier)
                        .Call()),
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