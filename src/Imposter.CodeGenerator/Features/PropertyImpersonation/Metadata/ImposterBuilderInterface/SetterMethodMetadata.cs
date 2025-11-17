using Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.SetterImposterBuilderInterface;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.ImposterBuilderInterface;

internal readonly struct SetterMethodMetadata
{
    internal readonly string Name = "Setter";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata CriteriaParameter;

    internal SetterMethodMetadata(
        in ImposterPropertyCoreMetadata property,
        in PropertySetterImposterBuilderInterfaceMetadata setterInterfaceMetadata
    )
    {
        ReturnType = setterInterfaceMetadata.Syntax;
        CriteriaParameter = new ParameterMetadata("criteria", property.AsArgType);
    }
}
