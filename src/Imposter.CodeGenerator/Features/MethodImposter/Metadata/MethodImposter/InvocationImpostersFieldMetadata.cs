namespace Imposter.CodeGenerator.Features.MethodImposter.Metadata.MethodImposter;

internal readonly struct InvocationImpostersFieldMetadata
{
    internal readonly string Name;

    internal InvocationImpostersFieldMetadata(IParameterNameContextProvider parameterNameContextProvider)
    {
        var nameContext = parameterNameContextProvider.CreateParameterNameContext();
        Name = nameContext.Use("_invocationImposters");
    }
}
