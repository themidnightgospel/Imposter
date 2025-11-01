using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct ReturnsMethodMetadata
{
    internal readonly string Name = "Returns";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ValueParameter;

    internal readonly ParameterMetadata ValueGeneratorParameter;

    internal ReturnsMethodMetadata(in ImposterPropertyCoreMetadata property, TypeSyntax builderInterfaceTypeSyntax)
    {
        ReturnType = builderInterfaceTypeSyntax;
        ValueParameter = new ParameterMetadata("value", property.TypeSyntax);
        ValueGeneratorParameter = new ParameterMetadata("valueGenerator", property.AsSystemFuncType);
    }
}