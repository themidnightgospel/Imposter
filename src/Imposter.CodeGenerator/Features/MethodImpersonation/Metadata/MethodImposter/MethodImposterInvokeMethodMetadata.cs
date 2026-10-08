namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly struct MethodImposterInvokeMethodMetadata
{
    internal const string Name = "Invoke";

    internal readonly string ExceptionVariableName;

    internal readonly string ResultVariableName;

    internal readonly string MatchingInvocationImposterGroupVariableName;

    internal readonly string ArgumentsVariableName;

    internal readonly string BaseInvocationParameterName;

    internal readonly string CallbackIterationVariableName;

    internal readonly string InvocationBehaviorParameterName;

    internal readonly string MethodDisplayNameParameterName;

    internal readonly string InvocationImposterVariableName;

    public MethodImposterInvokeMethodMetadata(in ReservedParameterNames reservedParameterNames)
    {
        var parameterNameContext = reservedParameterNames.CreateNameSet();

        ExceptionVariableName = parameterNameContext.Use("ex");
        ResultVariableName = parameterNameContext.Use("result");
        MatchingInvocationImposterGroupVariableName = parameterNameContext.Use(
            "matchingInvocationImposterGroup"
        );
        ArgumentsVariableName = parameterNameContext.Use("arguments");
        BaseInvocationParameterName = parameterNameContext.Use("baseImplementation");
        CallbackIterationVariableName = parameterNameContext.Use("callback");
        InvocationBehaviorParameterName = parameterNameContext.Use("invocationBehavior");
        MethodDisplayNameParameterName = parameterNameContext.Use("methodDisplayName");
        InvocationImposterVariableName = parameterNameContext.Use("invocationImposter");
    }
}
