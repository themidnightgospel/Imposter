using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.SetterImposterBuilderInterface;

internal readonly struct IndexerSetterImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly CallbackMethodMetadata CallbackMethod;

    internal readonly CalledMethodMetadata CalledMethod;

    internal readonly ThenMethodMetadata ThenMethod;

    internal IndexerSetterImposterBuilderInterfaceMetadata(
        in ImposterIndexerCoreMetadata core,
        in IndexerDelegateMetadata delegatesMetadata)
    {
        Name = $"I{core.UniqueName}IndexerSetterBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        CallbackMethod = new CallbackMethodMetadata(delegatesMetadata, TypeSyntax);
        CalledMethod = new CalledMethodMetadata();
        ThenMethod = new ThenMethodMetadata(TypeSyntax);
    }
}
