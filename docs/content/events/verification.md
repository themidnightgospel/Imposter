# Event Verification

Verify handler subscriptions and invocations.

## Subscribed verification

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/EventImposter/VerificationTests.cs#L17"}
    EventHandler h = (s, e) => { };

    service.SomethingHappened += h;
    service.SomethingHappened += h;

    imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.Exactly(2));
    imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.AtLeast(1));
    imposter.SomethingHappened.Subscribed(Arg<EventHandler>.Is(h), Count.AtMost(2));
    ```

## HandlerInvoked verification

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/EventImposter/VerificationTests.cs#L66"}
    int count = 0;
    EventHandler h = (s, e) => count++;
    service.SomethingHappened += h;

    imposter.SomethingHappened.Raise(this, EventArgs.Empty).Raise(this, EventArgs.Empty);

    count.ShouldBe(2);
    imposter.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Exactly(2));
    ```

## Failures

When verification fails, `VerificationFailedException` is thrown with a message that clearly reports expected vs actual counts.