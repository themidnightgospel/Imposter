using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposter;

internal readonly struct CallbackMethodMetadata
{
    internal readonly string Name = "Callback";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata CriteriaParameter;

    internal readonly ParameterMetadata CallbackParameter;

    internal CallbackMethodMetadata(in ImposterPropertyCoreMetadata property)
    {
        ReturnType = WellKnownTypes.Void;
        CallbackParameter = new ParameterMetadata(
            "callback",
            WellKnownTypes.System.ActionOfT(property.TypeSyntax)
        );
        CriteriaParameter = new ParameterMetadata("criteria", property.AsArgType);
    }
}
