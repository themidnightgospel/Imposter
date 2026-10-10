using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImpersonation.Metadata.GetterImposterBuilderInterface;

internal readonly struct ReturnsMethodMetadata
{
    internal readonly string Name = "Returns";

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    // Returns(value) keeps the value, so there's none for a value passed through.
    internal readonly ParameterMetadata? ValueParameter;

    internal readonly ParameterMetadata ValueGeneratorParameter;

    internal ReturnsMethodMetadata(
        in ImposterPropertyCoreMetadata property,
        TypeSyntax returnType,
        NameSyntax interfaceSyntax
    )
    {
        ReturnType = returnType;
        InterfaceSyntax = interfaceSyntax;
        ValueParameter = property.IsPassedThrough
            ? null
            : new ParameterMetadata("value", property.NullableAwareStoredTypeSyntax);
        ValueGeneratorParameter = new ParameterMetadata(
            "valueGenerator",
            property.ValueGeneratorType
        );
    }
}
