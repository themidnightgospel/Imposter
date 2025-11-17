namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct MethodInvocationImposterMetadata
{
    internal readonly string ResultVariableName;

    internal MethodInvocationImposterMetadata(
        IParameterNameContextProvider parameterNameContextProvider
    )
    {
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        ResultVariableName = nameContext.Use("result");
    }
}
