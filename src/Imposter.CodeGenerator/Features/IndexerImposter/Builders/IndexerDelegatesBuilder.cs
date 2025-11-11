using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Builders;

internal static class IndexerDelegatesBuilder
{
    internal static MemberDeclarationSyntax[] Build(in ImposterIndexerMetadata indexer) =>
    [
        BuildValueDelegate(indexer),
        BuildGetterCallbackDelegate(indexer),
        BuildSetterCallbackDelegate(indexer),
        BuildExceptionDelegate(indexer)
    ];

    private static DelegateDeclarationSyntax BuildValueDelegate(in ImposterIndexerMetadata indexer)
        => DelegateDeclaration(indexer.Core.TypeSyntax, Identifier(indexer.Delegates.ValueDelegateName))
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(IndexerParameters(indexer));

    private static DelegateDeclarationSyntax BuildGetterCallbackDelegate(in ImposterIndexerMetadata indexer)
        => DelegateDeclaration(WellKnownTypes.Void, Identifier(indexer.Delegates.GetterCallbackDelegateName))
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(IndexerParameters(indexer));

    private static DelegateDeclarationSyntax BuildSetterCallbackDelegate(in ImposterIndexerMetadata indexer)
        => DelegateDeclaration(WellKnownTypes.Void, Identifier(indexer.Delegates.SetterCallbackDelegateName))
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(
                IndexerParameters(indexer)
                    .Concat([Parameter(Identifier(indexer.SetterImplementation.ValueParameterName)).WithType(indexer.Core.TypeSyntax)])
                    .ToArray());

    private static DelegateDeclarationSyntax BuildExceptionDelegate(in ImposterIndexerMetadata indexer)
        => DelegateDeclaration(WellKnownTypes.System.Exception, Identifier(indexer.Delegates.ExceptionDelegateName))
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(IndexerParameters(indexer));

    private static ParameterSyntax[] IndexerParameters(in ImposterIndexerMetadata indexer)
        => indexer.Core.Parameters
            .Select(parameter => SyntaxFactoryHelper.ParameterSyntax(parameter.TypeSyntax, parameter.Name))
            .ToArray();
}
