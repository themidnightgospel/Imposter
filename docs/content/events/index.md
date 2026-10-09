# Event Impersonation

## Creating an imposter

Define the target interface and enable generation:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/EventImpersonation/IEventSetupSut.cs#L1"}
    using System;
    using System.Threading.Tasks;
    using Imposter.Abstractions;
    using Imposter.Tests.Features.EventImpersonation;

    [assembly: GenerateImposter(typeof(IEventSetupSut))]

    public interface IEventSetupSut
    {
        event EventHandler SomethingHappened;

        event Func<object?, EventArgs, Task>? AsyncSomethingHappened;

        event Func<object?, EventArgs, ValueTask>? ValueTaskSomethingHappened;

        event AsyncEventHandler<EventArgs>? CustomAsyncSomethingHappened;
    }

    public delegate Task AsyncEventHandler<in TEventArgs>(object? sender, TEventArgs args)
        where TEventArgs : EventArgs;
    ```

## Subscribe/Unsubscribe Verification

!!! example
    ```csharp
    EventHandler h = (s, e) => { };

    service.SomethingHappened += h;
    service.SomethingHappened -= h;

    imposter.SomethingHappened.Subscribed(h, Count.Once());
    imposter.SomethingHappened.Unsubscribed(h, Count.Once());
    ```

## Raise an event

!!! example
    ```csharp
    // Raise in-order for current subscribers
    imposter.SomethingHappened.Raise(this, EventArgs.Empty);
    ```

!!! note
    - `Raise(sender, args)` notifies the handlers currently subscribed at the time of the call, in subscription order. As with a C# event, `-=` removes the handler's last subscription, so a handler that is removed and added again runs after the others.
    - Callbacks registered with `Callback(...)` run before the subscribed handlers, for both `Raise` and `RaiseAsync`.
    - If no one is subscribed, `Raise` is a no-op.
    - Exceptions thrown by a handler bubble up and stop further handlers unless your SUT or test catches them (see Event Exceptions).

## Interceptors and Invocation Counts

!!! example
    ```csharp
    // Observe subscriptions/unsubscriptions
    imposter.SomethingHappened.OnSubscribe(handler => { /* inspect */ });
    imposter.SomethingHappened.OnUnsubscribe(handler => { /* inspect */ });

    // Verify handler invocation count
    imposter.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Exactly(2));
    ```

## Span parameters

An event whose delegate takes a `Span<T>` or `ReadOnlySpan<T>` can be impersonated. `Raise` passes the span to the callbacks and the subscribed handlers, and the raise history keeps a copy of its elements, so `Raised` matches them with `SpanArg<T>` or `ReadOnlySpanArg<T>` (see [Span parameters](../arguments-matching.md#span-parameters)).

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/EventImpersonation/SpanEventTests.cs#L37"}
    // delegate void TextReceivedHandler(ReadOnlySpan<char> text);
    imposter.TextReceived.Raise("ab".AsSpan());

    imposter.TextReceived.Raised(ReadOnlySpanArg<char>.Is('a', 'b'), Count.Once());
    ```

!!! warning
    The span must be passed by value, `in` or `ref readonly`, and the delegate can't be async: an async raise can't take a span, and a handler's change to a `ref` span can't reach the raiser through a copy. Such an event reports [IMP009](../diagnostics.md#imp009).
