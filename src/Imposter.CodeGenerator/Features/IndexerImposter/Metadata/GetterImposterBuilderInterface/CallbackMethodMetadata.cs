using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct CallbackMethodMetadata
{
    internal readonly string Name = "Callback";

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly ParameterMetadata CallbackParameter;

    internal CallbackMethodMetadata(
        in IndexerDelegateMetadata delegatesMetadata,
        TypeSyntax returnType,
        NameSyntax interfaceSyntax)
    {
        ReturnType = returnType;
        InterfaceSyntax = interfaceSyntax;
        CallbackParameter = new ParameterMetadata("callback", delegatesMetadata.GetterCallbackDelegateType);
    }
}
