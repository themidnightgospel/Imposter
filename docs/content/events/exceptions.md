# Event Exceptions

Understand how exceptions interact with the event pipeline and how to assert failure paths.

## No subscribers

Raising with no subscribers is a no-op:

!!! example
    ```csharp
    imposter.SomethingHappened.Raise(this, EventArgs.Empty); // does nothing
    ```

## Subscriber throws

If a subscribed handler throws, the exception bubbles up and subsequent handlers are not invoked unless the exception is caught by your code under test:

!!! example
    ```csharp
    EventHandler boom = (s, e) => throw new InvalidOperationException("boom");
    service.SomethingHappened += boom;

    Assert.Throws<InvalidOperationException>(() => imposter.SomethingHappened.Raise(this, EventArgs.Empty));
    ```

If you want to continue invoking the remaining handlers even if one throws, catch exceptions in your code under test and make that behavior explicit.
