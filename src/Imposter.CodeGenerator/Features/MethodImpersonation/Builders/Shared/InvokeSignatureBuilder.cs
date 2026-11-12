using System.Collections.Generic;
using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Imposter.CodeGenerator.SyntaxHelpers.SyntaxFactoryHelper;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Builders.Shared;

// The Invoke methods that pass a call from the method imposter, through the invocation imposter group, to the
// invocation imposter that handles it.
internal static class InvokeSignatureBuilder
{
    // The method imposter's: the method's parameters, then its base implementation when it has one.
    internal static ParameterListSyntax MethodImposterParameters(
        in ImposterTargetMethodMetadata method
    ) =>
        method.SupportsBaseImplementation
            ? method.Parameters.ParameterListSyntaxIncludingNullable.AddParameters(
                ParameterSyntax(method.MethodImposter.InvokeMethod.BaseInvocationParameter)
            )
            : method.Parameters.ParameterListSyntaxIncludingNullable;

    internal static ParameterListSyntax WithCapturedSpans(in ImposterTargetMethodMetadata method)
    {
        var names = method.InvocationHistory.ArgumentFields;
        return ParameterListSyntax([
            .. method
                .Parameters.InputParameterMetadata.Where(parameter => parameter.IsSpan)
                .Select(parameter =>
                    ParameterSyntax(parameter.NullableAwareKeptTypeSyntax, names[parameter.Name])
                ),
            .. MethodImposterParameters(method).Parameters,
        ]);
    }

    // The group's and the invocation imposter's, which also take the imposter's mode and the method's display name to
    // report a missing imposter.
    internal static ParameterListSyntax InvocationImposterParameters(
        in ImposterTargetMethodMetadata method
    ) =>
        ParameterListSyntax([
            ParameterSyntax(
                WellKnownTypes.Imposter.Abstractions.ImposterMode,
                method.MethodImposter.InvokeMethod.InvocationBehaviorParameterName
            ),
            ParameterSyntax(
                WellKnownTypes.String,
                method.MethodImposter.InvokeMethod.MethodDisplayNameParameterName
            ),
            .. MethodImposterParameters(method).Parameters,
        ]);

    // The arguments for InvocationImposterParameters, passing on the method's parameters and base implementation.
    internal static ArgumentListSyntax InvocationImposterArguments(
        in ImposterTargetMethodMetadata method,
        ExpressionSyntax invocationBehavior,
        ExpressionSyntax methodDisplayName
    )
    {
        List<ArgumentSyntax> arguments =
        [
            Argument(invocationBehavior),
            Argument(methodDisplayName),
            .. ArgumentListSyntax(method.Parameters.AllParameters).Arguments,
        ];

        if (method.SupportsBaseImplementation)
        {
            arguments.Add(
                method.MethodImposter.InvokeMethod.BaseInvocationParameter.Name.ToArgument()
            );
        }

        return ArgumentListSyntax(arguments);
    }
}
