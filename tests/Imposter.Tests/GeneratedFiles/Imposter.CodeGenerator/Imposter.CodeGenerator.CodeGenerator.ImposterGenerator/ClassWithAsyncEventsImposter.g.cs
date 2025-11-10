#pragma warning disable nullable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using global::Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Tests.Features.ClassImposter.Suts;

namespace Imposter.Tests.Features.ClassImposter.Suts
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class ClassWithAsyncEventsImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithAsyncEvents>
    {
        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithAsyncEvents global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithAsyncEvents>.Instance()
        {
            return _imposterInstance;
        }

        public ICustomAsyncEventEventImposterBuilder CustomAsyncEvent => _CustomAsyncEvent;

        private readonly CustomAsyncEventEventImposterBuilder _CustomAsyncEvent;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICustomAsyncEventEventImposterBuilder : ICustomAsyncEventEventImposterSetupBuilder, ICustomAsyncEventEventImposterVerificationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICustomAsyncEventEventImposterSetupBuilder
        {
            ICustomAsyncEventEventImposterSetupBuilder Callback(global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs> callback);
            System.Threading.Tasks.Task<ICustomAsyncEventEventImposterSetupBuilder> RaiseAsync(object sender, global::System.EventArgs args);
            ICustomAsyncEventEventImposterSetupBuilder OnSubscribe(System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> interceptor);
            ICustomAsyncEventEventImposterSetupBuilder OnUnsubscribe(System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> interceptor);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICustomAsyncEventEventImposterVerificationBuilder
        {
            ICustomAsyncEventEventImposterVerificationBuilder Subscribed(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> criteria, global::Imposter.Abstractions.Count count);
            ICustomAsyncEventEventImposterVerificationBuilder Unsubscribed(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> criteria, global::Imposter.Abstractions.Count count);
            ICustomAsyncEventEventImposterVerificationBuilder Raised(global::Imposter.Abstractions.Arg<object> senderCriteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> argsCriteria, global::Imposter.Abstractions.Count count);
            ICustomAsyncEventEventImposterVerificationBuilder HandlerInvoked(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> handlerCriteria, global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal sealed class CustomAsyncEventEventImposterBuilder : ICustomAsyncEventEventImposterBuilder, ICustomAsyncEventEventImposterSetupBuilder, ICustomAsyncEventEventImposterVerificationBuilder
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
            internal CustomAsyncEventEventImposterBuilder()
            {
            }

            internal void Subscribe(global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs> handler)
            {
                ArgumentNullException.ThrowIfNull(handler);
                _handlerOrder.Enqueue(handler);
                _handlerCounts.AddOrUpdate(handler, 1, (_, count) => count + 1);
                _subscribeHistory.Enqueue(handler);
                foreach (var interceptor in _subscribeInterceptors)
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
                foreach (var interceptor in _unsubscribeInterceptors)
                {
                    interceptor(handler);
                }
            }

            ICustomAsyncEventEventImposterSetupBuilder ICustomAsyncEventEventImposterSetupBuilder.Callback(global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs> callback)
            {
                ArgumentNullException.ThrowIfNull(callback);
                _callbacks.Enqueue(callback);
                return this;
            }

            async System.Threading.Tasks.Task<ICustomAsyncEventEventImposterSetupBuilder> ICustomAsyncEventEventImposterSetupBuilder.RaiseAsync(object sender, global::System.EventArgs args)
            {
                await RaiseCoreAsync(sender, args).ConfigureAwait(false);
                return this;
            }

            ICustomAsyncEventEventImposterVerificationBuilder ICustomAsyncEventEventImposterVerificationBuilder.Subscribed(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_subscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "subscribed");
                return this;
            }

            ICustomAsyncEventEventImposterVerificationBuilder ICustomAsyncEventEventImposterVerificationBuilder.Unsubscribed(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_unsubscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "unsubscribed");
                return this;
            }

            ICustomAsyncEventEventImposterSetupBuilder ICustomAsyncEventEventImposterSetupBuilder.OnSubscribe(System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _subscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            ICustomAsyncEventEventImposterSetupBuilder ICustomAsyncEventEventImposterSetupBuilder.OnUnsubscribe(System.Action<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _unsubscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            ICustomAsyncEventEventImposterVerificationBuilder ICustomAsyncEventEventImposterVerificationBuilder.Raised(global::Imposter.Abstractions.Arg<object> senderCriteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> argsCriteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(senderCriteria);
                ArgumentNullException.ThrowIfNull(argsCriteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_history, entry => senderCriteria.Matches(entry.sender) && argsCriteria.Matches(entry.args));
                EnsureCountMatches(actual, count, "raised");
                return this;
            }

            ICustomAsyncEventEventImposterVerificationBuilder ICustomAsyncEventEventImposterVerificationBuilder.HandlerInvoked(global::Imposter.Abstractions.Arg<global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs>> handlerCriteria, global::Imposter.Abstractions.Count count)
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
                foreach (var callback in _callbacks)
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
                foreach (var handler in _handlerOrder)
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

        public ITaskBasedEventEventImposterBuilder TaskBasedEvent => _TaskBasedEvent;

        private readonly TaskBasedEventEventImposterBuilder _TaskBasedEvent;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ITaskBasedEventEventImposterBuilder : ITaskBasedEventEventImposterSetupBuilder, ITaskBasedEventEventImposterVerificationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ITaskBasedEventEventImposterSetupBuilder
        {
            ITaskBasedEventEventImposterSetupBuilder Callback(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task> callback);
            System.Threading.Tasks.Task<ITaskBasedEventEventImposterSetupBuilder> RaiseAsync(object arg1, global::System.EventArgs arg2);
            ITaskBasedEventEventImposterSetupBuilder OnSubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> interceptor);
            ITaskBasedEventEventImposterSetupBuilder OnUnsubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> interceptor);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ITaskBasedEventEventImposterVerificationBuilder
        {
            ITaskBasedEventEventImposterVerificationBuilder Subscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> criteria, global::Imposter.Abstractions.Count count);
            ITaskBasedEventEventImposterVerificationBuilder Unsubscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> criteria, global::Imposter.Abstractions.Count count);
            ITaskBasedEventEventImposterVerificationBuilder Raised(global::Imposter.Abstractions.Arg<object> arg1Criteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> arg2Criteria, global::Imposter.Abstractions.Count count);
            ITaskBasedEventEventImposterVerificationBuilder HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> handlerCriteria, global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal sealed class TaskBasedEventEventImposterBuilder : ITaskBasedEventEventImposterBuilder, ITaskBasedEventEventImposterSetupBuilder, ITaskBasedEventEventImposterVerificationBuilder
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
            internal TaskBasedEventEventImposterBuilder()
            {
            }

            internal void Subscribe(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task> handler)
            {
                ArgumentNullException.ThrowIfNull(handler);
                _handlerOrder.Enqueue(handler);
                _handlerCounts.AddOrUpdate(handler, 1, (_, count) => count + 1);
                _subscribeHistory.Enqueue(handler);
                foreach (var interceptor in _subscribeInterceptors)
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
                foreach (var interceptor in _unsubscribeInterceptors)
                {
                    interceptor(handler);
                }
            }

            ITaskBasedEventEventImposterSetupBuilder ITaskBasedEventEventImposterSetupBuilder.Callback(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task> callback)
            {
                ArgumentNullException.ThrowIfNull(callback);
                _callbacks.Enqueue(callback);
                return this;
            }

            async System.Threading.Tasks.Task<ITaskBasedEventEventImposterSetupBuilder> ITaskBasedEventEventImposterSetupBuilder.RaiseAsync(object arg1, global::System.EventArgs arg2)
            {
                await RaiseCoreAsync(arg1, arg2).ConfigureAwait(false);
                return this;
            }

            ITaskBasedEventEventImposterVerificationBuilder ITaskBasedEventEventImposterVerificationBuilder.Subscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_subscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "subscribed");
                return this;
            }

            ITaskBasedEventEventImposterVerificationBuilder ITaskBasedEventEventImposterVerificationBuilder.Unsubscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_unsubscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "unsubscribed");
                return this;
            }

            ITaskBasedEventEventImposterSetupBuilder ITaskBasedEventEventImposterSetupBuilder.OnSubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _subscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            ITaskBasedEventEventImposterSetupBuilder ITaskBasedEventEventImposterSetupBuilder.OnUnsubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _unsubscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            ITaskBasedEventEventImposterVerificationBuilder ITaskBasedEventEventImposterVerificationBuilder.Raised(global::Imposter.Abstractions.Arg<object> arg1Criteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> arg2Criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(arg1Criteria);
                ArgumentNullException.ThrowIfNull(arg2Criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_history, entry => arg1Criteria.Matches(entry.arg1) && arg2Criteria.Matches(entry.arg2));
                EnsureCountMatches(actual, count, "raised");
                return this;
            }

            ITaskBasedEventEventImposterVerificationBuilder ITaskBasedEventEventImposterVerificationBuilder.HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task>> handlerCriteria, global::Imposter.Abstractions.Count count)
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
                foreach (var callback in _callbacks)
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
                foreach (var handler in _handlerOrder)
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

        public IValueTaskBasedEventEventImposterBuilder ValueTaskBasedEvent => _ValueTaskBasedEvent;

        private readonly ValueTaskBasedEventEventImposterBuilder _ValueTaskBasedEvent;
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IValueTaskBasedEventEventImposterBuilder : IValueTaskBasedEventEventImposterSetupBuilder, IValueTaskBasedEventEventImposterVerificationBuilder
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IValueTaskBasedEventEventImposterSetupBuilder
        {
            IValueTaskBasedEventEventImposterSetupBuilder Callback(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask> callback);
            System.Threading.Tasks.Task<IValueTaskBasedEventEventImposterSetupBuilder> RaiseAsync(object arg1, global::System.EventArgs arg2);
            IValueTaskBasedEventEventImposterSetupBuilder OnSubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> interceptor);
            IValueTaskBasedEventEventImposterSetupBuilder OnUnsubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> interceptor);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IValueTaskBasedEventEventImposterVerificationBuilder
        {
            IValueTaskBasedEventEventImposterVerificationBuilder Subscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> criteria, global::Imposter.Abstractions.Count count);
            IValueTaskBasedEventEventImposterVerificationBuilder Unsubscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> criteria, global::Imposter.Abstractions.Count count);
            IValueTaskBasedEventEventImposterVerificationBuilder Raised(global::Imposter.Abstractions.Arg<object> arg1Criteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> arg2Criteria, global::Imposter.Abstractions.Count count);
            IValueTaskBasedEventEventImposterVerificationBuilder HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> handlerCriteria, global::Imposter.Abstractions.Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal sealed class ValueTaskBasedEventEventImposterBuilder : IValueTaskBasedEventEventImposterBuilder, IValueTaskBasedEventEventImposterSetupBuilder, IValueTaskBasedEventEventImposterVerificationBuilder
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
            internal ValueTaskBasedEventEventImposterBuilder()
            {
            }

            internal void Subscribe(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask> handler)
            {
                ArgumentNullException.ThrowIfNull(handler);
                _handlerOrder.Enqueue(handler);
                _handlerCounts.AddOrUpdate(handler, 1, (_, count) => count + 1);
                _subscribeHistory.Enqueue(handler);
                foreach (var interceptor in _subscribeInterceptors)
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
                foreach (var interceptor in _unsubscribeInterceptors)
                {
                    interceptor(handler);
                }
            }

            IValueTaskBasedEventEventImposterSetupBuilder IValueTaskBasedEventEventImposterSetupBuilder.Callback(global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask> callback)
            {
                ArgumentNullException.ThrowIfNull(callback);
                _callbacks.Enqueue(callback);
                return this;
            }

            async System.Threading.Tasks.Task<IValueTaskBasedEventEventImposterSetupBuilder> IValueTaskBasedEventEventImposterSetupBuilder.RaiseAsync(object arg1, global::System.EventArgs arg2)
            {
                await RaiseCoreAsync(arg1, arg2).ConfigureAwait(false);
                return this;
            }

            IValueTaskBasedEventEventImposterVerificationBuilder IValueTaskBasedEventEventImposterVerificationBuilder.Subscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_subscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "subscribed");
                return this;
            }

            IValueTaskBasedEventEventImposterVerificationBuilder IValueTaskBasedEventEventImposterVerificationBuilder.Unsubscribed(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_unsubscribeHistory, entry => criteria.Matches(entry));
                EnsureCountMatches(actual, count, "unsubscribed");
                return this;
            }

            IValueTaskBasedEventEventImposterSetupBuilder IValueTaskBasedEventEventImposterSetupBuilder.OnSubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _subscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            IValueTaskBasedEventEventImposterSetupBuilder IValueTaskBasedEventEventImposterSetupBuilder.OnUnsubscribe(System.Action<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> interceptor)
            {
                ArgumentNullException.ThrowIfNull(interceptor);
                _unsubscribeInterceptors.Enqueue(interceptor);
                return this;
            }

            IValueTaskBasedEventEventImposterVerificationBuilder IValueTaskBasedEventEventImposterVerificationBuilder.Raised(global::Imposter.Abstractions.Arg<object> arg1Criteria, global::Imposter.Abstractions.Arg<global::System.EventArgs> arg2Criteria, global::Imposter.Abstractions.Count count)
            {
                ArgumentNullException.ThrowIfNull(arg1Criteria);
                ArgumentNullException.ThrowIfNull(arg2Criteria);
                ArgumentNullException.ThrowIfNull(count);
                int actual = CountMatches(_history, entry => arg1Criteria.Matches(entry.arg1) && arg2Criteria.Matches(entry.arg2));
                EnsureCountMatches(actual, count, "raised");
                return this;
            }

            IValueTaskBasedEventEventImposterVerificationBuilder IValueTaskBasedEventEventImposterVerificationBuilder.HandlerInvoked(global::Imposter.Abstractions.Arg<global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask>> handlerCriteria, global::Imposter.Abstractions.Count count)
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
                foreach (var callback in _callbacks)
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
                foreach (var handler in _handlerOrder)
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

        public ClassWithAsyncEventsImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._CustomAsyncEvent = new CustomAsyncEventEventImposterBuilder();
            this._TaskBasedEvent = new TaskBasedEventEventImposterBuilder();
            this._ValueTaskBasedEvent = new ValueTaskBasedEventEventImposterBuilder();
            this._imposterInstance = new ImposterTargetInstance();
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithAsyncEvents
        {
            ClassWithAsyncEventsImposter _imposter;
            internal void InitializeImposter(ClassWithAsyncEventsImposter imposter)
            {
                _imposter = imposter;
            }

            internal ImposterTargetInstance() : base()
            {
            }

            protected override event global::Imposter.Tests.Features.EventImposter.AsyncEventHandler<global::System.EventArgs> CustomAsyncEvent
            {
                add
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._CustomAsyncEvent.Subscribe(value);
                }

                remove
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._CustomAsyncEvent.Unsubscribe(value);
                }
            }

            protected override event global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.Task> TaskBasedEvent
            {
                add
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._TaskBasedEvent.Subscribe(value);
                }

                remove
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._TaskBasedEvent.Unsubscribe(value);
                }
            }

            protected override event global::System.Func<object, global::System.EventArgs, global::System.Threading.Tasks.ValueTask> ValueTaskBasedEvent
            {
                add
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._ValueTaskBasedEvent.Subscribe(value);
                }

                remove
                {
                    ArgumentNullException.ThrowIfNull(value);
                    _imposter._ValueTaskBasedEvent.Unsubscribe(value);
                }
            }
        }
    }
}