# Ordered verification

Use `Imposters.InOrder()` to check the relative entry order of selected calls across one or more imposters. Add at least one step with `Then(...)` before calling `Verify()`; an empty order throws `InvalidOperationException`.

!!! note "Minimum C# version"
    These verification helpers require C# 8 or later. C# 14 also supports the `Target.Imposter()` factory syntax; use the generated imposter constructor on earlier versions.

!!! example "Order calls across imposters"
    === "C# 14"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/OrderedVerification/OrderedVerificationTests.cs#L21"}
        var repository = IOrderedExample.Imposter();
        var audit = IOrderedExample.Imposter();
        repository.Instance().Save(42);
        audit.Instance().Save(42);
        Imposters.InOrder().Then(repository.Save(42)).Then(audit.Save(42)).Verify();
        Imposters.VerifyNoOtherCalls(repository, audit);
        ```

    === "C# 8-13"

        ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/OrderedVerification/OrderedVerificationTests.cs#L24"}
        var repository = new IOrderedExampleImposter();
        var audit = new IOrderedExampleImposter();
        repository.Instance().Save(42);
        audit.Instance().Save(42);
        Imposters.InOrder().Then(repository.Save(42)).Then(audit.Save(42)).Verify();
        Imposters.VerifyNoOtherCalls(repository, audit);
        ```

Each step selects the earliest matching entry after the previous selected entry. Repeated steps require distinct entries. Unrelated calls between steps are allowed. Entry sequence stamps place a caller before a nested callback or base-implementation call, even when the nested call finishes first.

## Event action handles

The count-less `Subscribed(handlerMatcher)` and `Unsubscribed(handlerMatcher)` overloads return `IInvocationStep` handles. Creating a handle performs no assertion and configures no behavior. Handles read current history when `Verify()` runs.

!!! example "Order event subscription actions"
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Docs/OrderedVerification/OrderedVerificationTests.cs#L36"}
    var imposter = new IOrderedExampleImposter();
    var service = imposter.Instance();
    EventHandler h = (sender, args) => { };
    var subscribed = imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h));
    var unsubscribed = imposter.SomethingHappened.Unsubscribed(Arg<EventHandler>.Is(h));
    service.SomethingHappened += h;
    service.SomethingHappened -= h;
    Imposters.InOrder().Then(subscribed).Then(unsubscribed).Verify();
    imposter.VerifyNoOtherCalls();
    ```

Property setters, indexer getters/setters and method builders also supply steps. Counter-only property getters, test-driven event raises and handler executions are excluded. Recorded subscription failures still participate in ordering.

## Coverage and reuse

A successful check covers exactly its selected entries. A failed check covers none, even when earlier steps matched. Coverage is cumulative with prior count/order checks; `VerifyNoOtherCalls()` after an order checks remaining coverage, not an exclusive history sequence.

Build an order once to reuse it. Constructing the order and adding steps can allocate; successful `Verify()` does not allocate, including its first use. Failed checks build diagnostic history separately.

!!! info "Next steps"
    See [Verification completeness](verification-completeness.md) for remaining-call checks, [Event verification](events/verification.md) for event counts, and [Invocation history](invocation-history.md) for snapshots.
