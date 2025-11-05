using Imposter.CodeGenerator.Features.MethodImposter.Metadata;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct ReturnsMethodMetadata
{
    internal readonly string Name = "Returns";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ValueParameter;

    internal readonly ParameterMetadata FuncParameter;

    internal readonly ParameterMetadata DelegateParameter;

    internal ReturnsMethodMetadata(
        in ImposterIndexerCoreMetadata core,
        in IndexerDelegateMetadata delegatesMetadata,
        TypeSyntax interfaceType)
    {
        ReturnType = interfaceType;
        ValueParameter = new ParameterMetadata("value", core.TypeSyntax);
        FuncParameter = new ParameterMetadata("valueGenerator", core.AsSystemFuncType);
        DelegateParameter = new ParameterMetadata("valueGenerator", delegatesMetadata.ValueDelegateType);
    }
}
