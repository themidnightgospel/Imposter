using System.Collections.Generic;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.Features.Shared.Builders;

namespace Imposter.CodeGenerator.Helpers;

// Invocation history helpers appear inside both target-generic and method-generic types.
// Only fixed members need type-parameter renaming. Locals and parameters are allocated from metadata NameSets.
// TypeParameterNames preserves the target's public namespace/file identity when it performs this renaming.
internal static class GeneratedVerificationNames
{
    internal static readonly HashSet<string> Reserved =
    [
        "Invocations",
        InvocationLifecycleBuilder.ClearMethodName,
        InvocationHistoryCollectionMetadata.ClearMethodName,
        "Snapshot",
        "MatchingInvocations",
        "CreateInvocation",
        "CaptureInvocationValue",
        "StoredKey",
        "CanReuseCapturedSpans",
        "InvokeWithCapturedSpans",
        "_outcome",
        "Arguments",
        "_value",
        "Member",
        "Sequence",
        "Exception",
        "Value",
        "Result",
        "_runtimeCriteria",
    ];
}
