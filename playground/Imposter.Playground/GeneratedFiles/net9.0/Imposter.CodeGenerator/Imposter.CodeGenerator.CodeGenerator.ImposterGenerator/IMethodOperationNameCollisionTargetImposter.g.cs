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
    public class IMethodOperationNameCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodOperationNameCollisionTarget>
    {
        private readonly ReturnsMethodImposter _returnsMethodImposter;
        private readonly ReturnsAsyncMethodImposter _returnsAsyncMethodImposter;
        private readonly ThrowsMethodImposter _throwsMethodImposter;
        private readonly ThrowsAsyncMethodImposter _throwsAsyncMethodImposter;
        private readonly CallbackMethodImposter _callbackMethodImposter;
        private readonly ThenMethodImposter _thenMethodImposter;
        private readonly UseBaseImplementationMethodImposter _useBaseImplementationMethodImposter;
        private readonly DefaultResultGeneratorMethodImposter _defaultResultGeneratorMethodImposter;
        private readonly DefaultMethodImposter _defaultMethodImposter;
        private readonly ReturnsMethodInvocationHistoryCollection _returnsMethodInvocationHistoryCollection = new ReturnsMethodInvocationHistoryCollection();
        private readonly ReturnsAsyncMethodInvocationHistoryCollection _returnsAsyncMethodInvocationHistoryCollection = new ReturnsAsyncMethodInvocationHistoryCollection();
        private readonly ThrowsMethodInvocationHistoryCollection _throwsMethodInvocationHistoryCollection = new ThrowsMethodInvocationHistoryCollection();
        private readonly ThrowsAsyncMethodInvocationHistoryCollection _throwsAsyncMethodInvocationHistoryCollection = new ThrowsAsyncMethodInvocationHistoryCollection();
        private readonly CallbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection = new CallbackMethodInvocationHistoryCollection();
        private readonly ThenMethodInvocationHistoryCollection _thenMethodInvocationHistoryCollection = new ThenMethodInvocationHistoryCollection();
        private readonly UseBaseImplementationMethodInvocationHistoryCollection _useBaseImplementationMethodInvocationHistoryCollection = new UseBaseImplementationMethodInvocationHistoryCollection();
        private readonly DefaultResultGeneratorMethodInvocationHistoryCollection _defaultResultGeneratorMethodInvocationHistoryCollection = new DefaultResultGeneratorMethodInvocationHistoryCollection();
        private readonly DefaultMethodInvocationHistoryCollection _defaultMethodInvocationHistoryCollection = new DefaultMethodInvocationHistoryCollection();
        public IReturnsMethodImposterBuilder Returns()
        {
            return new ReturnsMethodImposter.Builder(_returnsMethodImposter, _returnsMethodInvocationHistoryCollection);
        }

        public IReturnsAsyncMethodImposterBuilder ReturnsAsync()
        {
            return new ReturnsAsyncMethodImposter.Builder(_returnsAsyncMethodImposter, _returnsAsyncMethodInvocationHistoryCollection);
        }

        public IThrowsMethodImposterBuilder Throws()
        {
            return new ThrowsMethodImposter.Builder(_throwsMethodImposter, _throwsMethodInvocationHistoryCollection);
        }

        public IThrowsAsyncMethodImposterBuilder ThrowsAsync()
        {
            return new ThrowsAsyncMethodImposter.Builder(_throwsAsyncMethodImposter, _throwsAsyncMethodInvocationHistoryCollection);
        }

        public ICallbackMethodImposterBuilder Callback()
        {
            return new CallbackMethodImposter.Builder(_callbackMethodImposter, _callbackMethodInvocationHistoryCollection);
        }

        public IThenMethodImposterBuilder Then()
        {
            return new ThenMethodImposter.Builder(_thenMethodImposter, _thenMethodInvocationHistoryCollection);
        }

        public IUseBaseImplementationMethodImposterBuilder UseBaseImplementation()
        {
            return new UseBaseImplementationMethodImposter.Builder(_useBaseImplementationMethodImposter, _useBaseImplementationMethodInvocationHistoryCollection);
        }

        public IDefaultResultGeneratorMethodImposterBuilder DefaultResultGenerator()
        {
            return new DefaultResultGeneratorMethodImposter.Builder(_defaultResultGeneratorMethodImposter, _defaultResultGeneratorMethodInvocationHistoryCollection);
        }

        public IDefaultMethodImposterBuilder Default()
        {
            return new DefaultMethodImposter.Builder(_defaultMethodImposter, _defaultMethodInvocationHistoryCollection);
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodOperationNameCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodOperationNameCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // void IMethodOperationNameCollisionTarget.Callback()
        public delegate void CallbackDelegate();
        // void IMethodOperationNameCollisionTarget.Callback()
        public delegate void CallbackCallbackDelegate();
        // void IMethodOperationNameCollisionTarget.Callback()
        public delegate System.Exception CallbackExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICallbackMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CallbackMethodInvocationHistory : ICallbackMethodInvocationHistory
        {
            internal System.Exception Exception;
            public CallbackMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CallbackMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ICallbackMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ICallbackMethodInvocationHistory>();
            internal void Add(ICallbackMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodOperationNameCollisionTarget.Callback()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class CallbackMethodInvocationImposterGroup
        {
            internal static CallbackMethodInvocationImposterGroup Default = new CallbackMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public CallbackMethodInvocationImposterGroup()
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

                private CallbackDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<CallbackCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<CallbackCallbackDelegate>();
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

                internal void Callback(CallbackCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(CallbackExceptionGeneratorDelegate exceptionGenerator)
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
        public interface ICallbackMethodInvocationImposterGroupCallback
        {
            ICallbackMethodInvocationImposterGroupContinuation Callback(CallbackCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICallbackMethodInvocationImposterGroupContinuation : ICallbackMethodInvocationImposterGroupCallback
        {
            ICallbackMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICallbackMethodInvocationImposterGroup : ICallbackMethodInvocationImposterGroupCallback
        {
            ICallbackMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            ICallbackMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            ICallbackMethodInvocationImposterGroupContinuation Throws(CallbackExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CallbackInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodOperationNameCollisionTarget.Callback()
        public interface ICallbackMethodImposterBuilder : ICallbackMethodInvocationImposterGroup, ICallbackMethodInvocationImposterGroupCallback, CallbackInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CallbackMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CallbackMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<CallbackMethodInvocationImposterGroup>();
            private readonly CallbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public CallbackMethodImposter(CallbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._callbackMethodInvocationHistoryCollection = _callbackMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private CallbackMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodOperationNameCollisionTarget.Callback()");
                    }

                    matchingInvocationImposterGroup = CallbackMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodOperationNameCollisionTarget.Callback()");
                    _callbackMethodInvocationHistoryCollection.Add(new CallbackMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    _callbackMethodInvocationHistoryCollection.Add(new CallbackMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ICallbackMethodImposterBuilder, ICallbackMethodInvocationImposterGroupContinuation
            {
                private readonly CallbackMethodImposter _imposter;
                private readonly CallbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection;
                private readonly CallbackMethodInvocationImposterGroup _invocationImposterGroup;
                private CallbackMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(CallbackMethodImposter _imposter, CallbackMethodInvocationHistoryCollection _callbackMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._callbackMethodInvocationHistoryCollection = _callbackMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new CallbackMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ICallbackMethodInvocationImposterGroupContinuation ICallbackMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ICallbackMethodInvocationImposterGroupContinuation ICallbackMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ICallbackMethodInvocationImposterGroupContinuation ICallbackMethodInvocationImposterGroup.Throws(CallbackExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                ICallbackMethodInvocationImposterGroupContinuation ICallbackMethodInvocationImposterGroupCallback.Callback(CallbackCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ICallbackMethodInvocationImposterGroup ICallbackMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void CallbackInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _callbackMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodOperationNameCollisionTarget.Default()
        public delegate int DefaultDelegate();
        // int IMethodOperationNameCollisionTarget.Default()
        public delegate void DefaultCallbackDelegate();
        // int IMethodOperationNameCollisionTarget.Default()
        public delegate System.Exception DefaultExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DefaultMethodInvocationHistory : IDefaultMethodInvocationHistory
        {
            internal int Result;
            internal System.Exception Exception;
            public DefaultMethodInvocationHistory(int Result, System.Exception Exception)
            {
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DefaultMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IDefaultMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IDefaultMethodInvocationHistory>();
            internal void Add(IDefaultMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // int IMethodOperationNameCollisionTarget.Default()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class DefaultMethodInvocationImposterGroup
        {
            internal static DefaultMethodInvocationImposterGroup Default = new DefaultMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public DefaultMethodInvocationImposterGroup()
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

            public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName);
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

                private DefaultDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<DefaultCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<DefaultCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke();
                    foreach (var callback in _callbacks)
                    {
                        callback();
                    }

                    return result;
                }

                internal void Callback(DefaultCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(DefaultDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = () =>
                    {
                        return value;
                    };
                }

                internal void Throws(DefaultExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = () =>
                    {
                        throw exceptionGenerator();
                    };
                }

                internal static int DefaultResultGenerator()
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultMethodInvocationImposterGroupCallback
        {
            IDefaultMethodInvocationImposterGroupContinuation Callback(DefaultCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultMethodInvocationImposterGroupContinuation : IDefaultMethodInvocationImposterGroupCallback
        {
            IDefaultMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultMethodInvocationImposterGroup : IDefaultMethodInvocationImposterGroupCallback
        {
            IDefaultMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IDefaultMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IDefaultMethodInvocationImposterGroupContinuation Throws(DefaultExceptionGeneratorDelegate exceptionGenerator);
            IDefaultMethodInvocationImposterGroupContinuation Returns(DefaultDelegate resultGenerator);
            IDefaultMethodInvocationImposterGroupContinuation Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface DefaultInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodOperationNameCollisionTarget.Default()
        public interface IDefaultMethodImposterBuilder : IDefaultMethodInvocationImposterGroup, IDefaultMethodInvocationImposterGroupCallback, DefaultInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DefaultMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<DefaultMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<DefaultMethodInvocationImposterGroup>();
            private readonly DefaultMethodInvocationHistoryCollection _defaultMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public DefaultMethodImposter(DefaultMethodInvocationHistoryCollection _defaultMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._defaultMethodInvocationHistoryCollection = _defaultMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private DefaultMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
            {
                if (_invocationImposters.TryPeek(out var invocationImposterGroup))
                    return invocationImposterGroup;
                else
                    return null;
            }

            public int Invoke()
            {
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup();
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("int IMethodOperationNameCollisionTarget.Default()");
                    }

                    matchingInvocationImposterGroup = DefaultMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "int IMethodOperationNameCollisionTarget.Default()");
                    _defaultMethodInvocationHistoryCollection.Add(new DefaultMethodInvocationHistory(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _defaultMethodInvocationHistoryCollection.Add(new DefaultMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IDefaultMethodImposterBuilder, IDefaultMethodInvocationImposterGroupContinuation
            {
                private readonly DefaultMethodImposter _imposter;
                private readonly DefaultMethodInvocationHistoryCollection _defaultMethodInvocationHistoryCollection;
                private readonly DefaultMethodInvocationImposterGroup _invocationImposterGroup;
                private DefaultMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(DefaultMethodImposter _imposter, DefaultMethodInvocationHistoryCollection _defaultMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._defaultMethodInvocationHistoryCollection = _defaultMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new DefaultMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IDefaultMethodInvocationImposterGroupContinuation IDefaultMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IDefaultMethodInvocationImposterGroupContinuation IDefaultMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IDefaultMethodInvocationImposterGroupContinuation IDefaultMethodInvocationImposterGroup.Throws(DefaultExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IDefaultMethodInvocationImposterGroupContinuation IDefaultMethodInvocationImposterGroupCallback.Callback(DefaultCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IDefaultMethodInvocationImposterGroupContinuation IDefaultMethodInvocationImposterGroup.Returns(DefaultDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IDefaultMethodInvocationImposterGroupContinuation IDefaultMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IDefaultMethodInvocationImposterGroup IDefaultMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void DefaultInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _defaultMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodOperationNameCollisionTarget.DefaultResultGenerator()
        public delegate int DefaultResultGeneratorDelegate();
        // int IMethodOperationNameCollisionTarget.DefaultResultGenerator()
        public delegate void DefaultResultGeneratorCallbackDelegate();
        // int IMethodOperationNameCollisionTarget.DefaultResultGenerator()
        public delegate System.Exception DefaultResultGeneratorExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultResultGeneratorMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DefaultResultGeneratorMethodInvocationHistory : IDefaultResultGeneratorMethodInvocationHistory
        {
            internal int Result;
            internal System.Exception Exception;
            public DefaultResultGeneratorMethodInvocationHistory(int Result, System.Exception Exception)
            {
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DefaultResultGeneratorMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IDefaultResultGeneratorMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IDefaultResultGeneratorMethodInvocationHistory>();
            internal void Add(IDefaultResultGeneratorMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // int IMethodOperationNameCollisionTarget.DefaultResultGenerator()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class DefaultResultGeneratorMethodInvocationImposterGroup
        {
            internal static DefaultResultGeneratorMethodInvocationImposterGroup Default = new DefaultResultGeneratorMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public DefaultResultGeneratorMethodInvocationImposterGroup()
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

            public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName);
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

                private DefaultResultGeneratorDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<DefaultResultGeneratorCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<DefaultResultGeneratorCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke();
                    foreach (var callback in _callbacks)
                    {
                        callback();
                    }

                    return result;
                }

                internal void Callback(DefaultResultGeneratorCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(DefaultResultGeneratorDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = () =>
                    {
                        return value;
                    };
                }

                internal void Throws(DefaultResultGeneratorExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = () =>
                    {
                        throw exceptionGenerator();
                    };
                }

                internal static int DefaultResultGenerator()
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultResultGeneratorMethodInvocationImposterGroupCallback
        {
            IDefaultResultGeneratorMethodInvocationImposterGroupContinuation Callback(DefaultResultGeneratorCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultResultGeneratorMethodInvocationImposterGroupContinuation : IDefaultResultGeneratorMethodInvocationImposterGroupCallback
        {
            IDefaultResultGeneratorMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDefaultResultGeneratorMethodInvocationImposterGroup : IDefaultResultGeneratorMethodInvocationImposterGroupCallback
        {
            IDefaultResultGeneratorMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IDefaultResultGeneratorMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IDefaultResultGeneratorMethodInvocationImposterGroupContinuation Throws(DefaultResultGeneratorExceptionGeneratorDelegate exceptionGenerator);
            IDefaultResultGeneratorMethodInvocationImposterGroupContinuation Returns(DefaultResultGeneratorDelegate resultGenerator);
            IDefaultResultGeneratorMethodInvocationImposterGroupContinuation Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface DefaultResultGeneratorInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodOperationNameCollisionTarget.DefaultResultGenerator()
        public interface IDefaultResultGeneratorMethodImposterBuilder : IDefaultResultGeneratorMethodInvocationImposterGroup, IDefaultResultGeneratorMethodInvocationImposterGroupCallback, DefaultResultGeneratorInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DefaultResultGeneratorMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<DefaultResultGeneratorMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<DefaultResultGeneratorMethodInvocationImposterGroup>();
            private readonly DefaultResultGeneratorMethodInvocationHistoryCollection _defaultResultGeneratorMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public DefaultResultGeneratorMethodImposter(DefaultResultGeneratorMethodInvocationHistoryCollection _defaultResultGeneratorMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._defaultResultGeneratorMethodInvocationHistoryCollection = _defaultResultGeneratorMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private DefaultResultGeneratorMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
            {
                if (_invocationImposters.TryPeek(out var invocationImposterGroup))
                    return invocationImposterGroup;
                else
                    return null;
            }

            public int Invoke()
            {
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup();
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("int IMethodOperationNameCollisionTarget.DefaultResultGenerator()");
                    }

                    matchingInvocationImposterGroup = DefaultResultGeneratorMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "int IMethodOperationNameCollisionTarget.DefaultResultGenerator()");
                    _defaultResultGeneratorMethodInvocationHistoryCollection.Add(new DefaultResultGeneratorMethodInvocationHistory(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _defaultResultGeneratorMethodInvocationHistoryCollection.Add(new DefaultResultGeneratorMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IDefaultResultGeneratorMethodImposterBuilder, IDefaultResultGeneratorMethodInvocationImposterGroupContinuation
            {
                private readonly DefaultResultGeneratorMethodImposter _imposter;
                private readonly DefaultResultGeneratorMethodInvocationHistoryCollection _defaultResultGeneratorMethodInvocationHistoryCollection;
                private readonly DefaultResultGeneratorMethodInvocationImposterGroup _invocationImposterGroup;
                private DefaultResultGeneratorMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(DefaultResultGeneratorMethodImposter _imposter, DefaultResultGeneratorMethodInvocationHistoryCollection _defaultResultGeneratorMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._defaultResultGeneratorMethodInvocationHistoryCollection = _defaultResultGeneratorMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new DefaultResultGeneratorMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IDefaultResultGeneratorMethodInvocationImposterGroupContinuation IDefaultResultGeneratorMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IDefaultResultGeneratorMethodInvocationImposterGroupContinuation IDefaultResultGeneratorMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IDefaultResultGeneratorMethodInvocationImposterGroupContinuation IDefaultResultGeneratorMethodInvocationImposterGroup.Throws(DefaultResultGeneratorExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IDefaultResultGeneratorMethodInvocationImposterGroupContinuation IDefaultResultGeneratorMethodInvocationImposterGroupCallback.Callback(DefaultResultGeneratorCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IDefaultResultGeneratorMethodInvocationImposterGroupContinuation IDefaultResultGeneratorMethodInvocationImposterGroup.Returns(DefaultResultGeneratorDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IDefaultResultGeneratorMethodInvocationImposterGroupContinuation IDefaultResultGeneratorMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IDefaultResultGeneratorMethodInvocationImposterGroup IDefaultResultGeneratorMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void DefaultResultGeneratorInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _defaultResultGeneratorMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodOperationNameCollisionTarget.Returns()
        public delegate int ReturnsDelegate();
        // int IMethodOperationNameCollisionTarget.Returns()
        public delegate void ReturnsCallbackDelegate();
        // int IMethodOperationNameCollisionTarget.Returns()
        public delegate System.Exception ReturnsExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsMethodInvocationHistory : IReturnsMethodInvocationHistory
        {
            internal int Result;
            internal System.Exception Exception;
            public ReturnsMethodInvocationHistory(int Result, System.Exception Exception)
            {
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IReturnsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IReturnsMethodInvocationHistory>();
            internal void Add(IReturnsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // int IMethodOperationNameCollisionTarget.Returns()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ReturnsMethodInvocationImposterGroup
        {
            internal static ReturnsMethodInvocationImposterGroup Default = new ReturnsMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ReturnsMethodInvocationImposterGroup()
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

            public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName);
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

                private ReturnsDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ReturnsCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ReturnsCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke();
                    foreach (var callback in _callbacks)
                    {
                        callback();
                    }

                    return result;
                }

                internal void Callback(ReturnsCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(ReturnsDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = () =>
                    {
                        return value;
                    };
                }

                internal void Throws(ReturnsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = () =>
                    {
                        throw exceptionGenerator();
                    };
                }

                internal static int DefaultResultGenerator()
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsMethodInvocationImposterGroupCallback
        {
            IReturnsMethodInvocationImposterGroupContinuation Callback(ReturnsCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsMethodInvocationImposterGroupContinuation : IReturnsMethodInvocationImposterGroupCallback
        {
            IReturnsMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsMethodInvocationImposterGroup : IReturnsMethodInvocationImposterGroupCallback
        {
            IReturnsMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IReturnsMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IReturnsMethodInvocationImposterGroupContinuation Throws(ReturnsExceptionGeneratorDelegate exceptionGenerator);
            IReturnsMethodInvocationImposterGroupContinuation Returns(ReturnsDelegate resultGenerator);
            IReturnsMethodInvocationImposterGroupContinuation Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ReturnsInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodOperationNameCollisionTarget.Returns()
        public interface IReturnsMethodImposterBuilder : IReturnsMethodInvocationImposterGroup, IReturnsMethodInvocationImposterGroupCallback, ReturnsInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ReturnsMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ReturnsMethodInvocationImposterGroup>();
            private readonly ReturnsMethodInvocationHistoryCollection _returnsMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ReturnsMethodImposter(ReturnsMethodInvocationHistoryCollection _returnsMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._returnsMethodInvocationHistoryCollection = _returnsMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private ReturnsMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
            {
                if (_invocationImposters.TryPeek(out var invocationImposterGroup))
                    return invocationImposterGroup;
                else
                    return null;
            }

            public int Invoke()
            {
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup();
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("int IMethodOperationNameCollisionTarget.Returns()");
                    }

                    matchingInvocationImposterGroup = ReturnsMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "int IMethodOperationNameCollisionTarget.Returns()");
                    _returnsMethodInvocationHistoryCollection.Add(new ReturnsMethodInvocationHistory(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _returnsMethodInvocationHistoryCollection.Add(new ReturnsMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IReturnsMethodImposterBuilder, IReturnsMethodInvocationImposterGroupContinuation
            {
                private readonly ReturnsMethodImposter _imposter;
                private readonly ReturnsMethodInvocationHistoryCollection _returnsMethodInvocationHistoryCollection;
                private readonly ReturnsMethodInvocationImposterGroup _invocationImposterGroup;
                private ReturnsMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ReturnsMethodImposter _imposter, ReturnsMethodInvocationHistoryCollection _returnsMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._returnsMethodInvocationHistoryCollection = _returnsMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new ReturnsMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IReturnsMethodInvocationImposterGroupContinuation IReturnsMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IReturnsMethodInvocationImposterGroupContinuation IReturnsMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IReturnsMethodInvocationImposterGroupContinuation IReturnsMethodInvocationImposterGroup.Throws(ReturnsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IReturnsMethodInvocationImposterGroupContinuation IReturnsMethodInvocationImposterGroupCallback.Callback(ReturnsCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IReturnsMethodInvocationImposterGroupContinuation IReturnsMethodInvocationImposterGroup.Returns(ReturnsDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IReturnsMethodInvocationImposterGroupContinuation IReturnsMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IReturnsMethodInvocationImposterGroup IReturnsMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ReturnsInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _returnsMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // Task<int> IMethodOperationNameCollisionTarget.ReturnsAsync()
        public delegate global::System.Threading.Tasks.Task<int> ReturnsAsyncDelegate();
        // Task<int> IMethodOperationNameCollisionTarget.ReturnsAsync()
        public delegate System.Threading.Tasks.Task ReturnsAsyncCallbackDelegate();
        // Task<int> IMethodOperationNameCollisionTarget.ReturnsAsync()
        public delegate System.Exception ReturnsAsyncExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsAsyncMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsAsyncMethodInvocationHistory : IReturnsAsyncMethodInvocationHistory
        {
            internal global::System.Threading.Tasks.Task<int> Result;
            internal System.Exception Exception;
            public ReturnsAsyncMethodInvocationHistory(global::System.Threading.Tasks.Task<int> Result, System.Exception Exception)
            {
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsAsyncMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IReturnsAsyncMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IReturnsAsyncMethodInvocationHistory>();
            internal void Add(IReturnsAsyncMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // Task<int> IMethodOperationNameCollisionTarget.ReturnsAsync()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ReturnsAsyncMethodInvocationImposterGroup
        {
            internal static ReturnsAsyncMethodInvocationImposterGroup Default = new ReturnsAsyncMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ReturnsAsyncMethodInvocationImposterGroup()
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

            public global::System.Threading.Tasks.Task<int> Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName);
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

                private ReturnsAsyncDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ReturnsAsyncCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ReturnsAsyncCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public global::System.Threading.Tasks.Task<int> Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    global::System.Threading.Tasks.Task<int> result = _resultGenerator.Invoke();
                    foreach (var callback in _callbacks)
                    {
                        callback();
                    }

                    return result;
                }

                internal void Callback(ReturnsAsyncCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(ReturnsAsyncDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(global::System.Threading.Tasks.Task<int> value)
                {
                    _resultGenerator = () =>
                    {
                        return value;
                    };
                }

                internal void ReturnsAsync(int value)
                {
                    _resultGenerator = () =>
                    {
                        return System.Threading.Tasks.Task.FromResult(value);
                    };
                }

                internal void Throws(ReturnsAsyncExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = () =>
                    {
                        throw exceptionGenerator();
                    };
                }

                internal void ThrowsAsync(System.Exception exception)
                {
                    _resultGenerator = () =>
                    {
                        return System.Threading.Tasks.Task.FromException<int>(exception);
                    };
                }

                internal static async global::System.Threading.Tasks.Task<int> DefaultResultGenerator()
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsAsyncMethodInvocationImposterGroupCallback
        {
            IReturnsAsyncMethodInvocationImposterGroupContinuation Callback(ReturnsAsyncCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsAsyncMethodInvocationImposterGroupContinuation : IReturnsAsyncMethodInvocationImposterGroupCallback
        {
            IReturnsAsyncMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IReturnsAsyncMethodInvocationImposterGroup : IReturnsAsyncMethodInvocationImposterGroupCallback
        {
            IReturnsAsyncMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IReturnsAsyncMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IReturnsAsyncMethodInvocationImposterGroupContinuation Throws(ReturnsAsyncExceptionGeneratorDelegate exceptionGenerator);
            IReturnsAsyncMethodInvocationImposterGroupContinuation Returns(ReturnsAsyncDelegate resultGenerator);
            IReturnsAsyncMethodInvocationImposterGroupContinuation Returns(global::System.Threading.Tasks.Task<int> value);
            IReturnsAsyncMethodInvocationImposterGroupContinuation ReturnsAsync(int value);
            IReturnsAsyncMethodInvocationImposterGroupContinuation ThrowsAsync(System.Exception exception);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ReturnsAsyncInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // Task<int> IMethodOperationNameCollisionTarget.ReturnsAsync()
        public interface IReturnsAsyncMethodImposterBuilder : IReturnsAsyncMethodInvocationImposterGroup, IReturnsAsyncMethodInvocationImposterGroupCallback, ReturnsAsyncInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ReturnsAsyncMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ReturnsAsyncMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ReturnsAsyncMethodInvocationImposterGroup>();
            private readonly ReturnsAsyncMethodInvocationHistoryCollection _returnsAsyncMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ReturnsAsyncMethodImposter(ReturnsAsyncMethodInvocationHistoryCollection _returnsAsyncMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._returnsAsyncMethodInvocationHistoryCollection = _returnsAsyncMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private ReturnsAsyncMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
            {
                if (_invocationImposters.TryPeek(out var invocationImposterGroup))
                    return invocationImposterGroup;
                else
                    return null;
            }

            public global::System.Threading.Tasks.Task<int> Invoke()
            {
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup();
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("Task<int> IMethodOperationNameCollisionTarget.ReturnsAsync()");
                    }

                    matchingInvocationImposterGroup = ReturnsAsyncMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "Task<int> IMethodOperationNameCollisionTarget.ReturnsAsync()");
                    _returnsAsyncMethodInvocationHistoryCollection.Add(new ReturnsAsyncMethodInvocationHistory(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _returnsAsyncMethodInvocationHistoryCollection.Add(new ReturnsAsyncMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IReturnsAsyncMethodImposterBuilder, IReturnsAsyncMethodInvocationImposterGroupContinuation
            {
                private readonly ReturnsAsyncMethodImposter _imposter;
                private readonly ReturnsAsyncMethodInvocationHistoryCollection _returnsAsyncMethodInvocationHistoryCollection;
                private readonly ReturnsAsyncMethodInvocationImposterGroup _invocationImposterGroup;
                private ReturnsAsyncMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ReturnsAsyncMethodImposter _imposter, ReturnsAsyncMethodInvocationHistoryCollection _returnsAsyncMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._returnsAsyncMethodInvocationHistoryCollection = _returnsAsyncMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new ReturnsAsyncMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IReturnsAsyncMethodInvocationImposterGroupContinuation IReturnsAsyncMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IReturnsAsyncMethodInvocationImposterGroupContinuation IReturnsAsyncMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IReturnsAsyncMethodInvocationImposterGroupContinuation IReturnsAsyncMethodInvocationImposterGroup.Throws(ReturnsAsyncExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IReturnsAsyncMethodInvocationImposterGroupContinuation IReturnsAsyncMethodInvocationImposterGroupCallback.Callback(ReturnsAsyncCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IReturnsAsyncMethodInvocationImposterGroupContinuation IReturnsAsyncMethodInvocationImposterGroup.Returns(ReturnsAsyncDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IReturnsAsyncMethodInvocationImposterGroupContinuation IReturnsAsyncMethodInvocationImposterGroup.Returns(global::System.Threading.Tasks.Task<int> value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IReturnsAsyncMethodInvocationImposterGroupContinuation IReturnsAsyncMethodInvocationImposterGroup.ReturnsAsync(int value)
                {
                    _currentInvocationImposter.ReturnsAsync(value);
                    return this;
                }

                IReturnsAsyncMethodInvocationImposterGroupContinuation IReturnsAsyncMethodInvocationImposterGroup.ThrowsAsync(System.Exception exception)
                {
                    _currentInvocationImposter.ThrowsAsync(exception);
                    return this;
                }

                IReturnsAsyncMethodInvocationImposterGroup IReturnsAsyncMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ReturnsAsyncInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _returnsAsyncMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodOperationNameCollisionTarget.Then()
        public delegate void ThenDelegate();
        // void IMethodOperationNameCollisionTarget.Then()
        public delegate void ThenCallbackDelegate();
        // void IMethodOperationNameCollisionTarget.Then()
        public delegate System.Exception ThenExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThenMethodInvocationHistory : IThenMethodInvocationHistory
        {
            internal System.Exception Exception;
            public ThenMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThenMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IThenMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IThenMethodInvocationHistory>();
            internal void Add(IThenMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodOperationNameCollisionTarget.Then()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ThenMethodInvocationImposterGroup
        {
            internal static ThenMethodInvocationImposterGroup Default = new ThenMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ThenMethodInvocationImposterGroup()
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

                private ThenDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ThenCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ThenCallbackDelegate>();
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

                internal void Callback(ThenCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(ThenExceptionGeneratorDelegate exceptionGenerator)
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
        public interface IThenMethodInvocationImposterGroupCallback
        {
            IThenMethodInvocationImposterGroupContinuation Callback(ThenCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenMethodInvocationImposterGroupContinuation : IThenMethodInvocationImposterGroupCallback
        {
            IThenMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThenMethodInvocationImposterGroup : IThenMethodInvocationImposterGroupCallback
        {
            IThenMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IThenMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IThenMethodInvocationImposterGroupContinuation Throws(ThenExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ThenInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodOperationNameCollisionTarget.Then()
        public interface IThenMethodImposterBuilder : IThenMethodInvocationImposterGroup, IThenMethodInvocationImposterGroupCallback, ThenInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThenMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ThenMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ThenMethodInvocationImposterGroup>();
            private readonly ThenMethodInvocationHistoryCollection _thenMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ThenMethodImposter(ThenMethodInvocationHistoryCollection _thenMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._thenMethodInvocationHistoryCollection = _thenMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private ThenMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodOperationNameCollisionTarget.Then()");
                    }

                    matchingInvocationImposterGroup = ThenMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodOperationNameCollisionTarget.Then()");
                    _thenMethodInvocationHistoryCollection.Add(new ThenMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    _thenMethodInvocationHistoryCollection.Add(new ThenMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IThenMethodImposterBuilder, IThenMethodInvocationImposterGroupContinuation
            {
                private readonly ThenMethodImposter _imposter;
                private readonly ThenMethodInvocationHistoryCollection _thenMethodInvocationHistoryCollection;
                private readonly ThenMethodInvocationImposterGroup _invocationImposterGroup;
                private ThenMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ThenMethodImposter _imposter, ThenMethodInvocationHistoryCollection _thenMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._thenMethodInvocationHistoryCollection = _thenMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new ThenMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IThenMethodInvocationImposterGroupContinuation IThenMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IThenMethodInvocationImposterGroupContinuation IThenMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IThenMethodInvocationImposterGroupContinuation IThenMethodInvocationImposterGroup.Throws(ThenExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IThenMethodInvocationImposterGroupContinuation IThenMethodInvocationImposterGroupCallback.Callback(ThenCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IThenMethodInvocationImposterGroup IThenMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ThenInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _thenMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodOperationNameCollisionTarget.Throws()
        public delegate void ThrowsDelegate();
        // void IMethodOperationNameCollisionTarget.Throws()
        public delegate void ThrowsCallbackDelegate();
        // void IMethodOperationNameCollisionTarget.Throws()
        public delegate System.Exception ThrowsExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThrowsMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThrowsMethodInvocationHistory : IThrowsMethodInvocationHistory
        {
            internal System.Exception Exception;
            public ThrowsMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThrowsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IThrowsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IThrowsMethodInvocationHistory>();
            internal void Add(IThrowsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodOperationNameCollisionTarget.Throws()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ThrowsMethodInvocationImposterGroup
        {
            internal static ThrowsMethodInvocationImposterGroup Default = new ThrowsMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ThrowsMethodInvocationImposterGroup()
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

                private ThrowsDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ThrowsCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ThrowsCallbackDelegate>();
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

                internal void Callback(ThrowsCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(ThrowsExceptionGeneratorDelegate exceptionGenerator)
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
        public interface IThrowsMethodInvocationImposterGroupCallback
        {
            IThrowsMethodInvocationImposterGroupContinuation Callback(ThrowsCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThrowsMethodInvocationImposterGroupContinuation : IThrowsMethodInvocationImposterGroupCallback
        {
            IThrowsMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThrowsMethodInvocationImposterGroup : IThrowsMethodInvocationImposterGroupCallback
        {
            IThrowsMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IThrowsMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IThrowsMethodInvocationImposterGroupContinuation Throws(ThrowsExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ThrowsInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodOperationNameCollisionTarget.Throws()
        public interface IThrowsMethodImposterBuilder : IThrowsMethodInvocationImposterGroup, IThrowsMethodInvocationImposterGroupCallback, ThrowsInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThrowsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ThrowsMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ThrowsMethodInvocationImposterGroup>();
            private readonly ThrowsMethodInvocationHistoryCollection _throwsMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ThrowsMethodImposter(ThrowsMethodInvocationHistoryCollection _throwsMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._throwsMethodInvocationHistoryCollection = _throwsMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private ThrowsMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodOperationNameCollisionTarget.Throws()");
                    }

                    matchingInvocationImposterGroup = ThrowsMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodOperationNameCollisionTarget.Throws()");
                    _throwsMethodInvocationHistoryCollection.Add(new ThrowsMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    _throwsMethodInvocationHistoryCollection.Add(new ThrowsMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IThrowsMethodImposterBuilder, IThrowsMethodInvocationImposterGroupContinuation
            {
                private readonly ThrowsMethodImposter _imposter;
                private readonly ThrowsMethodInvocationHistoryCollection _throwsMethodInvocationHistoryCollection;
                private readonly ThrowsMethodInvocationImposterGroup _invocationImposterGroup;
                private ThrowsMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ThrowsMethodImposter _imposter, ThrowsMethodInvocationHistoryCollection _throwsMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._throwsMethodInvocationHistoryCollection = _throwsMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new ThrowsMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IThrowsMethodInvocationImposterGroupContinuation IThrowsMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IThrowsMethodInvocationImposterGroupContinuation IThrowsMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IThrowsMethodInvocationImposterGroupContinuation IThrowsMethodInvocationImposterGroup.Throws(ThrowsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IThrowsMethodInvocationImposterGroupContinuation IThrowsMethodInvocationImposterGroupCallback.Callback(ThrowsCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IThrowsMethodInvocationImposterGroup IThrowsMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ThrowsInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _throwsMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // Task IMethodOperationNameCollisionTarget.ThrowsAsync()
        public delegate global::System.Threading.Tasks.Task ThrowsAsyncDelegate();
        // Task IMethodOperationNameCollisionTarget.ThrowsAsync()
        public delegate System.Threading.Tasks.Task ThrowsAsyncCallbackDelegate();
        // Task IMethodOperationNameCollisionTarget.ThrowsAsync()
        public delegate System.Exception ThrowsAsyncExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThrowsAsyncMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThrowsAsyncMethodInvocationHistory : IThrowsAsyncMethodInvocationHistory
        {
            internal global::System.Threading.Tasks.Task Result;
            internal System.Exception Exception;
            public ThrowsAsyncMethodInvocationHistory(global::System.Threading.Tasks.Task Result, System.Exception Exception)
            {
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThrowsAsyncMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IThrowsAsyncMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IThrowsAsyncMethodInvocationHistory>();
            internal void Add(IThrowsAsyncMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // Task IMethodOperationNameCollisionTarget.ThrowsAsync()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ThrowsAsyncMethodInvocationImposterGroup
        {
            internal static ThrowsAsyncMethodInvocationImposterGroup Default = new ThrowsAsyncMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ThrowsAsyncMethodInvocationImposterGroup()
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

            public global::System.Threading.Tasks.Task Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName);
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

                private ThrowsAsyncDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ThrowsAsyncCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ThrowsAsyncCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public global::System.Threading.Tasks.Task Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    global::System.Threading.Tasks.Task result = _resultGenerator.Invoke();
                    foreach (var callback in _callbacks)
                    {
                        callback();
                    }

                    return result;
                }

                internal void Callback(ThrowsAsyncCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(ThrowsAsyncDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(global::System.Threading.Tasks.Task value)
                {
                    _resultGenerator = () =>
                    {
                        return value;
                    };
                }

                internal void Throws(ThrowsAsyncExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = () =>
                    {
                        throw exceptionGenerator();
                    };
                }

                internal void ThrowsAsync(System.Exception exception)
                {
                    _resultGenerator = () =>
                    {
                        return System.Threading.Tasks.Task.FromException(exception);
                    };
                }

                internal static async global::System.Threading.Tasks.Task DefaultResultGenerator()
                {
                    return;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThrowsAsyncMethodInvocationImposterGroupCallback
        {
            IThrowsAsyncMethodInvocationImposterGroupContinuation Callback(ThrowsAsyncCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThrowsAsyncMethodInvocationImposterGroupContinuation : IThrowsAsyncMethodInvocationImposterGroupCallback
        {
            IThrowsAsyncMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IThrowsAsyncMethodInvocationImposterGroup : IThrowsAsyncMethodInvocationImposterGroupCallback
        {
            IThrowsAsyncMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IThrowsAsyncMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IThrowsAsyncMethodInvocationImposterGroupContinuation Throws(ThrowsAsyncExceptionGeneratorDelegate exceptionGenerator);
            IThrowsAsyncMethodInvocationImposterGroupContinuation Returns(ThrowsAsyncDelegate resultGenerator);
            IThrowsAsyncMethodInvocationImposterGroupContinuation Returns(global::System.Threading.Tasks.Task value);
            IThrowsAsyncMethodInvocationImposterGroupContinuation ThrowsAsync(System.Exception exception);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ThrowsAsyncInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // Task IMethodOperationNameCollisionTarget.ThrowsAsync()
        public interface IThrowsAsyncMethodImposterBuilder : IThrowsAsyncMethodInvocationImposterGroup, IThrowsAsyncMethodInvocationImposterGroupCallback, ThrowsAsyncInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ThrowsAsyncMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ThrowsAsyncMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ThrowsAsyncMethodInvocationImposterGroup>();
            private readonly ThrowsAsyncMethodInvocationHistoryCollection _throwsAsyncMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ThrowsAsyncMethodImposter(ThrowsAsyncMethodInvocationHistoryCollection _throwsAsyncMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._throwsAsyncMethodInvocationHistoryCollection = _throwsAsyncMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private ThrowsAsyncMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
            {
                if (_invocationImposters.TryPeek(out var invocationImposterGroup))
                    return invocationImposterGroup;
                else
                    return null;
            }

            public global::System.Threading.Tasks.Task Invoke()
            {
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup();
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("Task IMethodOperationNameCollisionTarget.ThrowsAsync()");
                    }

                    matchingInvocationImposterGroup = ThrowsAsyncMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "Task IMethodOperationNameCollisionTarget.ThrowsAsync()");
                    _throwsAsyncMethodInvocationHistoryCollection.Add(new ThrowsAsyncMethodInvocationHistory(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _throwsAsyncMethodInvocationHistoryCollection.Add(new ThrowsAsyncMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IThrowsAsyncMethodImposterBuilder, IThrowsAsyncMethodInvocationImposterGroupContinuation
            {
                private readonly ThrowsAsyncMethodImposter _imposter;
                private readonly ThrowsAsyncMethodInvocationHistoryCollection _throwsAsyncMethodInvocationHistoryCollection;
                private readonly ThrowsAsyncMethodInvocationImposterGroup _invocationImposterGroup;
                private ThrowsAsyncMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ThrowsAsyncMethodImposter _imposter, ThrowsAsyncMethodInvocationHistoryCollection _throwsAsyncMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._throwsAsyncMethodInvocationHistoryCollection = _throwsAsyncMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new ThrowsAsyncMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IThrowsAsyncMethodInvocationImposterGroupContinuation IThrowsAsyncMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IThrowsAsyncMethodInvocationImposterGroupContinuation IThrowsAsyncMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IThrowsAsyncMethodInvocationImposterGroupContinuation IThrowsAsyncMethodInvocationImposterGroup.Throws(ThrowsAsyncExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IThrowsAsyncMethodInvocationImposterGroupContinuation IThrowsAsyncMethodInvocationImposterGroupCallback.Callback(ThrowsAsyncCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IThrowsAsyncMethodInvocationImposterGroupContinuation IThrowsAsyncMethodInvocationImposterGroup.Returns(ThrowsAsyncDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IThrowsAsyncMethodInvocationImposterGroupContinuation IThrowsAsyncMethodInvocationImposterGroup.Returns(global::System.Threading.Tasks.Task value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IThrowsAsyncMethodInvocationImposterGroupContinuation IThrowsAsyncMethodInvocationImposterGroup.ThrowsAsync(System.Exception exception)
                {
                    _currentInvocationImposter.ThrowsAsync(exception);
                    return this;
                }

                IThrowsAsyncMethodInvocationImposterGroup IThrowsAsyncMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ThrowsAsyncInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _throwsAsyncMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodOperationNameCollisionTarget.UseBaseImplementation()
        public delegate void UseBaseImplementationDelegate();
        // void IMethodOperationNameCollisionTarget.UseBaseImplementation()
        public delegate void UseBaseImplementationCallbackDelegate();
        // void IMethodOperationNameCollisionTarget.UseBaseImplementation()
        public delegate System.Exception UseBaseImplementationExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class UseBaseImplementationMethodInvocationHistory : IUseBaseImplementationMethodInvocationHistory
        {
            internal System.Exception Exception;
            public UseBaseImplementationMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class UseBaseImplementationMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IUseBaseImplementationMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IUseBaseImplementationMethodInvocationHistory>();
            internal void Add(IUseBaseImplementationMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodOperationNameCollisionTarget.UseBaseImplementation()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class UseBaseImplementationMethodInvocationImposterGroup
        {
            internal static UseBaseImplementationMethodInvocationImposterGroup Default = new UseBaseImplementationMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public UseBaseImplementationMethodInvocationImposterGroup()
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

                private UseBaseImplementationDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<UseBaseImplementationCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<UseBaseImplementationCallbackDelegate>();
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

                internal void Callback(UseBaseImplementationCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(UseBaseImplementationExceptionGeneratorDelegate exceptionGenerator)
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
        public interface IUseBaseImplementationMethodInvocationImposterGroupCallback
        {
            IUseBaseImplementationMethodInvocationImposterGroupContinuation Callback(UseBaseImplementationCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationMethodInvocationImposterGroupContinuation : IUseBaseImplementationMethodInvocationImposterGroupCallback
        {
            IUseBaseImplementationMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IUseBaseImplementationMethodInvocationImposterGroup : IUseBaseImplementationMethodInvocationImposterGroupCallback
        {
            IUseBaseImplementationMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IUseBaseImplementationMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IUseBaseImplementationMethodInvocationImposterGroupContinuation Throws(UseBaseImplementationExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface UseBaseImplementationInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodOperationNameCollisionTarget.UseBaseImplementation()
        public interface IUseBaseImplementationMethodImposterBuilder : IUseBaseImplementationMethodInvocationImposterGroup, IUseBaseImplementationMethodInvocationImposterGroupCallback, UseBaseImplementationInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class UseBaseImplementationMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<UseBaseImplementationMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<UseBaseImplementationMethodInvocationImposterGroup>();
            private readonly UseBaseImplementationMethodInvocationHistoryCollection _useBaseImplementationMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public UseBaseImplementationMethodImposter(UseBaseImplementationMethodInvocationHistoryCollection _useBaseImplementationMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._useBaseImplementationMethodInvocationHistoryCollection = _useBaseImplementationMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private UseBaseImplementationMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodOperationNameCollisionTarget.UseBaseImplementation()");
                    }

                    matchingInvocationImposterGroup = UseBaseImplementationMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodOperationNameCollisionTarget.UseBaseImplementation()");
                    _useBaseImplementationMethodInvocationHistoryCollection.Add(new UseBaseImplementationMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    _useBaseImplementationMethodInvocationHistoryCollection.Add(new UseBaseImplementationMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IUseBaseImplementationMethodImposterBuilder, IUseBaseImplementationMethodInvocationImposterGroupContinuation
            {
                private readonly UseBaseImplementationMethodImposter _imposter;
                private readonly UseBaseImplementationMethodInvocationHistoryCollection _useBaseImplementationMethodInvocationHistoryCollection;
                private readonly UseBaseImplementationMethodInvocationImposterGroup _invocationImposterGroup;
                private UseBaseImplementationMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(UseBaseImplementationMethodImposter _imposter, UseBaseImplementationMethodInvocationHistoryCollection _useBaseImplementationMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._useBaseImplementationMethodInvocationHistoryCollection = _useBaseImplementationMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new UseBaseImplementationMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IUseBaseImplementationMethodInvocationImposterGroupContinuation IUseBaseImplementationMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IUseBaseImplementationMethodInvocationImposterGroupContinuation IUseBaseImplementationMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IUseBaseImplementationMethodInvocationImposterGroupContinuation IUseBaseImplementationMethodInvocationImposterGroup.Throws(UseBaseImplementationExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IUseBaseImplementationMethodInvocationImposterGroupContinuation IUseBaseImplementationMethodInvocationImposterGroupCallback.Callback(UseBaseImplementationCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IUseBaseImplementationMethodInvocationImposterGroup IUseBaseImplementationMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void UseBaseImplementationInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _useBaseImplementationMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodOperationNameCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._returnsMethodImposter = new ReturnsMethodImposter(_returnsMethodInvocationHistoryCollection, invocationBehavior);
            this._returnsAsyncMethodImposter = new ReturnsAsyncMethodImposter(_returnsAsyncMethodInvocationHistoryCollection, invocationBehavior);
            this._throwsMethodImposter = new ThrowsMethodImposter(_throwsMethodInvocationHistoryCollection, invocationBehavior);
            this._throwsAsyncMethodImposter = new ThrowsAsyncMethodImposter(_throwsAsyncMethodInvocationHistoryCollection, invocationBehavior);
            this._callbackMethodImposter = new CallbackMethodImposter(_callbackMethodInvocationHistoryCollection, invocationBehavior);
            this._thenMethodImposter = new ThenMethodImposter(_thenMethodInvocationHistoryCollection, invocationBehavior);
            this._useBaseImplementationMethodImposter = new UseBaseImplementationMethodImposter(_useBaseImplementationMethodInvocationHistoryCollection, invocationBehavior);
            this._defaultResultGeneratorMethodImposter = new DefaultResultGeneratorMethodImposter(_defaultResultGeneratorMethodInvocationHistoryCollection, invocationBehavior);
            this._defaultMethodImposter = new DefaultMethodImposter(_defaultMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodOperationNameCollisionTarget
        {
            IMethodOperationNameCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodOperationNameCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int Returns()
            {
                return _imposter._returnsMethodImposter.Invoke();
            }

            public global::System.Threading.Tasks.Task<int> ReturnsAsync()
            {
                return _imposter._returnsAsyncMethodImposter.Invoke();
            }

            public void Throws()
            {
                _imposter._throwsMethodImposter.Invoke();
            }

            public global::System.Threading.Tasks.Task ThrowsAsync()
            {
                return _imposter._throwsAsyncMethodImposter.Invoke();
            }

            public void Callback()
            {
                _imposter._callbackMethodImposter.Invoke();
            }

            public void Then()
            {
                _imposter._thenMethodImposter.Invoke();
            }

            public void UseBaseImplementation()
            {
                _imposter._useBaseImplementationMethodImposter.Invoke();
            }

            public int DefaultResultGenerator()
            {
                return _imposter._defaultResultGeneratorMethodImposter.Invoke();
            }

            public int Default()
            {
                return _imposter._defaultMethodImposter.Invoke();
            }
        }
    }
}
#pragma warning restore nullable
