using Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.SetterImposterBuilderInterface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.ImposterBuilderInterface;

internal readonly struct SetterMethodMetadata
{
    internal readonly string Name = "Setter";

    internal readonly TypeSyntax ReturnType;

    internal SetterMethodMetadata(
        in IndexerSetterImposterBuilderInterfaceMetadata setterInterfaceMetadata
    )
    {
        ReturnType = setterInterfaceMetadata.TypeSyntax;
    }
}
