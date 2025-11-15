using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct ReturnsMethodMetadata
{
    internal readonly string Name = "Returns";

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly ParameterMetadata ValueParameter;

    internal readonly ParameterMetadata FuncParameter;

    internal readonly ParameterMetadata DelegateParameter;

    internal ReturnsMethodMetadata(
        in ImposterIndexerCoreMetadata core,
        in IndexerDelegateMetadata delegatesMetadata,
        TypeSyntax returnType,
        NameSyntax interfaceSyntax
    )
    {
        ReturnType = returnType;
        InterfaceSyntax = interfaceSyntax;
        ValueParameter = new ParameterMetadata("value", core.TypeSyntax);
        FuncParameter = new ParameterMetadata("valueGenerator", core.AsSystemFuncType);
        DelegateParameter = new ParameterMetadata(
            "valueGenerator",
            delegatesMetadata.ValueDelegateType
        );
    }
}
