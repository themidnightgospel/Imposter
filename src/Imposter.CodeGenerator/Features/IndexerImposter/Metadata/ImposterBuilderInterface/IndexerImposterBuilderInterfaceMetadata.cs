using Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.IndexerImposter.Metadata.SetterImposterBuilderInterface;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.ImposterBuilderInterface;

internal readonly struct IndexerImposterBuilderInterfaceMetadata
{
    internal readonly string Name;

    internal readonly NameSyntax TypeSyntax;

    internal readonly GetterMethodMetadata GetterMethod;

    internal readonly SetterMethodMetadata SetterMethod;

    internal IndexerImposterBuilderInterfaceMetadata(
        in ImposterIndexerCoreMetadata core,
        in IndexerSetterImposterBuilderInterfaceMetadata setterInterfaceMetadata,
        in IndexerGetterImposterBuilderInterfaceMetadata getterInterfaceMetadata
    )
    {
        Name = $"I{core.UniqueName}IndexerBuilder";
        TypeSyntax = SyntaxFactory.ParseName(Name);
        GetterMethod = new GetterMethodMetadata(core, getterInterfaceMetadata);
        SetterMethod = new SetterMethodMetadata(core, setterInterfaceMetadata);
    }
}
