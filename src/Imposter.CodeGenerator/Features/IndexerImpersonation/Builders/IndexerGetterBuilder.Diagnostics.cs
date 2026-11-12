using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static partial class IndexerGetterBuilder
{
    private static MethodDeclarationSyntax BuildGetterConfiguredSetups(
        in ImposterIndexerMetadata indexer
    )
    {
        var getter = indexer.GetterImplementation;
        var listType = WellKnownTypes.System.Collections.Generic.List(WellKnownTypes.String);
        var result = IdentifierName(indexer.Locals["result"]);
        var setup = IdentifierName(indexer.Locals["setup"]);
        return new MethodDeclarationBuilder(listType, getter.ConfiguredSetupsMethodName)
            .AddModifier(Token(SyntaxKind.PrivateKeyword))
            .WithBody(
                Block(
                    LocalVariableDeclarationSyntax(Var, indexer.Locals["result"], listType.New()),
                    IfStatement(
                        IdentifierName(getter.SetupsField.Name).IsNotNull(),
                        ForEachStatement(
                            Var,
                            setup.Identifier,
                            IdentifierName(getter.SetupsField.Name),
                            Block(
                                IfStatement(
                                    setup
                                        .Dot(
                                            IdentifierName(
                                                IndexerGetterImposterMetadata
                                                    .GetterInvocationMetadata
                                                    .OutcomeDescriptionsFieldName
                                            )
                                        )
                                        .IsNotNull(),
                                    result
                                        .Dot(IdentifierName("Add"))
                                        .Call(
                                            Argument(setup.Dot(IdentifierName("ToString")).Call())
                                        )
                                        .ToStatementSyntax()
                                )
                            )
                        )
                    ),
                    ReturnStatement(result)
                )
            )
            .Build();
    }
}
