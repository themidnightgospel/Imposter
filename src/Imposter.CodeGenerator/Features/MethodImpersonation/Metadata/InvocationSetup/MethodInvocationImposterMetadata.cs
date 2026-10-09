using Imposter.CodeGenerator.Helpers;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct MethodInvocationImposterMetadata
{
    internal readonly string ResultVariableName;

    internal readonly string ResultGeneratorFieldName;

    internal readonly string CallbacksFieldName;

    internal readonly string UseBaseImplementationFieldName;

    internal readonly string InitializeOutParametersMethodName;

    // The async local function that runs the async part of a result generator whose method has span parameters.
    internal readonly string AsyncResultFunctionName;

    internal MethodInvocationImposterMetadata(
        in ReservedParameterNames reservedParameterNames,
        NameSet memberNames
    )
    {
        var nameContext = reservedParameterNames.CreateNameSet();
        ResultVariableName = nameContext.Use("result");
        AsyncResultFunctionName = nameContext.Use("AsyncResult");
        ResultGeneratorFieldName = memberNames.Use("_resultGenerator");
        CallbacksFieldName = memberNames.Use("_callbacks");
        UseBaseImplementationFieldName = memberNames.Use("_useBaseImplementation");
        InitializeOutParametersMethodName = memberNames.Use(
            "InitializeOutParametersWithDefaultValues"
        );
    }
}
