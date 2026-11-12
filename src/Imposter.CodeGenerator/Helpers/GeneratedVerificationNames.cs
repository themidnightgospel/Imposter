using System.Collections.Generic;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.Features.Shared.Builders;

namespace Imposter.CodeGenerator.Helpers;

// Lifecycle and verification helpers appear inside both target-generic and method-generic types.
// Only fixed members need type-parameter renaming. Locals and parameters are allocated from metadata NameSets.
// TypeParameterNames preserves the target's public namespace/file identity when it performs this renaming.
internal static class GeneratedVerificationNames
{
    internal static readonly HashSet<string> Reserved =
    [
        InvocationStepNames.TryFindAfterMethodName,
        "Invocations",
        InvocationLifecycleBuilder.ClearMethodName,
        InvocationHistoryCollectionMetadata.ClearMethodName,
        InvocationStepNames.OwnerPropertyName,
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
        "IsVerified",
        "MarkVerified",
        VerificationSourceNames.LenientMethodName,
        "VerifyNoOtherCalls",
        "VerifyNoInteractions",
        "VerifyAllSetupsUsed",
        VerificationSourceNames.InteractionsMethodName,
        VerificationSourceNames.UncoveredInteractionsMethodName,
        VerificationSourceNames.UnusedSetupsMethodName,
        "_runtimeCriteria",
        "_setup",
        "_verification",
    ];
}
