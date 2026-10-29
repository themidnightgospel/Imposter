using System.Collections.Generic;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationHistory;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.InvocationSetup;
using Imposter.CodeGenerator.Features.MethodImpersonation.Metadata.MethodImposter;
using Imposter.CodeGenerator.Features.Shared.BuilderInterface;
using Imposter.CodeGenerator.Features.Shared.Builders;

namespace Imposter.CodeGenerator.Helpers;

// A generic method's type parameters become type parameters of its generated helper types. They cannot share
// a name with those types' members, including private helpers. Keep the fixed names tied to their declarations;
// members named through MemberNames already avoid type parameters. The output-derived regression tests guard
// both paths when a builder gains a new member.
internal static class GeneratedMethodMemberNames
{
    internal static readonly HashSet<string> Reserved =
    [
        MethodImposterInvokeMethodMetadata.Name,
        ArgumentCriteriaTypeMetadata.AsMethodMetadata.BaseName,
        ArgumentCriteriaTypeMetadata.MatchesMethodMetadata.BaseName,
        InvocationHistoryMatchesMethodMetadata.Name,
        InvocationHistoryTypeMetadata.ArgumentsFieldName,
        InvocationHistoryTypeMetadata.ResultFieldName,
        InvocationHistoryTypeMetadata.ExceptionFieldName,
        ReturnsMethodMetadata.BaseName,
        ReturnsAsyncMethodMetadata.BaseName,
        ThrowsMethodMetadata.BaseName,
        ThrowsAsyncMethodMetadata.BaseName,
        Features.MethodImpersonation.Metadata.InvocationSetup.CallbackMethodMetadata.BaseName,
        ThenMethodMetadata.BaseName,
        UseBaseImplementationMethodMetadata.BaseName,
        MethodInvocationImposterGroupMetadata.CreateInvocationImposterMethodName,
        MethodInvocationImposterGroupMetadata.AddInvocationImposterMethodName,
        MethodInvocationImposterGroupMetadata.InvocationImpostersFieldName,
        MethodInvocationImposterGroupMetadata.ArgumentsCriteriaPropertyName,
        DefaultInvocationImposterGroupFieldMetadata.BaseName,
        HasMatchingInvocationImposterGroupMethodMetadata.BaseName,
        FormatValueMethodBuilder.Name,
        nameof(ToString),
    ];
}
