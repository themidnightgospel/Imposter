using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilderInterface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.ImposterBuilderInterface;

internal readonly struct GetterMethodMetadata
{
    internal readonly string Name = "Getter";

    internal readonly TypeSyntax ReturnType;

    internal GetterMethodMetadata(
        in PropertyGetterImposterBuilderInterfaceMetadata getterInterfaceMetadata
    )
    {
        ReturnType = getterInterfaceMetadata.TypeSyntax;
    }
}
