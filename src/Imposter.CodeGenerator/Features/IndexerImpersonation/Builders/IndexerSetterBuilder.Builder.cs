using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.IndexerImpersonation.Builders.IndexerImposterBuilderCommon;
using static Imposter.CodeGenerator.Features.Shared.Builders.ThenMethodBuilder;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

// The SetterImposter's nested Builder class, which configures and verifies assignments for some criteria.
internal static partial class IndexerSetterBuilder
{
    private static ClassDeclarationSyntax BuildSetterBuilder(in ImposterIndexerMetadata indexer)
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        return AccessorBuilder(
                builderMetadata,
                indexer.SetterImplementation.TypeSyntax,
                indexer.ArgumentsCriteria.TypeSyntax,
                indexer.SetterBuilderInterface.TypeSyntax,
                indexer.SetterBuilderInterface.FluentInterfaceTypeSyntax
            )
            .AddMember(BuildSetterBuilderCallbackMethod(indexer))
            .AddMembers(IndexerVerificationBuilder.Step(indexer, false))
            .AddMember(IndexerVerificationBuilder.Lenient(indexer, false))
            .AddMember(IndexerHistoryBuilder.Invocations(indexer, false, true))
            .AddMember(
                ForwardCalled(
                    builderMetadata,
                    indexer.SetterBuilderInterface.VerificationInterfaceTypeSyntax,
                    indexer.SetterBuilderInterface.CalledMethod,
                    IndexerSetterImposterMetadata.CalledMethodName
                )
            )
            .AddMember(ThenReturningThis(indexer.SetterBuilderInterface.ThenMethod))
            .AddMember(
                ForwardCalled(
                    builderMetadata,
                    indexer.SetterBuilderInterface.VerificationInterfaceTypeSyntax,
                    indexer.SetterBuilderInterface.CalledMethod,
                    IndexerSetterImposterMetadata.CalledMethodName,
                    withReason: true
                )
            )
            .AddMember(
                indexer.SetterBuilderInterface.UseBaseImplementationMethod is not null
                    ? BuildSetterBuilderUseBaseImplementationMethod(indexer)
                    : null
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderCallbackMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        var callbackMetadata = indexer.SetterBuilderInterface.CallbackMethod;
        var parameter = ParameterSyntax(callbackMetadata.CallbackParameter);

        return new MethodDeclarationBuilder(callbackMetadata.ReturnType, callbackMetadata.Name)
            .WithExplicitInterfaceSpecifier(callbackMetadata.InterfaceSyntax)
            .AddParameter(parameter)
            .WithBody(
                Block(
                    IdentifierName(builderMetadata.ImposterFieldName)
                        .Dot(IdentifierName(IndexerSetterImposterMetadata.CallbackMethodName))
                        .Call([
                            Argument(IdentifierName(builderMetadata.CriteriaFieldName)),
                            Argument(IdentifierName(parameter.Identifier)),
                        ])
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
    }

    private static MethodDeclarationSyntax BuildSetterBuilderUseBaseImplementationMethod(
        in ImposterIndexerMetadata indexer
    )
    {
        var builderMetadata = indexer.SetterImplementation.Builder;
        var metadata = indexer.SetterBuilderInterface.UseBaseImplementationMethod!.Value;

        return new MethodDeclarationBuilder(metadata.ReturnType, metadata.Name)
            .WithExplicitInterfaceSpecifier(metadata.InterfaceSyntax)
            .WithBody(
                Block(
                    IdentifierName(builderMetadata.ImposterFieldName)
                        .Dot(
                            IdentifierName(
                                IndexerSetterImposterMetadata.UseBaseImplementationMethodName
                            )
                        )
                        .Call(Argument(IdentifierName(builderMetadata.CriteriaFieldName)))
                        .ToStatementSyntax(),
                    ReturnThis
                )
            )
            .Build();
    }
}
