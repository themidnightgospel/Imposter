namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.MethodImposter;

internal readonly struct MethodImposterInvokeMethodMetadata
{
    internal const string Name = "Invoke";

    internal readonly string ExceptionVariableName;

    internal readonly string ResultVariableName;

    internal readonly string MatchingInvocationImposterGroupVariableName;

    internal readonly string ArgumentsVariableName;

    internal readonly string BaseInvocationParameterName;

    internal readonly string CallbackIterationVariableName;

    public MethodImposterInvokeMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider
    )
    {
        var parameterNameContext = parameterNameContextProvider.CreateParameterNameContext();

        ExceptionVariableName = parameterNameContext.Use("ex");
        ResultVariableName = parameterNameContext.Use("result");
        MatchingInvocationImposterGroupVariableName = parameterNameContext.Use(
            "matchingInvocationImposterGroup"
        );
        ArgumentsVariableName = parameterNameContext.Use("arguments");
        BaseInvocationParameterName = parameterNameContext.Use("baseImplementation");
        CallbackIterationVariableName = parameterNameContext.Use("callback");
    }
}
