using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Tests.Features.EventImposter;

#pragma warning disable nullable
namespace Imposter.Tests.Features.EventImposter
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IEventSetupSutImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.EventImposter.IEventSetupSut>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        public global::Imposter.Tests.Features.EventImposter.IEventSetupSut Instance() => _imposterInstance;
        global::Imposter.Tests.Features.EventImposter.IEventSetupSut global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.EventImposter.IEventSetupSut>.Instance()
        {
            return _imposterInstance;
        }

        public ISomethingHappenedEventImposterBuilder SomethingHappened => _SomethingHappened;

        private readonly SomethingHappenedEventImposterBuilder _SomethingHappened;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISomethingHappenedEventImposterBuilder
        {
            ISomethingHappenedEventImposterBuilder Callback(global::System.EventHandler callback);
            ISomethingHappenedEventImposterBuilder Raise(object sender, global::System.EventArgs e);
            ISomethingHappenedEventImposterBuilder Subscribed(global::Imposter.Abstractions.Arg<global::System.EventHandler> criteria, global::Imposter.Abstractions.Count count);
            ISomethingHappenedEventImposterBuilder Unsubscribed(global::Imposter.Abstractions.Arg<global::System.EventHandler> criteria, global::Imposter.Abstractions.Count count);
            ISomethingHappenedEventImposterBuilder OnSubscribe(System.Action<global::System.EventHandler> interceptor);
            ISomethingHappenedEventImposterBuilder OnUnsubscribe(System.Action<global::System.EventHandler> interceptor);
            ISomethingHappenedEventImposterBuilder Raised(global::Imposter.Abstractions.Arg<object> senderCriteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> eCriteria, global::Imposter.Abstractions.Count count);
            ISomethingHappenedEventImposterBuilder HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.EventHandler> handlerCriteria, global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal sealed class SomethingHappenedEventImposterBuilder : ISomethingHappenedEventImposterBuilder
        {
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _handlerOrder = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentDictionary<global::System.EventHandler, int> _handlerCounts = new System.Collections.Concurrent.ConcurrentDictionary<global::System.EventHandler, int>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(object sender, global::System.EventArgs e)> _history = new System.Collections.Concurrent.ConcurrentQueue<(object sender, global::System.EventArgs e)>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _subscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler> _unsubscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::System.EventHandler>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>> _subscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>> _unsubscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.EventHandler>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(global::System.EventHandler Handler, object sender, global::System.EventArgs e)> _handlerInvocations = new System.Collections.Concurrent.ConcurrentQueue<(global::System.EventHandler Handler, object sender, global::System.EventArgs e)>();
            internal SomethingHappenedEventImposterBuilder()
            {
            }

            internal void Subscribe(global::System.EventHandler handler)
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

            internal void Unsubscribe(global::System.EventHandler handler)
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

            ISomethingHappenedEventImposterBuilder ISomethingHappenedEventImposterBuilder.Callback(global::System.EventHandler callback)
            {
                ArgumentNullException.ThrowIfNull(callback);
                _callbacks.Enqueue(callback);
                return this;
            }

            ISomethingHappenedEventImposterBuilder ISomethingHappenedEventImposterBuilder.Raise(object sender, global::System.EventArgs e)
            {
                RaiseInternal(sender, e);
                return this;
            }

            ISomethingHappenedEventImposterBuilder ISomethingHappenedEventImposterBuilder.Subscribed(global::Imposter.Abstractions.Arg<global::System.EventHandler> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_subscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "subscribed");
                return this;
            }

            ISomethingHappenedEventImposterBuilder ISomethingHappenedEventImposterBuilder.Unsubscribed(global::Imposter.Abstractions.Arg<global::System.EventHandler> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_unsubscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "unsubscribed");
                return this;
            }

            ISomethingHappenedEventImposterBuilder ISomethingHappenedEventImposterBuilder.OnSubscribe(System.Action<global::System.EventHandler> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _subscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            ISomethingHappenedEventImposterBuilder ISomethingHappenedEventImposterBuilder.OnUnsubscribe(System.Action<global::System.EventHandler> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _unsubscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            ISomethingHappenedEventImposterBuilder ISomethingHappenedEventImposterBuilder.Raised(global::Imposter.Abstractions.Arg<object> senderCriteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> eCriteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(senderCriteria);
                ArgumentNullException.ThrowIfNull(eCriteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_history, entry => senderCriteria.Matches(entry.sender) && eCriteria.Matches(entry.e));
                EnsureCountMatches(actual, count, "raised");
                return this;
            }

            ISomethingHappenedEventImposterBuilder ISomethingHappenedEventImposterBuilder.HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.EventHandler> handlerCriteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(handlerCriteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_handlerInvocations, entry => handlerCriteria.Matches(entry.Handler));
                EnsureCountMatches(actual, count, "invoked");
                return this;
            }

            private void RaiseInternal(object sender, global::System.EventArgs e)
            {
                _history.Enqueue((sender, e));
                foreach (var callback in _callbacks.ToArray())
                {
                    callback(sender, e);
                }

                foreach (var handler in EnumerateActiveHandlers())
                {
                    _handlerInvocations.Enqueue((handler, sender, e));
                    handler(sender, e);
                }
            }

            private System.Collections.Generic.IEnumerable<global::System.EventHandler> EnumerateActiveHandlers()
            {
                System.Collections.Generic.Dictionary<global::System.EventHandler, int> budgets = new System.Collections.Generic.Dictionary<global::System.EventHandler, int>(_handlerCounts);
                foreach (var handler in _handlerOrder.ToArray())
                {
                    int remaining;
                    if (budgets.TryGetValue(handler, out remaining))
                    {
                        if (remaining > 0)
                        {
                            budgets[handler] = remaining - 1;
                            yield return handler;
                        }
                    }
                }
            }

            private static int CountMatches<T>(System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate)
            {
                int count = 0;
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        count++;
                    }
                }

                return count;
            }

            private static void EnsureCountMatches(int actual, global::Imposter.Abstractions.Count expected, string action)
            {
                if (!expected.Matches(actual))
                {
                    throw new global::Imposter.Abstractions.VerificationFailedException(expected, actual);
                }
            }
        }

        public IAsyncSomethingHappenedEventImposterBuilder AsyncSomethingHappened => _AsyncSomethingHappened;

        private readonly AsyncSomethingHappenedEventImposterBuilder _AsyncSomethingHappened;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAsyncSomethingHappenedEventImposterBuilder
        {
            IAsyncSomethingHappenedEventImposterBuilder Callback(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task> callback);
            System.Threading.Tasks.Task<IAsyncSomethingHappenedEventImposterBuilder> RaiseAsync(object arg1, global::System.EventArgs arg2);
            IAsyncSomethingHappenedEventImposterBuilder Subscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> criteria, global::Imposter.Abstractions.Count count);
            IAsyncSomethingHappenedEventImposterBuilder Unsubscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> criteria, global::Imposter.Abstractions.Count count);
            IAsyncSomethingHappenedEventImposterBuilder OnSubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> interceptor);
            IAsyncSomethingHappenedEventImposterBuilder OnUnsubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> interceptor);
            IAsyncSomethingHappenedEventImposterBuilder Raised(global::Imposter.Abstractions.Arg<object> arg1Criteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> arg2Criteria, global::Imposter.Abstractions.Count count);
            IAsyncSomethingHappenedEventImposterBuilder HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> handlerCriteria, global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal sealed class AsyncSomethingHappenedEventImposterBuilder : IAsyncSomethingHappenedEventImposterBuilder
        {
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> _handlerOrder = new System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>>();
            private readonly System.Collections.Concurrent.ConcurrentDictionary<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>, int> _handlerCounts = new System.Collections.Concurrent.ConcurrentDictionary<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>, int>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(object arg1, global::System.EventArgs arg2)> _history = new System.Collections.Concurrent.ConcurrentQueue<(object arg1, global::System.EventArgs arg2)>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> _subscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> _unsubscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>>> _subscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>>> _unsubscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task> Handler, object arg1, global::System.EventArgs arg2)> _handlerInvocations = new System.Collections.Concurrent.ConcurrentQueue<(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task> Handler, object arg1, global::System.EventArgs arg2)>();
            internal AsyncSomethingHappenedEventImposterBuilder()
            {
            }

            internal void Subscribe(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task> handler)
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

            internal void Unsubscribe(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task> handler)
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

            IAsyncSomethingHappenedEventImposterBuilder IAsyncSomethingHappenedEventImposterBuilder.Callback(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task> callback)
            {
                ArgumentNullException.ThrowIfNull(callback);
                _callbacks.Enqueue(callback);
                return this;
            }

            async System.Threading.Tasks.Task<IAsyncSomethingHappenedEventImposterBuilder> IAsyncSomethingHappenedEventImposterBuilder.RaiseAsync(object arg1, global::System.EventArgs arg2)
            {
                await RaiseCoreAsync(arg1, arg2).ConfigureAwait(false);
                return this;
            }

            IAsyncSomethingHappenedEventImposterBuilder IAsyncSomethingHappenedEventImposterBuilder.Subscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_subscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "subscribed");
                return this;
            }

            IAsyncSomethingHappenedEventImposterBuilder IAsyncSomethingHappenedEventImposterBuilder.Unsubscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_unsubscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "unsubscribed");
                return this;
            }

            IAsyncSomethingHappenedEventImposterBuilder IAsyncSomethingHappenedEventImposterBuilder.OnSubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _subscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            IAsyncSomethingHappenedEventImposterBuilder IAsyncSomethingHappenedEventImposterBuilder.OnUnsubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _unsubscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            IAsyncSomethingHappenedEventImposterBuilder IAsyncSomethingHappenedEventImposterBuilder.Raised(global::Imposter.Abstractions.Arg<object> arg1Criteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> arg2Criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(arg1Criteria);
                ArgumentNullException.ThrowIfNull(arg2Criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_history, entry => arg1Criteria.Matches(entry.arg1) && arg2Criteria.Matches(entry.arg2));
                EnsureCountMatches(actual, count, "raised");
                return this;
            }

            IAsyncSomethingHappenedEventImposterBuilder IAsyncSomethingHappenedEventImposterBuilder.HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> handlerCriteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(handlerCriteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_handlerInvocations, entry => handlerCriteria.Matches(entry.Handler));
                EnsureCountMatches(actual, count, "invoked");
                return this;
            }

            private async System.Threading.Tasks.Task RaiseCoreAsync(object arg1, global::System.EventArgs arg2)
            {
                _history.Enqueue((arg1, arg2));
                System.Collections.Generic.List<System.Threading.Tasks.Task> pendingTasks = new System.Collections.Generic.List<System.Threading.Tasks.Task>();
                foreach (var callback in _callbacks.ToArray())
                {
                    System.Threading.Tasks.Task task = callback(arg1, arg2);
                    if (task != null)
                    {
                        pendingTasks.Add(task);
                    }
                }

                foreach (var handler in EnumerateActiveHandlers())
                {
                    _handlerInvocations.Enqueue((handler, arg1, arg2));
                    System.Threading.Tasks.Task task = handler(arg1, arg2);
                    if (task != null)
                    {
                        pendingTasks.Add(task);
                    }
                }

                if (pendingTasks.Count > 0)
                {
                    await System.Threading.Tasks.Task.WhenAll(pendingTasks).ConfigureAwait(false);
                }
            }

            private System.Collections.Generic.IEnumerable<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> EnumerateActiveHandlers()
            {
                System.Collections.Generic.Dictionary<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>, int> budgets = new System.Collections.Generic.Dictionary<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>, int>(_handlerCounts);
                foreach (var handler in _handlerOrder.ToArray())
                {
                    int remaining;
                    if (budgets.TryGetValue(handler, out remaining))
                    {
                        if (remaining > 0)
                        {
                            budgets[handler] = remaining - 1;
                            yield return handler;
                        }
                    }
                }
            }

            private static int CountMatches<T>(System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate)
            {
                int count = 0;
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        count++;
                    }
                }

                return count;
            }

            private static void EnsureCountMatches(int actual, global::Imposter.Abstractions.Count expected, string action)
            {
                if (!expected.Matches(actual))
                {
                    throw new global::Imposter.Abstractions.VerificationFailedException(expected, actual);
                }
            }
        }

        public IValueTaskSomethingHappenedEventImposterBuilder ValueTaskSomethingHappened => _ValueTaskSomethingHappened;

        private readonly ValueTaskSomethingHappenedEventImposterBuilder _ValueTaskSomethingHappened;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IValueTaskSomethingHappenedEventImposterBuilder
        {
            IValueTaskSomethingHappenedEventImposterBuilder Callback(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask> callback);
            System.Threading.Tasks.Task<IValueTaskSomethingHappenedEventImposterBuilder> RaiseAsync(object arg1, global::System.EventArgs arg2);
            IValueTaskSomethingHappenedEventImposterBuilder Subscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> criteria, global::Imposter.Abstractions.Count count);
            IValueTaskSomethingHappenedEventImposterBuilder Unsubscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> criteria, global::Imposter.Abstractions.Count count);
            IValueTaskSomethingHappenedEventImposterBuilder OnSubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> interceptor);
            IValueTaskSomethingHappenedEventImposterBuilder OnUnsubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> interceptor);
            IValueTaskSomethingHappenedEventImposterBuilder Raised(global::Imposter.Abstractions.Arg<object> arg1Criteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> arg2Criteria, global::Imposter.Abstractions.Count count);
            IValueTaskSomethingHappenedEventImposterBuilder HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> handlerCriteria, global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal sealed class ValueTaskSomethingHappenedEventImposterBuilder : IValueTaskSomethingHappenedEventImposterBuilder
        {
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> _handlerOrder = new System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>>();
            private readonly System.Collections.Concurrent.ConcurrentDictionary<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>, int> _handlerCounts = new System.Collections.Concurrent.ConcurrentDictionary<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>, int>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(object arg1, global::System.EventArgs arg2)> _history = new System.Collections.Concurrent.ConcurrentQueue<(object arg1, global::System.EventArgs arg2)>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> _subscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> _unsubscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>>> _subscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>>> _unsubscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask> Handler, object arg1, global::System.EventArgs arg2)> _handlerInvocations = new System.Collections.Concurrent.ConcurrentQueue<(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask> Handler, object arg1, global::System.EventArgs arg2)>();
            internal ValueTaskSomethingHappenedEventImposterBuilder()
            {
            }

            internal void Subscribe(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask> handler)
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

            internal void Unsubscribe(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask> handler)
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

            IValueTaskSomethingHappenedEventImposterBuilder IValueTaskSomethingHappenedEventImposterBuilder.Callback(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask> callback)
            {
                ArgumentNullException.ThrowIfNull(callback);
                _callbacks.Enqueue(callback);
                return this;
            }

            async System.Threading.Tasks.Task<IValueTaskSomethingHappenedEventImposterBuilder> IValueTaskSomethingHappenedEventImposterBuilder.RaiseAsync(object arg1, global::System.EventArgs arg2)
            {
                await RaiseCoreAsync(arg1, arg2).ConfigureAwait(false);
                return this;
            }

            IValueTaskSomethingHappenedEventImposterBuilder IValueTaskSomethingHappenedEventImposterBuilder.Subscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_subscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "subscribed");
                return this;
            }

            IValueTaskSomethingHappenedEventImposterBuilder IValueTaskSomethingHappenedEventImposterBuilder.Unsubscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_unsubscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "unsubscribed");
                return this;
            }

            IValueTaskSomethingHappenedEventImposterBuilder IValueTaskSomethingHappenedEventImposterBuilder.OnSubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _subscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            IValueTaskSomethingHappenedEventImposterBuilder IValueTaskSomethingHappenedEventImposterBuilder.OnUnsubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _unsubscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            IValueTaskSomethingHappenedEventImposterBuilder IValueTaskSomethingHappenedEventImposterBuilder.Raised(global::Imposter.Abstractions.Arg<object> arg1Criteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> arg2Criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(arg1Criteria);
                ArgumentNullException.ThrowIfNull(arg2Criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_history, entry => arg1Criteria.Matches(entry.arg1) && arg2Criteria.Matches(entry.arg2));
                EnsureCountMatches(actual, count, "raised");
                return this;
            }

            IValueTaskSomethingHappenedEventImposterBuilder IValueTaskSomethingHappenedEventImposterBuilder.HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> handlerCriteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(handlerCriteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_handlerInvocations, entry => handlerCriteria.Matches(entry.Handler));
                EnsureCountMatches(actual, count, "invoked");
                return this;
            }

            private async System.Threading.Tasks.Task RaiseCoreAsync(object arg1, global::System.EventArgs arg2)
            {
                _history.Enqueue((arg1, arg2));
                System.Collections.Generic.List<System.Threading.Tasks.Task> pendingTasks = new System.Collections.Generic.List<System.Threading.Tasks.Task>();
                foreach (var callback in _callbacks.ToArray())
                {
                    System.Threading.Tasks.ValueTask task = callback(arg1, arg2);
                    if (task != null)
                    {
                        pendingTasks.Add(task.AsTask());
                    }
                }

                foreach (var handler in EnumerateActiveHandlers())
                {
                    _handlerInvocations.Enqueue((handler, arg1, arg2));
                    System.Threading.Tasks.ValueTask task = handler(arg1, arg2);
                    if (task != null)
                    {
                        pendingTasks.Add(task.AsTask());
                    }
                }

                if (pendingTasks.Count > 0)
                {
                    await System.Threading.Tasks.Task.WhenAll(pendingTasks).ConfigureAwait(false);
                }
            }

            private System.Collections.Generic.IEnumerable<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> EnumerateActiveHandlers()
            {
                System.Collections.Generic.Dictionary<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>, int> budgets = new System.Collections.Generic.Dictionary<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>, int>(_handlerCounts);
                foreach (var handler in _handlerOrder.ToArray())
                {
                    int remaining;
                    if (budgets.TryGetValue(handler, out remaining))
                    {
                        if (remaining > 0)
                        {
                            budgets[handler] = remaining - 1;
                            yield return handler;
                        }
                    }
                }
            }

            private static int CountMatches<T>(System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate)
            {
                int count = 0;
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        count++;
                    }
                }

                return count;
            }

            private static void EnsureCountMatches(int actual, global::Imposter.Abstractions.Count expected, string action)
            {
                if (!expected.Matches(actual))
                {
                    throw new global::Imposter.Abstractions.VerificationFailedException(expected, actual);
                }
            }
        }

        public ICustomAsyncSomethingHappenedEventImposterBuilder CustomAsyncSomethingHappened => _CustomAsyncSomethingHappened;

        private readonly CustomAsyncSomethingHappenedEventImposterBuilder _CustomAsyncSomethingHappened;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICustomAsyncSomethingHappenedEventImposterBuilder
        {
            ICustomAsyncSomethingHappenedEventImposterBuilder Callback(global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs> callback);
            System.Threading.Tasks.Task<ICustomAsyncSomethingHappenedEventImposterBuilder> RaiseAsync(object sender, global::System.EventArgs args);
            ICustomAsyncSomethingHappenedEventImposterBuilder Subscribed(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> criteria, global::Imposter.Abstractions.Count count);
            ICustomAsyncSomethingHappenedEventImposterBuilder Unsubscribed(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> criteria, global::Imposter.Abstractions.Count count);
            ICustomAsyncSomethingHappenedEventImposterBuilder OnSubscribe(System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> interceptor);
            ICustomAsyncSomethingHappenedEventImposterBuilder OnUnsubscribe(System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> interceptor);
            ICustomAsyncSomethingHappenedEventImposterBuilder Raised(global::Imposter.Abstractions.Arg<object> senderCriteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> argsCriteria, global::Imposter.Abstractions.Count count);
            ICustomAsyncSomethingHappenedEventImposterBuilder HandlerInvoked(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> handlerCriteria, global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal sealed class CustomAsyncSomethingHappenedEventImposterBuilder : ICustomAsyncSomethingHappenedEventImposterBuilder
        {
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> _handlerOrder = new System.Collections.Concurrent.ConcurrentQueue<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>>();
            private readonly System.Collections.Concurrent.ConcurrentDictionary<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>, int> _handlerCounts = new System.Collections.Concurrent.ConcurrentDictionary<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>, int>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(object sender, global::System.EventArgs args)> _history = new System.Collections.Concurrent.ConcurrentQueue<(object sender, global::System.EventArgs args)>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> _subscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> _unsubscribeHistory = new System.Collections.Concurrent.ConcurrentQueue<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>>> _subscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>>> _unsubscribeInterceptors = new System.Collections.Concurrent.ConcurrentQueue<System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>>>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<(global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs> Handler, object sender, global::System.EventArgs args)> _handlerInvocations = new System.Collections.Concurrent.ConcurrentQueue<(global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs> Handler, object sender, global::System.EventArgs args)>();
            internal CustomAsyncSomethingHappenedEventImposterBuilder()
            {
            }

            internal void Subscribe(global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs> handler)
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

            internal void Unsubscribe(global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs> handler)
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

            ICustomAsyncSomethingHappenedEventImposterBuilder ICustomAsyncSomethingHappenedEventImposterBuilder.Callback(global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs> callback)
            {
                ArgumentNullException.ThrowIfNull(callback);
                _callbacks.Enqueue(callback);
                return this;
            }

            async System.Threading.Tasks.Task<ICustomAsyncSomethingHappenedEventImposterBuilder> ICustomAsyncSomethingHappenedEventImposterBuilder.RaiseAsync(object sender, global::System.EventArgs args)
            {
                await RaiseCoreAsync(sender, args).ConfigureAwait(false);
                return this;
            }

            ICustomAsyncSomethingHappenedEventImposterBuilder ICustomAsyncSomethingHappenedEventImposterBuilder.Subscribed(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_subscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "subscribed");
                return this;
            }

            ICustomAsyncSomethingHappenedEventImposterBuilder ICustomAsyncSomethingHappenedEventImposterBuilder.Unsubscribed(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_unsubscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "unsubscribed");
                return this;
            }

            ICustomAsyncSomethingHappenedEventImposterBuilder ICustomAsyncSomethingHappenedEventImposterBuilder.OnSubscribe(System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _subscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            ICustomAsyncSomethingHappenedEventImposterBuilder ICustomAsyncSomethingHappenedEventImposterBuilder.OnUnsubscribe(System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _unsubscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            ICustomAsyncSomethingHappenedEventImposterBuilder ICustomAsyncSomethingHappenedEventImposterBuilder.Raised(global::Imposter.Abstractions.Arg<object> senderCriteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> argsCriteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(senderCriteria);
                ArgumentNullException.ThrowIfNull(argsCriteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_history, entry => senderCriteria.Matches(entry.sender) && argsCriteria.Matches(entry.args));
                EnsureCountMatches(actual, count, "raised");
                return this;
            }

            ICustomAsyncSomethingHappenedEventImposterBuilder ICustomAsyncSomethingHappenedEventImposterBuilder.HandlerInvoked(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> handlerCriteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(handlerCriteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_handlerInvocations, entry => handlerCriteria.Matches(entry.Handler));
                EnsureCountMatches(actual, count, "invoked");
                return this;
            }

            private async System.Threading.Tasks.Task RaiseCoreAsync(object sender, global::System.EventArgs args)
            {
                _history.Enqueue((sender, args));
                System.Collections.Generic.List<System.Threading.Tasks.Task> pendingTasks = new System.Collections.Generic.List<System.Threading.Tasks.Task>();
                foreach (var callback in _callbacks.ToArray())
                {
                    System.Threading.Tasks.Task task = callback(sender, args);
                    if (task != null)
                    {
                        pendingTasks.Add(task);
                    }
                }

                foreach (var handler in EnumerateActiveHandlers())
                {
                    _handlerInvocations.Enqueue((handler, sender, args));
                    System.Threading.Tasks.Task task = handler(sender, args);
                    if (task != null)
                    {
                        pendingTasks.Add(task);
                    }
                }

                if (pendingTasks.Count > 0)
                {
                    await System.Threading.Tasks.Task.WhenAll(pendingTasks).ConfigureAwait(false);
                }
            }

            private System.Collections.Generic.IEnumerable<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> EnumerateActiveHandlers()
            {
                System.Collections.Generic.Dictionary<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>, int> budgets = new System.Collections.Generic.Dictionary<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>, int>(_handlerCounts);
                foreach (var handler in _handlerOrder.ToArray())
                {
                    int remaining;
                    if (budgets.TryGetValue(handler, out remaining))
                    {
                        if (remaining > 0)
                        {
                            budgets[handler] = remaining - 1;
                            yield return handler;
                        }
                    }
                }
            }

            private static int CountMatches<T>(System.Collections.Generic.IEnumerable<T> source, System.Func<T, bool> predicate)
            {
                int count = 0;
                foreach (var item in source)
                {
                    if (predicate(item))
                    {
                        count++;
                    }
                }

                return count;
            }

            private static void EnsureCountMatches(int actual, global::Imposter.Abstractions.Count expected, string action)
            {
                if (!expected.Matches(actual))
                {
                    throw new global::Imposter.Abstractions.VerificationFailedException(expected, actual);
                }
            }
        }

        public IEventSetupSutImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._SomethingHappened = new SomethingHappenedEventImposterBuilder();
            this._AsyncSomethingHappened = new AsyncSomethingHappenedEventImposterBuilder();
            this._ValueTaskSomethingHappened = new ValueTaskSomethingHappenedEventImposterBuilder();
            this._CustomAsyncSomethingHappened = new CustomAsyncSomethingHappenedEventImposterBuilder();
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.EventImposter.IEventSetupSut
        {
            IEventSetupSutImposter _imposter;
            public ImposterTargetInstance(IEventSetupSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public event global::System.EventHandler SomethingHappened
            {
                add
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._SomethingHappened.Subscribe(value);
                }

                remove
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._SomethingHappened.Unsubscribe(value);
                }
            }

            public event global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task> AsyncSomethingHappened
            {
                add
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._AsyncSomethingHappened.Subscribe(value);
                }

                remove
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._AsyncSomethingHappened.Unsubscribe(value);
                }
            }

            public event global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask> ValueTaskSomethingHappened
            {
                add
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._ValueTaskSomethingHappened.Subscribe(value);
                }

                remove
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._ValueTaskSomethingHappened.Unsubscribe(value);
                }
            }

            public event global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs> CustomAsyncSomethingHappened
            {
                add
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._CustomAsyncSomethingHappened.Subscribe(value);
                }

                remove
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._CustomAsyncSomethingHappened.Unsubscribe(value);
                }
            }
        }
    }
}