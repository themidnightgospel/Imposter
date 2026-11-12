using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;

internal readonly struct InvocationHistoryCollectionCountMethodMetadata
{
    internal const string Name = "Count";
    internal readonly string ArgumentsCriteriaParameterName;
    internal readonly string CountVariableName;
    internal readonly string InvocationVariableName;

    internal InvocationHistoryCollectionCountMethodMetadata(in ImposterTargetMethodMetadata method)
    {
        var names = method.ReservedParameterNames.CreateNameSet();
        ArgumentsCriteriaParameterName = names.Use("argumentsCriteria");
        CountVariableName = names.Use("count");
        InvocationVariableName = names.Use("invocation");
    }
}
