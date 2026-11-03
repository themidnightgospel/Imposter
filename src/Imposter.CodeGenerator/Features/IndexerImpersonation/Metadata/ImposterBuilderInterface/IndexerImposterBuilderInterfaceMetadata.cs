using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.SetterImposterBuilderInterface;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.ImposterBuilderInterface;

internal readonly struct IndexerImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly MethodMetadata GetterMethod;

    internal readonly MethodMetadata SetterMethod;

    internal IndexerImposterBuilderInterfaceMetadata(
        in ImposterIndexerCoreMetadata core,
        in IndexerSetterImposterBuilderInterfaceMetadata setterInterfaceMetadata,
        in IndexerGetterImposterBuilderInterfaceMetadata getterInterfaceMetadata
    )
    {
        Name = $"I{core.UniqueName}IndexerBuilder";
        TypeSyntax = SyntaxFactory.IdentifierName(Name);
        GetterMethod = new MethodMetadata("Getter", getterInterfaceMetadata.TypeSyntax);
        SetterMethod = new MethodMetadata("Setter", setterInterfaceMetadata.TypeSyntax);
    }
}
