using Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.ImposterBuilderInterface;

internal readonly struct GetterMethodMetadata
{
    internal readonly string Name = "Getter";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata[] Parameters;

    internal GetterMethodMetadata(
        in ImposterIndexerCoreMetadata core,
        in IndexerGetterImposterBuilderInterfaceMetadata getterInterfaceMetadata)
    {
        ReturnType = getterInterfaceMetadata.TypeSyntax;
        Parameters = [];
    }
}