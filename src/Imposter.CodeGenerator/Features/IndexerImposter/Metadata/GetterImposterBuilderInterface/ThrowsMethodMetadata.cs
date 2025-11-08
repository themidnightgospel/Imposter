using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct ThrowsMethodMetadata
{
    internal readonly string Name = "Throws";

    internal readonly TypeSyntax ReturnType;

    internal readonly ParameterMetadata ExceptionParameter;

    internal readonly ParameterMetadata DelegateParameter;

    internal ThrowsMethodMetadata(in IndexerDelegateMetadata delegatesMetadata, TypeSyntax interfaceType)
    {
        ReturnType = interfaceType;
        ExceptionParameter = new ParameterMetadata("exception", WellKnownTypes.System.Exception);
        DelegateParameter = new ParameterMetadata("exceptionGenerator", delegatesMetadata.ExceptionDelegateType);
    }
}
