namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct MethodInvocationImposterMetadata
{
    internal readonly string ResultVariableName;

    internal MethodInvocationImposterMetadata(in ReservedParameterNames reservedParameterNames)
    {
        var nameContext = reservedParameterNames.CreateNameSet();
        ResultVariableName = nameContext.Use("result");
    }
}
