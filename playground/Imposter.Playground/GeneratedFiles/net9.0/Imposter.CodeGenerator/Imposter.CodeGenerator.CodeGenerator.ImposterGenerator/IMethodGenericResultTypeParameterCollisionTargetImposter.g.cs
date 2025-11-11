#pragma warning disable nullable
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using global::Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Playground;

namespace Imposter.Playground
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IMethodGenericResultTypeParameterCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodGenericResultTypeParameterCollisionTarget>
    {
        private readonly ReturnsGenericMethodImposterCollection _returnsGenericMethodImposterCollection;
        private readonly ReturnsWithGeneratorMethodImposterCollection _returnsWithGeneratorMethodImposterCollection;
        private readonly ReturnsGenericMethodInvocationHistoryCollection _returnsGenericMethodInvocationHistoryCollection = new ReturnsGenericMethodInvocationHistoryCollection();
        private readonly ReturnsWithGeneratorMethodInvocationHistoryCollection _returnsWithGeneratorMethodInvocationHistoryCollection = new ReturnsWithGeneratorMethodInvocationHistoryCollection();
        public IReturnsGenericMethodImposterBuilder<TResult> ReturnsGeneric<TResult>(global::Imposter.Abstractions.Arg<TResult> value)
        {
            return new ReturnsGenericMethodImposter<TResult>.Builder(_returnsGenericMethodImposterCollection, _returnsGenericMethodInvocationHistoryCollection, new ReturnsGenericArgumentsCriteria<TResult>(value));
        }

        public IReturnsWithGeneratorMethodImposterBuilder<TResult> ReturnsWithGenerator<TResult>(global::Imposter.Abstractions.Arg<global::System.Func<TResult>> defaultResultGenerator)
        {
            return new ReturnsWithGeneratorMethodImposter<TResult>.Builder(_returnsWithGeneratorMethodImposterCollection, _returnsWithGeneratorMethodInvocationHistoryCollection, new ReturnsWithGeneratorArgumentsCriteria<TResult>(defaultResultGenerator));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodGenericResultTypeParameterCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodGenericResultTypeParameterCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsGeneric<TResult>(TResult value)
        public delegate TResult ReturnsGenericDelegate<TResult>(TResult value);
        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsGeneric<TResult>(TResult value)
        public delegate void ReturnsGenericCallbackDelegate<TResult>(TResult value);
        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsGeneric<TResult>(TResult value)
        public delegate System.Exception ReturnsGenericExceptionGeneratorDelegate<TResult>(TResult value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ReturnsGenericArguments<TResult>
        {
            public TResult value;
            internal ReturnsGenericArguments(TResult value)
            {
                this.value = value;
            }

            public ReturnsGenericArguments<TResultTarget> As<TResultTarget>()
            {
                return new ReturnsGenericArguments<TResultTarget>(TypeCaster.Cast<TResult, TResultTarget>(value));
            }
        }

        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsGeneric<TResult>(TResult value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ReturnsGenericArgumentsCriteria<TResult>
        {
            public global::Imposter.Abstractions.Arg<TResult> value { get; }

            public ReturnsGenericArgumentsCriteria(global::Imposter.Abstractions.Arg<TResult> value)
            {
                this.value = value;
            }

            public bool Matches(ReturnsGenericArguments<TResult> arguments)
            {
                return value.Matches(arguments.value);
            }

            public ReturnsGenericArgumentsCriteria<TResultTarget> As<TResultTarget>()
            {
                return new ReturnsGenericArgumentsCriteria<TResultTarget>(global::Imposter.Abstractions.Arg<TResultTarget>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<TResultTarget, TResult>(it, out TResult valueTarget) && value.Matches(valueTarget)));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsGenericMethodInvocationHistory
        {
            bool Matches<TResultTarget>(ReturnsGenericArgumentsCriteria<TResultTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsGenericMethodInvocationHistory<TResult> : IReturnsGenericMethodInvocationHistory
        {
            internal ReturnsGenericArguments<TResult> Arguments;
            internal TResult Result;
            internal System.Exception Exception;
            public ReturnsGenericMethodInvocationHistory(ReturnsGenericArguments<TResult> Arguments, TResult Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TResultTarget>(ReturnsGenericArgumentsCriteria<TResultTarget> criteria)
            {
                return ((typeof(TResultTarget) == typeof(TResult)) && (typeof(TResult) == typeof(TResultTarget))) && criteria.As<TResult>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsGenericMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IReturnsGenericMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IReturnsGenericMethodInvocationHistory>();
            internal void Add(IReturnsGenericMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TResult>(ReturnsGenericArgumentsCriteria<TResult> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TResult>(argumentsCriteria));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsGenericMethodImposterCollection
        {
            private readonly ReturnsGenericMethodInvocationHistoryCollection _returnsGenericMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ReturnsGenericMethodImposterCollection(ReturnsGenericMethodInvocationHistoryCollection _returnsGenericMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._returnsGenericMethodInvocationHistoryCollection = _returnsGenericMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IReturnsGenericMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IReturnsGenericMethodImposter>();
            internal ReturnsGenericMethodImposter<TResult> AddNew<TResult>()
            {
                var imposter = new ReturnsGenericMethodImposter<TResult>(_returnsGenericMethodInvocationHistoryCollection, _invocationBehavior);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IReturnsGenericMethodImposter<TResult> GetImposterWithMatchingSetup<TResult>(ReturnsGenericArguments<TResult> arguments)
            {
                return _imposters.Select(it => it.As<TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TResult>();
            }
        }

        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsGeneric<TResult>(TResult value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ReturnsGenericMethodInvocationImposterGroup<TResult>
        {
            internal static ReturnsGenericMethodInvocationImposterGroup<TResult> Default = new ReturnsGenericMethodInvocationImposterGroup<TResult>(new ReturnsGenericArgumentsCriteria<TResult>(global::Imposter.Abstractions.Arg<TResult>.Any()));
            internal ReturnsGenericArgumentsCriteria<TResult> ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ReturnsGenericMethodInvocationImposterGroup(ReturnsGenericArgumentsCriteria<TResult> argumentsCriteria)
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
                }

                return _lastestInvocationImposter;
            }

            public TResult Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, TResult value)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, value);
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

                private ReturnsGenericDelegate<TResult> _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ReturnsGenericCallbackDelegate<TResult>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ReturnsGenericCallbackDelegate<TResult>>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public TResult Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, TResult value)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    TResult result = _resultGenerator.Invoke(value);
                    foreach (var callback in _callbacks)
                    {
                        callback(value);
                    }

                    return result;
                }

                internal void Callback(ReturnsGenericCallbackDelegate<TResult> callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(ReturnsGenericDelegate<TResult> resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(TResult value_1)
                {
                    _resultGenerator = (TResult value) =>
                    {
                        return value_1;
                    };
                }

                internal void Throws(ReturnsGenericExceptionGeneratorDelegate<TResult> exceptionGenerator)
                {
                    _resultGenerator = (TResult value) =>
                    {
                        throw exceptionGenerator(value);
                    };
                }

                internal static TResult DefaultResultGenerator(TResult value)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsGenericMethodInvocationImposterGroupCallback<TResult>
        {
            IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> Callback(ReturnsGenericCallbackDelegate<TResult> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> : IReturnsGenericMethodInvocationImposterGroupCallback<TResult>
        {
            IReturnsGenericMethodInvocationImposterGroup<TResult> Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsGenericMethodInvocationImposterGroup<TResult> : IReturnsGenericMethodInvocationImposterGroupCallback<TResult>
        {
            IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> Throws<TException>()
                where TException : Exception, new();
            IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> Throws(System.Exception exception);
            IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> Throws(ReturnsGenericExceptionGeneratorDelegate<TResult> exceptionGenerator);
            IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> Returns(ReturnsGenericDelegate<TResult> resultGenerator);
            IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> Returns(TResult value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IReturnsGenericMethodImposter
        {
            IReturnsGenericMethodImposter<TResultTarget>? As<TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IReturnsGenericMethodImposter<TResult> : IReturnsGenericMethodImposter
        {
            TResult Invoke(TResult value);
            bool HasMatchingSetup(ReturnsGenericArguments<TResult> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ReturnsGenericInvocationVerifier<TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsGeneric<TResult>(TResult value)
        public interface IReturnsGenericMethodImposterBuilder<TResult> : IReturnsGenericMethodInvocationImposterGroup<TResult>, IReturnsGenericMethodInvocationImposterGroupCallback<TResult>, ReturnsGenericInvocationVerifier<TResult>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsGenericMethodImposter<TResult> : IReturnsGenericMethodImposter<TResult>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ReturnsGenericMethodInvocationImposterGroup<TResult>> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ReturnsGenericMethodInvocationImposterGroup<TResult>>();
            private readonly ReturnsGenericMethodInvocationHistoryCollection _returnsGenericMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ReturnsGenericMethodImposter(ReturnsGenericMethodInvocationHistoryCollection _returnsGenericMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._returnsGenericMethodInvocationHistoryCollection = _returnsGenericMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            IReturnsGenericMethodImposter<TResultTarget>? IReturnsGenericMethodImposter.As<TResultTarget>()
            {
                if (typeof(TResultTarget).IsAssignableTo(typeof(TResult)) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TResultTarget>(this);
                }

                return null;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private class Adapter<TResultTarget> : IReturnsGenericMethodImposter<TResultTarget>
            {
                private readonly ReturnsGenericMethodImposter<TResult> _target;
                public Adapter(ReturnsGenericMethodImposter<TResult> target)
                {
                    _target = target;
                }

                public TResultTarget Invoke(TResultTarget value)
                {
                    var result = _target.Invoke(global::Imposter.Abstractions.TypeCaster.Cast<TResultTarget, TResult>(value));
                    return global::Imposter.Abstractions.TypeCaster.Cast<TResult, TResultTarget>(result);
                }

                public bool HasMatchingSetup(ReturnsGenericArguments<TResultTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TResult>());
                }

                IReturnsGenericMethodImposter<TResultTarget1>? IReturnsGenericMethodImposter.As<TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(ReturnsGenericArguments<TResult> arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private ReturnsGenericMethodInvocationImposterGroup<TResult>? FindMatchingInvocationImposterGroup(ReturnsGenericArguments<TResult> arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public TResult Invoke(TResult value)
            {
                var arguments = new ReturnsGenericArguments<TResult>(value);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsGeneric<TResult>(TResult value)");
                    }

                    matchingInvocationImposterGroup = ReturnsGenericMethodInvocationImposterGroup<TResult>.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsGeneric<TResult>(TResult value)", value);
                    _returnsGenericMethodInvocationHistoryCollection.Add(new ReturnsGenericMethodInvocationHistory<TResult>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _returnsGenericMethodInvocationHistoryCollection.Add(new ReturnsGenericMethodInvocationHistory<TResult>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IReturnsGenericMethodImposterBuilder<TResult>, IReturnsGenericMethodInvocationImposterGroupContinuation<TResult>
            {
                private readonly ReturnsGenericMethodImposterCollection _imposterCollection;
                private readonly ReturnsGenericMethodInvocationHistoryCollection _returnsGenericMethodInvocationHistoryCollection;
                private readonly ReturnsGenericArgumentsCriteria<TResult> _argumentsCriteria;
                private readonly ReturnsGenericMethodInvocationImposterGroup<TResult> _invocationImposterGroup;
                private ReturnsGenericMethodInvocationImposterGroup<TResult>.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ReturnsGenericMethodImposterCollection _imposterCollection, ReturnsGenericMethodInvocationHistoryCollection _returnsGenericMethodInvocationHistoryCollection, ReturnsGenericArgumentsCriteria<TResult> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._returnsGenericMethodInvocationHistoryCollection = _returnsGenericMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new ReturnsGenericMethodInvocationImposterGroup<TResult>(_argumentsCriteria);
                    ReturnsGenericMethodImposter<TResult> methodImposter = _imposterCollection.AddNew<TResult>();
                    methodImposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> IReturnsGenericMethodInvocationImposterGroup<TResult>.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((TResult value) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> IReturnsGenericMethodInvocationImposterGroup<TResult>.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((TResult value) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> IReturnsGenericMethodInvocationImposterGroup<TResult>.Throws(ReturnsGenericExceptionGeneratorDelegate<TResult> exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((TResult value) =>
                    {
                        throw exceptionGenerator.Invoke(value);
                    });
                    return this;
                }

                IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> IReturnsGenericMethodInvocationImposterGroupCallback<TResult>.Callback(ReturnsGenericCallbackDelegate<TResult> callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> IReturnsGenericMethodInvocationImposterGroup<TResult>.Returns(ReturnsGenericDelegate<TResult> resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IReturnsGenericMethodInvocationImposterGroupContinuation<TResult> IReturnsGenericMethodInvocationImposterGroup<TResult>.Returns(TResult value_1)
                {
                    _currentInvocationImposter.Returns(value_1);
                    return this;
                }

                IReturnsGenericMethodInvocationImposterGroup<TResult> IReturnsGenericMethodInvocationImposterGroupContinuation<TResult>.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ReturnsGenericInvocationVerifier<TResult>.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _returnsGenericMethodInvocationHistoryCollection.Count<TResult>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsWithGenerator<TResult>(Func<TResult> defaultResultGenerator)
        public delegate TResult ReturnsWithGeneratorDelegate<TResult>(global::System.Func<TResult> defaultResultGenerator);
        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsWithGenerator<TResult>(Func<TResult> defaultResultGenerator)
        public delegate void ReturnsWithGeneratorCallbackDelegate<TResult>(global::System.Func<TResult> defaultResultGenerator);
        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsWithGenerator<TResult>(Func<TResult> defaultResultGenerator)
        public delegate System.Exception ReturnsWithGeneratorExceptionGeneratorDelegate<TResult>(global::System.Func<TResult> defaultResultGenerator);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ReturnsWithGeneratorArguments<TResult>
        {
            public global::System.Func<TResult> defaultResultGenerator;
            internal ReturnsWithGeneratorArguments(global::System.Func<TResult> defaultResultGenerator)
            {
                this.defaultResultGenerator = defaultResultGenerator;
            }

            public ReturnsWithGeneratorArguments<TResultTarget> As<TResultTarget>()
            {
                return new ReturnsWithGeneratorArguments<TResultTarget>(TypeCaster.Cast<global::System.Func<TResult>, global::System.Func<TResultTarget>>(defaultResultGenerator));
            }
        }

        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsWithGenerator<TResult>(Func<TResult> defaultResultGenerator)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ReturnsWithGeneratorArgumentsCriteria<TResult>
        {
            public global::Imposter.Abstractions.Arg<global::System.Func<TResult>> defaultResultGenerator { get; }

            public ReturnsWithGeneratorArgumentsCriteria(global::Imposter.Abstractions.Arg<global::System.Func<TResult>> defaultResultGenerator)
            {
                this.defaultResultGenerator = defaultResultGenerator;
            }

            public bool Matches(ReturnsWithGeneratorArguments<TResult> arguments)
            {
                return defaultResultGenerator.Matches(arguments.defaultResultGenerator);
            }

            public ReturnsWithGeneratorArgumentsCriteria<TResultTarget> As<TResultTarget>()
            {
                return new ReturnsWithGeneratorArgumentsCriteria<TResultTarget>(global::Imposter.Abstractions.Arg<global::System.Func<TResultTarget>>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<global::System.Func<TResultTarget>, global::System.Func<TResult>>(it, out global::System.Func<TResult> defaultResultGeneratorTarget) && defaultResultGenerator.Matches(defaultResultGeneratorTarget)));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsWithGeneratorMethodInvocationHistory
        {
            bool Matches<TResultTarget>(ReturnsWithGeneratorArgumentsCriteria<TResultTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsWithGeneratorMethodInvocationHistory<TResult> : IReturnsWithGeneratorMethodInvocationHistory
        {
            internal ReturnsWithGeneratorArguments<TResult> Arguments;
            internal TResult Result;
            internal System.Exception Exception;
            public ReturnsWithGeneratorMethodInvocationHistory(ReturnsWithGeneratorArguments<TResult> Arguments, TResult Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches<TResultTarget>(ReturnsWithGeneratorArgumentsCriteria<TResultTarget> criteria)
            {
                return ((typeof(global::System.Func<TResultTarget>) == typeof(global::System.Func<TResult>)) && (typeof(TResult) == typeof(TResultTarget))) && criteria.As<TResult>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsWithGeneratorMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IReturnsWithGeneratorMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IReturnsWithGeneratorMethodInvocationHistory>();
            internal void Add(IReturnsWithGeneratorMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TResult>(ReturnsWithGeneratorArgumentsCriteria<TResult> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<TResult>(argumentsCriteria));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsWithGeneratorMethodImposterCollection
        {
            private readonly ReturnsWithGeneratorMethodInvocationHistoryCollection _returnsWithGeneratorMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ReturnsWithGeneratorMethodImposterCollection(ReturnsWithGeneratorMethodInvocationHistoryCollection _returnsWithGeneratorMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._returnsWithGeneratorMethodInvocationHistoryCollection = _returnsWithGeneratorMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IReturnsWithGeneratorMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IReturnsWithGeneratorMethodImposter>();
            internal ReturnsWithGeneratorMethodImposter<TResult> AddNew<TResult>()
            {
                var imposter = new ReturnsWithGeneratorMethodImposter<TResult>(_returnsWithGeneratorMethodInvocationHistoryCollection, _invocationBehavior);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IReturnsWithGeneratorMethodImposter<TResult> GetImposterWithMatchingSetup<TResult>(ReturnsWithGeneratorArguments<TResult> arguments)
            {
                return _imposters.Select(it => it.As<TResult>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<TResult>();
            }
        }

        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsWithGenerator<TResult>(Func<TResult> defaultResultGenerator)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ReturnsWithGeneratorMethodInvocationImposterGroup<TResult>
        {
            internal static ReturnsWithGeneratorMethodInvocationImposterGroup<TResult> Default = new ReturnsWithGeneratorMethodInvocationImposterGroup<TResult>(new ReturnsWithGeneratorArgumentsCriteria<TResult>(global::Imposter.Abstractions.Arg<global::System.Func<TResult>>.Any()));
            internal ReturnsWithGeneratorArgumentsCriteria<TResult> ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ReturnsWithGeneratorMethodInvocationImposterGroup(ReturnsWithGeneratorArgumentsCriteria<TResult> argumentsCriteria)
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
                }

                return _lastestInvocationImposter;
            }

            public TResult Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, global::System.Func<TResult> defaultResultGenerator)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, defaultResultGenerator);
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

                private ReturnsWithGeneratorDelegate<TResult> _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ReturnsWithGeneratorCallbackDelegate<TResult>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ReturnsWithGeneratorCallbackDelegate<TResult>>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public TResult Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, global::System.Func<TResult> defaultResultGenerator)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    TResult result = _resultGenerator.Invoke(defaultResultGenerator);
                    foreach (var callback in _callbacks)
                    {
                        callback(defaultResultGenerator);
                    }

                    return result;
                }

                internal void Callback(ReturnsWithGeneratorCallbackDelegate<TResult> callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(ReturnsWithGeneratorDelegate<TResult> resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(TResult value)
                {
                    _resultGenerator = (global::System.Func<TResult> defaultResultGenerator) =>
                    {
                        return value;
                    };
                }

                internal void Throws(ReturnsWithGeneratorExceptionGeneratorDelegate<TResult> exceptionGenerator)
                {
                    _resultGenerator = (global::System.Func<TResult> defaultResultGenerator) =>
                    {
                        throw exceptionGenerator(defaultResultGenerator);
                    };
                }

                internal static TResult DefaultResultGenerator(global::System.Func<TResult> defaultResultGenerator)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsWithGeneratorMethodInvocationImposterGroupCallback<TResult>
        {
            IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> Callback(ReturnsWithGeneratorCallbackDelegate<TResult> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> : IReturnsWithGeneratorMethodInvocationImposterGroupCallback<TResult>
        {
            IReturnsWithGeneratorMethodInvocationImposterGroup<TResult> Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsWithGeneratorMethodInvocationImposterGroup<TResult> : IReturnsWithGeneratorMethodInvocationImposterGroupCallback<TResult>
        {
            IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> Throws<TException>()
                where TException : Exception, new();
            IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> Throws(System.Exception exception);
            IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> Throws(ReturnsWithGeneratorExceptionGeneratorDelegate<TResult> exceptionGenerator);
            IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> Returns(ReturnsWithGeneratorDelegate<TResult> resultGenerator);
            IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> Returns(TResult value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IReturnsWithGeneratorMethodImposter
        {
            IReturnsWithGeneratorMethodImposter<TResultTarget>? As<TResultTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IReturnsWithGeneratorMethodImposter<TResult> : IReturnsWithGeneratorMethodImposter
        {
            TResult Invoke(global::System.Func<TResult> defaultResultGenerator);
            bool HasMatchingSetup(ReturnsWithGeneratorArguments<TResult> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ReturnsWithGeneratorInvocationVerifier<TResult>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsWithGenerator<TResult>(Func<TResult> defaultResultGenerator)
        public interface IReturnsWithGeneratorMethodImposterBuilder<TResult> : IReturnsWithGeneratorMethodInvocationImposterGroup<TResult>, IReturnsWithGeneratorMethodInvocationImposterGroupCallback<TResult>, ReturnsWithGeneratorInvocationVerifier<TResult>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsWithGeneratorMethodImposter<TResult> : IReturnsWithGeneratorMethodImposter<TResult>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ReturnsWithGeneratorMethodInvocationImposterGroup<TResult>> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ReturnsWithGeneratorMethodInvocationImposterGroup<TResult>>();
            private readonly ReturnsWithGeneratorMethodInvocationHistoryCollection _returnsWithGeneratorMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ReturnsWithGeneratorMethodImposter(ReturnsWithGeneratorMethodInvocationHistoryCollection _returnsWithGeneratorMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._returnsWithGeneratorMethodInvocationHistoryCollection = _returnsWithGeneratorMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            IReturnsWithGeneratorMethodImposter<TResultTarget>? IReturnsWithGeneratorMethodImposter.As<TResultTarget>()
            {
                if (typeof(global::System.Func<TResultTarget>).IsAssignableTo(typeof(global::System.Func<TResult>)) && typeof(TResult).IsAssignableTo(typeof(TResultTarget)))
                {
                    return new Adapter<TResultTarget>(this);
                }

                return null;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private class Adapter<TResultTarget> : IReturnsWithGeneratorMethodImposter<TResultTarget>
            {
                private readonly ReturnsWithGeneratorMethodImposter<TResult> _target;
                public Adapter(ReturnsWithGeneratorMethodImposter<TResult> target)
                {
                    _target = target;
                }

                public TResultTarget Invoke(Func<TResultTarget> defaultResultGenerator)
                {
                    var result = _target.Invoke(global::Imposter.Abstractions.TypeCaster.Cast<global::System.Func<TResultTarget>, global::System.Func<TResult>>(defaultResultGenerator));
                    return global::Imposter.Abstractions.TypeCaster.Cast<TResult, TResultTarget>(result);
                }

                public bool HasMatchingSetup(ReturnsWithGeneratorArguments<TResultTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<TResult>());
                }

                IReturnsWithGeneratorMethodImposter<TResultTarget1>? IReturnsWithGeneratorMethodImposter.As<TResultTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(ReturnsWithGeneratorArguments<TResult> arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private ReturnsWithGeneratorMethodInvocationImposterGroup<TResult>? FindMatchingInvocationImposterGroup(ReturnsWithGeneratorArguments<TResult> arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public TResult Invoke(global::System.Func<TResult> defaultResultGenerator)
            {
                var arguments = new ReturnsWithGeneratorArguments<TResult>(defaultResultGenerator);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsWithGenerator<TResult>(Func<TResult> defaultResultGenerator)");
                    }

                    matchingInvocationImposterGroup = ReturnsWithGeneratorMethodInvocationImposterGroup<TResult>.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "TResult IMethodGenericResultTypeParameterCollisionTarget.ReturnsWithGenerator<TResult>(Func<TResult> defaultResultGenerator)", defaultResultGenerator);
                    _returnsWithGeneratorMethodInvocationHistoryCollection.Add(new ReturnsWithGeneratorMethodInvocationHistory<TResult>(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _returnsWithGeneratorMethodInvocationHistoryCollection.Add(new ReturnsWithGeneratorMethodInvocationHistory<TResult>(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IReturnsWithGeneratorMethodImposterBuilder<TResult>, IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult>
            {
                private readonly ReturnsWithGeneratorMethodImposterCollection _imposterCollection;
                private readonly ReturnsWithGeneratorMethodInvocationHistoryCollection _returnsWithGeneratorMethodInvocationHistoryCollection;
                private readonly ReturnsWithGeneratorArgumentsCriteria<TResult> _argumentsCriteria;
                private readonly ReturnsWithGeneratorMethodInvocationImposterGroup<TResult> _invocationImposterGroup;
                private ReturnsWithGeneratorMethodInvocationImposterGroup<TResult>.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ReturnsWithGeneratorMethodImposterCollection _imposterCollection, ReturnsWithGeneratorMethodInvocationHistoryCollection _returnsWithGeneratorMethodInvocationHistoryCollection, ReturnsWithGeneratorArgumentsCriteria<TResult> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._returnsWithGeneratorMethodInvocationHistoryCollection = _returnsWithGeneratorMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new ReturnsWithGeneratorMethodInvocationImposterGroup<TResult>(_argumentsCriteria);
                    ReturnsWithGeneratorMethodImposter<TResult> methodImposter = _imposterCollection.AddNew<TResult>();
                    methodImposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> IReturnsWithGeneratorMethodInvocationImposterGroup<TResult>.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((global::System.Func<TResult> defaultResultGenerator) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> IReturnsWithGeneratorMethodInvocationImposterGroup<TResult>.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((global::System.Func<TResult> defaultResultGenerator) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> IReturnsWithGeneratorMethodInvocationImposterGroup<TResult>.Throws(ReturnsWithGeneratorExceptionGeneratorDelegate<TResult> exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((global::System.Func<TResult> defaultResultGenerator) =>
                    {
                        throw exceptionGenerator.Invoke(defaultResultGenerator);
                    });
                    return this;
                }

                IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> IReturnsWithGeneratorMethodInvocationImposterGroupCallback<TResult>.Callback(ReturnsWithGeneratorCallbackDelegate<TResult> callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> IReturnsWithGeneratorMethodInvocationImposterGroup<TResult>.Returns(ReturnsWithGeneratorDelegate<TResult> resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult> IReturnsWithGeneratorMethodInvocationImposterGroup<TResult>.Returns(TResult value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IReturnsWithGeneratorMethodInvocationImposterGroup<TResult> IReturnsWithGeneratorMethodInvocationImposterGroupContinuation<TResult>.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ReturnsWithGeneratorInvocationVerifier<TResult>.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _returnsWithGeneratorMethodInvocationHistoryCollection.Count<TResult>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodGenericResultTypeParameterCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._returnsGenericMethodImposterCollection = new ReturnsGenericMethodImposterCollection(_returnsGenericMethodInvocationHistoryCollection, invocationBehavior);
            this._returnsWithGeneratorMethodImposterCollection = new ReturnsWithGeneratorMethodImposterCollection(_returnsWithGeneratorMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodGenericResultTypeParameterCollisionTarget
        {
            IMethodGenericResultTypeParameterCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodGenericResultTypeParameterCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public TResult ReturnsGeneric<TResult>(TResult value)
            {
                return _imposter._returnsGenericMethodImposterCollection.GetImposterWithMatchingSetup<TResult>(new ReturnsGenericArguments<TResult>(value)).Invoke(value);
            }

            public TResult ReturnsWithGenerator<TResult>(global::System.Func<TResult> defaultResultGenerator)
            {
                return _imposter._returnsWithGeneratorMethodImposterCollection.GetImposterWithMatchingSetup<TResult>(new ReturnsWithGeneratorArguments<TResult>(defaultResultGenerator)).Invoke(defaultResultGenerator);
            }
        }
    }
}
#pragma warning restore nullable
