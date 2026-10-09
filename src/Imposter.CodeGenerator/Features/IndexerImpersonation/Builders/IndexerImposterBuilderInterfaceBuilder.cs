using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata;
using Imposter.CodeGenerator.SyntaxHelpers.Builders;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.Features.Shared.Builders.InterfaceMethodBuilder;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Builders;

internal static class IndexerImposterBuilderInterfaceBuilder
{
    internal static InterfaceDeclarationSyntax Build(in ImposterIndexerMetadata indexer)
    {
        var getter = indexer.BuilderInterface.GetterMethod;
        var setter = indexer.BuilderInterface.SetterMethod;

        return new InterfaceDeclarationBuilder(indexer.BuilderInterface.Name)
            .AddModifier(Token(SyntaxKind.PublicKeyword))
            .AddMember(
                indexer.Core.HasGetter ? InterfaceMethod(getter.ReturnType, getter.Name) : null
            )
            .AddMember(
                indexer.Core.HasSetter ? InterfaceMethod(setter.ReturnType, setter.Name) : null
            )
            .Build();
    }
}
