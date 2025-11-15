using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilder;

internal readonly struct GetMethodMetadata
{
    internal readonly string Name = "Get";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata BaseImplementationParameter;

    internal GetMethodMetadata(in ImposterPropertyCoreMetadata property)
    {
        ReturnType = property.TypeSyntax;
        BaseImplementationParameter = new ParameterMetadata(
            "baseImplementation",
            property.AsSystemFuncType.ToNullableType(),
            Null
        );
    }
}
