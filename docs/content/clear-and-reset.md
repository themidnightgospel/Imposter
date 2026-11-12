# Clearing history and resetting an imposter

Use `ClearInvocations()` between test phases when the configured behavior should stay the same. Use `Reset()` when the same instance needs fresh imposter-owned state.

!!! example "Reuse the same instance between phases"
    === "C# 14"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/ClearAndReset/ClearAndResetTests.cs#L29"}
        var imposter = ILifecycleExample.Imposter();
        imposter.Next().Returns(1).Then().Returns(2);
        var instance = imposter.Instance();
        instance.Next().ShouldBe(1);
        var snapshot = imposter.Next().Invocations;

        imposter.ClearInvocations();
        imposter.Next().Called(Count.Never());
        snapshot.Count.ShouldBe(1);
        instance.Next().ShouldBe(2);

        imposter.Reset();
        imposter.Next().Returns(42);
        instance.Next().ShouldBe(42);
        imposter.Instance().ShouldBeSameAs(instance);
        ```

    === "C# 8-13"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/ClearAndReset/ClearAndResetTests.cs#L31"}
        var imposter = new ILifecycleExampleImposter();
        imposter.Next().Returns(1).Then().Returns(2);
        var instance = imposter.Instance();
        instance.Next().ShouldBe(1);
        var snapshot = imposter.Next().Invocations;

        imposter.ClearInvocations();
        imposter.Next().Called(Count.Never());
        snapshot.Count.ShouldBe(1);
        instance.Next().ShouldBe(2);

        imposter.Reset();
        imposter.Next().Returns(42);
        instance.Next().ShouldBe(42);
        imposter.Instance().ShouldBeSameAs(instance);
        ```

| State | `ClearInvocations()` | `Reset()` |
| --- | --- | --- |
| Invocation history and verification coverage | Cleared | Cleared |
| Setups, their usage and leniency | Kept | Cleared |
| Position in sequential outcomes | Kept | Cleared with the setup |
| Stored property and indexer values | Kept | Cleared |
| Imposter-owned event subscriptions | Kept | Cleared |
| Underlying instance identity | Kept | Kept |
| Implicit or Explicit mode | Kept | Kept |
| Previously obtained setup builders | Still connected | Detached |

!!! warning "Obtain fresh builders after Reset"
    Configuring a builder retained from before the reset has no effect on subsequent instance calls. The retained instance itself stays connected.

For class targets, reset does not rerun the constructor or roll back base-class state changed through `UseBaseImplementation()`. An unconfigured property reads its current base getter again on the first read after reset; that value may differ from the original initializer. Likewise, subscriptions managed by a base event accessor belong to the base instance, not to the imposter's subscription storage.

`ClearInvocations()` is safe while instance calls are running. An overlapping call may appear in the remaining history or be cleared; history traversal remains valid. Clearing several members does not promise one simultaneous cutoff across all members. Do not race `Reset()` with instance calls, setup changes or verification.

!!! note "Segmented history and concurrent clearing"
    Setters and event accessors publish into segmented buffers. An overlapping writer may finish publishing into the old generation after `ClearInvocations()` replaces it, so that completed interaction may be absent from subsequent snapshots. Wait for calls to finish before clearing when the test requires a precise phase boundary. Previously returned lists retain their records.

The global invocation sequence is never reset. Previously obtained invocation-list snapshots remain usable after either operation.

If the target itself declares a member called `Reset` or `ClearInvocations`, that member keeps its setup name. Access the lifecycle operation through the interface:

!!! example "Call lifecycle operations when target members share their names"
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/ClearAndReset/ClearAndResetTests.cs#L52"}
    var imposter = new ILifecycleNamedMembersExampleImposter();
    var instance = imposter.Instance();
    imposter.Reset().Returns(7);
    imposter.ClearInvocations().Returns(11);
    instance.Reset().ShouldBe(7);
    instance.ClearInvocations().ShouldBe(11);

    ((IImposter)imposter).ClearInvocations();
    imposter.Reset().Called(Count.Never());
    imposter.ClearInvocations().Called(Count.Never());
    instance.Reset().ShouldBe(7);
    instance.ClearInvocations().ShouldBe(11);

    ((IImposter)imposter).Reset();
    instance.Reset().ShouldBe(0);
    instance.ClearInvocations().ShouldBe(0);
    imposter.Instance().ShouldBeSameAs(instance);
    ```

Reset clears the histories observed by retained builders before detaching them. Those builders cannot configure or observe new calls on the reset imposter. Snapshot lists obtained before the reset keep their original records.

!!! info "Next steps"
    Read [Invocation history](invocation-history.md) to inspect captured calls, or [Verification](verification.md) to check invocation counts.
