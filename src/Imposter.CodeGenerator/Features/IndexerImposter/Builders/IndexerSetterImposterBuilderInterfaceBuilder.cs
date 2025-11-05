using Imposter.CodeGenerator.Features.IndexerImposter.Metadata;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata.SetterImposterBuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Builders;

internal static class IndexerSetterImposterBuilderInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax? Build(in ImposterIndexerMetadata indexer)
    {
        if (!indexer.Core.HasSetter)
        {
            return null;
        }

        var setterInterface = indexer.SetterBuilderInterface;

        return new InterfaceDeclarationBuilder(setterInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(BuildCallbackMethod(setterInterface))
            .AddMember(BuildCalledMethod(setterInterface))
            .AddMember(BuildThenMethod(setterInterface))
            .Build();
    }

    private static MethodDeclarationSyntax BuildCallbackMethod(IndexerSetterImposterBuilderInterfaceMetadata setterInterface) =>
        new MethodDeclarationBuilder(setterInterface.CallbackMethod.ReturnType, setterInterface.CallbackMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(setterInterface.CallbackMethod.CallbackParameter))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildCalledMethod(IndexerSetterImposterBuilderInterfaceMetadata setterInterface) =>
        new MethodDeclarationBuilder(setterInterface.CalledMethod.ReturnType, setterInterface.CalledMethod.Name)
            .AddParameter(SyntaxFactoryHelper.ParameterSyntax(setterInterface.CalledMethod.CountParameter))
            .WithSemicolon()
            .Build();

    private static MethodDeclarationSyntax BuildThenMethod(IndexerSetterImposterBuilderInterfaceMetadata setterInterface) =>
        new MethodDeclarationBuilder(setterInterface.ThenMethod.ReturnType, setterInterface.ThenMethod.Name)
            .WithSemicolon()
            .Build();
}
