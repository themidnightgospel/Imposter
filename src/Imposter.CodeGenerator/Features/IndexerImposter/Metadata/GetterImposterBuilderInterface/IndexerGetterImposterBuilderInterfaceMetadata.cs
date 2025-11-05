using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct IndexerGetterImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly ReturnsMethodMetadata ReturnsMethod;

    internal readonly ThrowsMethodMetadata ThrowsMethod;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly ThenMethodMetadata ThenMethod;

    internal IndexerGetterImposterBuilderInterfaceMetadata(
        in ImposterIndexerCoreMetadata core,
        in IndexerDelegateMetadata delegatesMetadata)
    {
        Name = $"I{core.UniqueName}IndexerGetterBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        ReturnsMethod = new ReturnsMethodMetadata(core, delegatesMetadata, TypeSyntax);
        ThrowsMethod = new ThrowsMethodMetadata(delegatesMetadata, TypeSyntax);
        CallbackMethod = new CallbackMethodMetadata(delegatesMetadata, TypeSyntax);
        CalledMethod = new CalledMethodMetadata();
        ThenMethod = new ThenMethodMetadata(TypeSyntax);
    }
}
