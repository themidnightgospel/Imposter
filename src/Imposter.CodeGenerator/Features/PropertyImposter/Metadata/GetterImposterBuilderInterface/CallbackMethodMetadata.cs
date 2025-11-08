using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.PropertyImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct CallbackMethodMetadata
{
    internal readonly string Name = "Callback";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata CallbackParameter;

    internal CallbackMethodMetadata(TypeSyntax builderInterfaceTypeSyntax)
    {
        ReturnType = builderInterfaceTypeSyntax;
        CallbackParameter = new ParameterMetadata("callback", WellKnownTypes.System.Action);
    }
}