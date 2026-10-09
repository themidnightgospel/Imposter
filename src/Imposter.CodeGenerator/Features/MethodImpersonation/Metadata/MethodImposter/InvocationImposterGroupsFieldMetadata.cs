namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly struct InvocationImposterGroupsFieldMetadata
{
    internal readonly string Name;

    internal InvocationImposterGroupsFieldMetadata(in ReservedParameterNames reservedParameterNames)
    {
        var nameContext = reservedParameterNames.CreateNameSet();
        Name = nameContext.Use("_invocationImposters");
    }
}
