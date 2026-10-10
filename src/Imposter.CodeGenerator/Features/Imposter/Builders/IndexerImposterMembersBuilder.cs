using System.Linq;
using Imposter.CodeGenerator.Features.Imposter.ImposterInstance;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.Imposter.Builders;

internal readonly ref struct IndexerImposterMembersBuilder(
    in ClassDeclarationBuilder imposterBuilder,
    BlockBuilder constructorBodyBuilder,
    string invocationBehaviorParameterName,
    in ImposterInstanceBuilder imposterInstanceBuilder
)
{
    private readonly ClassDeclarationBuilder _imposterBuilder = imposterBuilder;
    private readonly ImposterInstanceBuilder _imposterInstanceBuilder = imposterInstanceBuilder;

    internal void AddIndexer(in ImposterIndexerMetadata indexer)
    {
        var propertyDisplayLiteral = indexer.Core.DisplayName.StringLiteral();

        _imposterBuilder.AddMember(
            SyntaxFactoryHelper.SinglePrivateReadonlyVariableField(
                indexer.Builder.TypeSyntax,
                indexer.BuilderField.Name
            )
        );

        var invocationBuilderCreation = indexer.Builder.InvocationBuilder.TypeSyntax.New(
            SyntaxFactoryHelper.ArgumentListSyntax([
                Argument(IdentifierName(indexer.BuilderField.Name)),
                Argument(
                    indexer.ArgumentsCriteria.TypeSyntax.New(
                        SyntaxFactoryHelper.ArgumentListSyntax(
                            indexer.Core.MatchedParameters.Select(parameter =>
                                Argument(IdentifierName(parameter.Name))
                            )
                        )
                    )
                ),
            ])
        );

        var setupParameters = SeparatedList(
            indexer.Core.MatchedParameters.Select(parameter =>
                SyntaxFactoryHelper.ParameterSyntax(parameter.ArgTypeSyntax, parameter.Name)
            )
        );

        // Indexers that collide with another interface's indexer can't share the imposter's this[...]. Each is set
        // up through its interface's view, which calls this method instead. An indexer with a setup method of its
        // own is set up by calling it directly.
        _imposterBuilder.AddMember(
            indexer.IsSetUpByMethod
                ? new MethodDeclarationBuilder(
                    indexer.BuilderInterface.TypeSyntax,
                    indexer.Core.UniqueName
                )
                    .AddModifier(
                        Token(
                            indexer.RequiresExplicitInterfaceImplementation
                                ? SyntaxKind.PrivateKeyword
                                : SyntaxKind.PublicKeyword
                        )
                    )
                    .WithParameterList(ParameterList(setupParameters))
                    .WithExpressionBody(ArrowExpressionClause(invocationBuilderCreation))
                    .WithSemicolon()
                    .Build()
                : IndexerDeclaration(indexer.BuilderInterface.TypeSyntax)
                    .AddModifiers(Token(SyntaxKind.PublicKeyword))
                    .WithParameterList(BracketedParameterList(setupParameters))
                    .WithExpressionBody(ArrowExpressionClause(invocationBuilderCreation))
                    .WithSemicolonToken(Token(SyntaxKind.SemicolonToken))
        );

        constructorBodyBuilder.AddStatement(
            ThisExpression()
                .Dot(IdentifierName(indexer.BuilderField.Name))
                .Assign(
                    indexer.Builder.TypeSyntax.New(
                        SyntaxFactoryHelper.ArgumentListSyntax([
                            Argument(IdentifierName(invocationBehaviorParameterName)),
                            Argument(propertyDisplayLiteral),
                        ])
                    )
                )
                .ToStatementSyntax()
        );

        _imposterInstanceBuilder.AddIndexer(indexer);
    }
}
