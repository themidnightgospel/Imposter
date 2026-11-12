using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.FluentInterfaceBuilder;
using static Imposter.CodeGenerator.Features.Shared.Builders.InterfaceMethodBuilder;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerSetterImposterBuilderInterfaceBuilder
{
    internal static MemberDeclarationSyntax[] Build(in ImposterIndexerMetadata indexer)
    {
        if (!indexer.Core.HasSetter)
        {
            return [];
        }

        return
        [
            CallbackInterface(
                indexer.SetterBuilderInterface.CallbackInterfaceName,
                indexer.SetterBuilderInterface.CallbackMethod
            ),
            ContinuationInterface(
                indexer.SetterBuilderInterface.ContinuationInterfaceName,
                indexer.SetterBuilderInterface.CallbackInterfaceTypeSyntax,
                indexer.SetterBuilderInterface.ThenMethod
            ),
            BuildFluentInterface(indexer),
            VerificationInterface(
                    indexer.SetterBuilderInterface.VerificationInterfaceName,
                    indexer.SetterBuilderInterface.CalledMethod
                )
                .AddMembers(IndexerHistoryBuilder.Invocations(indexer, false, false)),
            BuildBuilderInterface(indexer),
        ];
    }

    private static InterfaceDeclarationSyntax BuildBuilderInterface(
        in ImposterIndexerMetadata indexer
    )
    {
        var builder = new InterfaceDeclarationBuilder(indexer.SetterBuilderInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(indexer.SetterBuilderInterface.CallbackInterfaceTypeSyntax))
            .AddBaseType(
                SimpleBaseType(indexer.SetterBuilderInterface.VerificationInterfaceTypeSyntax)
            );

        if (indexer.SetterBuilderInterface.UseBaseImplementationMethod is { } useBaseImplementation)
        {
            builder.AddMember(
                InterfaceMethod(useBaseImplementation.ReturnType, useBaseImplementation.Name)
            );
        }

        return builder.Build();
    }

    private static InterfaceDeclarationSyntax BuildFluentInterface(
        in ImposterIndexerMetadata indexer
    ) =>
        new InterfaceDeclarationBuilder(indexer.SetterBuilderInterface.FluentInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(indexer.SetterBuilderInterface.CallbackInterfaceTypeSyntax))
            .AddBaseType(
                SimpleBaseType(indexer.SetterBuilderInterface.ContinuationInterfaceTypeSyntax)
            )
            .Build();
}
