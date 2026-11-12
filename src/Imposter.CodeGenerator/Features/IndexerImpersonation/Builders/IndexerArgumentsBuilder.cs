using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerArgumentsBuilder
{
    internal static StructDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var members = new List<MemberDeclarationSyntax>(Fields(indexer));
        if (indexer.Core.MatchedParameters.Length != 0)
            members.Add(Constructor(indexer, indexer.Arguments.Name));
        return StructDeclaration(indexer.Arguments.Name)
            .WithModifiers(
                TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.ReadOnlyKeyword))
            )
            .WithMembers(List(members));
    }

    internal static IEnumerable<FieldDeclarationSyntax> Fields(
        in ImposterIndexerMetadata indexer
    ) =>
        indexer.Core.MatchedParameters.Select(parameter =>
            SingleVariableField(
                parameter.KeptTypeSyntax,
                parameter.FieldName,
                TokenList(Token(SyntaxKind.InternalKeyword), Token(SyntaxKind.ReadOnlyKeyword))
            )
        );

    internal static ConstructorDeclarationSyntax Constructor(
        in ImposterIndexerMetadata indexer,
        string name
    )
    {
        var constructor = new ConstructorBuilder(name).WithModifiers(
            TokenList(Token(SyntaxKind.InternalKeyword))
        );
        var body = new BlockBuilder();
        foreach (var parameter in indexer.Core.MatchedParameters)
        {
            constructor.AddParameter(parameter.ParameterSyntax);
            body.AddStatement(
                ThisExpression()
                    .Dot(IdentifierName(parameter.FieldName))
                    .Assign(IdentifierName(parameter.Name))
                    .ToStatementSyntax()
            );
        }
        return constructor.WithBody(body.Build()).Build();
    }
}
