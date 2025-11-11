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
    public class IMethodGenericExceptionTypeParameterCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodGenericExceptionTypeParameterCollisionTarget>
    {
        private readonly ThrowsGenericMethodImposterCollection _throwsGenericMethodImposterCollection;
        private readonly ThrowsGenericMethodInvocationHistoryCollection _throwsGenericMethodInvocationHistoryCollection = new ThrowsGenericMethodInvocationHistoryCollection();
        public IThrowsGenericMethodImposterBuilder<TException> ThrowsGeneric<TException>()
        {
            return new ThrowsGenericMethodImposter<TException>.Builder(_throwsGenericMethodImposterCollection, _throwsGenericMethodInvocationHistoryCollection);
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodGenericExceptionTypeParameterCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodGenericExceptionTypeParameterCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // void IMethodGenericExceptionTypeParameterCollisionTarget.ThrowsGeneric<TException>()
        public delegate void ThrowsGenericDelegate<TException>();
        // void IMethodGenericExceptionTypeParameterCollisionTarget.ThrowsGeneric<TException>()
        public delegate void ThrowsGenericCallbackDelegate<TException>();
        // void IMethodGenericExceptionTypeParameterCollisionTarget.ThrowsGeneric<TException>()
        public delegate System.Exception ThrowsGenericExceptionGeneratorDelegate<TException>();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThrowsGenericMethodInvocationHistory
        {
            bool Matches<TExceptionTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThrowsGenericMethodInvocationHistory<TException> : IThrowsGenericMethodInvocationHistory
        {
            internal System.Exception Exception;
            public ThrowsGenericMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches<TExceptionTarget>()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThrowsGenericMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IThrowsGenericMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IThrowsGenericMethodInvocationHistory>();
            internal void Add(IThrowsGenericMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count<TException>()
            {
                return _invocationHistory.Count(it => it.Matches<TException>());
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThrowsGenericMethodImposterCollection
        {
            private readonly ThrowsGenericMethodInvocationHistoryCollection _throwsGenericMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ThrowsGenericMethodImposterCollection(ThrowsGenericMethodInvocationHistoryCollection _throwsGenericMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._throwsGenericMethodInvocationHistoryCollection = _throwsGenericMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            private readonly System.Collections.Concurrent.ConcurrentStack<IThrowsGenericMethodImposter> _imposters = new System.Collections.Concurrent.ConcurrentStack<IThrowsGenericMethodImposter>();
            internal ThrowsGenericMethodImposter<TException> AddNew<TException>()
            {
                var imposter = new ThrowsGenericMethodImposter<TException>(_throwsGenericMethodInvocationHistoryCollection, _invocationBehavior);
                _imposters.Push(imposter);
                return imposter;
            }

            internal IThrowsGenericMethodImposter<TException> GetImposterWithMatchingSetup<TException>()
            {
                return _imposters.Select(it => it.As<TException>()).Where(it => it != null).Select(it => it!).FirstOrDefault(it => it.HasMatchingSetup()) ?? AddNew<TException>();
            }
        }

        // void IMethodGenericExceptionTypeParameterCollisionTarget.ThrowsGeneric<TException>()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ThrowsGenericMethodInvocationImposterGroup<TException>
        {
            internal static ThrowsGenericMethodInvocationImposterGroup<TException> Default = new ThrowsGenericMethodInvocationImposterGroup<TException>();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ThrowsGenericMethodInvocationImposterGroup()
            {
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName);
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

                private ThrowsGenericDelegate<TException> _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ThrowsGenericCallbackDelegate<TException>> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ThrowsGenericCallbackDelegate<TException>>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke();
                    foreach (var callback in _callbacks)
                    {
                        callback();
                    }
                }

                internal void Callback(ThrowsGenericCallbackDelegate<TException> callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(ThrowsGenericExceptionGeneratorDelegate<TException> exceptionGenerator)
                {
                    _resultGenerator = () =>
                    {
                        throw exceptionGenerator();
                    };
                }

                internal static void DefaultResultGenerator()
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThrowsGenericMethodInvocationImposterGroupCallback<TException>
        {
            IThrowsGenericMethodInvocationImposterGroupContinuation<TException> Callback(ThrowsGenericCallbackDelegate<TException> callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThrowsGenericMethodInvocationImposterGroupContinuation<TException> : IThrowsGenericMethodInvocationImposterGroupCallback<TException>
        {
            IThrowsGenericMethodInvocationImposterGroup<TException> Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThrowsGenericMethodInvocationImposterGroup<TException> : IThrowsGenericMethodInvocationImposterGroupCallback<TException>
        {
            IThrowsGenericMethodInvocationImposterGroupContinuation<TException> Throws<TException>()
                where TException : System.Exception, new();
            IThrowsGenericMethodInvocationImposterGroupContinuation<TException> Throws(System.Exception exception);
            IThrowsGenericMethodInvocationImposterGroupContinuation<TException> Throws(ThrowsGenericExceptionGeneratorDelegate<TException> exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IThrowsGenericMethodImposter
        {
            IThrowsGenericMethodImposter<TExceptionTarget>? As<TExceptionTarget>();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal interface IThrowsGenericMethodImposter<TException> : IThrowsGenericMethodImposter
        {
            void Invoke();
            bool HasMatchingSetup();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ThrowsGenericInvocationVerifier<TException>
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodGenericExceptionTypeParameterCollisionTarget.ThrowsGeneric<TException>()
        public interface IThrowsGenericMethodImposterBuilder<TException> : IThrowsGenericMethodInvocationImposterGroup<TException>, IThrowsGenericMethodInvocationImposterGroupCallback<TException>, ThrowsGenericInvocationVerifier<TException>
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThrowsGenericMethodImposter<TException> : IThrowsGenericMethodImposter<TException>
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ThrowsGenericMethodInvocationImposterGroup<TException>> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ThrowsGenericMethodInvocationImposterGroup<TException>>();
            private readonly ThrowsGenericMethodInvocationHistoryCollection _throwsGenericMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ThrowsGenericMethodImposter(ThrowsGenericMethodInvocationHistoryCollection _throwsGenericMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._throwsGenericMethodInvocationHistoryCollection = _throwsGenericMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            IThrowsGenericMethodImposter<TExceptionTarget>? IThrowsGenericMethodImposter.As<TExceptionTarget>()
            {
                if (true)
                {
                    return new Adapter<TExceptionTarget>(this);
                }

                return null;
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            private class Adapter<TExceptionTarget> : IThrowsGenericMethodImposter<TExceptionTarget>
            {
                private readonly ThrowsGenericMethodImposter<TException> _target;
                public Adapter(ThrowsGenericMethodImposter<TException> target)
                {
                    _target = target;
                }

                public void Invoke()
                {
                    _target.Invoke();
                }

                public bool HasMatchingSetup()
                {
                    return _target.HasMatchingSetup();
                }

                IThrowsGenericMethodImposter<TExceptionTarget1>? IThrowsGenericMethodImposter.As<TExceptionTarget1>()
                {
                    throw new NotImplementedException();
                }
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private ThrowsGenericMethodInvocationImposterGroup<TException>? FindMatchingInvocationImposterGroup()
            {
                if (_invocationImposters.TryPeek(out var invocationImposterGroup))
                    return invocationImposterGroup;
                else
                    return null;
            }

            public void Invoke()
            {
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup();
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodGenericExceptionTypeParameterCollisionTarget.ThrowsGeneric<TException>()");
                    }

                    matchingInvocationImposterGroup = ThrowsGenericMethodInvocationImposterGroup<TException>.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodGenericExceptionTypeParameterCollisionTarget.ThrowsGeneric<TException>()");
                    _throwsGenericMethodInvocationHistoryCollection.Add(new ThrowsGenericMethodInvocationHistory<TException>(default));
                }
                catch (System.Exception ex)
                {
                    _throwsGenericMethodInvocationHistoryCollection.Add(new ThrowsGenericMethodInvocationHistory<TException>(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IThrowsGenericMethodImposterBuilder<TException>, IThrowsGenericMethodInvocationImposterGroupContinuation<TException>
            {
                private readonly ThrowsGenericMethodImposterCollection _imposterCollection;
                private readonly ThrowsGenericMethodInvocationHistoryCollection _throwsGenericMethodInvocationHistoryCollection;
                private readonly ThrowsGenericMethodInvocationImposterGroup<TException> _invocationImposterGroup;
                private ThrowsGenericMethodInvocationImposterGroup<TException>.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ThrowsGenericMethodImposterCollection _imposterCollection, ThrowsGenericMethodInvocationHistoryCollection _throwsGenericMethodInvocationHistoryCollection)
                {
                    this._imposterCollection = _imposterCollection;
                    this._throwsGenericMethodInvocationHistoryCollection = _throwsGenericMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new ThrowsGenericMethodInvocationImposterGroup<TException>();
                    ThrowsGenericMethodImposter<TException> methodImposter = _imposterCollection.AddNew<TException>();
                    methodImposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IThrowsGenericMethodInvocationImposterGroupContinuation<TException> IThrowsGenericMethodInvocationImposterGroup<TException>.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IThrowsGenericMethodInvocationImposterGroupContinuation<TException> IThrowsGenericMethodInvocationImposterGroup<TException>.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IThrowsGenericMethodInvocationImposterGroupContinuation<TException> IThrowsGenericMethodInvocationImposterGroup<TException>.Throws(ThrowsGenericExceptionGeneratorDelegate<TException> exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IThrowsGenericMethodInvocationImposterGroupContinuation<TException> IThrowsGenericMethodInvocationImposterGroupCallback<TException>.Callback(ThrowsGenericCallbackDelegate<TException> callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IThrowsGenericMethodInvocationImposterGroup<TException> IThrowsGenericMethodInvocationImposterGroupContinuation<TException>.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ThrowsGenericInvocationVerifier<TException>.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _throwsGenericMethodInvocationHistoryCollection.Count<TException>();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodGenericExceptionTypeParameterCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._throwsGenericMethodImposterCollection = new ThrowsGenericMethodImposterCollection(_throwsGenericMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodGenericExceptionTypeParameterCollisionTarget
        {
            IMethodGenericExceptionTypeParameterCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodGenericExceptionTypeParameterCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public void ThrowsGeneric<TException>()
                where TException : global::System.Exception
            {
                _imposter._throwsGenericMethodImposterCollection.GetImposterWithMatchingSetup<TException>().Invoke();
            }
        }
    }
}
#pragma warning restore nullable
