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
    public class ClassWithOverloadsAndGenericsImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithOverloadsAndGenerics>
    {
        private readonly FormatMethodImposter _formatMethodImposter;
        private readonly Format_1MethodImposter _format_1MethodImposter;
        private readonly EchoMethodImposterCollection _echoMethodImposterCollection;
        private readonly SelectFirstMethodImposterCollection _selectFirstMethodImposterCollection;
        private readonly FormatMethodInvocationHistoryCollection _formatMethodInvocationHistoryCollection = new FormatMethodInvocationHistoryCollection();
        private readonly Format_1MethodInvocationHistoryCollection _format_1MethodInvocationHistoryCollection = new Format_1MethodInvocationHistoryCollection();
        private readonly EchoMethodInvocationHistoryCollection _echoMethodInvocationHistoryCollection = new EchoMethodInvocationHistoryCollection();
        private readonly SelectFirstMethodInvocationHistoryCollection _selectFirstMethodInvocationHistoryCollection = new SelectFirstMethodInvocationHistoryCollection();
        public IFormatMethodImposterBuilder Format(global::Imposter.Abstractions.Arg<int> value)
        {
            return new FormatMethodImposter.Builder(_formatMethodImposter, _formatMethodInvocationHistoryCollection, new FormatArgumentsCriteria(value));
        }

        public IFormat_1MethodImposterBuilder Format(global::Imposter.Abstractions.Arg<string> value, global::Imposter.Abstractions.Arg<int> padding)
        {
            return new Format_1MethodImposter.Builder(_format_1MethodImposter, _format_1MethodInvocationHistoryCollection, new Format_1ArgumentsCriteria(value, padding));
        }

        public IEchoMethodImposterBuilder<T> Echo<T>(global::Imposter.Abstractions.Arg<T> item)
        {
            return new EchoMethodImposter<T>.Builder(_echoMethodImposterCollection, _echoMethodInvocationHistoryCollection, new EchoArgumentsCriteria<T>(item));
        }

        public ISelectFirstMethodImposterBuilder<TFirst, TSecond> SelectFirst<TFirst, TSecond>(global::Imposter.Abstractions.Arg<TFirst> first, global::Imposter.Abstractions.Arg<TSecond> second)
        {
            return new SelectFirstMethodImposter<TFirst, TSecond>.Builder(_selectFirstMethodImposterCollection, _selectFirstMethodInvocationHistoryCollection, new SelectFirstArgumentsCriteria<TFirst, TSecond>(first, second));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithOverloadsAndGenerics global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithOverloadsAndGenerics>.Instance()
        {
            return _imposterInstance;
        }

        // virtual T ClassWithOverloadsAndGenerics.Echo<T>(T item)
        public delegate T EchoDelegate<T>(T item);
        // virtual T ClassWithOverloadsAndGenerics.Echo<T>(T item)
        public delegate void EchoCallbackDelegate<T>(T item);
        // virtual T ClassWithOverloadsAndGenerics.Echo<T>(T item)
        public delegate System.Exception EchoExceptionGeneratorDelegate<T>(T item);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class EchoArguments<T>
        {
            public T item;
            internal EchoArguments(T item)
            {
                this.item = item;
            }

            public EchoArguments<TTarget> As<TTarget>()
            {
                return new EchoArguments<TTarget>(TypeCaster.Cast<T, TTarget>(item));
            }
        }

        // virtual T ClassWithOverloadsAndGenerics.Echo<T>(T item)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class EchoArgumentsCriteria<T>
        {
            public global::Imposter.Abstractions.Arg<T> item { get; }

            public EchoArgumentsCriteria(global::Imposter.Abstractions.Arg<T> item)
            {
                this.item = item;
            }

            public bool Matches(EchoArguments<T> arguments)
            {
                return item.Matches(arguments.item);
            }

            public EchoArgumentsCriteria<TTarget> As<TTarget>()
            {
                return new EchoArgumentsCriteria<TTarget>(global::Imposter.Abstractions.Arg<TTarget>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<TTarget, T>(it, out T itemTarget) && item.Matches(itemTarget)));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IEchoMethodInvocationHistory
        {
            bool Matches<TTarget>(EchoArgumentsCriteria<TTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class EchoMethodInvocationHistory<T> : IEchoMethodInvocationHistory
        {
            internal EchoArguments<T> Arguments;
            internal T Result;
            internal System.Exception Exception;
            public EchoMethodInvocationHistory(EchoArguments<T> Arguments, T Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TTarget>(EchoArgumentsCriteria<TTarget> criteria)
            {
                return ((typeof(TTarget) == typeof(T)) && (typeof(T) == typeof(TTarget))) && criteria.As<T>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class EchoMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IEchoMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IEchoMethodInvocationHistory>();
            internal void Add(IEchoMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<T>(EchoArgumentsCriteria<T> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<T>(argumentsCriteria));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class EchoMethodImposterCollection
        {
            private readonly EchoMethodInvocationHistoryCollection _echoMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public EchoMethodImposterCollection(EchoMethodInvocationHistoryCollection _echoMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._echoMethodInvocationHistoryCollection = _echoMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IEchoMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IEchoMethodImposter>();
            internal EchoMethodImposter<T> AddNew<T>()
            {
                var imposter = new EchoMethodImposter<T>(_echoMethodInvocationHistoryCollection, _invocationBehavior);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IEchoMethodImposter<T> GetImposterWithMatchingSetup<T>(EchoArguments<T> arguments)
            {
                return _imposters.Select(it => it.As<T>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<T>();
            }
        }

        // virtual T ClassWithOverloadsAndGenerics.Echo<T>(T item)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class EchoMethodInvocationImposterGroup<T>
        {
            internal static EchoMethodInvocationImposterGroup<T> Default = new EchoMethodInvocationImposterGroup<T>(new EchoArgumentsCriteria<T>(global::Imposter.Abstractions.Arg<T>.Any()));
            internal EchoArgumentsCriteria<T> ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public EchoMethodInvocationImposterGroup(EchoArgumentsCriteria<T> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
            }

            internal MethodInvocationImposter AddInvocationImposter()
            {
                MethodInvocationImposter invocationImposter = new MethodInvocationImposter();
                _invocationImposters.Enqueue(invocationImposter);
                return invocationImposter;
            }

            private MethodInvocationImposter? GetInvocationImposter()
            {
                MethodInvocationImposter invocationImposter;
                if (_invocationImposters.TryDequeue(out invocationImposter))
                {
                    if (!invocationImposter.IsEmpty)
                    {
                        _lastestInvocationImposter = invocationImposter;
                    }

                    return invocationImposter;
                }

                return _lastestInvocationImposter;
            }

            public T Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, T item, EchoDelegate<T> baseImplementation = null)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter();
                if (invocationImposter == null)
                {
                    if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    invocationImposter = MethodInvocationImposter.Default;
                }

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, item, baseImplementation);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default.Returns(DefaultResultGenerator);
                }

                private EchoDelegate<T> _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<EchoCallbackDelegate<T>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<EchoCallbackDelegate<T>>();
                private bool _useBaseImplementation;
                internal bool IsEmpty => !_useBaseImplementation && ((_resultGenerator == null) && (_callbacks.Count == 0));

                public T Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, T item, EchoDelegate<T> baseImplementation = null)
                {
                    if (_useBaseImplementation)
                    {
                        _resultGenerator = baseImplementation ?? throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    T result = _resultGenerator.Invoke(item);
                    foreach (var callback in _callbacks)
                    {
                        callback(item);
                    }

                    return result;
                }

                internal void Callback(EchoCallbackDelegate<T> callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(EchoDelegate<T> resultGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(T value)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (T item) =>
                    {
                        return value;
                    };
                }

                internal void Throws(EchoExceptionGeneratorDelegate<T> exceptionGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (T item) =>
                    {
                        throw exceptionGenerator(item);
                    };
                }

                internal void UseBaseImplementation()
                {
                    _useBaseImplementation = true;
                    _resultGenerator = null;
                }

                internal static T DefaultResultGenerator(T item)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IEchoMethodInvocationImposterGroupCallback<T>
        {
            IEchoMethodInvocationImposterGroupContinuation<T> Callback(EchoCallbackDelegate<T> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IEchoMethodInvocationImposterGroupContinuation<T> : IEchoMethodInvocationImposterGroupCallback<T>
        {
            IEchoMethodInvocationImposterGroup<T> Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IEchoMethodInvocationImposterGroup<T> : IEchoMethodInvocationImposterGroupCallback<T>
        {
            IEchoMethodInvocationImposterGroupContinuation<T> Throws<TException>()
                where TException : System.Exception, new();
            IEchoMethodInvocationImposterGroupContinuation<T> Throws(System.Exception exception);
            IEchoMethodInvocationImposterGroupContinuation<T> Throws(EchoExceptionGeneratorDelegate<T> exceptionGenerator);
            IEchoMethodInvocationImposterGroupContinuation<T> Returns(EchoDelegate<T> resultGenerator);
            IEchoMethodInvocationImposterGroupContinuation<T> Returns(T value);
            IEchoMethodInvocationImposterGroupContinuation<T> UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IEchoMethodImposter
        {
            IEchoMethodImposter<TTarget>? As<TTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IEchoMethodImposter<T> : IEchoMethodImposter
        {
            T Invoke(T item, EchoDelegate<T> baseImplementation = null);
            bool HasMatchingSetup(EchoArguments<T> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface EchoInvocationVerifier<T>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual T ClassWithOverloadsAndGenerics.Echo<T>(T item)
        public interface IEchoMethodImposterBuilder<T> : IEchoMethodInvocationImposterGroup<T>, IEchoMethodInvocationImposterGroupCallback<T>, EchoInvocationVerifier<T>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class EchoMethodImposter<T> : IEchoMethodImposter<T>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<EchoMethodInvocationImposterGroup<T>> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<EchoMethodInvocationImposterGroup<T>>();
            private readonly EchoMethodInvocationHistoryCollection _echoMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public EchoMethodImposter(EchoMethodInvocationHistoryCollection _echoMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._echoMethodInvocationHistoryCollection = _echoMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            IEchoMethodImposter<TTarget>? IEchoMethodImposter.As<TTarget>()
            {
                if (typeof(TTarget).IsAssignableTo(typeof(T)) && typeof(T).IsAssignableTo(typeof(TTarget)))
                {
                    return new Adapter<TTarget>(this);
                }

                return null;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private class Adapter<TTarget> : IEchoMethodImposter<TTarget>
            {
                private readonly EchoMethodImposter<T> _target;
                public Adapter(EchoMethodImposter<T> target)
                {
                    _target = target;
                }

                public TTarget Invoke(TTarget item, EchoDelegate<TTarget> baseImplementation = null)
                {
                    var result = _target.Invoke(global::Imposter.Abstractions.TypeCaster.Cast<TTarget, T>(item), global::Imposter.Abstractions.TypeCaster.Cast<EchoDelegate<TTarget>, EchoDelegate<T>>(baseImplementation));
                    return global::Imposter.Abstractions.TypeCaster.Cast<T, TTarget>(result);
                }

                public bool HasMatchingSetup(EchoArguments<TTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<T>());
                }

                IEchoMethodImposter<TTarget1>? IEchoMethodImposter.As<TTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(EchoArguments<T> arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private EchoMethodInvocationImposterGroup<T>? FindMatchingInvocationImposterGroup(EchoArguments<T> arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public T Invoke(T item, EchoDelegate<T> baseImplementation = null)
            {
                var arguments = new EchoArguments<T>(item);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("virtual T ClassWithOverloadsAndGenerics.Echo<T>(T item)");
                    }

                    matchingInvocationImposterGroup = EchoMethodInvocationImposterGroup<T>.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual T ClassWithOverloadsAndGenerics.Echo<T>(T item)", item, baseImplementation);
                    _echoMethodInvocationHistoryCollection.Add(new EchoMethodInvocationHistory<T>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _echoMethodInvocationHistoryCollection.Add(new EchoMethodInvocationHistory<T>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IEchoMethodImposterBuilder<T>, IEchoMethodInvocationImposterGroupContinuation<T>
            {
                private readonly EchoMethodImposterCollection _imposterCollection;
                private readonly EchoMethodInvocationHistoryCollection _echoMethodInvocationHistoryCollection;
                private readonly EchoArgumentsCriteria<T> _argumentsCriteria;
                private readonly EchoMethodInvocationImposterGroup<T> _invocationImposterGroup;
                private EchoMethodInvocationImposterGroup<T>.MethodInvocationImposter _currentInvocationImposter;
                public Builder(EchoMethodImposterCollection _imposterCollection, EchoMethodInvocationHistoryCollection _echoMethodInvocationHistoryCollection, EchoArgumentsCriteria<T> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._echoMethodInvocationHistoryCollection = _echoMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new EchoMethodInvocationImposterGroup<T>(_argumentsCriteria);
                    EchoMethodImposter<T> methodImposter = _imposterCollection.AddNew<T>();
                    methodImposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IEchoMethodInvocationImposterGroupContinuation<T> IEchoMethodInvocationImposterGroup<T>.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((T item) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IEchoMethodInvocationImposterGroupContinuation<T> IEchoMethodInvocationImposterGroup<T>.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((T item) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IEchoMethodInvocationImposterGroupContinuation<T> IEchoMethodInvocationImposterGroup<T>.Throws(EchoExceptionGeneratorDelegate<T> exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((T item) =>
                    {
                        throw exceptionGenerator.Invoke(item);
                    });
                    return this;
                }

                IEchoMethodInvocationImposterGroupContinuation<T> IEchoMethodInvocationImposterGroupCallback<T>.Callback(EchoCallbackDelegate<T> callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IEchoMethodInvocationImposterGroupContinuation<T> IEchoMethodInvocationImposterGroup<T>.Returns(EchoDelegate<T> resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IEchoMethodInvocationImposterGroupContinuation<T> IEchoMethodInvocationImposterGroup<T>.Returns(T value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IEchoMethodInvocationImposterGroupContinuation<T> IEchoMethodInvocationImposterGroup<T>.UseBaseImplementation()
                {
                    _currentInvocationImposter.UseBaseImplementation();
                    return this;
                }

                IEchoMethodInvocationImposterGroup<T> IEchoMethodInvocationImposterGroupContinuation<T>.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void EchoInvocationVerifier<T>.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _echoMethodInvocationHistoryCollection.Count<T>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // virtual string ClassWithOverloadsAndGenerics.Format(int value)
        public delegate string FormatDelegate(int value);
        // virtual string ClassWithOverloadsAndGenerics.Format(int value)
        public delegate void FormatCallbackDelegate(int value);
        // virtual string ClassWithOverloadsAndGenerics.Format(int value)
        public delegate System.Exception FormatExceptionGeneratorDelegate(int value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class FormatArguments
        {
            public int value;
            internal FormatArguments(int value)
            {
                this.value = value;
            }
        }

        // virtual string ClassWithOverloadsAndGenerics.Format(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class FormatArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> value { get; }

            public FormatArgumentsCriteria(global::Imposter.Abstractions.Arg<int> value)
            {
                this.value = value;
            }

            public bool Matches(FormatArguments arguments)
            {
                return value.Matches(arguments.value);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IFormatMethodInvocationHistory
        {
            bool Matches(FormatArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class FormatMethodInvocationHistory : IFormatMethodInvocationHistory
        {
            internal FormatArguments Arguments;
            internal string Result;
            internal System.Exception Exception;
            public FormatMethodInvocationHistory(FormatArguments Arguments, string Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(FormatArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class FormatMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IFormatMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IFormatMethodInvocationHistory>();
            internal void Add(IFormatMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(FormatArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // virtual string ClassWithOverloadsAndGenerics.Format(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class FormatMethodInvocationImposterGroup
        {
            internal static FormatMethodInvocationImposterGroup Default = new FormatMethodInvocationImposterGroup(new FormatArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any()));
            internal FormatArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public FormatMethodInvocationImposterGroup(FormatArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
            }

            internal MethodInvocationImposter AddInvocationImposter()
            {
                MethodInvocationImposter invocationImposter = new MethodInvocationImposter();
                _invocationImposters.Enqueue(invocationImposter);
                return invocationImposter;
            }

            private MethodInvocationImposter? GetInvocationImposter()
            {
                MethodInvocationImposter invocationImposter;
                if (_invocationImposters.TryDequeue(out invocationImposter))
                {
                    if (!invocationImposter.IsEmpty)
                    {
                        _lastestInvocationImposter = invocationImposter;
                    }

                    return invocationImposter;
                }

                return _lastestInvocationImposter;
            }

            public string Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value, FormatDelegate baseImplementation = null)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter();
                if (invocationImposter == null)
                {
                    if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    invocationImposter = MethodInvocationImposter.Default;
                }

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, value, baseImplementation);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default.Returns(DefaultResultGenerator);
                }

                private FormatDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<FormatCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<FormatCallbackDelegate>();
                private bool _useBaseImplementation;
                internal bool IsEmpty => !_useBaseImplementation && ((_resultGenerator == null) && (_callbacks.Count == 0));

                public string Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value, FormatDelegate baseImplementation = null)
                {
                    if (_useBaseImplementation)
                    {
                        _resultGenerator = baseImplementation ?? throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    string result = _resultGenerator.Invoke(value);
                    foreach (var callback in _callbacks)
                    {
                        callback(value);
                    }

                    return result;
                }

                internal void Callback(FormatCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(FormatDelegate resultGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(string value_1)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (int value) =>
                    {
                        return value_1;
                    };
                }

                internal void Throws(FormatExceptionGeneratorDelegate exceptionGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (int value) =>
                    {
                        throw exceptionGenerator(value);
                    };
                }

                internal void UseBaseImplementation()
                {
                    _useBaseImplementation = true;
                    _resultGenerator = null;
                }

                internal static string DefaultResultGenerator(int value)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IFormatMethodInvocationImposterGroupCallback
        {
            IFormatMethodInvocationImposterGroupContinuation Callback(FormatCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IFormatMethodInvocationImposterGroupContinuation : IFormatMethodInvocationImposterGroupCallback
        {
            IFormatMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IFormatMethodInvocationImposterGroup : IFormatMethodInvocationImposterGroupCallback
        {
            IFormatMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IFormatMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IFormatMethodInvocationImposterGroupContinuation Throws(FormatExceptionGeneratorDelegate exceptionGenerator);
            IFormatMethodInvocationImposterGroupContinuation Returns(FormatDelegate resultGenerator);
            IFormatMethodInvocationImposterGroupContinuation Returns(string value);
            IFormatMethodInvocationImposterGroupContinuation UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface FormatInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual string ClassWithOverloadsAndGenerics.Format(int value)
        public interface IFormatMethodImposterBuilder : IFormatMethodInvocationImposterGroup, IFormatMethodInvocationImposterGroupCallback, FormatInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class FormatMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<FormatMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<FormatMethodInvocationImposterGroup>();
            private readonly FormatMethodInvocationHistoryCollection _formatMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public FormatMethodImposter(FormatMethodInvocationHistoryCollection _formatMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._formatMethodInvocationHistoryCollection = _formatMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(FormatArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private FormatMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(FormatArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public string Invoke(int value, FormatDelegate baseImplementation = null)
            {
                var arguments = new FormatArguments(value);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("virtual string ClassWithOverloadsAndGenerics.Format(int value)");
                    }

                    matchingInvocationImposterGroup = FormatMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual string ClassWithOverloadsAndGenerics.Format(int value)", value, baseImplementation);
                    _formatMethodInvocationHistoryCollection.Add(new FormatMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _formatMethodInvocationHistoryCollection.Add(new FormatMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IFormatMethodImposterBuilder, IFormatMethodInvocationImposterGroupContinuation
            {
                private readonly FormatMethodImposter _imposter;
                private readonly FormatMethodInvocationHistoryCollection _formatMethodInvocationHistoryCollection;
                private readonly FormatArgumentsCriteria _argumentsCriteria;
                private readonly FormatMethodInvocationImposterGroup _invocationImposterGroup;
                private FormatMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(FormatMethodImposter _imposter, FormatMethodInvocationHistoryCollection _formatMethodInvocationHistoryCollection, FormatArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._formatMethodInvocationHistoryCollection = _formatMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new FormatMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IFormatMethodInvocationImposterGroupContinuation IFormatMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IFormatMethodInvocationImposterGroupContinuation IFormatMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IFormatMethodInvocationImposterGroupContinuation IFormatMethodInvocationImposterGroup.Throws(FormatExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exceptionGenerator.Invoke(value);
                    });
                    return this;
                }

                IFormatMethodInvocationImposterGroupContinuation IFormatMethodInvocationImposterGroupCallback.Callback(FormatCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IFormatMethodInvocationImposterGroupContinuation IFormatMethodInvocationImposterGroup.Returns(FormatDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IFormatMethodInvocationImposterGroupContinuation IFormatMethodInvocationImposterGroup.Returns(string value_1)
                {
                    _currentInvocationImposter.Returns(value_1);
                    return this;
                }

                IFormatMethodInvocationImposterGroupContinuation IFormatMethodInvocationImposterGroup.UseBaseImplementation()
                {
                    _currentInvocationImposter.UseBaseImplementation();
                    return this;
                }

                IFormatMethodInvocationImposterGroup IFormatMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void FormatInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _formatMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // virtual string ClassWithOverloadsAndGenerics.Format(string value, int padding)
        public delegate string Format_1Delegate(string value, int padding);
        // virtual string ClassWithOverloadsAndGenerics.Format(string value, int padding)
        public delegate void Format_1CallbackDelegate(string value, int padding);
        // virtual string ClassWithOverloadsAndGenerics.Format(string value, int padding)
        public delegate System.Exception Format_1ExceptionGeneratorDelegate(string value, int padding);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Format_1Arguments
        {
            public string value;
            public int padding;
            internal Format_1Arguments(string value, int padding)
            {
                this.value = value;
                this.padding = padding;
            }
        }

        // virtual string ClassWithOverloadsAndGenerics.Format(string value, int padding)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Format_1ArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<string> value { get; }
            public global::Imposter.Abstractions.Arg<int> padding { get; }

            public Format_1ArgumentsCriteria(global::Imposter.Abstractions.Arg<string> value, global::Imposter.Abstractions.Arg<int> padding)
            {
                this.value = value;
                this.padding = padding;
            }

            public bool Matches(Format_1Arguments arguments)
            {
                return value.Matches(arguments.value) && padding.Matches(arguments.padding);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IFormat_1MethodInvocationHistory
        {
            bool Matches(Format_1ArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Format_1MethodInvocationHistory : IFormat_1MethodInvocationHistory
        {
            internal Format_1Arguments Arguments;
            internal string Result;
            internal System.Exception Exception;
            public Format_1MethodInvocationHistory(Format_1Arguments Arguments, string Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(Format_1ArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Format_1MethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IFormat_1MethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IFormat_1MethodInvocationHistory>();
            internal void Add(IFormat_1MethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(Format_1ArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // virtual string ClassWithOverloadsAndGenerics.Format(string value, int padding)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Format_1MethodInvocationImposterGroup
        {
            internal static Format_1MethodInvocationImposterGroup Default = new Format_1MethodInvocationImposterGroup(new Format_1ArgumentsCriteria(global::Imposter.Abstractions.Arg<string>.Any(), global::Imposter.Abstractions.Arg<int>.Any()));
            internal Format_1ArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public Format_1MethodInvocationImposterGroup(Format_1ArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
            }

            internal MethodInvocationImposter AddInvocationImposter()
            {
                MethodInvocationImposter invocationImposter = new MethodInvocationImposter();
                _invocationImposters.Enqueue(invocationImposter);
                return invocationImposter;
            }

            private MethodInvocationImposter? GetInvocationImposter()
            {
                MethodInvocationImposter invocationImposter;
                if (_invocationImposters.TryDequeue(out invocationImposter))
                {
                    if (!invocationImposter.IsEmpty)
                    {
                        _lastestInvocationImposter = invocationImposter;
                    }

                    return invocationImposter;
                }

                return _lastestInvocationImposter;
            }

            public string Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string value, int padding, Format_1Delegate baseImplementation = null)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter();
                if (invocationImposter == null)
                {
                    if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    invocationImposter = MethodInvocationImposter.Default;
                }

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, value, padding, baseImplementation);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default.Returns(DefaultResultGenerator);
                }

                private Format_1Delegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<Format_1CallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<Format_1CallbackDelegate>();
                private bool _useBaseImplementation;
                internal bool IsEmpty => !_useBaseImplementation && ((_resultGenerator == null) && (_callbacks.Count == 0));

                public string Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string value, int padding, Format_1Delegate baseImplementation = null)
                {
                    if (_useBaseImplementation)
                    {
                        _resultGenerator = baseImplementation ?? throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    string result = _resultGenerator.Invoke(value, padding);
                    foreach (var callback in _callbacks)
                    {
                        callback(value, padding);
                    }

                    return result;
                }

                internal void Callback(Format_1CallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(Format_1Delegate resultGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(string value_1)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (string value, int padding) =>
                    {
                        return value_1;
                    };
                }

                internal void Throws(Format_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (string value, int padding) =>
                    {
                        throw exceptionGenerator(value, padding);
                    };
                }

                internal void UseBaseImplementation()
                {
                    _useBaseImplementation = true;
                    _resultGenerator = null;
                }

                internal static string DefaultResultGenerator(string value, int padding)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IFormat_1MethodInvocationImposterGroupCallback
        {
            IFormat_1MethodInvocationImposterGroupContinuation Callback(Format_1CallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IFormat_1MethodInvocationImposterGroupContinuation : IFormat_1MethodInvocationImposterGroupCallback
        {
            IFormat_1MethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IFormat_1MethodInvocationImposterGroup : IFormat_1MethodInvocationImposterGroupCallback
        {
            IFormat_1MethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IFormat_1MethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IFormat_1MethodInvocationImposterGroupContinuation Throws(Format_1ExceptionGeneratorDelegate exceptionGenerator);
            IFormat_1MethodInvocationImposterGroupContinuation Returns(Format_1Delegate resultGenerator);
            IFormat_1MethodInvocationImposterGroupContinuation Returns(string value);
            IFormat_1MethodInvocationImposterGroupContinuation UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface Format_1InvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual string ClassWithOverloadsAndGenerics.Format(string value, int padding)
        public interface IFormat_1MethodImposterBuilder : IFormat_1MethodInvocationImposterGroup, IFormat_1MethodInvocationImposterGroupCallback, Format_1InvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Format_1MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<Format_1MethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<Format_1MethodInvocationImposterGroup>();
            private readonly Format_1MethodInvocationHistoryCollection _format_1MethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public Format_1MethodImposter(Format_1MethodInvocationHistoryCollection _format_1MethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._format_1MethodInvocationHistoryCollection = _format_1MethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(Format_1Arguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private Format_1MethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(Format_1Arguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public string Invoke(string value, int padding, Format_1Delegate baseImplementation = null)
            {
                var arguments = new Format_1Arguments(value, padding);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("virtual string ClassWithOverloadsAndGenerics.Format(string value, int padding)");
                    }

                    matchingInvocationImposterGroup = Format_1MethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual string ClassWithOverloadsAndGenerics.Format(string value, int padding)", value, padding, baseImplementation);
                    _format_1MethodInvocationHistoryCollection.Add(new Format_1MethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _format_1MethodInvocationHistoryCollection.Add(new Format_1MethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IFormat_1MethodImposterBuilder, IFormat_1MethodInvocationImposterGroupContinuation
            {
                private readonly Format_1MethodImposter _imposter;
                private readonly Format_1MethodInvocationHistoryCollection _format_1MethodInvocationHistoryCollection;
                private readonly Format_1ArgumentsCriteria _argumentsCriteria;
                private readonly Format_1MethodInvocationImposterGroup _invocationImposterGroup;
                private Format_1MethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(Format_1MethodImposter _imposter, Format_1MethodInvocationHistoryCollection _format_1MethodInvocationHistoryCollection, Format_1ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._format_1MethodInvocationHistoryCollection = _format_1MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new Format_1MethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IFormat_1MethodInvocationImposterGroupContinuation IFormat_1MethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((string value, int padding) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IFormat_1MethodInvocationImposterGroupContinuation IFormat_1MethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((string value, int padding) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IFormat_1MethodInvocationImposterGroupContinuation IFormat_1MethodInvocationImposterGroup.Throws(Format_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((string value, int padding) =>
                    {
                        throw exceptionGenerator.Invoke(value, padding);
                    });
                    return this;
                }

                IFormat_1MethodInvocationImposterGroupContinuation IFormat_1MethodInvocationImposterGroupCallback.Callback(Format_1CallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IFormat_1MethodInvocationImposterGroupContinuation IFormat_1MethodInvocationImposterGroup.Returns(Format_1Delegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IFormat_1MethodInvocationImposterGroupContinuation IFormat_1MethodInvocationImposterGroup.Returns(string value_1)
                {
                    _currentInvocationImposter.Returns(value_1);
                    return this;
                }

                IFormat_1MethodInvocationImposterGroupContinuation IFormat_1MethodInvocationImposterGroup.UseBaseImplementation()
                {
                    _currentInvocationImposter.UseBaseImplementation();
                    return this;
                }

                IFormat_1MethodInvocationImposterGroup IFormat_1MethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void Format_1InvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _format_1MethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // virtual TFirst ClassWithOverloadsAndGenerics.SelectFirst<TFirst, TSecond>(TFirst first, TSecond second)
        public delegate TFirst SelectFirstDelegate<TFirst, TSecond>(TFirst first, TSecond second);
        // virtual TFirst ClassWithOverloadsAndGenerics.SelectFirst<TFirst, TSecond>(TFirst first, TSecond second)
        public delegate void SelectFirstCallbackDelegate<TFirst, TSecond>(TFirst first, TSecond second);
        // virtual TFirst ClassWithOverloadsAndGenerics.SelectFirst<TFirst, TSecond>(TFirst first, TSecond second)
        public delegate System.Exception SelectFirstExceptionGeneratorDelegate<TFirst, TSecond>(TFirst first, TSecond second);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class SelectFirstArguments<TFirst, TSecond>
        {
            public TFirst first;
            public TSecond second;
            internal SelectFirstArguments(TFirst first, TSecond second)
            {
                this.first = first;
                this.second = second;
            }

            public SelectFirstArguments<TFirstTarget, TSecondTarget> As<TFirstTarget, TSecondTarget>()
            {
                return new SelectFirstArguments<TFirstTarget, TSecondTarget>(TypeCaster.Cast<TFirst, TFirstTarget>(first), TypeCaster.Cast<TSecond, TSecondTarget>(second));
            }
        }

        // virtual TFirst ClassWithOverloadsAndGenerics.SelectFirst<TFirst, TSecond>(TFirst first, TSecond second)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class SelectFirstArgumentsCriteria<TFirst, TSecond>
        {
            public global::Imposter.Abstractions.Arg<TFirst> first { get; }
            public global::Imposter.Abstractions.Arg<TSecond> second { get; }

            public SelectFirstArgumentsCriteria(global::Imposter.Abstractions.Arg<TFirst> first, global::Imposter.Abstractions.Arg<TSecond> second)
            {
                this.first = first;
                this.second = second;
            }

            public bool Matches(SelectFirstArguments<TFirst, TSecond> arguments)
            {
                return first.Matches(arguments.first) && second.Matches(arguments.second);
            }

            public SelectFirstArgumentsCriteria<TFirstTarget, TSecondTarget> As<TFirstTarget, TSecondTarget>()
            {
                return new SelectFirstArgumentsCriteria<TFirstTarget, TSecondTarget>(global::Imposter.Abstractions.Arg<TFirstTarget>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<TFirstTarget, TFirst>(it, out TFirst firstTarget) && first.Matches(firstTarget)), global::Imposter.Abstractions.Arg<TSecondTarget>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<TSecondTarget, TSecond>(it, out TSecond secondTarget) && second.Matches(secondTarget)));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISelectFirstMethodInvocationHistory
        {
            bool Matches<TFirstTarget, TSecondTarget>(SelectFirstArgumentsCriteria<TFirstTarget, TSecondTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SelectFirstMethodInvocationHistory<TFirst, TSecond> : ISelectFirstMethodInvocationHistory
        {
            internal SelectFirstArguments<TFirst, TSecond> Arguments;
            internal TFirst Result;
            internal System.Exception Exception;
            public SelectFirstMethodInvocationHistory(SelectFirstArguments<TFirst, TSecond> Arguments, TFirst Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TFirstTarget, TSecondTarget>(SelectFirstArgumentsCriteria<TFirstTarget, TSecondTarget> criteria)
            {
                return (((typeof(TFirstTarget) == typeof(TFirst)) && (typeof(TSecondTarget) == typeof(TSecond))) && (typeof(TFirst) == typeof(TFirstTarget))) && criteria.As<TFirst, TSecond>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SelectFirstMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ISelectFirstMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ISelectFirstMethodInvocationHistory>();
            internal void Add(ISelectFirstMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TFirst, TSecond>(SelectFirstArgumentsCriteria<TFirst, TSecond> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TFirst, TSecond>(argumentsCriteria));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SelectFirstMethodImposterCollection
        {
            private readonly SelectFirstMethodInvocationHistoryCollection _selectFirstMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public SelectFirstMethodImposterCollection(SelectFirstMethodInvocationHistoryCollection _selectFirstMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._selectFirstMethodInvocationHistoryCollection = _selectFirstMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<ISelectFirstMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<ISelectFirstMethodImposter>();
            internal SelectFirstMethodImposter<TFirst, TSecond> AddNew<TFirst, TSecond>()
            {
                var imposter = new SelectFirstMethodImposter<TFirst, TSecond>(_selectFirstMethodInvocationHistoryCollection, _invocationBehavior);
                _imposters.Push(imposter);
                return imposter;
            }

            internal ISelectFirstMethodImposter<TFirst, TSecond> GetImposterWithMatchingSetup<TFirst, TSecond>(SelectFirstArguments<TFirst, TSecond> arguments)
            {
                return _imposters.Select(it => it.As<TFirst, TSecond>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TFirst, TSecond>();
            }
        }

        // virtual TFirst ClassWithOverloadsAndGenerics.SelectFirst<TFirst, TSecond>(TFirst first, TSecond second)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class SelectFirstMethodInvocationImposterGroup<TFirst, TSecond>
        {
            internal static SelectFirstMethodInvocationImposterGroup<TFirst, TSecond> Default = new SelectFirstMethodInvocationImposterGroup<TFirst, TSecond>(new SelectFirstArgumentsCriteria<TFirst, TSecond>(global::Imposter.Abstractions.Arg<TFirst>.Any(), global::Imposter.Abstractions.Arg<TSecond>.Any()));
            internal SelectFirstArgumentsCriteria<TFirst, TSecond> ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public SelectFirstMethodInvocationImposterGroup(SelectFirstArgumentsCriteria<TFirst, TSecond> argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
            }

            internal MethodInvocationImposter AddInvocationImposter()
            {
                MethodInvocationImposter invocationImposter = new MethodInvocationImposter();
                _invocationImposters.Enqueue(invocationImposter);
                return invocationImposter;
            }

            private MethodInvocationImposter? GetInvocationImposter()
            {
                MethodInvocationImposter invocationImposter;
                if (_invocationImposters.TryDequeue(out invocationImposter))
                {
                    if (!invocationImposter.IsEmpty)
                    {
                        _lastestInvocationImposter = invocationImposter;
                    }

                    return invocationImposter;
                }

                return _lastestInvocationImposter;
            }

            public TFirst Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, TFirst first, TSecond second, SelectFirstDelegate<TFirst, TSecond> baseImplementation = null)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter();
                if (invocationImposter == null)
                {
                    if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    invocationImposter = MethodInvocationImposter.Default;
                }

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, first, second, baseImplementation);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default.Returns(DefaultResultGenerator);
                }

                private SelectFirstDelegate<TFirst, TSecond> _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<SelectFirstCallbackDelegate<TFirst, TSecond>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<SelectFirstCallbackDelegate<TFirst, TSecond>>();
                private bool _useBaseImplementation;
                internal bool IsEmpty => !_useBaseImplementation && ((_resultGenerator == null) && (_callbacks.Count == 0));

                public TFirst Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, TFirst first, TSecond second, SelectFirstDelegate<TFirst, TSecond> baseImplementation = null)
                {
                    if (_useBaseImplementation)
                    {
                        _resultGenerator = baseImplementation ?? throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    TFirst result = _resultGenerator.Invoke(first, second);
                    foreach (var callback in _callbacks)
                    {
                        callback(first, second);
                    }

                    return result;
                }

                internal void Callback(SelectFirstCallbackDelegate<TFirst, TSecond> callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(SelectFirstDelegate<TFirst, TSecond> resultGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(TFirst value)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (TFirst first, TSecond second) =>
                    {
                        return value;
                    };
                }

                internal void Throws(SelectFirstExceptionGeneratorDelegate<TFirst, TSecond> exceptionGenerator)
                {
                    _useBaseImplementation = false;
                    _resultGenerator = (TFirst first, TSecond second) =>
                    {
                        throw exceptionGenerator(first, second);
                    };
                }

                internal void UseBaseImplementation()
                {
                    _useBaseImplementation = true;
                    _resultGenerator = null;
                }

                internal static TFirst DefaultResultGenerator(TFirst first, TSecond second)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISelectFirstMethodInvocationImposterGroupCallback<TFirst, TSecond>
        {
            ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> Callback(SelectFirstCallbackDelegate<TFirst, TSecond> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> : ISelectFirstMethodInvocationImposterGroupCallback<TFirst, TSecond>
        {
            ISelectFirstMethodInvocationImposterGroup<TFirst, TSecond> Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISelectFirstMethodInvocationImposterGroup<TFirst, TSecond> : ISelectFirstMethodInvocationImposterGroupCallback<TFirst, TSecond>
        {
            ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> Throws<TException>()
                where TException : System.Exception, new();
            ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> Throws(System.Exception exception);
            ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> Throws(SelectFirstExceptionGeneratorDelegate<TFirst, TSecond> exceptionGenerator);
            ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> Returns(SelectFirstDelegate<TFirst, TSecond> resultGenerator);
            ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> Returns(TFirst value);
            ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> UseBaseImplementation();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface ISelectFirstMethodImposter
        {
            ISelectFirstMethodImposter<TFirstTarget, TSecondTarget>? As<TFirstTarget, TSecondTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface ISelectFirstMethodImposter<TFirst, TSecond> : ISelectFirstMethodImposter
        {
            TFirst Invoke(TFirst first, TSecond second, SelectFirstDelegate<TFirst, TSecond> baseImplementation = null);
            bool HasMatchingSetup(SelectFirstArguments<TFirst, TSecond> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface SelectFirstInvocationVerifier<TFirst, TSecond>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual TFirst ClassWithOverloadsAndGenerics.SelectFirst<TFirst, TSecond>(TFirst first, TSecond second)
        public interface ISelectFirstMethodImposterBuilder<TFirst, TSecond> : ISelectFirstMethodInvocationImposterGroup<TFirst, TSecond>, ISelectFirstMethodInvocationImposterGroupCallback<TFirst, TSecond>, SelectFirstInvocationVerifier<TFirst, TSecond>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SelectFirstMethodImposter<TFirst, TSecond> : ISelectFirstMethodImposter<TFirst, TSecond>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<SelectFirstMethodInvocationImposterGroup<TFirst, TSecond>> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<SelectFirstMethodInvocationImposterGroup<TFirst, TSecond>>();
            private readonly SelectFirstMethodInvocationHistoryCollection _selectFirstMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public SelectFirstMethodImposter(SelectFirstMethodInvocationHistoryCollection _selectFirstMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._selectFirstMethodInvocationHistoryCollection = _selectFirstMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            ISelectFirstMethodImposter<TFirstTarget, TSecondTarget>? ISelectFirstMethodImposter.As<TFirstTarget, TSecondTarget>()
            {
                if ((typeof(TFirstTarget).IsAssignableTo(typeof(TFirst)) && typeof(TSecondTarget).IsAssignableTo(typeof(TSecond))) && typeof(TFirst).IsAssignableTo(typeof(TFirstTarget)))
                {
                    return new Adapter<TFirstTarget, TSecondTarget>(this);
                }

                return null;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private class Adapter<TFirstTarget, TSecondTarget> : ISelectFirstMethodImposter<TFirstTarget, TSecondTarget>
            {
                private readonly SelectFirstMethodImposter<TFirst, TSecond> _target;
                public Adapter(SelectFirstMethodImposter<TFirst, TSecond> target)
                {
                    _target = target;
                }

                public TFirstTarget Invoke(TFirstTarget first, TSecondTarget second, SelectFirstDelegate<TFirstTarget, TSecondTarget> baseImplementation = null)
                {
                    var result = _target.Invoke(global::Imposter.Abstractions.TypeCaster.Cast<TFirstTarget, TFirst>(first), global::Imposter.Abstractions.TypeCaster.Cast<TSecondTarget, TSecond>(second), global::Imposter.Abstractions.TypeCaster.Cast<SelectFirstDelegate<TFirstTarget, TSecondTarget>, SelectFirstDelegate<TFirst, TSecond>>(baseImplementation));
                    return global::Imposter.Abstractions.TypeCaster.Cast<TFirst, TFirstTarget>(result);
                }

                public bool HasMatchingSetup(SelectFirstArguments<TFirstTarget, TSecondTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TFirst, TSecond>());
                }

                ISelectFirstMethodImposter<TFirstTarget1, TSecondTarget1>? ISelectFirstMethodImposter.As<TFirstTarget1, TSecondTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(SelectFirstArguments<TFirst, TSecond> arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private SelectFirstMethodInvocationImposterGroup<TFirst, TSecond>? FindMatchingInvocationImposterGroup(SelectFirstArguments<TFirst, TSecond> arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public TFirst Invoke(TFirst first, TSecond second, SelectFirstDelegate<TFirst, TSecond> baseImplementation = null)
            {
                var arguments = new SelectFirstArguments<TFirst, TSecond>(first, second);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("virtual TFirst ClassWithOverloadsAndGenerics.SelectFirst<TFirst, TSecond>(TFirst first, TSecond second)");
                    }

                    matchingInvocationImposterGroup = SelectFirstMethodInvocationImposterGroup<TFirst, TSecond>.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual TFirst ClassWithOverloadsAndGenerics.SelectFirst<TFirst, TSecond>(TFirst first, TSecond second)", first, second, baseImplementation);
                    _selectFirstMethodInvocationHistoryCollection.Add(new SelectFirstMethodInvocationHistory<TFirst, TSecond>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _selectFirstMethodInvocationHistoryCollection.Add(new SelectFirstMethodInvocationHistory<TFirst, TSecond>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ISelectFirstMethodImposterBuilder<TFirst, TSecond>, ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond>
            {
                private readonly SelectFirstMethodImposterCollection _imposterCollection;
                private readonly SelectFirstMethodInvocationHistoryCollection _selectFirstMethodInvocationHistoryCollection;
                private readonly SelectFirstArgumentsCriteria<TFirst, TSecond> _argumentsCriteria;
                private readonly SelectFirstMethodInvocationImposterGroup<TFirst, TSecond> _invocationImposterGroup;
                private SelectFirstMethodInvocationImposterGroup<TFirst, TSecond>.MethodInvocationImposter _currentInvocationImposter;
                public Builder(SelectFirstMethodImposterCollection _imposterCollection, SelectFirstMethodInvocationHistoryCollection _selectFirstMethodInvocationHistoryCollection, SelectFirstArgumentsCriteria<TFirst, TSecond> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._selectFirstMethodInvocationHistoryCollection = _selectFirstMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new SelectFirstMethodInvocationImposterGroup<TFirst, TSecond>(_argumentsCriteria);
                    SelectFirstMethodImposter<TFirst, TSecond> methodImposter = _imposterCollection.AddNew<TFirst, TSecond>();
                    methodImposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> ISelectFirstMethodInvocationImposterGroup<TFirst, TSecond>.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((TFirst first, TSecond second) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> ISelectFirstMethodInvocationImposterGroup<TFirst, TSecond>.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((TFirst first, TSecond second) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> ISelectFirstMethodInvocationImposterGroup<TFirst, TSecond>.Throws(SelectFirstExceptionGeneratorDelegate<TFirst, TSecond> exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((TFirst first, TSecond second) =>
                    {
                        throw exceptionGenerator.Invoke(first, second);
                    });
                    return this;
                }

                ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> ISelectFirstMethodInvocationImposterGroupCallback<TFirst, TSecond>.Callback(SelectFirstCallbackDelegate<TFirst, TSecond> callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> ISelectFirstMethodInvocationImposterGroup<TFirst, TSecond>.Returns(SelectFirstDelegate<TFirst, TSecond> resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> ISelectFirstMethodInvocationImposterGroup<TFirst, TSecond>.Returns(TFirst value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond> ISelectFirstMethodInvocationImposterGroup<TFirst, TSecond>.UseBaseImplementation()
                {
                    _currentInvocationImposter.UseBaseImplementation();
                    return this;
                }

                ISelectFirstMethodInvocationImposterGroup<TFirst, TSecond> ISelectFirstMethodInvocationImposterGroupContinuation<TFirst, TSecond>.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void SelectFirstInvocationVerifier<TFirst, TSecond>.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _selectFirstMethodInvocationHistoryCollection.Count<TFirst, TSecond>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public ClassWithOverloadsAndGenericsImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._formatMethodImposter = new FormatMethodImposter(_formatMethodInvocationHistoryCollection, invocationBehavior);
            this._format_1MethodImposter = new Format_1MethodImposter(_format_1MethodInvocationHistoryCollection, invocationBehavior);
            this._echoMethodImposterCollection = new EchoMethodImposterCollection(_echoMethodInvocationHistoryCollection, invocationBehavior);
            this._selectFirstMethodImposterCollection = new SelectFirstMethodImposterCollection(_selectFirstMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance();
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.ClassImposter.Suts.ClassWithOverloadsAndGenerics
        {
            ClassWithOverloadsAndGenericsImposter _imposter;
            internal void InitializeImposter(ClassWithOverloadsAndGenericsImposter imposter)
            {
                _imposter = imposter;
            }

            internal ImposterTargetInstance() : base()
            {
            }

            public override string Format(int value)
            {
                return _imposter._formatMethodImposter.Invoke(value, base.Format);
            }

            public override string Format(string value, int padding)
            {
                return _imposter._format_1MethodImposter.Invoke(value, padding, base.Format);
            }

            public override T Echo<T>(T item)
                where T : class
            {
                return _imposter._echoMethodImposterCollection.GetImposterWithMatchingSetup<T>(new EchoArguments<T>(item)).Invoke(item, base.Echo);
            }

            public override TFirst SelectFirst<TFirst, TSecond>(TFirst first, TSecond second)
            {
                return _imposter._selectFirstMethodImposterCollection.GetImposterWithMatchingSetup<TFirst, TSecond>(new SelectFirstArguments<TFirst, TSecond>(first, second)).Invoke(first, second, base.SelectFirst);
            }
        }
    }
}
#pragma warning restore nullable
