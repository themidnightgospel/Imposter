using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention.CollisionCompilation;

namespace Imposter.CodeGenerator.Tests.Features.NamingCollisionPrevention;

public class VerificationTypeParameterCollisionTests
{
    // Include numbered alternatives to ensure renaming cannot collide with another target parameter.
    private static readonly string[] AdversarialNames =
    [
        "invocations",
        "invocations_1",
        "cursor",
        "cursor_1",
        "snapshot",
        "snapshot_1",
        "setups",
        "item",
        "index",
        "interceptorIndex",
        "performedInvocations",
        "Owner",
        "Snapshot",
        "source",
        "entry",
        "sequence",
        "matches",
        "criteria",
        "count",
        "value",
        "arguments",
        "invocation",
        "current",
        "head",
        "result",
        "findings",
        "failure",
        "exception",
        "setup",
        "registration",
        "actual",
        "because",
        "description",
        "owner",
        "Member",
        "Sequence",
        "Exception",
        "Value",
        "Result",
        "Invocations",
        "MatchingInvocations",
        "CreateInvocation",
        "CaptureInvocationValue",
        "StoredKey",
        "CompleteInvocation",
        "CanReuseCapturedSpans",
        "InvokeWithCapturedSpans",
        "_outcome",
        "selectedImposter",
        "exactImposter",
        "Arguments",
        "_value",
        "IsVerified",
        "MarkVerified",
        "Lenient",
        "ClearInvocations",
        "Reset",
        "VerifyNoOtherCalls",
        "VerifyNoInteractions",
        "VerifyAllSetupsUsed",
        "Interactions",
        "UncoveredInteractions",
        "UnusedSetups",
        "Description",
        "Imposter",
        "assignmentValue",
        "assignmentCapturedValue",
        "assignmentSequence",
        "assignmentFailure",
        "assignmentException",
        "_runtimeCriteria",
        "_setup",
        "_verification",
    ];

    [Fact]
    public async Task GivenGenericTargetParametersNamedLikeVerificationHelpers_WhenUsingAllMemberKinds_ShouldCompile()
    {
        var typeParameters = string.Join(", ", AdversarialNames);
        var typeArguments = string.Join(", ", AdversarialNames.Select(_ => "int"));
        var openArguments = new string(',', AdversarialNames.Length - 1);
        await AssertCompiles(
            $"Sample.IService<{openArguments}>",
            $$"""
            public ref struct Cursor { public int Position; }
            public interface IService<{{typeParameters}}>
            {
                invocations Echo(invocations input);
                System.Threading.Tasks.Task<int> Fetch();
                bool Match<T>(System.ReadOnlySpan<T> items, params int[] flags);
                int Number { get; set; }
                System.Span<byte> Buffer { get; set; }
                Cursor Current { get; set; }
                int this[int key] { get; set; }
                event System.Action Changed;
            }
            """,
            $$"""
            var imposter = new IServiceImposter<{{typeArguments}}>();
            imposter.Fetch().ReturnsAsync(1);
            _ = imposter.Instance().Fetch();
            imposter.Match<int>(ReadOnlySpanArg<int>.Any(), Arg<int[]>.Any()).Returns(true);
            _ = imposter.Instance().Match<int>(new[] { 1 });
            imposter.Number.Setter(Arg<int>.Any()).Callback(_ => { });
            imposter.Buffer.Setter(SpanArg<byte>.Any()).Callback(_ => { });
            imposter.Current.Setter().Callback(_ => { });
            imposter.Instance().Number = 4;
            imposter.Instance().Buffer = new byte[] { 1 };
            imposter.Instance().Current = new Cursor();
            imposter.Instance()[1] = 2;
            imposter.Instance().Changed += () => { };
            _ = imposter.Number.Setter(4).Invocations;
            _ = imposter.Buffer.Setter(SpanArg<byte>.Any()).Invocations;
            _ = imposter.Current.Setter().Invocations;
            _ = imposter[1].Setter().Invocations;
            _ = imposter.Invocations();
            """,
            nameof(
                GivenGenericTargetParametersNamedLikeVerificationHelpers_WhenUsingAllMemberKinds_ShouldCompile
            )
        );
    }

    [Fact]
    public async Task GivenGenericClassParametersNamedLikeInitCaptureLocals_WhenGeneratingBaseInitAssignment_ShouldCompile()
    {
        var typeParameters = string.Join(", ", AdversarialNames);
        var typeArguments = string.Join(", ", AdversarialNames.Select(_ => "int"));
        var openArguments = new string(',', AdversarialNames.Length - 1);
        await AssertCompiles(
            $"Sample.Service<{openArguments}>",
            $$"""
            public class Service<{{typeParameters}}>
            {
                public virtual int Number { get; init; }
                public virtual invocations Echo(invocations input) => input;
            }
            """,
            $$"""
            var imposter = new ServiceImposter<{{typeArguments}}>();
            imposter.Number.Setter(Arg<int>.Any()).UseBaseImplementation();
            imposter.Number.Setter(Arg<int>.Any()).Called(Count.Never());
            _ = imposter.Number.Setter(Arg<int>.Any()).Invocations;
            """,
            nameof(
                GivenGenericClassParametersNamedLikeInitCaptureLocals_WhenGeneratingBaseInitAssignment_ShouldCompile
            )
        );
    }
}
