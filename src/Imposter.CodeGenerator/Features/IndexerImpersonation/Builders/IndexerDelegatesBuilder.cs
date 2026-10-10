using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerDelegatesBuilder
{
    internal static MemberDeclarationSyntax[] Build(in ImposterIndexerMetadata indexer) =>
        [
            BuildValueDelegate(indexer),
            BuildGetterCallbackDelegate(indexer),
            BuildSetterCallbackDelegate(indexer),
            BuildExceptionDelegate(indexer),
            .. BuildPassedThroughValueDelegates(indexer),
        ];

    // The delegates that stand in for Func<T> and Action when the value is passed through (see
    // IndexerDelegateMetadata). They take the internal arguments class, so they're internal too.
    private static List<DelegateDeclarationSyntax> BuildPassedThroughValueDelegates(
        in ImposterIndexerMetadata indexer
    )
    {
        var delegates = new List<DelegateDeclarationSyntax>();
        if (!indexer.Core.IsPassedThrough)
        {
            return delegates;
        }

        var value = indexer.Core.NullableAwareStoredTypeSyntax;
        var arguments = SyntaxFactoryHelper.ParameterSyntax(
            indexer.Arguments.TypeSyntax,
            indexer.GetterImplementation.ArgumentsVariableName
        );
        if (indexer.Core.HasGetter)
        {
            delegates.Add(InternalDelegate(value, indexer.Delegates.BaseGetterDelegateName));
            delegates.Add(
                InternalDelegate(value, indexer.Delegates.ReturnGeneratorDelegateName, arguments)
            );
            delegates.Add(
                InternalDelegate(
                    value,
                    indexer.Delegates.ReturnHandlerDelegateName,
                    arguments,
                    SyntaxFactoryHelper.ParameterSyntax(
                        indexer.GetterImplementation.BaseImplementationParameter.Type,
                        indexer.GetterImplementation.BaseImplementationParameter.Name
                    )
                )
            );
        }

        if (indexer.Core.HasSetter)
        {
            delegates.Add(
                InternalDelegate(
                    WellKnownTypes.Void,
                    indexer.Delegates.BaseSetterDelegateName,
                    SyntaxFactoryHelper.ParameterSyntax(
                        value,
                        indexer.SetterImplementation.ValueParameterName
                    )
                )
            );
        }

        return delegates;
    }

    private static DelegateDeclarationSyntax InternalDelegate(
        TypeSyntax returnType,
        string name,
        params ParameterSyntax[] parameters
    ) =>
        DelegateDeclaration(returnType, Identifier(name))
            .AddModifiers(Token(SyntaxKind.InternalKeyword))
            .AddParameterListParameters(parameters);

    private static DelegateDeclarationSyntax BuildValueDelegate(
        in ImposterIndexerMetadata indexer
    ) =>
        DelegateDeclaration(
                indexer.Core.NullableAwareStoredTypeSyntax,
                Identifier(indexer.Delegates.ValueDelegateName)
            )
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(IndexerParameters(indexer));

    private static DelegateDeclarationSyntax BuildGetterCallbackDelegate(
        in ImposterIndexerMetadata indexer
    ) =>
        DelegateDeclaration(
                WellKnownTypes.Void,
                Identifier(indexer.Delegates.GetterCallbackDelegateName)
            )
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(IndexerParameters(indexer));

    private static DelegateDeclarationSyntax BuildSetterCallbackDelegate(
        in ImposterIndexerMetadata indexer
    ) =>
        DelegateDeclaration(
                WellKnownTypes.Void,
                Identifier(indexer.Delegates.SetterCallbackDelegateName)
            )
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(
                IndexerParameters(indexer)
                    .Concat([
                        SyntaxFactoryHelper.ParameterSyntax(
                            indexer.Core.NullableAwareStoredTypeSyntax,
                            indexer.SetterImplementation.ValueParameterName
                        ),
                    ])
                    .ToArray()
            );

    private static DelegateDeclarationSyntax BuildExceptionDelegate(
        in ImposterIndexerMetadata indexer
    ) =>
        DelegateDeclaration(
                WellKnownTypes.System.Exception,
                Identifier(indexer.Delegates.ExceptionDelegateName)
            )
            .AddModifiers(Token(SyntaxKind.PublicKeyword))
            .AddParameterListParameters(IndexerParameters(indexer));

    private static ParameterSyntax[] IndexerParameters(in ImposterIndexerMetadata indexer) =>
        indexer
            .Core.Parameters.Select(parameter =>
                SyntaxFactoryHelper.ParameterSyntax(parameter.TypeSyntax, parameter.Name)
            )
            .ToArray();
}
