using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.SetterImposterBuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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

        if (indexer.SetterBuilderInterface.UseBaseImplementationMethod is not null)
        {
            builder.AddMember(BuildUseBaseImplementationMethod(indexer.SetterBuilderInterface));
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
        return new InterfaceDeclarationBuilder(
            indexer.SetterBuilderInterface.ContinuationInterfaceName
        )
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddBaseType(SimpleBaseType(indexer.SetterBuilderInterface.CallbackInterfaceTypeSyntax))
            .AddMember(BuildThenMethod(indexer.SetterBuilderInterface))
            .Build();
    }

    private static InterfaceDeclarationSyntax BuildCallbackInterface(
        in ImposterIndexerMetadata indexer
    ) =>
        new InterfaceDeclarationBuilder(indexer.SetterBuilderInterface.CallbackInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildCallbackMethod(indexer.SetterBuilderInterface))
            .Build();

    private static InterfaceDeclarationSyntax BuildVerificationInterface(
        in ImposterIndexerMetadata indexer
    ) =>
        new InterfaceDeclarationBuilder(indexer.SetterBuilderInterface.VerificationInterfaceName)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildCalledMethod(indexer.SetterBuilderInterface))
            .Build();

    private static MethodDeclarationSyntax BuildCallbackMethod(
        IndexerSetterImposterBuilderInterfaceMetadata setterInterface
    ) =>
        new MethodDeclarationBuilder(
            setterInterface.CallbackMethod.ReturnType,
            setterInterface.CallbackMethod.Name
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(
                    setterInterface.CallbackMethod.CallbackParameter
                )
            )
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildCalledMethod(
        IndexerSetterImposterBuilderInterfaceMetadata setterInterface
    ) =>
        new MethodDeclarationBuilder(
            setterInterface.CalledMethod.ReturnType,
            setterInterface.CalledMethod.Name
        )
            .AddParameter(
                SyntaxFactoryHelper.ParameterSyntax(setterInterface.CalledMethod.CountParameter)
            )
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildThenMethod(
        IndexerSetterImposterBuilderInterfaceMetadata setterInterface
    ) =>
        new MethodDeclarationBuilder(
            setterInterface.ThenMethod.ReturnType,
            setterInterface.ThenMethod.Name
        )
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildUseBaseImplementationMethod(
        IndexerSetterImposterBuilderInterfaceMetadata setterInterface
    )
    {
        var metadata = setterInterface.UseBaseImplementationMethod!.Value;

        return new MethodDeclarationBuilder(metadata.ReturnType, metadata.Name)
            .WithSemicolon()
            .Build();
    }
}
