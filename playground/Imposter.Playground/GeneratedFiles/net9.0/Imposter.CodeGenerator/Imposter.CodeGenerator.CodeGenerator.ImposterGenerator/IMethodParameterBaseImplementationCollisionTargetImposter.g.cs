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
    public class IMethodParameterBaseImplementationCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterBaseImplementationCollisionTarget>
    {
        private readonly BaseImplementationNameMethodImposter _baseImplementationNameMethodImposter;
        private readonly BaseImplementationNameMethodInvocationHistoryCollection _baseImplementationNameMethodInvocationHistoryCollection = new BaseImplementationNameMethodInvocationHistoryCollection();
        public IBaseImplementationNameMethodImposterBuilder BaseImplementationName(global::Imposter.Abstractions.Arg<int> baseImplementation)
        {
            return new BaseImplementationNameMethodImposter.Builder(_baseImplementationNameMethodImposter, _baseImplementationNameMethodInvocationHistoryCollection, new BaseImplementationNameArgumentsCriteria(baseImplementation));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodParameterBaseImplementationCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterBaseImplementationCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // int IMethodParameterBaseImplementationCollisionTarget.BaseImplementationName(int baseImplementation)
        public delegate int BaseImplementationNameDelegate(int baseImplementation);
        // int IMethodParameterBaseImplementationCollisionTarget.BaseImplementationName(int baseImplementation)
        public delegate void BaseImplementationNameCallbackDelegate(int baseImplementation);
        // int IMethodParameterBaseImplementationCollisionTarget.BaseImplementationName(int baseImplementation)
        public delegate System.Exception BaseImplementationNameExceptionGeneratorDelegate(int baseImplementation);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class BaseImplementationNameArguments
        {
            public int baseImplementation;
            internal BaseImplementationNameArguments(int baseImplementation)
            {
                this.baseImplementation = baseImplementation;
            }
        }

        // int IMethodParameterBaseImplementationCollisionTarget.BaseImplementationName(int baseImplementation)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class BaseImplementationNameArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> baseImplementation { get; }

            public BaseImplementationNameArgumentsCriteria(global::Imposter.Abstractions.Arg<int> baseImplementation)
            {
                this.baseImplementation = baseImplementation;
            }

            public bool Matches(BaseImplementationNameArguments arguments)
            {
                return baseImplementation.Matches(arguments.baseImplementation);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IBaseImplementationNameMethodInvocationHistory
        {
            bool Matches(BaseImplementationNameArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class BaseImplementationNameMethodInvocationHistory : IBaseImplementationNameMethodInvocationHistory
        {
            internal BaseImplementationNameArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public BaseImplementationNameMethodInvocationHistory(BaseImplementationNameArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(BaseImplementationNameArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class BaseImplementationNameMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IBaseImplementationNameMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IBaseImplementationNameMethodInvocationHistory>();
            internal void Add(IBaseImplementationNameMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(BaseImplementationNameArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodParameterBaseImplementationCollisionTarget.BaseImplementationName(int baseImplementation)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class BaseImplementationNameMethodInvocationImposterGroup
        {
            internal static BaseImplementationNameMethodInvocationImposterGroup Default = new BaseImplementationNameMethodInvocationImposterGroup(new BaseImplementationNameArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any()));
            internal BaseImplementationNameArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public BaseImplementationNameMethodInvocationImposterGroup(BaseImplementationNameArgumentsCriteria argumentsCriteria)
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

            public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int baseImplementation)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, baseImplementation);
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

                private BaseImplementationNameDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<BaseImplementationNameCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<BaseImplementationNameCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int baseImplementation)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke(baseImplementation);
                    foreach (var callback in _callbacks)
                    {
                        callback(baseImplementation);
                    }

                    return result;
                }

                internal void Callback(BaseImplementationNameCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(BaseImplementationNameDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (int baseImplementation) =>
                    {
                        return value;
                    };
                }

                internal void Throws(BaseImplementationNameExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int baseImplementation) =>
                    {
                        throw exceptionGenerator(baseImplementation);
                    };
                }

                internal static int DefaultResultGenerator(int baseImplementation)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IBaseImplementationNameMethodInvocationImposterGroupCallback
        {
            IBaseImplementationNameMethodInvocationImposterGroupContinuation Callback(BaseImplementationNameCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IBaseImplementationNameMethodInvocationImposterGroupContinuation : IBaseImplementationNameMethodInvocationImposterGroupCallback
        {
            IBaseImplementationNameMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IBaseImplementationNameMethodInvocationImposterGroup : IBaseImplementationNameMethodInvocationImposterGroupCallback
        {
            IBaseImplementationNameMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IBaseImplementationNameMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IBaseImplementationNameMethodInvocationImposterGroupContinuation Throws(BaseImplementationNameExceptionGeneratorDelegate exceptionGenerator);
            IBaseImplementationNameMethodInvocationImposterGroupContinuation Returns(BaseImplementationNameDelegate resultGenerator);
            IBaseImplementationNameMethodInvocationImposterGroupContinuation Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface BaseImplementationNameInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodParameterBaseImplementationCollisionTarget.BaseImplementationName(int baseImplementation)
        public interface IBaseImplementationNameMethodImposterBuilder : IBaseImplementationNameMethodInvocationImposterGroup, IBaseImplementationNameMethodInvocationImposterGroupCallback, BaseImplementationNameInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class BaseImplementationNameMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<BaseImplementationNameMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<BaseImplementationNameMethodInvocationImposterGroup>();
            private readonly BaseImplementationNameMethodInvocationHistoryCollection _baseImplementationNameMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public BaseImplementationNameMethodImposter(BaseImplementationNameMethodInvocationHistoryCollection _baseImplementationNameMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._baseImplementationNameMethodInvocationHistoryCollection = _baseImplementationNameMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(BaseImplementationNameArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private BaseImplementationNameMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(BaseImplementationNameArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(int baseImplementation)
            {
                var arguments = new BaseImplementationNameArguments(baseImplementation);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("int IMethodParameterBaseImplementationCollisionTarget.BaseImplementationName(int baseImplementation)");
                    }

                    matchingInvocationImposterGroup = BaseImplementationNameMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "int IMethodParameterBaseImplementationCollisionTarget.BaseImplementationName(int baseImplementation)", baseImplementation);
                    _baseImplementationNameMethodInvocationHistoryCollection.Add(new BaseImplementationNameMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _baseImplementationNameMethodInvocationHistoryCollection.Add(new BaseImplementationNameMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IBaseImplementationNameMethodImposterBuilder, IBaseImplementationNameMethodInvocationImposterGroupContinuation
            {
                private readonly BaseImplementationNameMethodImposter _imposter;
                private readonly BaseImplementationNameMethodInvocationHistoryCollection _baseImplementationNameMethodInvocationHistoryCollection;
                private readonly BaseImplementationNameArgumentsCriteria _argumentsCriteria;
                private readonly BaseImplementationNameMethodInvocationImposterGroup _invocationImposterGroup;
                private BaseImplementationNameMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(BaseImplementationNameMethodImposter _imposter, BaseImplementationNameMethodInvocationHistoryCollection _baseImplementationNameMethodInvocationHistoryCollection, BaseImplementationNameArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._baseImplementationNameMethodInvocationHistoryCollection = _baseImplementationNameMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new BaseImplementationNameMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IBaseImplementationNameMethodInvocationImposterGroupContinuation IBaseImplementationNameMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int baseImplementation) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IBaseImplementationNameMethodInvocationImposterGroupContinuation IBaseImplementationNameMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int baseImplementation) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IBaseImplementationNameMethodInvocationImposterGroupContinuation IBaseImplementationNameMethodInvocationImposterGroup.Throws(BaseImplementationNameExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int baseImplementation) =>
                    {
                        throw exceptionGenerator.Invoke(baseImplementation);
                    });
                    return this;
                }

                IBaseImplementationNameMethodInvocationImposterGroupContinuation IBaseImplementationNameMethodInvocationImposterGroupCallback.Callback(BaseImplementationNameCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IBaseImplementationNameMethodInvocationImposterGroupContinuation IBaseImplementationNameMethodInvocationImposterGroup.Returns(BaseImplementationNameDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IBaseImplementationNameMethodInvocationImposterGroupContinuation IBaseImplementationNameMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IBaseImplementationNameMethodInvocationImposterGroup IBaseImplementationNameMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void BaseImplementationNameInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _baseImplementationNameMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodParameterBaseImplementationCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._baseImplementationNameMethodImposter = new BaseImplementationNameMethodImposter(_baseImplementationNameMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodParameterBaseImplementationCollisionTarget
        {
            IMethodParameterBaseImplementationCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodParameterBaseImplementationCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int BaseImplementationName(int baseImplementation)
            {
                return _imposter._baseImplementationNameMethodImposter.Invoke(baseImplementation);
            }
        }
    }
}
#pragma warning restore nullable
