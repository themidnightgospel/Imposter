using Imposter.CodeGenerator.Features.IndexerImposter.Metadata.SetterImposterBuilderInterface;
using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.ImposterBuilderInterface;

internal readonly struct SetterMethodMetadata
{
    internal readonly string Name = "Setter";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata[] Parameters;

    internal SetterMethodMetadata(
        in ImposterIndexerCoreMetadata core,
        in IndexerSetterImposterBuilderInterfaceMetadata setterInterfaceMetadata)
    {
        ReturnType = setterInterfaceMetadata.TypeSyntax;
        Parameters = [];
    }
}