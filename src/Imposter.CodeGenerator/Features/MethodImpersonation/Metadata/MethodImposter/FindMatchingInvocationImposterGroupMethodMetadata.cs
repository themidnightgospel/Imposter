namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly struct FindMatchingInvocationImposterGroupMethodMetadata
{
    internal readonly string Name;

    internal readonly string SetupVariableName;

    public FindMatchingInvocationImposterGroupMethodMetadata(
        in ReservedParameterNames reservedParameterNames
    )
    {
        Name = "FindMatchingInvocationImposterGroup";
        var nameContext = reservedParameterNames.CreateNameSet();
        SetupVariableName = nameContext.Use("invocationImposterGroup");
    }
}
