using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
            BuildCallbackInterface(indexer),
            BuildContinuationInterface(indexer),
            BuildFluentInterface(indexer),
            BuildVerificationInterface(indexer),
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

    private static InterfaceDeclarationSyntax BuildContinuationInterface(
        in ImposterIndexerMetadata indexer
    )
    {
        var then = indexer.SetterBuilderInterface.ThenMethod;

        return new InterfaceDeclarationBuilder(
            indexer.SetterBuilderInterface.ContinuationInterfaceName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(indexer.SetterBuilderInterface.CallbackInterfaceTypeSyntax))
            .AddMember(InterfaceMethod(then.ReturnType, then.Name))
            .Build();
    }

    private static InterfaceDeclarationSyntax BuildCallbackInterface(
        in ImposterIndexerMetadata indexer
    )
    {
        var callback = indexer.SetterBuilderInterface.CallbackMethod;

        return new InterfaceDeclarationBuilder(indexer.SetterBuilderInterface.CallbackInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                InterfaceMethod(callback.ReturnType, callback.Name, callback.CallbackParameter)
            )
            .Build();
    }

    private static InterfaceDeclarationSyntax BuildVerificationInterface(
        in ImposterIndexerMetadata indexer
    )
    {
        var called = indexer.SetterBuilderInterface.CalledMethod;

        return new InterfaceDeclarationBuilder(
            indexer.SetterBuilderInterface.VerificationInterfaceName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(InterfaceMethod(called.ReturnType, called.Name, called.CountParameter))
            .Build();
    }
}
