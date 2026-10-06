namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;

internal readonly struct InvocationImpostersFieldMetadata
{
    internal readonly string Name;

    internal InvocationImpostersFieldMetadata(in ReservedParameterNames reservedParameterNames)
    {
        var nameContext = reservedParameterNames.CreateNameSet();
        Name = nameContext.Use("_invocationImposters");
    }
}
