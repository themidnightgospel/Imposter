# Event Impersonation

## Creating an imposter

Define the target interface and enable generation:

Example

```
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

Example

```
EventHandler h = (s, e) => { };

service.SomethingHappened += h;
service.SomethingHappened -= h;

imposter.SomethingHappened.Subscribed(h, Count.Once());
imposter.SomethingHappened.Unsubscribed(h, Count.Once());
```

## Raise an event

Example

```
// Raise in-order for current subscribers
imposter.SomethingHappened.Raise(this, EventArgs.Empty);
```

Note

- `Raise(sender, args)` notifies the handlers currently subscribed at the time of the call, in subscription order. As with a C# event, `-=` removes the handler's last subscription, so a handler that is removed and added again runs after the others.
- Callbacks registered with `Callback(...)` run before the subscribed handlers, for both `Raise` and `RaiseAsync`.
- If no one is subscribed, `Raise` is a no-op.
- Exceptions thrown by a handler bubble up and stop further handlers unless your SUT or test catches them (see Event Exceptions).
- When the delegate takes a `ref` argument, `Raise` passes it to the callbacks and handlers by reference, so their changes reach the raiser. An `out` argument starts as the default and ends with the value assigned to it last. `RaiseAsync` takes such arguments by value, since an async method can't take them by reference, so changes stay inside the raise.

## Interceptors and Invocation Counts

Example

```
// Observe subscriptions/unsubscriptions
imposter.SomethingHappened.OnSubscribe(handler => { /* inspect */ });
imposter.SomethingHappened.OnUnsubscribe(handler => { /* inspect */ });

// Verify handler invocation count
imposter.SomethingHappened.HandlerInvoked(Arg<EventHandler>.Is(h), Count.Exactly(2));
```

## Span parameters

An event whose delegate takes a `Span<T>` or `ReadOnlySpan<T>` can be impersonated. `Raise` passes the span to the callbacks and the subscribed handlers, and the raise history keeps a copy of the elements it arrives with, so `Raised` matches them with `SpanArg<T>` or `ReadOnlySpanArg<T>` (see [Span parameters](https://themidnightgospel.github.io/Imposter/0.2.11/arguments-matching/#span-parameters)). A span the delegate takes by `ref` or `out` is passed by reference, so the handlers' changes reach the raiser. An `out` span starts empty.

Example

```
// delegate void TextReceivedHandler(ReadOnlySpan<char> text);
imposter.TextReceived.Raise("ab".AsSpan());

imposter.TextReceived.Raised(ReadOnlySpanArg<char>.Is('a', 'b'), Count.Once());
```

An async method can't take a span, so for an async delegate `RaiseAsync` takes the array the span covers, and the callbacks and handlers get a span over that array. Their writes to a `Span<T>` land in it.

Example

```
// delegate Task TextReceivedAsyncHandler(object? sender, ReadOnlySpan<char> text);
await imposter.TextReceived.RaiseAsync(this, new[] { 'a', 'b' });

imposter.TextReceived.Raised(Arg<object?>.Is(this), ReadOnlySpanArg<char>.Is('a', 'b'), Count.Once());
```

Warning

An async delegate can't take the span by `ref`, `out` or `ref readonly`: passing a span by reference needs a span variable, which an async method can't declare. Such an event reports [IMP009](https://themidnightgospel.github.io/Imposter/0.2.11/diagnostics/#imp009).

## Ref struct parameters

An event whose delegate takes another `ref struct` can be impersonated too. `Raise` passes the argument on to the callbacks and the subscribed handlers, by reference when the delegate takes it by `ref` or `out`. The imposter can't keep or match it, so the raise and handler-invocation histories leave it out, and `Raised` matches the other arguments only (see [Ref struct parameters](https://themidnightgospel.github.io/Imposter/0.2.11/arguments-matching/#ref-struct-parameters)). An async delegate can't take one, because `RaiseAsync` can't, so such an event reports [IMP009](https://themidnightgospel.github.io/Imposter/0.2.11/diagnostics/#imp009).
