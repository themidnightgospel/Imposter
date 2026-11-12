# Event Verification

Verify handler subscriptions and invocations.

## Subscribed verification

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/EventImpersonation/VerificationTests.cs#L17"}
    EventHandler h = (s, e) => { };

    service.SomethingHappened += h;
    service.SomethingHappened += h;

    imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.Exactly(2));
    imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.AtLeast(1));
    imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.AtMost(2));
    ```

## HandlerInvoked verification

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/EventImpersonation/VerificationTests.cs#L81"}
    int count = 0;
    EventHandler h = (s, e) => count++;
    service.SomethingHappened += h;

    imposter.SomethingHappened.Raise(this, EventArgs.Empty).Raise(this, EventArgs.Empty);

    count.ShouldBe(2);
    imposter.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Exactly(2));
    ```

## Failures

When verification fails, `VerificationFailedException` is thrown with a message that clearly reports expected vs actual counts.

## Subscription order

The `Subscribed(handlerMatcher)` and `Unsubscribed(handlerMatcher)` overloads without a count create ordered-verification handles. They do not perform an assertion or configure behavior when created.

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

Handles read current history when `Verify()` runs. Each step selects the earliest matching action after the previous step, including actions whose subscription or unsubscription interceptor threw. Repeated steps require distinct actions. A successful order check covers only the selected actions; a failing check covers none. Steps can be mixed with method, property setter and indexer accessor handles across imposters. See [ordered verification](../ordered-verification.md) for cumulative coverage semantics.

Event raises and handler invocations are test-driven operations and are not ordered interaction steps.
