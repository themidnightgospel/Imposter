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
    public class IMethodImposterMemberCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodImposterMemberCollisionTarget>
    {
        private readonly InstanceMethodImposter _instanceMethodImposter;
        private readonly ImposterTargetInstanceMethodImposter _imposterTargetInstanceMethodImposter;
        private readonly _imposterInstanceMethodImposter __imposterInstanceMethodImposter;
        private readonly _invocationBehaviorMethodImposter __invocationBehaviorMethodImposter;
        private readonly InitializeOutParametersWithDefaultValuesMethodImposter _initializeOutParametersWithDefaultValuesMethodImposter;
        private readonly InstanceMethodInvocationHistoryCollection _instanceMethodInvocationHistoryCollection = new InstanceMethodInvocationHistoryCollection();
        private readonly ImposterTargetInstanceMethodInvocationHistoryCollection _imposterTargetInstanceMethodInvocationHistoryCollection = new ImposterTargetInstanceMethodInvocationHistoryCollection();
        private readonly _imposterInstanceMethodInvocationHistoryCollection __imposterInstanceMethodInvocationHistoryCollection = new _imposterInstanceMethodInvocationHistoryCollection();
        private readonly _invocationBehaviorMethodInvocationHistoryCollection __invocationBehaviorMethodInvocationHistoryCollection = new _invocationBehaviorMethodInvocationHistoryCollection();
        private readonly InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection = new InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection();
        public IInstanceMethodImposterBuilder Instance()
        {
            return new InstanceMethodImposter.Builder(_instanceMethodImposter, _instanceMethodInvocationHistoryCollection);
        }

        public IImposterTargetInstanceMethodImposterBuilder ImposterTargetInstance()
        {
            return new ImposterTargetInstanceMethodImposter.Builder(_imposterTargetInstanceMethodImposter, _imposterTargetInstanceMethodInvocationHistoryCollection);
        }

        public I_imposterInstanceMethodImposterBuilder _imposterInstance()
        {
            return new _imposterInstanceMethodImposter.Builder(__imposterInstanceMethodImposter, __imposterInstanceMethodInvocationHistoryCollection);
        }

        public I_invocationBehaviorMethodImposterBuilder _invocationBehavior()
        {
            return new _invocationBehaviorMethodImposter.Builder(__invocationBehaviorMethodImposter, __invocationBehaviorMethodInvocationHistoryCollection);
        }

        public IInitializeOutParametersWithDefaultValuesMethodImposterBuilder InitializeOutParametersWithDefaultValues(global::Imposter.Abstractions.Arg<int> seed, global::Imposter.Abstractions.OutArg<int> doubled)
        {
            return new InitializeOutParametersWithDefaultValuesMethodImposter.Builder(_initializeOutParametersWithDefaultValuesMethodImposter, _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection, new InitializeOutParametersWithDefaultValuesArgumentsCriteria(seed, doubled));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior_1;
        private ImposterTargetInstance_1 _imposterInstance_1;
        global::Imposter.Playground.IMethodImposterMemberCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodImposterMemberCollisionTarget>.Instance()
        {
            return _imposterInstance_1;
        }

        // void IMethodImposterMemberCollisionTarget.ImposterTargetInstance()
        public delegate void ImposterTargetInstanceDelegate();
        // void IMethodImposterMemberCollisionTarget.ImposterTargetInstance()
        public delegate void ImposterTargetInstanceCallbackDelegate();
        // void IMethodImposterMemberCollisionTarget.ImposterTargetInstance()
        public delegate System.Exception ImposterTargetInstanceExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstanceMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ImposterTargetInstanceMethodInvocationHistory : IImposterTargetInstanceMethodInvocationHistory
        {
            internal System.Exception Exception;
            public ImposterTargetInstanceMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ImposterTargetInstanceMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IImposterTargetInstanceMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IImposterTargetInstanceMethodInvocationHistory>();
            internal void Add(IImposterTargetInstanceMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodImposterMemberCollisionTarget.ImposterTargetInstance()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstanceMethodInvocationImposterGroup
        {
            internal static ImposterTargetInstanceMethodInvocationImposterGroup Default = new ImposterTargetInstanceMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ImposterTargetInstanceMethodInvocationImposterGroup()
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

                private ImposterTargetInstanceDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ImposterTargetInstanceCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ImposterTargetInstanceCallbackDelegate>();
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

                internal void Callback(ImposterTargetInstanceCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(ImposterTargetInstanceExceptionGeneratorDelegate exceptionGenerator)
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
        public interface IImposterTargetInstanceMethodInvocationImposterGroupCallback
        {
            IImposterTargetInstanceMethodInvocationImposterGroupContinuation Callback(ImposterTargetInstanceCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstanceMethodInvocationImposterGroupContinuation : IImposterTargetInstanceMethodInvocationImposterGroupCallback
        {
            IImposterTargetInstanceMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IImposterTargetInstanceMethodInvocationImposterGroup : IImposterTargetInstanceMethodInvocationImposterGroupCallback
        {
            IImposterTargetInstanceMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IImposterTargetInstanceMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IImposterTargetInstanceMethodInvocationImposterGroupContinuation Throws(ImposterTargetInstanceExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ImposterTargetInstanceInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodImposterMemberCollisionTarget.ImposterTargetInstance()
        public interface IImposterTargetInstanceMethodImposterBuilder : IImposterTargetInstanceMethodInvocationImposterGroup, IImposterTargetInstanceMethodInvocationImposterGroupCallback, ImposterTargetInstanceInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ImposterTargetInstanceMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ImposterTargetInstanceMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ImposterTargetInstanceMethodInvocationImposterGroup>();
            private readonly ImposterTargetInstanceMethodInvocationHistoryCollection _imposterTargetInstanceMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ImposterTargetInstanceMethodImposter(ImposterTargetInstanceMethodInvocationHistoryCollection _imposterTargetInstanceMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._imposterTargetInstanceMethodInvocationHistoryCollection = _imposterTargetInstanceMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private ImposterTargetInstanceMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodImposterMemberCollisionTarget.ImposterTargetInstance()");
                    }

                    matchingInvocationImposterGroup = ImposterTargetInstanceMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodImposterMemberCollisionTarget.ImposterTargetInstance()");
                    _imposterTargetInstanceMethodInvocationHistoryCollection.Add(new ImposterTargetInstanceMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    _imposterTargetInstanceMethodInvocationHistoryCollection.Add(new ImposterTargetInstanceMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IImposterTargetInstanceMethodImposterBuilder, IImposterTargetInstanceMethodInvocationImposterGroupContinuation
            {
                private readonly ImposterTargetInstanceMethodImposter _imposter;
                private readonly ImposterTargetInstanceMethodInvocationHistoryCollection _imposterTargetInstanceMethodInvocationHistoryCollection;
                private readonly ImposterTargetInstanceMethodInvocationImposterGroup _invocationImposterGroup;
                private ImposterTargetInstanceMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ImposterTargetInstanceMethodImposter _imposter, ImposterTargetInstanceMethodInvocationHistoryCollection _imposterTargetInstanceMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._imposterTargetInstanceMethodInvocationHistoryCollection = _imposterTargetInstanceMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new ImposterTargetInstanceMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IImposterTargetInstanceMethodInvocationImposterGroupContinuation IImposterTargetInstanceMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IImposterTargetInstanceMethodInvocationImposterGroupContinuation IImposterTargetInstanceMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IImposterTargetInstanceMethodInvocationImposterGroupContinuation IImposterTargetInstanceMethodInvocationImposterGroup.Throws(ImposterTargetInstanceExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IImposterTargetInstanceMethodInvocationImposterGroupContinuation IImposterTargetInstanceMethodInvocationImposterGroupCallback.Callback(ImposterTargetInstanceCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IImposterTargetInstanceMethodInvocationImposterGroup IImposterTargetInstanceMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ImposterTargetInstanceInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _imposterTargetInstanceMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodImposterMemberCollisionTarget.InitializeOutParametersWithDefaultValues(ref int seed, out int doubled)
        public delegate void InitializeOutParametersWithDefaultValuesDelegate(ref int seed, out int doubled);
        // void IMethodImposterMemberCollisionTarget.InitializeOutParametersWithDefaultValues(ref int seed, out int doubled)
        public delegate void InitializeOutParametersWithDefaultValuesCallbackDelegate(ref int seed, out int doubled);
        // void IMethodImposterMemberCollisionTarget.InitializeOutParametersWithDefaultValues(ref int seed, out int doubled)
        public delegate System.Exception InitializeOutParametersWithDefaultValuesExceptionGeneratorDelegate(ref int seed, out int doubled);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class InitializeOutParametersWithDefaultValuesArguments
        {
            public int seed;
            internal InitializeOutParametersWithDefaultValuesArguments(int seed)
            {
                this.seed = seed;
            }
        }

        // void IMethodImposterMemberCollisionTarget.InitializeOutParametersWithDefaultValues(ref int seed, out int doubled)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class InitializeOutParametersWithDefaultValuesArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> seed { get; }
            public global::Imposter.Abstractions.OutArg<int> doubled { get; }

            public InitializeOutParametersWithDefaultValuesArgumentsCriteria(global::Imposter.Abstractions.Arg<int> seed, global::Imposter.Abstractions.OutArg<int> doubled)
            {
                this.seed = seed;
                this.doubled = doubled;
            }

            public bool Matches(InitializeOutParametersWithDefaultValuesArguments arguments)
            {
                return seed.Matches(arguments.seed);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesMethodInvocationHistory
        {
            bool Matches(InitializeOutParametersWithDefaultValuesArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InitializeOutParametersWithDefaultValuesMethodInvocationHistory : IInitializeOutParametersWithDefaultValuesMethodInvocationHistory
        {
            internal InitializeOutParametersWithDefaultValuesArguments Arguments;
            internal System.Exception Exception;
            public InitializeOutParametersWithDefaultValuesMethodInvocationHistory(InitializeOutParametersWithDefaultValuesArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(InitializeOutParametersWithDefaultValuesArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IInitializeOutParametersWithDefaultValuesMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IInitializeOutParametersWithDefaultValuesMethodInvocationHistory>();
            internal void Add(IInitializeOutParametersWithDefaultValuesMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(InitializeOutParametersWithDefaultValuesArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodImposterMemberCollisionTarget.InitializeOutParametersWithDefaultValues(ref int seed, out int doubled)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup
        {
            internal static InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup Default = new InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup(new InitializeOutParametersWithDefaultValuesArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any(), global::Imposter.Abstractions.OutArg<int>.Any()));
            internal InitializeOutParametersWithDefaultValuesArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup(InitializeOutParametersWithDefaultValuesArgumentsCriteria argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, ref int seed, out int doubled)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, ref seed, out doubled);
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

                private InitializeOutParametersWithDefaultValuesDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<InitializeOutParametersWithDefaultValuesCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<InitializeOutParametersWithDefaultValuesCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, ref int seed, out int doubled)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(ref seed, out doubled);
                    foreach (var callback in _callbacks)
                    {
                        callback(ref seed, out doubled);
                    }
                }

                internal void Callback(InitializeOutParametersWithDefaultValuesCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(InitializeOutParametersWithDefaultValuesExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (ref int seed, out int doubled) =>
                    {
                        throw exceptionGenerator(ref seed, out doubled);
                    };
                }

                private static void InitializeOutParametersWithDefaultValues(out int doubled)
                {
                    doubled = default(int);
                }

                internal static void DefaultResultGenerator(ref int seed, out int doubled)
                {
                    InitializeOutParametersWithDefaultValues(out doubled);
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupCallback
        {
            IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupContinuation Callback(InitializeOutParametersWithDefaultValuesCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupContinuation : IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupCallback
        {
            IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup : IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupCallback
        {
            IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupContinuation Throws(InitializeOutParametersWithDefaultValuesExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface InitializeOutParametersWithDefaultValuesInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodImposterMemberCollisionTarget.InitializeOutParametersWithDefaultValues(ref int seed, out int doubled)
        public interface IInitializeOutParametersWithDefaultValuesMethodImposterBuilder : IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup, IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupCallback, InitializeOutParametersWithDefaultValuesInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InitializeOutParametersWithDefaultValuesMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup>();
            private readonly InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public InitializeOutParametersWithDefaultValuesMethodImposter(InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection = _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            private static void InitializeOutParametersWithDefaultValues(out int doubled)
            {
                doubled = default(int);
            }

            public bool HasMatchingSetup(InitializeOutParametersWithDefaultValuesArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(InitializeOutParametersWithDefaultValuesArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(ref int seed, out int doubled)
            {
                var arguments = new InitializeOutParametersWithDefaultValuesArguments(seed);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodImposterMemberCollisionTarget.InitializeOutParametersWithDefaultValues(ref int seed, out int doubled)");
                    }

                    matchingInvocationImposterGroup = InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodImposterMemberCollisionTarget.InitializeOutParametersWithDefaultValues(ref int seed, out int doubled)", ref seed, out doubled);
                    _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection.Add(new InitializeOutParametersWithDefaultValuesMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection.Add(new InitializeOutParametersWithDefaultValuesMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInitializeOutParametersWithDefaultValuesMethodImposterBuilder, IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupContinuation
            {
                private readonly InitializeOutParametersWithDefaultValuesMethodImposter _imposter;
                private readonly InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection;
                private readonly InitializeOutParametersWithDefaultValuesArgumentsCriteria _argumentsCriteria;
                private readonly InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup _invocationImposterGroup;
                private InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(InitializeOutParametersWithDefaultValuesMethodImposter _imposter, InitializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection, InitializeOutParametersWithDefaultValuesArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection = _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new InitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupContinuation IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((ref int seed, out int doubled) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupContinuation IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((ref int seed, out int doubled) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupContinuation IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup.Throws(InitializeOutParametersWithDefaultValuesExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((ref int seed, out int doubled) =>
                    {
                        throw exceptionGenerator.Invoke(ref seed, out doubled);
                    });
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupContinuation IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupCallback.Callback(InitializeOutParametersWithDefaultValuesCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroup IInitializeOutParametersWithDefaultValuesMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void InitializeOutParametersWithDefaultValuesInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodImposterMemberCollisionTarget.Instance()
        public delegate void InstanceDelegate();
        // void IMethodImposterMemberCollisionTarget.Instance()
        public delegate void InstanceCallbackDelegate();
        // void IMethodImposterMemberCollisionTarget.Instance()
        public delegate System.Exception InstanceExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstanceMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InstanceMethodInvocationHistory : IInstanceMethodInvocationHistory
        {
            internal System.Exception Exception;
            public InstanceMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InstanceMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IInstanceMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IInstanceMethodInvocationHistory>();
            internal void Add(IInstanceMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodImposterMemberCollisionTarget.Instance()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class InstanceMethodInvocationImposterGroup
        {
            internal static InstanceMethodInvocationImposterGroup Default = new InstanceMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public InstanceMethodInvocationImposterGroup()
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

                private InstanceDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<InstanceCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<InstanceCallbackDelegate>();
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

                internal void Callback(InstanceCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(InstanceExceptionGeneratorDelegate exceptionGenerator)
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
        public interface IInstanceMethodInvocationImposterGroupCallback
        {
            IInstanceMethodInvocationImposterGroupContinuation Callback(InstanceCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstanceMethodInvocationImposterGroupContinuation : IInstanceMethodInvocationImposterGroupCallback
        {
            IInstanceMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInstanceMethodInvocationImposterGroup : IInstanceMethodInvocationImposterGroupCallback
        {
            IInstanceMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IInstanceMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IInstanceMethodInvocationImposterGroupContinuation Throws(InstanceExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface InstanceInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodImposterMemberCollisionTarget.Instance()
        public interface IInstanceMethodImposterBuilder : IInstanceMethodInvocationImposterGroup, IInstanceMethodInvocationImposterGroupCallback, InstanceInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InstanceMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<InstanceMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<InstanceMethodInvocationImposterGroup>();
            private readonly InstanceMethodInvocationHistoryCollection _instanceMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public InstanceMethodImposter(InstanceMethodInvocationHistoryCollection _instanceMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._instanceMethodInvocationHistoryCollection = _instanceMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private InstanceMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodImposterMemberCollisionTarget.Instance()");
                    }

                    matchingInvocationImposterGroup = InstanceMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodImposterMemberCollisionTarget.Instance()");
                    _instanceMethodInvocationHistoryCollection.Add(new InstanceMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    _instanceMethodInvocationHistoryCollection.Add(new InstanceMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInstanceMethodImposterBuilder, IInstanceMethodInvocationImposterGroupContinuation
            {
                private readonly InstanceMethodImposter _imposter;
                private readonly InstanceMethodInvocationHistoryCollection _instanceMethodInvocationHistoryCollection;
                private readonly InstanceMethodInvocationImposterGroup _invocationImposterGroup;
                private InstanceMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(InstanceMethodImposter _imposter, InstanceMethodInvocationHistoryCollection _instanceMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._instanceMethodInvocationHistoryCollection = _instanceMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new InstanceMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IInstanceMethodInvocationImposterGroupContinuation IInstanceMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IInstanceMethodInvocationImposterGroupContinuation IInstanceMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IInstanceMethodInvocationImposterGroupContinuation IInstanceMethodInvocationImposterGroup.Throws(InstanceExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IInstanceMethodInvocationImposterGroupContinuation IInstanceMethodInvocationImposterGroupCallback.Callback(InstanceCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IInstanceMethodInvocationImposterGroup IInstanceMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void InstanceInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _instanceMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodImposterMemberCollisionTarget._imposterInstance()
        public delegate void _imposterInstanceDelegate();
        // void IMethodImposterMemberCollisionTarget._imposterInstance()
        public delegate void _imposterInstanceCallbackDelegate();
        // void IMethodImposterMemberCollisionTarget._imposterInstance()
        public delegate System.Exception _imposterInstanceExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstanceMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _imposterInstanceMethodInvocationHistory : I_imposterInstanceMethodInvocationHistory
        {
            internal System.Exception Exception;
            public _imposterInstanceMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _imposterInstanceMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<I_imposterInstanceMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<I_imposterInstanceMethodInvocationHistory>();
            internal void Add(I_imposterInstanceMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodImposterMemberCollisionTarget._imposterInstance()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class _imposterInstanceMethodInvocationImposterGroup
        {
            internal static _imposterInstanceMethodInvocationImposterGroup Default = new _imposterInstanceMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public _imposterInstanceMethodInvocationImposterGroup()
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

                private _imposterInstanceDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<_imposterInstanceCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<_imposterInstanceCallbackDelegate>();
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

                internal void Callback(_imposterInstanceCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(_imposterInstanceExceptionGeneratorDelegate exceptionGenerator)
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
        public interface I_imposterInstanceMethodInvocationImposterGroupCallback
        {
            I_imposterInstanceMethodInvocationImposterGroupContinuation Callback(_imposterInstanceCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstanceMethodInvocationImposterGroupContinuation : I_imposterInstanceMethodInvocationImposterGroupCallback
        {
            I_imposterInstanceMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_imposterInstanceMethodInvocationImposterGroup : I_imposterInstanceMethodInvocationImposterGroupCallback
        {
            I_imposterInstanceMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            I_imposterInstanceMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            I_imposterInstanceMethodInvocationImposterGroupContinuation Throws(_imposterInstanceExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface _imposterInstanceInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodImposterMemberCollisionTarget._imposterInstance()
        public interface I_imposterInstanceMethodImposterBuilder : I_imposterInstanceMethodInvocationImposterGroup, I_imposterInstanceMethodInvocationImposterGroupCallback, _imposterInstanceInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _imposterInstanceMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<_imposterInstanceMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<_imposterInstanceMethodInvocationImposterGroup>();
            private readonly _imposterInstanceMethodInvocationHistoryCollection __imposterInstanceMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public _imposterInstanceMethodImposter(_imposterInstanceMethodInvocationHistoryCollection __imposterInstanceMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this.__imposterInstanceMethodInvocationHistoryCollection = __imposterInstanceMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private _imposterInstanceMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodImposterMemberCollisionTarget._imposterInstance()");
                    }

                    matchingInvocationImposterGroup = _imposterInstanceMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodImposterMemberCollisionTarget._imposterInstance()");
                    __imposterInstanceMethodInvocationHistoryCollection.Add(new _imposterInstanceMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    __imposterInstanceMethodInvocationHistoryCollection.Add(new _imposterInstanceMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : I_imposterInstanceMethodImposterBuilder, I_imposterInstanceMethodInvocationImposterGroupContinuation
            {
                private readonly _imposterInstanceMethodImposter _imposter;
                private readonly _imposterInstanceMethodInvocationHistoryCollection __imposterInstanceMethodInvocationHistoryCollection;
                private readonly _imposterInstanceMethodInvocationImposterGroup _invocationImposterGroup;
                private _imposterInstanceMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(_imposterInstanceMethodImposter _imposter, _imposterInstanceMethodInvocationHistoryCollection __imposterInstanceMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this.__imposterInstanceMethodInvocationHistoryCollection = __imposterInstanceMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new _imposterInstanceMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                I_imposterInstanceMethodInvocationImposterGroupContinuation I_imposterInstanceMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                I_imposterInstanceMethodInvocationImposterGroupContinuation I_imposterInstanceMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                I_imposterInstanceMethodInvocationImposterGroupContinuation I_imposterInstanceMethodInvocationImposterGroup.Throws(_imposterInstanceExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                I_imposterInstanceMethodInvocationImposterGroupContinuation I_imposterInstanceMethodInvocationImposterGroupCallback.Callback(_imposterInstanceCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                I_imposterInstanceMethodInvocationImposterGroup I_imposterInstanceMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void _imposterInstanceInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = __imposterInstanceMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodImposterMemberCollisionTarget._invocationBehavior()
        public delegate void _invocationBehaviorDelegate();
        // void IMethodImposterMemberCollisionTarget._invocationBehavior()
        public delegate void _invocationBehaviorCallbackDelegate();
        // void IMethodImposterMemberCollisionTarget._invocationBehavior()
        public delegate System.Exception _invocationBehaviorExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _invocationBehaviorMethodInvocationHistory : I_invocationBehaviorMethodInvocationHistory
        {
            internal System.Exception Exception;
            public _invocationBehaviorMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _invocationBehaviorMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<I_invocationBehaviorMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<I_invocationBehaviorMethodInvocationHistory>();
            internal void Add(I_invocationBehaviorMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodImposterMemberCollisionTarget._invocationBehavior()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class _invocationBehaviorMethodInvocationImposterGroup
        {
            internal static _invocationBehaviorMethodInvocationImposterGroup Default = new _invocationBehaviorMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public _invocationBehaviorMethodInvocationImposterGroup()
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

                private _invocationBehaviorDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<_invocationBehaviorCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<_invocationBehaviorCallbackDelegate>();
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

                internal void Callback(_invocationBehaviorCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(_invocationBehaviorExceptionGeneratorDelegate exceptionGenerator)
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
        public interface I_invocationBehaviorMethodInvocationImposterGroupCallback
        {
            I_invocationBehaviorMethodInvocationImposterGroupContinuation Callback(_invocationBehaviorCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorMethodInvocationImposterGroupContinuation : I_invocationBehaviorMethodInvocationImposterGroupCallback
        {
            I_invocationBehaviorMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface I_invocationBehaviorMethodInvocationImposterGroup : I_invocationBehaviorMethodInvocationImposterGroupCallback
        {
            I_invocationBehaviorMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            I_invocationBehaviorMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            I_invocationBehaviorMethodInvocationImposterGroupContinuation Throws(_invocationBehaviorExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface _invocationBehaviorInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodImposterMemberCollisionTarget._invocationBehavior()
        public interface I_invocationBehaviorMethodImposterBuilder : I_invocationBehaviorMethodInvocationImposterGroup, I_invocationBehaviorMethodInvocationImposterGroupCallback, _invocationBehaviorInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class _invocationBehaviorMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<_invocationBehaviorMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<_invocationBehaviorMethodInvocationImposterGroup>();
            private readonly _invocationBehaviorMethodInvocationHistoryCollection __invocationBehaviorMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public _invocationBehaviorMethodImposter(_invocationBehaviorMethodInvocationHistoryCollection __invocationBehaviorMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this.__invocationBehaviorMethodInvocationHistoryCollection = __invocationBehaviorMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private _invocationBehaviorMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodImposterMemberCollisionTarget._invocationBehavior()");
                    }

                    matchingInvocationImposterGroup = _invocationBehaviorMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodImposterMemberCollisionTarget._invocationBehavior()");
                    __invocationBehaviorMethodInvocationHistoryCollection.Add(new _invocationBehaviorMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    __invocationBehaviorMethodInvocationHistoryCollection.Add(new _invocationBehaviorMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : I_invocationBehaviorMethodImposterBuilder, I_invocationBehaviorMethodInvocationImposterGroupContinuation
            {
                private readonly _invocationBehaviorMethodImposter _imposter;
                private readonly _invocationBehaviorMethodInvocationHistoryCollection __invocationBehaviorMethodInvocationHistoryCollection;
                private readonly _invocationBehaviorMethodInvocationImposterGroup _invocationImposterGroup;
                private _invocationBehaviorMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(_invocationBehaviorMethodImposter _imposter, _invocationBehaviorMethodInvocationHistoryCollection __invocationBehaviorMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this.__invocationBehaviorMethodInvocationHistoryCollection = __invocationBehaviorMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new _invocationBehaviorMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                I_invocationBehaviorMethodInvocationImposterGroupContinuation I_invocationBehaviorMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                I_invocationBehaviorMethodInvocationImposterGroupContinuation I_invocationBehaviorMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                I_invocationBehaviorMethodInvocationImposterGroupContinuation I_invocationBehaviorMethodInvocationImposterGroup.Throws(_invocationBehaviorExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                I_invocationBehaviorMethodInvocationImposterGroupContinuation I_invocationBehaviorMethodInvocationImposterGroupCallback.Callback(_invocationBehaviorCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                I_invocationBehaviorMethodInvocationImposterGroup I_invocationBehaviorMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void _invocationBehaviorInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = __invocationBehaviorMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodImposterMemberCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._instanceMethodImposter = new InstanceMethodImposter(_instanceMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterTargetInstanceMethodImposter = new ImposterTargetInstanceMethodImposter(_imposterTargetInstanceMethodInvocationHistoryCollection, invocationBehavior);
            this.__imposterInstanceMethodImposter = new _imposterInstanceMethodImposter(__imposterInstanceMethodInvocationHistoryCollection, invocationBehavior);
            this.__invocationBehaviorMethodImposter = new _invocationBehaviorMethodImposter(__invocationBehaviorMethodInvocationHistoryCollection, invocationBehavior);
            this._initializeOutParametersWithDefaultValuesMethodImposter = new InitializeOutParametersWithDefaultValuesMethodImposter(_initializeOutParametersWithDefaultValuesMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance_1 = new ImposterTargetInstance_1(this);
            this._invocationBehavior_1 = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance_1 : global::Imposter.Playground.IMethodImposterMemberCollisionTarget
        {
            IMethodImposterMemberCollisionTargetImposter _imposter;
            public ImposterTargetInstance_1(IMethodImposterMemberCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public void Instance()
            {
                _imposter._instanceMethodImposter.Invoke();
            }

            public void ImposterTargetInstance()
            {
                _imposter._imposterTargetInstanceMethodImposter.Invoke();
            }

            public void _imposterInstance()
            {
                _imposter.__imposterInstanceMethodImposter.Invoke();
            }

            public void _invocationBehavior()
            {
                _imposter.__invocationBehaviorMethodImposter.Invoke();
            }

            public void InitializeOutParametersWithDefaultValues(ref int seed, out int doubled)
            {
                _imposter._initializeOutParametersWithDefaultValuesMethodImposter.Invoke(ref seed, out doubled);
            }
        }
    }
}
#pragma warning restore nullable
