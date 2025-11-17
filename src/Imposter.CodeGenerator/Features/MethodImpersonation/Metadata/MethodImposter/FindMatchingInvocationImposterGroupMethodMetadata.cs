namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.MethodImposter;

internal readonly struct FindMatchingInvocationImposterGroupMethodMetadata
{
    internal readonly string Name;

    internal readonly string SetupVariableName;

    public FindMatchingInvocationImposterGroupMethodMetadata(
        IParameterNameContextProvider parameterNameContextProvider
    )
    {
        Name = "FindMatchingInvocationImposterGroup";
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        SetupVariableName = nameContext.Use("invocationImposterGroup");
    }
}
