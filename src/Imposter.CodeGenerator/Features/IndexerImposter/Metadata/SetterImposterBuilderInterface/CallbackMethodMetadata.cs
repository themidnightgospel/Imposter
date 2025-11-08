using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.SetterImposterBuilderInterface;

internal readonly struct CallbackMethodMetadata
{
    internal readonly string Name = "Callback";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata CallbackParameter;

    internal CallbackMethodMetadata(in IndexerDelegateMetadata delegatesMetadata, TypeSyntax interfaceType)
    {
        ReturnType = interfaceType;
        CallbackParameter = new ParameterMetadata("callback", delegatesMetadata.SetterCallbackDelegateType);
    }
}
