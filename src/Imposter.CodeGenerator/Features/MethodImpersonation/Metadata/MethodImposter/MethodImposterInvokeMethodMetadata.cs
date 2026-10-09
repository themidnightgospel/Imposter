using Imposter.CodeGenerator.SyntaxHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly struct MethodImposterInvokeMethodMetadata
{
    internal const string Name = "Invoke";

    internal readonly string ExceptionVariableName;

    internal readonly string ResultVariableName;

    internal readonly string MatchingInvocationImposterGroupVariableName;

    internal readonly string ArgumentsVariableName;

    internal readonly ParameterMetadata BaseInvocationParameter;

    internal readonly string CallbackIterationVariableName;

    internal readonly string InvocationBehaviorParameterName;

    internal readonly string MethodDisplayNameParameterName;

    internal readonly string InvocationImposterVariableName;

    public MethodImposterInvokeMethodMetadata(
        in ReservedParameterNames reservedParameterNames,
        TypeSyntax delegateSyntax
    )
    {
        var parameterNameContext = reservedParameterNames.CreateNameSet();

        ExceptionVariableName = parameterNameContext.Use("ex");
        ResultVariableName = parameterNameContext.Use("result");
        MatchingInvocationImposterGroupVariableName = parameterNameContext.Use(
            "matchingInvocationImposterGroup"
        );
        ArgumentsVariableName = parameterNameContext.Use("arguments");
        BaseInvocationParameter = new ParameterMetadata(
            parameterNameContext.Use("baseImplementation"),
            delegateSyntax.ToNullableType(),
            SyntaxFactoryHelper.Null
        );
        CallbackIterationVariableName = parameterNameContext.Use("callback");
        InvocationBehaviorParameterName = parameterNameContext.Use("invocationBehavior");
        MethodDisplayNameParameterName = parameterNameContext.Use("methodDisplayName");
        InvocationImposterVariableName = parameterNameContext.Use("invocationImposter");
    }
}
