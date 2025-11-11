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
    public class IMethodGenericTypeParameterCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodGenericTypeParameterCollisionTarget>
    {
        private readonly GenericMethodMethodImposterCollection _genericMethodMethodImposterCollection;
        private readonly GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection = new GenericMethodMethodInvocationHistoryCollection();
        public IGenericMethodMethodImposterBuilder<T, U, TTarget, UTarget> GenericMethod<T, U, TTarget, UTarget>(global::Imposter.Abstractions.Arg<T> value, global::Imposter.Abstractions.Arg<U> other, global::Imposter.Abstractions.Arg<TTarget> target, global::Imposter.Abstractions.Arg<UTarget> otherTarget)
        {
            return new GenericMethodMethodImposter<T, U, TTarget, UTarget>.Builder(_genericMethodMethodImposterCollection, _genericMethodMethodInvocationHistoryCollection, new GenericMethodArgumentsCriteria<T, U, TTarget, UTarget>(value, other, target, otherTarget));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodGenericTypeParameterCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodGenericTypeParameterCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // void IMethodGenericTypeParameterCollisionTarget.GenericMethod<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget)
        public delegate void GenericMethodDelegate<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget);
        // void IMethodGenericTypeParameterCollisionTarget.GenericMethod<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget)
        public delegate void GenericMethodCallbackDelegate<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget);
        // void IMethodGenericTypeParameterCollisionTarget.GenericMethod<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget)
        public delegate System.Exception GenericMethodExceptionGeneratorDelegate<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericMethodArguments<T, U, TTarget, UTarget>
        {
            public T value;
            public U other;
            public TTarget target;
            public UTarget otherTarget;
            internal GenericMethodArguments(T value, U other, TTarget target, UTarget otherTarget)
            {
                this.value = value;
                this.other = other;
                this.target = target;
                this.otherTarget = otherTarget;
            }

            public GenericMethodArguments<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget> As<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>()
            {
                return new GenericMethodArguments<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>(TypeCaster.Cast<T, TTarget_1>(value), TypeCaster.Cast<U, UTarget_1>(other), TypeCaster.Cast<TTarget, TTargetTarget>(target), TypeCaster.Cast<UTarget, UTargetTarget>(otherTarget));
            }
        }

        // void IMethodGenericTypeParameterCollisionTarget.GenericMethod<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class GenericMethodArgumentsCriteria<T, U, TTarget, UTarget>
        {
            public global::Imposter.Abstractions.Arg<T> value { get; }
            public global::Imposter.Abstractions.Arg<U> other { get; }
            public global::Imposter.Abstractions.Arg<TTarget> target { get; }
            public global::Imposter.Abstractions.Arg<UTarget> otherTarget { get; }

            public GenericMethodArgumentsCriteria(global::Imposter.Abstractions.Arg<T> value, global::Imposter.Abstractions.Arg<U> other, global::Imposter.Abstractions.Arg<TTarget> target, global::Imposter.Abstractions.Arg<UTarget> otherTarget)
            {
                this.value = value;
                this.other = other;
                this.target = target;
                this.otherTarget = otherTarget;
            }

            public bool Matches(GenericMethodArguments<T, U, TTarget, UTarget> arguments)
            {
                return ((value.Matches(arguments.value) && other.Matches(arguments.other)) && target.Matches(arguments.target)) && otherTarget.Matches(arguments.otherTarget);
            }

            public GenericMethodArgumentsCriteria<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget> As<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>()
            {
                return new GenericMethodArgumentsCriteria<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>(global::Imposter.Abstractions.Arg<TTarget_1>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<TTarget_1, T>(it, out T valueTarget) && value.Matches(valueTarget)), global::Imposter.Abstractions.Arg<UTarget_1>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<UTarget_1, U>(it, out U otherTarget) && other.Matches(otherTarget)), global::Imposter.Abstractions.Arg<TTargetTarget>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<TTargetTarget, TTarget>(it, out TTarget targetTarget) && target.Matches(targetTarget)), global::Imposter.Abstractions.Arg<UTargetTarget>.Is(it => global::Imposter.Abstractions.TypeCaster.TryCast<UTargetTarget, UTarget>(it, out UTarget otherTargetTarget) && otherTarget.Matches(otherTargetTarget)));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericMethodMethodInvocationHistory
        {
            bool Matches<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>(GenericMethodArgumentsCriteria<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget> criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericMethodMethodInvocationHistory<T, U, TTarget, UTarget> : IGenericMethodMethodInvocationHistory
        {
            internal GenericMethodArguments<T, U, TTarget, UTarget> Arguments;
            internal System.Exception Exception;
            public GenericMethodMethodInvocationHistory(GenericMethodArguments<T, U, TTarget, UTarget> Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>(GenericMethodArgumentsCriteria<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget> criteria)
            {
                return ((((typeof(TTarget_1) == typeof(T)) && (typeof(UTarget_1) == typeof(U))) && (typeof(TTargetTarget) == typeof(TTarget))) && (typeof(UTargetTarget) == typeof(UTarget))) && criteria.As<T, U, TTarget, UTarget>().Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericMethodMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericMethodMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IGenericMethodMethodInvocationHistory>();
            internal void Add(IGenericMethodMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<T, U, TTarget, UTarget>(GenericMethodArgumentsCriteria<T, U, TTarget, UTarget> argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches<T, U, TTarget, UTarget>(argumentsCriteria));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericMethodMethodImposterCollection
        {
            private readonly GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public GenericMethodMethodImposterCollection(GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._genericMethodMethodInvocationHistoryCollection = _genericMethodMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IGenericMethodMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IGenericMethodMethodImposter>();
            internal GenericMethodMethodImposter<T, U, TTarget, UTarget> AddNew<T, U, TTarget, UTarget>()
            {
                var imposter = new GenericMethodMethodImposter<T, U, TTarget, UTarget>(_genericMethodMethodInvocationHistoryCollection, _invocationBehavior);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IGenericMethodMethodImposter<T, U, TTarget, UTarget> GetImposterWithMatchingSetup<T, U, TTarget, UTarget>(GenericMethodArguments<T, U, TTarget, UTarget> arguments)
            {
                return _imposters.Select(it => it.As<T, U, TTarget, UTarget>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup(arguments)) ?? AddNew<T, U, TTarget, UTarget>();
            }
        }

        // void IMethodGenericTypeParameterCollisionTarget.GenericMethod<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class GenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>
        {
            internal static GenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget> Default = new GenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>(new GenericMethodArgumentsCriteria<T, U, TTarget, UTarget>(global::Imposter.Abstractions.Arg<T>.Any(), global::Imposter.Abstractions.Arg<U>.Any(), global::Imposter.Abstractions.Arg<TTarget>.Any(), global::Imposter.Abstractions.Arg<UTarget>.Any()));
            internal GenericMethodArgumentsCriteria<T, U, TTarget, UTarget> ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public GenericMethodMethodInvocationImposterGroup(GenericMethodArgumentsCriteria<T, U, TTarget, UTarget> argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, T value, U other, TTarget target, UTarget otherTarget)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, value, other, target, otherTarget);
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class MethodInvocationImposter
            {
                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default._resultGenerator = DefaultResultGenerator;
                }

                private GenericMethodDelegate<T, U, TTarget, UTarget> _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<GenericMethodCallbackDelegate<T, U, TTarget, UTarget>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<GenericMethodCallbackDelegate<T, U, TTarget, UTarget>>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, T value, U other, TTarget target, UTarget otherTarget)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(value, other, target, otherTarget);
                    foreach (var callback in _callbacks)
                    {
                        callback(value, other, target, otherTarget);
                    }
                }

                internal void Callback(GenericMethodCallbackDelegate<T, U, TTarget, UTarget> callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(GenericMethodExceptionGeneratorDelegate<T, U, TTarget, UTarget> exceptionGenerator)
                {
                    _resultGenerator = (T value, U other, TTarget target, UTarget otherTarget) =>
                    {
                        throw exceptionGenerator(value, other, target, otherTarget);
                    };
                }

                internal static void DefaultResultGenerator(T value, U other, TTarget target, UTarget otherTarget)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericMethodMethodInvocationImposterGroupCallback<T, U, TTarget, UTarget>
        {
            IGenericMethodMethodInvocationImposterGroupContinuation<T, U, TTarget, UTarget> Callback(GenericMethodCallbackDelegate<T, U, TTarget, UTarget> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericMethodMethodInvocationImposterGroupContinuation<T, U, TTarget, UTarget> : IGenericMethodMethodInvocationImposterGroupCallback<T, U, TTarget, UTarget>
        {
            IGenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget> Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IGenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget> : IGenericMethodMethodInvocationImposterGroupCallback<T, U, TTarget, UTarget>
        {
            IGenericMethodMethodInvocationImposterGroupContinuation<T, U, TTarget, UTarget> Throws<TException>()
                where TException : System.Exception, new();
            IGenericMethodMethodInvocationImposterGroupContinuation<T, U, TTarget, UTarget> Throws(System.Exception exception);
            IGenericMethodMethodInvocationImposterGroupContinuation<T, U, TTarget, UTarget> Throws(GenericMethodExceptionGeneratorDelegate<T, U, TTarget, UTarget> exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericMethodMethodImposter
        {
            IGenericMethodMethodImposter<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>? As<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IGenericMethodMethodImposter<T, U, TTarget, UTarget> : IGenericMethodMethodImposter
        {
            void Invoke(T value, U other, TTarget target, UTarget otherTarget);
            bool HasMatchingSetup(GenericMethodArguments<T, U, TTarget, UTarget> arguments);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface GenericMethodInvocationVerifier<T, U, TTarget, UTarget>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodGenericTypeParameterCollisionTarget.GenericMethod<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget)
        public interface IGenericMethodMethodImposterBuilder<T, U, TTarget, UTarget> : IGenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>, IGenericMethodMethodInvocationImposterGroupCallback<T, U, TTarget, UTarget>, GenericMethodInvocationVerifier<T, U, TTarget, UTarget>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class GenericMethodMethodImposter<T, U, TTarget, UTarget> : IGenericMethodMethodImposter<T, U, TTarget, UTarget>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<GenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<GenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>>();
            private readonly GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public GenericMethodMethodImposter(GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._genericMethodMethodInvocationHistoryCollection = _genericMethodMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            IGenericMethodMethodImposter<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>? IGenericMethodMethodImposter.As<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>()
            {
                if (((typeof(TTarget_1).IsAssignableTo(typeof(T)) && typeof(UTarget_1).IsAssignableTo(typeof(U))) && typeof(TTargetTarget).IsAssignableTo(typeof(TTarget))) && typeof(UTargetTarget).IsAssignableTo(typeof(UTarget)))
                {
                    return new Adapter<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>(this);
                }

                return null;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private class Adapter<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget> : IGenericMethodMethodImposter<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget>
            {
                private readonly GenericMethodMethodImposter<T, U, TTarget, UTarget> _target;
                public Adapter(GenericMethodMethodImposter<T, U, TTarget, UTarget> target_1)
                {
                    _target = target_1;
                }

                public void Invoke(TTarget_1 value, UTarget_1 other, TTargetTarget target, UTargetTarget otherTarget)
                {
                    _target.Invoke(global::Imposter.Abstractions.TypeCaster.Cast<TTarget_1, T>(value), global::Imposter.Abstractions.TypeCaster.Cast<UTarget_1, U>(other), global::Imposter.Abstractions.TypeCaster.Cast<TTargetTarget, TTarget>(target), global::Imposter.Abstractions.TypeCaster.Cast<UTargetTarget, UTarget>(otherTarget));
                }

                public bool HasMatchingSetup(GenericMethodArguments<TTarget_1, UTarget_1, TTargetTarget, UTargetTarget> arguments)
                {
                    return _target.HasMatchingSetup(arguments.As<T, U, TTarget, UTarget>());
                }

                IGenericMethodMethodImposter<TTarget1, UTarget1, TTargetTarget1, UTargetTarget1>? IGenericMethodMethodImposter.As<TTarget1, UTarget1, TTargetTarget1, UTargetTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup(GenericMethodArguments<T, U, TTarget, UTarget> arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private GenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>? FindMatchingInvocationImposterGroup(GenericMethodArguments<T, U, TTarget, UTarget> arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(T value, U other, TTarget target, UTarget otherTarget)
            {
                var arguments = new GenericMethodArguments<T, U, TTarget, UTarget>(value, other, target, otherTarget);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodGenericTypeParameterCollisionTarget.GenericMethod<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget)");
                    }

                    matchingInvocationImposterGroup = GenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodGenericTypeParameterCollisionTarget.GenericMethod<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget)", value, other, target, otherTarget);
                    _genericMethodMethodInvocationHistoryCollection.Add(new GenericMethodMethodInvocationHistory<T, U, TTarget, UTarget>(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _genericMethodMethodInvocationHistoryCollection.Add(new GenericMethodMethodInvocationHistory<T, U, TTarget, UTarget>(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IGenericMethodMethodImposterBuilder<T, U, TTarget, UTarget>, IGenericMethodMethodInvocationImposterGroupContinuation<T, U, TTarget, UTarget>
            {
                private readonly GenericMethodMethodImposterCollection _imposterCollection;
                private readonly GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection;
                private readonly GenericMethodArgumentsCriteria<T, U, TTarget, UTarget> _argumentsCriteria;
                private readonly GenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget> _invocationImposterGroup;
                private GenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>.MethodInvocationImposter _currentInvocationImposter;
                public Builder(GenericMethodMethodImposterCollection _imposterCollection, GenericMethodMethodInvocationHistoryCollection _genericMethodMethodInvocationHistoryCollection, GenericMethodArgumentsCriteria<T, U, TTarget, UTarget> _argumentsCriteria)
                {
                    this._imposterCollection = _imposterCollection;
                    this._genericMethodMethodInvocationHistoryCollection = _genericMethodMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new GenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>(_argumentsCriteria);
                    GenericMethodMethodImposter<T, U, TTarget, UTarget> methodImposter = _imposterCollection.AddNew<T, U, TTarget, UTarget>();
                    methodImposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IGenericMethodMethodInvocationImposterGroupContinuation<T, U, TTarget, UTarget> IGenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((T value, U other, TTarget target, UTarget otherTarget) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IGenericMethodMethodInvocationImposterGroupContinuation<T, U, TTarget, UTarget> IGenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((T value, U other, TTarget target, UTarget otherTarget) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IGenericMethodMethodInvocationImposterGroupContinuation<T, U, TTarget, UTarget> IGenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget>.Throws(GenericMethodExceptionGeneratorDelegate<T, U, TTarget, UTarget> exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((T value, U other, TTarget target, UTarget otherTarget) =>
                    {
                        throw exceptionGenerator.Invoke(value, other, target, otherTarget);
                    });
                    return this;
                }

                IGenericMethodMethodInvocationImposterGroupContinuation<T, U, TTarget, UTarget> IGenericMethodMethodInvocationImposterGroupCallback<T, U, TTarget, UTarget>.Callback(GenericMethodCallbackDelegate<T, U, TTarget, UTarget> callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IGenericMethodMethodInvocationImposterGroup<T, U, TTarget, UTarget> IGenericMethodMethodInvocationImposterGroupContinuation<T, U, TTarget, UTarget>.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void GenericMethodInvocationVerifier<T, U, TTarget, UTarget>.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _genericMethodMethodInvocationHistoryCollection.Count<T, U, TTarget, UTarget>(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodGenericTypeParameterCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._genericMethodMethodImposterCollection = new GenericMethodMethodImposterCollection(_genericMethodMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodGenericTypeParameterCollisionTarget
        {
            IMethodGenericTypeParameterCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodGenericTypeParameterCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public void GenericMethod<T, U, TTarget, UTarget>(T value, U other, TTarget target, UTarget otherTarget)
            {
                _imposter._genericMethodMethodImposterCollection.GetImposterWithMatchingSetup<T, U, TTarget, UTarget>(new GenericMethodArguments<T, U, TTarget, UTarget>(value, other, target, otherTarget)).Invoke(value, other, target, otherTarget);
            }
        }
    }
}
#pragma warning restore nullable
