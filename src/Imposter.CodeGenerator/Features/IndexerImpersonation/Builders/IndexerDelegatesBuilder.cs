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
    internal static MemberDeclarationSyntax[] Build(in ImposterIndexerMetadata indexer)
    {
        var keys = IndexerParameters(indexer);
        var names = indexer.Delegates;

        return
        [
            Delegate(
                SyntaxKind.PublicKeyword,
                indexer.Core.NullableAwareValueTypeSyntax,
                names.ValueDelegateName,
                keys
            ),
            Delegate(
                SyntaxKind.PublicKeyword,
                WellKnownTypes.Void,
                names.GetterCallbackDelegateName,
                keys
            ),
            Delegate(
                SyntaxKind.PublicKeyword,
                WellKnownTypes.Void,
                names.SetterCallbackDelegateName,
                [
                    .. keys,
                    SyntaxFactoryHelper.ParameterSyntax(
                        indexer.Core.NullableAwareValueTypeSyntax,
                        indexer.SetterImplementation.ValueParameterName
                    ),
                ]
            ),
            Delegate(
                SyntaxKind.PublicKeyword,
                WellKnownTypes.System.Exception,
                names.ExceptionDelegateName,
                keys
            ),
            .. BuildGeneratedValueDelegates(indexer),
        ];
    }

    // See ImposterIndexerCoreMetadata.HasGeneratedValueDelegates for when these delegates are generated and why.
    private static List<DelegateDeclarationSyntax> BuildGeneratedValueDelegates(
        in ImposterIndexerMetadata indexer
    )
    {
        var delegates = new List<DelegateDeclarationSyntax>();
        if (!indexer.Core.HasGeneratedValueDelegates)
        {
            return delegates;
        }

        var value = indexer.Core.NullableAwareValueTypeSyntax;
        var arguments = SyntaxFactoryHelper.ParameterSyntax(
            indexer.Arguments.TypeSyntax,
            indexer.GetterImplementation.ArgumentsVariableName
        );
        var passedThroughKeys = IndexerImposterBuilderCommon
            .PassedThroughKeyParameters(indexer, indexer.Core.PassedThroughKeyNames)
            .ToArray();
        if (indexer.Core.HasGetter)
        {
            delegates.Add(
                Delegate(
                    SyntaxKind.InternalKeyword,
                    value,
                    indexer.Delegates.BaseGetterDelegateName,
                    passedThroughKeys
                )
            );
            delegates.Add(
                Delegate(
                    SyntaxKind.InternalKeyword,
                    value,
                    indexer.Delegates.ReturnGeneratorDelegateName,
                    [arguments, .. passedThroughKeys]
                )
            );
            delegates.Add(
                Delegate(
                    SyntaxKind.InternalKeyword,
                    value,
                    indexer.Delegates.ReturnHandlerDelegateName,
                    [
                        arguments,
                        .. passedThroughKeys,
                        SyntaxFactoryHelper.ParameterSyntax(
                            indexer.GetterImplementation.BaseImplementationParameter.Type,
                            indexer.GetterImplementation.BaseImplementationParameter.Name
                        ),
                    ]
                )
            );
        }

        if (indexer.Core.HasSetter)
        {
            delegates.Add(
                Delegate(
                    SyntaxKind.InternalKeyword,
                    WellKnownTypes.Void,
                    indexer.Delegates.BaseSetterDelegateName,
                    [
                        .. passedThroughKeys,
                        SyntaxFactoryHelper.ParameterSyntax(
                            value,
                            indexer.SetterImplementation.ValueParameterName
                        ),
                    ]
                )
            );
        }

        return delegates;
    }

    private static DelegateDeclarationSyntax Delegate(
        SyntaxKind accessibility,
        TypeSyntax returnType,
        string name,
        params ParameterSyntax[] parameters
    ) =>
        DelegateDeclaration(returnType, Identifier(name))
            .AddModifiers(Token(accessibility))
            .AddParameterListParameters(parameters);

    private static ParameterSyntax[] IndexerParameters(in ImposterIndexerMetadata indexer) =>
        indexer
            .Core.Parameters.Select(parameter =>
                SyntaxFactoryHelper.ParameterSyntax(parameter.TypeSyntax, parameter.Name)
            )
            .ToArray();
}
