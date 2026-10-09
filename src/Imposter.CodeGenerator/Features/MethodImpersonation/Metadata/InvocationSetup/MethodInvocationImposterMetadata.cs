using Imposter.CodeGenerator.Helpers;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct MethodInvocationImposterMetadata
{
    internal readonly string ResultVariableName;

    internal readonly string ResultGeneratorFieldName;

    internal readonly string CallbacksFieldName;

    internal readonly string UseBaseImplementationFieldName;

    internal readonly string InitializeOutParametersMethodName;

    internal MethodInvocationImposterMetadata(
        in ReservedParameterNames reservedParameterNames,
        NameSet memberNames
    )
    {
        var nameContext = reservedParameterNames.CreateNameSet();
        ResultVariableName = nameContext.Use("result");
        ResultGeneratorFieldName = memberNames.Use("_resultGenerator");
        CallbacksFieldName = memberNames.Use("_callbacks");
        UseBaseImplementationFieldName = memberNames.Use("_useBaseImplementation");
        InitializeOutParametersMethodName = memberNames.Use(
            "InitializeOutParametersWithDefaultValues"
        );
    }
}
