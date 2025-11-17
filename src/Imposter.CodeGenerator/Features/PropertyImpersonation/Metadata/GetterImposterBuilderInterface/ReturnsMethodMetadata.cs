using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilderInterface;

internal readonly struct ReturnsMethodMetadata
{
    internal readonly string Name = "Returns";

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly ParameterMetadata ValueParameter;

    internal readonly ParameterMetadata ValueGeneratorParameter;

    internal ReturnsMethodMetadata(
        in ImposterPropertyCoreMetadata property,
        TypeSyntax returnType,
        NameSyntax interfaceSyntax
    )
    {
        ReturnType = returnType;
        InterfaceSyntax = interfaceSyntax;
        ValueParameter = new ParameterMetadata("value", property.TypeSyntax);
        ValueGeneratorParameter = new ParameterMetadata(
            "valueGenerator",
            property.AsSystemFuncType
        );
    }
}
