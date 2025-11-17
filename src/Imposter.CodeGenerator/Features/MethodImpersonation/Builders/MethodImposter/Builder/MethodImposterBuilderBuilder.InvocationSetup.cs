using System.Collections.Generic;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.MethodImposter.Builder;

internal static partial class MethodImposterBuilderBuilder
{
    private static List<MemberDeclarationSyntax> ImplementInvocationSetupBuilderInterface(
        in ImposterTargetMethodMetadata method
    )
    {
        var implementations = new List<MemberDeclarationSyntax>
        {
            BuildThrowsGenericImplementation(method),
            BuildThrowsExceptionInstanceImplementation(method),
            BuildThrowsExceptionGeneratorImplementation(method),
            BuildCallbackImplementation(method),
        };

        if (method.HasReturnValue)
        {
            implementations.Add(BuildReturnsDelegateImplementation(method));
            implementations.Add(BuildReturnsValueImplementation(method));
        }

        if (method.MethodInvocationImposterGroup.ReturnsAsyncMethod is not null)
        {
            implementations.Add(BuildReturnsAsyncImplementation(method));
        }

        if (method.MethodInvocationImposterGroup.ThrowsAsyncMethod is not null)
        {
            implementations.Add(BuildThrowsAsyncImplementation(method));
        }

        if (method.MethodInvocationImposterGroup.UseBaseImplementationMethod is not null)
        {
            implementations.Add(BuildUseBaseImplementationImplementation(method));
        }

        implementations.Add(BuildThenImplementation(method));

        return implementations;
    }
}
