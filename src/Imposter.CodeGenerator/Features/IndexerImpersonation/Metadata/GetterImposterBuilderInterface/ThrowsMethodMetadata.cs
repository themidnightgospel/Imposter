using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.IndexerImpersonation.Metadata.GetterImposterBuilderInterface;

internal readonly struct ThrowsMethodMetadata
{
    internal readonly string Name = "Throws";

    internal readonly ExceptionTypeParameterMetadata ExceptionTypeParameter;

    internal readonly TypeSyntax ReturnType;

    internal readonly NameSyntax InterfaceSyntax;

    internal readonly ParameterMetadata ExceptionParameter;

    internal readonly ParameterMetadata DelegateParameter;

    internal ThrowsMethodMetadata(
        in IndexerDelegateMetadata delegatesMetadata,
        in ExceptionTypeParameterMetadata exceptionTypeParameter,
        TypeSyntax returnType,
        NameSyntax interfaceSyntax
    )
    {
        ExceptionTypeParameter = exceptionTypeParameter;
        ReturnType = returnType;
        InterfaceSyntax = interfaceSyntax;
        ExceptionParameter = new ParameterMetadata("exception", WellKnownTypes.System.Exception);
        DelegateParameter = new ParameterMetadata(
            "exceptionGenerator",
            delegatesMetadata.ExceptionDelegateType
        );
    }
}
