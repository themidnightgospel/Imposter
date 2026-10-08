using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class DefaultIndexerBehaviourBuilder
{
    internal static ClassDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        return new ClassDeclarationBuilder(indexer.DefaultIndexerBehaviour.Name)
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddMember(
                SingleVariableField(
                    indexer.DefaultIndexerBehaviour.IsOnBackingField.Type,
                    indexer.DefaultIndexerBehaviour.IsOnBackingField.Name,
                    TokenList(Token(SyntaxKind.PrivateKeyword)),
                    True
                )
            )
            .AddMember(BuildIsOnProperty(indexer))
            .AddMember(
                SingleVariableField(
                    indexer.DefaultIndexerBehaviour.BackingField,
                    SyntaxKind.InternalKeyword,
                    indexer.DefaultIndexerBehaviour.BackingField.Type.New(EmptyArgumentListSyntax)
                )
            )
            .AddMember(indexer.Core.HasGetter ? BuildGetMethod(indexer) : null)
            .AddMember(indexer.Core.HasSetter ? BuildSetMethod(indexer) : null)
            .Build();
    }

    private static PropertyDeclarationSyntax BuildIsOnProperty(in ImposterIndexerMetadata indexer)
    {
        var getBody = Block(
            ReturnStatement(
                WellKnownTypes
                    .System.Threading.Volatile.Dot(IdentifierName("Read"))
                    .Call(
                        ArgumentListSyntax([
                            Argument(
                                null,
                                Token(SyntaxKind.RefKeyword),
                                IdentifierName(
                                    indexer.DefaultIndexerBehaviour.IsOnBackingField.Name
                                )
                            ),
                        ])
                    )
            )
        );

        var setBody = Block(
            WellKnownTypes
                .System.Threading.Volatile.Dot(IdentifierName("Write"))
                .Call(
                    ArgumentListSyntax([
                        Argument(
                            null,
                            Token(SyntaxKind.RefKeyword),
                            IdentifierName(indexer.DefaultIndexerBehaviour.IsOnBackingField.Name)
                        ),
                        Argument(IdentifierName("value")),
                    ])
                )
                .ToStatementSyntax()
        );

        return new PropertyDeclarationBuilder(
            WellKnownTypes.Bool,
            indexer.DefaultIndexerBehaviour.IsOnPropertyName
        )
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .WithGetterBody(getBody)
            .WithSetterBody(setBody)
            .Build();
    }

    private static MethodDeclarationSyntax BuildGetMethod(in ImposterIndexerMetadata indexer)
    {
        var argumentsParam = ParameterSyntax(indexer.Arguments.TypeSyntax, "arguments");
        var baseImplementationParam = ParameterSyntax(
                indexer.Core.AsSystemFuncType.ToNullableType(),
                "baseImplementation"
            )
            .WithDefault(EqualsValueClause(Null));
        var valueIdentifier = IdentifierName("value");

        return new MethodDeclarationBuilder(indexer.Core.NullableAwareTypeSyntax, "Get")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(argumentsParam)
            .AddParameter(baseImplementationParam)
            .WithBody(
                Block(
                    IfStatement(
                        IdentifierName(indexer.DefaultIndexerBehaviour.BackingField.Name)
                            .Dot(IdentifierName("TryGetValue"))
                            .Call(
                                ArgumentListSyntax([
                                    Argument(IdentifierName("arguments")),
                                    Argument(
                                        null,
                                        Token(SyntaxKind.OutKeyword),
                                        DeclarationExpression(
                                            Var,
                                            SingleVariableDesignation(valueIdentifier.Identifier)
                                        )
                                    ),
                                ])
                            ),
                        ReturnStatement(valueIdentifier)
                    ),
                    IfStatement(
                        IdentifierName(baseImplementationParam.Identifier).IsNotNull(),
                        ReturnStatement(
                            IdentifierName(baseImplementationParam.Identifier)
                                .Call(EmptyArgumentListSyntax)
                        )
                    ),
                    ReturnDefaultNonNullable
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetMethod(in ImposterIndexerMetadata indexer)
    {
        var baseImplementationParam = ParameterSyntax(
                indexer.Core.AsSystemActionType.ToNullableType(),
                "baseImplementation"
            )
            .WithDefault(EqualsValueClause(Null));

        var argumentsParameter = ParameterSyntax(indexer.Arguments.TypeSyntax, "arguments");

        var assignment = ElementAccessExpression(
                IdentifierName(indexer.DefaultIndexerBehaviour.BackingField.Name)
            )
            .WithArgumentList(
                BracketedArgumentList(SingletonSeparatedList(Argument(IdentifierName("arguments"))))
            )
            .Assign(IdentifierName("value"))
            .ToStatementSyntax();

        var baseInvocation = IfStatement(
            IdentifierName(baseImplementationParam.Identifier).IsNotNull(),
            Block(
                IdentifierName(baseImplementationParam.Identifier).Call().ToStatementSyntax(),
                ReturnStatement()
            )
        );

        return new MethodDeclarationBuilder(WellKnownTypes.Void, "Set")
            .AddModifier(Token(SyntaxKind.InternalKeyword))
            .AddParameter(argumentsParameter)
            .AddParameter(ParameterSyntax(indexer.Core.NullableAwareTypeSyntax, "value"))
            .AddParameter(baseImplementationParam)
            .WithBody(Block(baseInvocation, assignment))
            .Build();
    }
}
