using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposter;

internal readonly struct SetMethodMetadata
{
    internal readonly string Name = "Set";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ValueParameter;

    internal SetMethodMetadata(in ImposterPropertyCoreMetadata property)
    {
        ReturnType = WellKnownTypes.Void;
        ValueParameter = new ParameterMetadata("value", property.TypeSyntax);
    }
}