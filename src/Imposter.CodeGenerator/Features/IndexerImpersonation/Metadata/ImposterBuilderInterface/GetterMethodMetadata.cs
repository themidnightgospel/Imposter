using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.ImposterBuilderInterface;

internal readonly struct GetterMethodMetadata
{
    internal readonly string Name = "Getter";

    internal readonly TypeSyntax ReturnType;

    internal GetterMethodMetadata(
        in IndexerGetterImposterBuilderInterfaceMetadata getterInterfaceMetadata
    )
    {
        ReturnType = getterInterfaceMetadata.TypeSyntax;
    }
}
