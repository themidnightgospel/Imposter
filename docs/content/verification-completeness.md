# Verification completeness

Use `VerifyNoOtherCalls()` after your member assertions to check that every recorded interaction was covered by a successful verification. `VerifyNoInteractions()` checks that no interactions occurred, including interactions you already verified. `VerifyAllSetupsUsed()` checks that configured setups executed.

!!! note "Minimum C# version"
    These verification helpers require C# 8 or later. C# 14 also supports the `Target.Imposter()` factory syntax; use the generated imposter constructor on earlier versions.

!!! example "Verify used and optional setups"
    === "C# 14"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/VerificationCompleteness/VerificationCompletenessTests.cs#L22"}
        var imposter = IRepositoryExample.Imposter();
        imposter.Save(42).Callback(_ => { });
        imposter.Load(42).Returns("optional").Then().Returns("later").Lenient();
        imposter.Instance().Save(42);
        imposter.Save(42).Called(Count.Once());
        imposter.VerifyNoOtherCalls();
        imposter.VerifyAllSetupsUsed();
        ```

    === "C# 8-13"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/VerificationCompleteness/VerificationCompletenessTests.cs#L24"}
        var imposter = new IRepositoryExampleImposter();
        imposter.Save(42).Callback(_ => { });
        imposter.Load(42).Returns("optional").Then().Returns("later").Lenient();
        imposter.Instance().Save(42);
        imposter.Save(42).Called(Count.Once());
        imposter.VerifyNoOtherCalls();
        imposter.VerifyAllSetupsUsed();
        ```

The same checks can aggregate several imposters and report all their failures:

!!! example "Verify several imposters"
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/VerificationCompleteness/VerificationCompletenessTests.cs#L37"}
    var repository = new IRepositoryExampleImposter();
    var audit = new IRepositoryExampleImposter();
    Imposters.VerifyNoInteractions(repository, audit);
    repository.Instance().Save(42);
    audit.Instance().Save(42);
    repository.Save(42).Called(Count.Once());
    audit.Save(42).Called(Count.Once());
    Imposters.VerifyNoOtherCalls(repository, audit);
    Imposters.VerifyAllSetupsUsed(repository, audit);
    ```

All generated imposters implement `IImposter`. If a target member has the same name as one of these helpers, use `((IImposter)imposter).VerifyNoOtherCalls()` or the `Imposters` helper to disambiguate it.

## Coverage

A passing `Called(count)` covers the matching calls in the history snapshot captured by that check. A failing check covers nothing. `Called(Count.Never())` can pass without covering any calls. Reading `Invocations` or calling `CallCount()` does not cover calls. Calls arriving after a snapshot are left uncovered.

The successful path counts and marks matches in two passes over the same captured history. Matchers must be deterministic: the same recorded input must return the same result when evaluated again. Successful verification may evaluate your matcher twice per record.

!!! note "Successful verification records coverage"
    Reading history or calling `CallCount()` does not cover calls. Failed count checks cover nothing.

Property getters use counters and retain no typed history; their successful count assertions cover the captured count. Property setters, methods, and indexer getters and setters retain invocation records. Event subscriptions and unsubscriptions are interactions and are covered by successful `Subscribed` and `Unsubscribed` assertions. Test-driven event raises and event handler executions are excluded from these completeness checks.

Coverage is cumulative. `ClearInvocations()` clears history and coverage, while retaining setup usage and leniency. `Reset()` also removes setups and their usage state.

## Unused setups and leniency

Creating a verification handle alone does not configure a setup. Method and accessor setup chains are considered used when any configured outcome or callback executes, including one that throws; remaining `Then()` outcomes need not all execute. If several accessor setup chains match an access, each chain whose callback executes is marked used. A separate chain that never executes remains unused, even if its matcher values equal those of a used chain.

Mark an optional setup with `Lenient()` to exclude it from the unused-setup check:

!!! example "Exclude an optional setup from usage checks"
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/VerificationCompleteness/VerificationCompletenessTests.cs#L27"}
    imposter.Load(42).Returns("optional").Then().Returns("later").Lenient();
    ```

Leniency does not hide interactions or change matching, outcomes, or count verification. Event leniency applies to the event's whole setup builder, including its subscription interceptors, unsubscription interceptors, callbacks, and base-implementation setup. Event registrations are tracked individually. Event callbacks become used when a raise actually executes them; a callback registered after the last raise remains unused. The raise itself is not a setup.

For relative call ordering across imposters, see [Ordered verification](ordered-verification.md).

!!! info "Next steps"
    Read [Verification](verification.md) for count checks, [Invocation history](invocation-history.md) to inspect recorded interactions, or [Clearing history and resetting an imposter](clear-and-reset.md) to separate test phases.
