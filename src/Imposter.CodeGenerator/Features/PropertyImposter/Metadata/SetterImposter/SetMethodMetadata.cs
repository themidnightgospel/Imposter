using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposter;

internal readonly struct SetMethodMetadata
{
    internal readonly string Name = "Set";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ValueParameter;

    internal readonly ParameterMetadata BaseImplementationParameter;

    internal SetMethodMetadata(in ImposterPropertyCoreMetadata property)
    {
        ReturnType = WellKnownTypes.Void;
        ValueParameter = new ParameterMetadata("value", property.TypeSyntax);
        BaseImplementationParameter = new ParameterMetadata(
            "baseImplementation",
            property.AsSystemActionType.ToNullableType(),
            Null);
    }
}
