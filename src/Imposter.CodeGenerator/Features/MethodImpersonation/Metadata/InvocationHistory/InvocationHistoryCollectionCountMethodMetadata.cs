using System.Linq;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.ImposterTargetMethod;
using Imposter.CodeGenerator.Helpers;

namespace Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;

internal readonly struct InvocationHistoryCollectionCountMethodMetadata
{
    internal const string Name = "Count";
    internal const string ArgumentsCriteriaParameterName = "argumentsCriteria";
    internal readonly string CountVariableName;
    internal readonly string InvocationVariableName;

    internal InvocationHistoryCollectionCountMethodMetadata(in ImposterTargetMethodMetadata method)
    {
        var names = new NameSet(
            method.Model.TypeParameters.Select(p => p.Name).Append(ArgumentsCriteriaParameterName)
        );
        CountVariableName = names.Use("count");
        InvocationVariableName = names.Use("invocation");
    }
}
