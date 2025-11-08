using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.SetterImposterBuilderInterface;

internal readonly struct CallbackMethodMetadata
{
    internal readonly string Name = "Callback";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata CallbackParameter;

    internal CallbackMethodMetadata(in ImposterPropertyCoreMetadata property, TypeSyntax setterBuilderInterfaceTypeSyntax)
    {
        ReturnType = setterBuilderInterfaceTypeSyntax;
        CallbackParameter = new ParameterMetadata("callback", property.AsSystemActionType);
    }
}