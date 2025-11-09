using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImposter.Metadata.GetterImposterBuilderInterface;

internal readonly struct ThrowsMethodMetadata
{
    internal readonly string Name = "Throws";

    internal readonly string GenericTypeParameterName = "TException";

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly ParameterMetadata ExceptionParameter;

    internal readonly ParameterMetadata DelegateParameter;

    internal ThrowsMethodMetadata(
        in IndexerDelegateMetadata delegatesMetadata,
        TypeSyntax returnType,
        NameSyntax interfaceSyntax)
    {
        ReturnType = returnType;
        InterfaceSyntax = interfaceSyntax;
        ExceptionParameter = new ParameterMetadata("exception", WellKnownTypes.System.Exception);
        DelegateParameter = new ParameterMetadata("exceptionGenerator", delegatesMetadata.ExceptionDelegateType);
    }
}
