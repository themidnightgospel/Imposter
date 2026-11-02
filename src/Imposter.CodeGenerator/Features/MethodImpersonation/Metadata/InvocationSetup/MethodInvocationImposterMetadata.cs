using Imposter.CodeGenerator.Helpers;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;

internal readonly struct MethodInvocationImposterMetadata
{
    internal readonly string IsConfiguredFieldName;

    internal readonly string ResultVariableName;

    internal readonly string ResultGeneratorFieldName;

    internal readonly string CallbacksFieldName;

    internal readonly string UseBaseImplementationFieldName;

    internal readonly string InitializeOutParametersMethodName;

    internal readonly string CachedAsyncResultVariableName;

    // Async factory used for default outcomes and setup-time ThrowsAsync caching.
    internal readonly string AsyncResultFunctionName;

    internal MethodInvocationImposterMetadata(
        in ReservedParameterNames reservedParameterNames,
        NameSet memberNames
    )
    {
        var nameContext = reservedParameterNames.CreateNameSet();
        ResultVariableName = nameContext.Use("result");
        AsyncResultFunctionName = nameContext.Use("AsyncResult");
        CachedAsyncResultVariableName = nameContext.Use("asyncResult");
        IsConfiguredFieldName = memberNames.Use("IsConfigured");
        ResultGeneratorFieldName = memberNames.Use("_resultGenerator");
        CallbacksFieldName = memberNames.Use("_callbacks");
        UseBaseImplementationFieldName = memberNames.Use("_useBaseImplementation");
        InitializeOutParametersMethodName = memberNames.Use(
            "InitializeOutParametersWithDefaultValues"
        );
    }
}
