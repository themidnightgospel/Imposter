using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imposter.Abstractions;

namespace Imposter.Ideation.EventImposterPoc
{
public interface IEventImposterPocSut
{
    event EventHandler SomethingHappened;

    event Func<object?, EventArgs, Task>? AsyncSomethingHappened;
}

public interface ISomethingHappenedEventImposterBuilder
{
    ISomethingHappenedEventImposterBuilder Callback(EventHandler callback);

    ISomethingHappenedEventImposterBuilder Raise(object? sender, EventArgs args);

    ISomethingHappenedEventImposterBuilder Subscribed(Arg<EventHandler> criteria, Count count);

    ISomethingHappenedEventImposterBuilder Unsubscribed(Arg<EventHandler> criteria, Count count);

    ISomethingHappenedEventImposterBuilder OnSubscribe(Action<EventHandler> interceptor);

    ISomethingHappenedEventImposterBuilder OnUnsubscribe(Action<EventHandler> interceptor);

    ISomethingHappenedEventImposterBuilder Raised(Arg<object?> senderCriteria, Arg<EventArgs> argsCriteria, Count count);

    ISomethingHappenedEventImposterBuilder HandlerInvoked(Arg<EventHandler> handlerCriteria, Count count);
}
public interface IAsyncSomethingHappenedEventImposterBuilder
{
    IAsyncSomethingHappenedEventImposterBuilder Callback(Func<object?, EventArgs, Task> callback);

    Task<IAsyncSomethingHappenedEventImposterBuilder> RaiseAsync(object? sender, EventArgs args);

    IAsyncSomethingHappenedEventImposterBuilder Subscribed(Arg<Func<object?, EventArgs, Task>> criteria, Count count);

    IAsyncSomethingHappenedEventImposterBuilder Unsubscribed(Arg<Func<object?, EventArgs, Task>> criteria, Count count);

    IAsyncSomethingHappenedEventImposterBuilder OnSubscribe(Action<Func<object?, EventArgs, Task>> interceptor);

    IAsyncSomethingHappenedEventImposterBuilder OnUnsubscribe(Action<Func<object?, EventArgs, Task>> interceptor);

    IAsyncSomethingHappenedEventImposterBuilder Raised(Arg<object?> senderCriteria, Arg<EventArgs> argsCriteria, Count count);

    IAsyncSomethingHappenedEventImposterBuilder HandlerInvoked(Arg<Func<object?, EventArgs, Task>> handlerCriteria, Count count);
}

public sealed class EventImposterPoc : IHaveImposterInstance<IEventImposterPocSut>
{
    private const string EventSignature = "Imposter.CodeGenerator.Tests.Features.EventImposterPoc.IEventImposterPocSut.SomethingHappened";

    private readonly SomethingHappenedEventImposterBuilder _somethingHappened;
    private readonly AsyncSomethingHappenedEventImposterBuilder _asyncSomethingHappened;
    private readonly IEventImposterPocSut _instance;

    public EventImposterPoc(ImposterMode mode = ImposterMode.Implicit)
    {
        _somethingHappened = new SomethingHappenedEventImposterBuilder();
        _asyncSomethingHappened = new AsyncSomethingHappenedEventImposterBuilder();
        _instance = new EventImposterPocSutInstance(_somethingHappened, _asyncSomethingHappened);
    }

    public ISomethingHappenedEventImposterBuilder SomethingHappened => _somethingHappened;

    public IAsyncSomethingHappenedEventImposterBuilder AsyncSomethingHappened => _asyncSomethingHappened;

    public IEventImposterPocSut Instance() => ((IHaveImposterInstance<IEventImposterPocSut>)this).Instance();

    IEventImposterPocSut IHaveImposterInstance<IEventImposterPocSut>.Instance() => _instance;

    private sealed class EventImposterPocSutInstance : IEventImposterPocSut
    {
        private readonly SomethingHappenedEventImposterBuilder _syncBuilder;
        private readonly AsyncSomethingHappenedEventImposterBuilder _asyncBuilder;

        public EventImposterPocSutInstance(
            SomethingHappenedEventImposterBuilder syncBuilder,
            AsyncSomethingHappenedEventImposterBuilder asyncBuilder)
        {
            _syncBuilder = syncBuilder;
            _asyncBuilder = asyncBuilder;
        }

        public event EventHandler? SomethingHappened
        {
            add => _syncBuilder.Subscribe(value ?? throw new ArgumentNullException(nameof(value)));
            remove => _syncBuilder.Unsubscribe(value ?? throw new ArgumentNullException(nameof(value)));
        }

        public event Func<object?, EventArgs, Task>? AsyncSomethingHappened
        {
            add => _asyncBuilder.Subscribe(value ?? throw new ArgumentNullException(nameof(value)));
            remove => _asyncBuilder.Unsubscribe(value ?? throw new ArgumentNullException(nameof(value)));
        }
    }

    private sealed class SomethingHappenedEventImposterBuilder : ISomethingHappenedEventImposterBuilder
    {
        private readonly ConcurrentQueue<EventHandler> _handlerOrder = new ConcurrentQueue<EventHandler>();
        private readonly ConcurrentDictionary<EventHandler, int> _handlerCounts = new ConcurrentDictionary<EventHandler, int>();
        private readonly ConcurrentQueue<EventHandler> _callbacks = new ConcurrentQueue<EventHandler>();
        private readonly ConcurrentQueue<(object? Sender, EventArgs Args)> _history = new ConcurrentQueue<(object? Sender, EventArgs Args)>();
        private readonly ConcurrentQueue<EventHandler> _subscribeHistory = new ConcurrentQueue<EventHandler>();
        private readonly ConcurrentQueue<EventHandler> _unsubscribeHistory = new ConcurrentQueue<EventHandler>();
        private readonly ConcurrentQueue<Action<EventHandler>> _subscribeInterceptors = new ConcurrentQueue<Action<EventHandler>>();
        private readonly ConcurrentQueue<Action<EventHandler>> _unsubscribeInterceptors = new ConcurrentQueue<Action<EventHandler>>();
        private readonly ConcurrentQueue<(EventHandler Handler, object? Sender, EventArgs Args)> _handlerInvocations = new ConcurrentQueue<(EventHandler Handler, object? Sender, EventArgs Args)>();

        public SomethingHappenedEventImposterBuilder()
        {
        }

        public void Subscribe(EventHandler handler)
        {
            ArgumentNullException.ThrowIfNull(handler);
            _handlerOrder.Enqueue(handler);
            _handlerCounts.AddOrUpdate(handler, 1, (_, count) => count + 1);
            _subscribeHistory.Enqueue(handler);
            foreach (var interceptor in _subscribeInterceptors.ToArray())
            {
                interceptor(handler);
            }
        }

        public void Unsubscribe(EventHandler handler)
        {
            ArgumentNullException.ThrowIfNull(handler);
            _handlerCounts.AddOrUpdate(handler, 0, (_, count) =>
            {
                if (count > 0)
                {
                    return count - 1;
                }

                return 0;
            });
            _unsubscribeHistory.Enqueue(handler);
            foreach (var interceptor in _unsubscribeInterceptors.ToArray())
            {
                interceptor(handler);
            }
        }

        public ISomethingHappenedEventImposterBuilder Callback(EventHandler callback)
        {
            ArgumentNullException.ThrowIfNull(callback);
            _callbacks.Enqueue(callback);
            return this;
        }

        public ISomethingHappenedEventImposterBuilder Raise(object? sender, EventArgs args)
        {
            RaiseInternal(sender, args ?? EventArgs.Empty);
            return this;
        }

        public ISomethingHappenedEventImposterBuilder Subscribed(Arg<EventHandler> criteria, Count count)
        {
            ArgumentNullException.ThrowIfNull(criteria);
            ArgumentNullException.ThrowIfNull(count);

            var actual = CountMatches(_subscribeHistory, handler => criteria.Matches(handler));
            EnsureCountMatches(actual, count, "subscribed");
            return this;
        }

        public ISomethingHappenedEventImposterBuilder Unsubscribed(Arg<EventHandler> criteria, Count count)
        {
            ArgumentNullException.ThrowIfNull(criteria);
            ArgumentNullException.ThrowIfNull(count);

            var actual = CountMatches(_unsubscribeHistory, handler => criteria.Matches(handler));
            EnsureCountMatches(actual, count, "unsubscribed");
            return this;
        }

        public ISomethingHappenedEventImposterBuilder OnSubscribe(Action<EventHandler> interceptor)
        {
            ArgumentNullException.ThrowIfNull(interceptor);
            _subscribeInterceptors.Enqueue(interceptor);
            return this;
        }

        public ISomethingHappenedEventImposterBuilder OnUnsubscribe(Action<EventHandler> interceptor)
        {
            ArgumentNullException.ThrowIfNull(interceptor);
            _unsubscribeInterceptors.Enqueue(interceptor);
            return this;
        }

        public ISomethingHappenedEventImposterBuilder Raised(Arg<object?> senderCriteria, Arg<EventArgs> argsCriteria, Count count)
        {
            ArgumentNullException.ThrowIfNull(senderCriteria);
            ArgumentNullException.ThrowIfNull(argsCriteria);
            ArgumentNullException.ThrowIfNull(count);

            var actual = CountMatches(_history, entry =>
                senderCriteria.Matches(entry.Sender) && argsCriteria.Matches(entry.Args));

            EnsureCountMatches(actual, count, "raised");
            return this;
        }

        public ISomethingHappenedEventImposterBuilder HandlerInvoked(Arg<EventHandler> handlerCriteria, Count count)
        {
            ArgumentNullException.ThrowIfNull(handlerCriteria);
            ArgumentNullException.ThrowIfNull(count);

            var actual = CountMatches(_handlerInvocations, entry => handlerCriteria.Matches(entry.Handler));
            EnsureCountMatches(actual, count, "invoked");
            return this;
        }

        private void RaiseInternal(object? sender, EventArgs args)
        {
            var invocationArgs = args ?? EventArgs.Empty;
            _history.Enqueue((sender, invocationArgs));

            foreach (var callback in _callbacks.ToArray())
            {
                callback(sender, invocationArgs);
            }

            foreach (var handler in EnumerateActiveHandlers())
            {
                _handlerInvocations.Enqueue((handler, sender, invocationArgs));
                handler(sender, invocationArgs);
            }
        }

        private IEnumerable<EventHandler> EnumerateActiveHandlers()
        {
            var budgets = new Dictionary<EventHandler, int>(_handlerCounts);
            foreach (var handler in _handlerOrder.ToArray())
            {
                if (budgets.TryGetValue(handler, out var remaining) && remaining > 0)
                {
                    budgets[handler] = remaining - 1;
                    yield return handler;
                }
            }
        }

        private static int CountMatches<T>(IEnumerable<T> source, Func<T, bool> predicate)
        {
            var total = 0;
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    total++;
                }
            }

            return total;
        }

        private void EnsureCountMatches(int actual, Count expected, string action)
        {
            if (!expected.Matches(actual))
            {
                throw new VerificationFailedException(expected, actual);
            }
        }
    }

    private sealed class AsyncSomethingHappenedEventImposterBuilder : IAsyncSomethingHappenedEventImposterBuilder
    {
        private readonly ConcurrentQueue<Func<object?, EventArgs, Task>> _handlerOrder = new ConcurrentQueue<Func<object?, EventArgs, Task>>();
        private readonly ConcurrentDictionary<Func<object?, EventArgs, Task>, int> _handlerCounts = new ConcurrentDictionary<Func<object?, EventArgs, Task>, int>();
        private readonly ConcurrentQueue<Func<object?, EventArgs, Task>> _callbacks = new ConcurrentQueue<Func<object?, EventArgs, Task>>();
        private readonly ConcurrentQueue<(object? Sender, EventArgs Args)> _history = new ConcurrentQueue<(object? Sender, EventArgs Args)>();
        private readonly ConcurrentQueue<Func<object?, EventArgs, Task>> _subscribeHistory = new ConcurrentQueue<Func<object?, EventArgs, Task>>();
        private readonly ConcurrentQueue<Func<object?, EventArgs, Task>> _unsubscribeHistory = new ConcurrentQueue<Func<object?, EventArgs, Task>>();
        private readonly ConcurrentQueue<Action<Func<object?, EventArgs, Task>>> _subscribeInterceptors = new ConcurrentQueue<Action<Func<object?, EventArgs, Task>>>();
        private readonly ConcurrentQueue<Action<Func<object?, EventArgs, Task>>> _unsubscribeInterceptors = new ConcurrentQueue<Action<Func<object?, EventArgs, Task>>>();
        private readonly ConcurrentQueue<(Func<object?, EventArgs, Task> Handler, object? Sender, EventArgs Args)> _handlerInvocations = new ConcurrentQueue<(Func<object?, EventArgs, Task> Handler, object? Sender, EventArgs Args)>();

        public AsyncSomethingHappenedEventImposterBuilder()
        {
        }

        public void Subscribe(Func<object?, EventArgs, Task> handler)
        {
            ArgumentNullException.ThrowIfNull(handler);
            _handlerOrder.Enqueue(handler);
            _handlerCounts.AddOrUpdate(handler, 1, (_, count) => count + 1);
            _subscribeHistory.Enqueue(handler);
            foreach (var interceptor in _subscribeInterceptors.ToArray())
            {
                interceptor(handler);
            }
        }

        public void Unsubscribe(Func<object?, EventArgs, Task> handler)
        {
            ArgumentNullException.ThrowIfNull(handler);
            _handlerCounts.AddOrUpdate(handler, 0, (_, count) =>
            {
                if (count > 0)
                {
                    return count - 1;
                }

                return 0;
            });
            _unsubscribeHistory.Enqueue(handler);
            foreach (var interceptor in _unsubscribeInterceptors.ToArray())
            {
                interceptor(handler);
            }
        }

        public IAsyncSomethingHappenedEventImposterBuilder Callback(Func<object?, EventArgs, Task> callback)
        {
            ArgumentNullException.ThrowIfNull(callback);
            _callbacks.Enqueue(callback);
            return this;
        }

        public async Task<IAsyncSomethingHappenedEventImposterBuilder> RaiseAsync(object? sender, EventArgs args)
        {
            await RaiseCoreAsync(sender, args ?? EventArgs.Empty).ConfigureAwait(false);
            return this;
        }

        public IAsyncSomethingHappenedEventImposterBuilder Subscribed(Arg<Func<object?, EventArgs, Task>> criteria, Count count)
        {
            ArgumentNullException.ThrowIfNull(criteria);
            ArgumentNullException.ThrowIfNull(count);

            var actual = CountMatches(_subscribeHistory, handler => criteria.Matches(handler));
            EnsureCountMatches(actual, count, "subscribed");
            return this;
        }

        public IAsyncSomethingHappenedEventImposterBuilder Unsubscribed(Arg<Func<object?, EventArgs, Task>> criteria, Count count)
        {
            ArgumentNullException.ThrowIfNull(criteria);
            ArgumentNullException.ThrowIfNull(count);

            var actual = CountMatches(_unsubscribeHistory, handler => criteria.Matches(handler));
            EnsureCountMatches(actual, count, "unsubscribed");
            return this;
        }

        public IAsyncSomethingHappenedEventImposterBuilder OnSubscribe(Action<Func<object?, EventArgs, Task>> interceptor)
        {
            ArgumentNullException.ThrowIfNull(interceptor);
            _subscribeInterceptors.Enqueue(interceptor);
            return this;
        }

        public IAsyncSomethingHappenedEventImposterBuilder OnUnsubscribe(Action<Func<object?, EventArgs, Task>> interceptor)
        {
            ArgumentNullException.ThrowIfNull(interceptor);
            _unsubscribeInterceptors.Enqueue(interceptor);
            return this;
        }

        public IAsyncSomethingHappenedEventImposterBuilder Raised(Arg<object?> senderCriteria, Arg<EventArgs> argsCriteria, Count count)
        {
            ArgumentNullException.ThrowIfNull(senderCriteria);
            ArgumentNullException.ThrowIfNull(argsCriteria);
            ArgumentNullException.ThrowIfNull(count);

            var actual = CountMatches(_history, entry =>
                senderCriteria.Matches(entry.Sender) && argsCriteria.Matches(entry.Args));

            EnsureCountMatches(actual, count, "raised");
            return this;
        }
        public IAsyncSomethingHappenedEventImposterBuilder HandlerInvoked(Arg<Func<object?, EventArgs, Task>> handlerCriteria, Count count)
        {
            ArgumentNullException.ThrowIfNull(handlerCriteria);
            ArgumentNullException.ThrowIfNull(count);

            var actual = CountMatches(_handlerInvocations, entry => handlerCriteria.Matches(entry.Handler));
            EnsureCountMatches(actual, count, "invoked");
            return this;
        }

        private async Task RaiseCoreAsync(object? sender, EventArgs args)
        {
            var invocationArgs = args ?? EventArgs.Empty;
            _history.Enqueue((sender, invocationArgs));

            var pendingTasks = new List<Task>();

            foreach (var callback in _callbacks.ToArray())
            {
                var task = callback(sender, invocationArgs);
                if (task != null)
                {
                    pendingTasks.Add(task);
                }
            }

            foreach (var handler in EnumerateActiveHandlers())
            {
                _handlerInvocations.Enqueue((handler, sender, invocationArgs));
                var task = handler(sender, invocationArgs);
                if (task != null)
                {
                    pendingTasks.Add(task);
                }
            }

            if (pendingTasks.Count > 0)
            {
                await Task.WhenAll(pendingTasks).ConfigureAwait(false);
            }
        }

        private IEnumerable<Func<object?, EventArgs, Task>> EnumerateActiveHandlers()
        {
            var budgets = new Dictionary<Func<object?, EventArgs, Task>, int>(_handlerCounts);
            foreach (var handler in _handlerOrder.ToArray())
            {
                if (budgets.TryGetValue(handler, out var remaining) && remaining > 0)
                {
                    budgets[handler] = remaining - 1;
                    yield return handler;
                }
            }
        }

        private static int CountMatches<T>(IEnumerable<T> source, Func<T, bool> predicate)
        {
            var total = 0;
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    total++;
                }
            }

            return total;
        }

        private void EnsureCountMatches(int actual, Count expected, string action)
        {
            if (!expected.Matches(actual))
            {
                throw new VerificationFailedException(expected, actual);
            }
        }
    }
}
}
