# Event Impersonation

## Creating an imposter

Define the target interface and enable generation:

!!! example
    ```csharp {data-gh-link="https://github.com/themidnightgospel/Imposter/blob/master/tests/Imposter.Tests/Features/EventImposter/IEventSetupSut.cs#L1"}
    using System;
    using System.Threading.Tasks;
    using Imposter.Abstractions;
    using Imposter.Tests.Features.EventImposter;

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
    - `Raise(sender, args)` notifies the handlers currently subscribed at the time of the call, in subscription order.
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