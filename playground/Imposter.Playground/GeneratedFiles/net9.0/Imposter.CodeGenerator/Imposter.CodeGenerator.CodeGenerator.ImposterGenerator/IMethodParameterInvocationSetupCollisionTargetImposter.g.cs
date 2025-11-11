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
    public class IMethodParameterInvocationSetupCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterInvocationSetupCollisionTarget>
    {
        private readonly SetupNamesMethodImposter _setupNamesMethodImposter;
        private readonly SetupNamesMethodInvocationHistoryCollection _setupNamesMethodInvocationHistoryCollection = new SetupNamesMethodInvocationHistoryCollection();
        public ISetupNamesMethodImposterBuilder SetupNames(global::Imposter.Abstractions.Arg<int> value, global::Imposter.Abstractions.Arg<global::System.Func<int>> resultGenerator, global::Imposter.Abstractions.Arg<global::System.Exception> exception, global::Imposter.Abstractions.Arg<global::System.Func<global::System.Exception>> exceptionGenerator, global::Imposter.Abstractions.Arg<global::System.Action> callback)
        {
            return new SetupNamesMethodImposter.Builder(_setupNamesMethodImposter, _setupNamesMethodInvocationHistoryCollection, new SetupNamesArgumentsCriteria(value, resultGenerator, exception, exceptionGenerator, callback));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodParameterInvocationSetupCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterInvocationSetupCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // int IMethodParameterInvocationSetupCollisionTarget.SetupNames(int value, Func<int> resultGenerator, Exception exception, Func<Exception> exceptionGenerator, Action callback)
        public delegate int SetupNamesDelegate(int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback);
        // int IMethodParameterInvocationSetupCollisionTarget.SetupNames(int value, Func<int> resultGenerator, Exception exception, Func<Exception> exceptionGenerator, Action callback)
        public delegate void SetupNamesCallbackDelegate(int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback);
        // int IMethodParameterInvocationSetupCollisionTarget.SetupNames(int value, Func<int> resultGenerator, Exception exception, Func<Exception> exceptionGenerator, Action callback)
        public delegate System.Exception SetupNamesExceptionGeneratorDelegate(int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class SetupNamesArguments
        {
            public int value;
            public global::System.Func<int> resultGenerator;
            public global::System.Exception exception;
            public global::System.Func<global::System.Exception> exceptionGenerator;
            public global::System.Action callback;
            internal SetupNamesArguments(int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback)
            {
                this.value = value;
                this.resultGenerator = resultGenerator;
                this.exception = exception;
                this.exceptionGenerator = exceptionGenerator;
                this.callback = callback;
            }
        }

        // int IMethodParameterInvocationSetupCollisionTarget.SetupNames(int value, Func<int> resultGenerator, Exception exception, Func<Exception> exceptionGenerator, Action callback)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class SetupNamesArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> value { get; }
            public global::Imposter.Abstractions.Arg<global::System.Func<int>> resultGenerator { get; }
            public global::Imposter.Abstractions.Arg<global::System.Exception> exception { get; }
            public global::Imposter.Abstractions.Arg<global::System.Func<global::System.Exception>> exceptionGenerator { get; }
            public global::Imposter.Abstractions.Arg<global::System.Action> callback { get; }

            public SetupNamesArgumentsCriteria(global::Imposter.Abstractions.Arg<int> value, global::Imposter.Abstractions.Arg<global::System.Func<int>> resultGenerator, global::Imposter.Abstractions.Arg<global::System.Exception> exception, global::Imposter.Abstractions.Arg<global::System.Func<global::System.Exception>> exceptionGenerator, global::Imposter.Abstractions.Arg<global::System.Action> callback)
            {
                this.value = value;
                this.resultGenerator = resultGenerator;
                this.exception = exception;
                this.exceptionGenerator = exceptionGenerator;
                this.callback = callback;
            }

            public bool Matches(SetupNamesArguments arguments)
            {
                return (((value.Matches(arguments.value) && resultGenerator.Matches(arguments.resultGenerator)) && exception.Matches(arguments.exception)) && exceptionGenerator.Matches(arguments.exceptionGenerator)) && callback.Matches(arguments.callback);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISetupNamesMethodInvocationHistory
        {
            bool Matches(SetupNamesArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SetupNamesMethodInvocationHistory : ISetupNamesMethodInvocationHistory
        {
            internal SetupNamesArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public SetupNamesMethodInvocationHistory(SetupNamesArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(SetupNamesArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SetupNamesMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ISetupNamesMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ISetupNamesMethodInvocationHistory>();
            internal void Add(ISetupNamesMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(SetupNamesArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodParameterInvocationSetupCollisionTarget.SetupNames(int value, Func<int> resultGenerator, Exception exception, Func<Exception> exceptionGenerator, Action callback)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class SetupNamesMethodInvocationImposterGroup
        {
            internal static SetupNamesMethodInvocationImposterGroup Default = new SetupNamesMethodInvocationImposterGroup(new SetupNamesArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any(), global::Imposter.Abstractions.Arg<global::System.Func<int>>.Any(), global::Imposter.Abstractions.Arg<global::System.Exception>.Any(), global::Imposter.Abstractions.Arg<global::System.Func<global::System.Exception>>.Any(), global::Imposter.Abstractions.Arg<global::System.Action>.Any()));
            internal SetupNamesArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public SetupNamesMethodInvocationImposterGroup(SetupNamesArgumentsCriteria argumentsCriteria)
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

            public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, value, resultGenerator, exception, exceptionGenerator, callback);
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

                private SetupNamesDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<SetupNamesCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<SetupNamesCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke(value, resultGenerator, exception, exceptionGenerator, callback);
                    foreach (var callback_1 in _callbacks)
                    {
                        callback_1(value, resultGenerator, exception, exceptionGenerator, callback);
                    }

                    return result;
                }

                internal void Callback(SetupNamesCallbackDelegate callback_1)
                {
                    _callbacks.Enqueue(callback_1);
                }

                internal void Returns(SetupNamesDelegate resultGenerator_1)
                {
                    _resultGenerator = resultGenerator_1;
                }

                internal void Returns(int value_1)
                {
                    _resultGenerator = (int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback) =>
                    {
                        return value_1;
                    };
                }

                internal void Throws(SetupNamesExceptionGeneratorDelegate exceptionGenerator_1)
                {
                    _resultGenerator = (int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback) =>
                    {
                        throw exceptionGenerator_1(value, resultGenerator, exception, exceptionGenerator, callback);
                    };
                }

                internal static int DefaultResultGenerator(int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISetupNamesMethodInvocationImposterGroupCallback
        {
            ISetupNamesMethodInvocationImposterGroupContinuation Callback(SetupNamesCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISetupNamesMethodInvocationImposterGroupContinuation : ISetupNamesMethodInvocationImposterGroupCallback
        {
            ISetupNamesMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISetupNamesMethodInvocationImposterGroup : ISetupNamesMethodInvocationImposterGroupCallback
        {
            ISetupNamesMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            ISetupNamesMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            ISetupNamesMethodInvocationImposterGroupContinuation Throws(SetupNamesExceptionGeneratorDelegate exceptionGenerator);
            ISetupNamesMethodInvocationImposterGroupContinuation Returns(SetupNamesDelegate resultGenerator);
            ISetupNamesMethodInvocationImposterGroupContinuation Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface SetupNamesInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodParameterInvocationSetupCollisionTarget.SetupNames(int value, Func<int> resultGenerator, Exception exception, Func<Exception> exceptionGenerator, Action callback)
        public interface ISetupNamesMethodImposterBuilder : ISetupNamesMethodInvocationImposterGroup, ISetupNamesMethodInvocationImposterGroupCallback, SetupNamesInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SetupNamesMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<SetupNamesMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<SetupNamesMethodInvocationImposterGroup>();
            private readonly SetupNamesMethodInvocationHistoryCollection _setupNamesMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public SetupNamesMethodImposter(SetupNamesMethodInvocationHistoryCollection _setupNamesMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._setupNamesMethodInvocationHistoryCollection = _setupNamesMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(SetupNamesArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private SetupNamesMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(SetupNamesArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback)
            {
                var arguments = new SetupNamesArguments(value, resultGenerator, exception, exceptionGenerator, callback);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("int IMethodParameterInvocationSetupCollisionTarget.SetupNames(int value, Func<int> resultGenerator, Exception exception, Func<Exception> exceptionGenerator, Action callback)");
                    }

                    matchingInvocationImposterGroup = SetupNamesMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "int IMethodParameterInvocationSetupCollisionTarget.SetupNames(int value, Func<int> resultGenerator, Exception exception, Func<Exception> exceptionGenerator, Action callback)", value, resultGenerator, exception, exceptionGenerator, callback);
                    _setupNamesMethodInvocationHistoryCollection.Add(new SetupNamesMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _setupNamesMethodInvocationHistoryCollection.Add(new SetupNamesMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ISetupNamesMethodImposterBuilder, ISetupNamesMethodInvocationImposterGroupContinuation
            {
                private readonly SetupNamesMethodImposter _imposter;
                private readonly SetupNamesMethodInvocationHistoryCollection _setupNamesMethodInvocationHistoryCollection;
                private readonly SetupNamesArgumentsCriteria _argumentsCriteria;
                private readonly SetupNamesMethodInvocationImposterGroup _invocationImposterGroup;
                private SetupNamesMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(SetupNamesMethodImposter _imposter, SetupNamesMethodInvocationHistoryCollection _setupNamesMethodInvocationHistoryCollection, SetupNamesArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._setupNamesMethodInvocationHistoryCollection = _setupNamesMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new SetupNamesMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ISetupNamesMethodInvocationImposterGroupContinuation ISetupNamesMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ISetupNamesMethodInvocationImposterGroupContinuation ISetupNamesMethodInvocationImposterGroup.Throws(System.Exception exception_1)
                {
                    _currentInvocationImposter.Throws((int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback) =>
                    {
                        throw exception_1;
                    });
                    return this;
                }

                ISetupNamesMethodInvocationImposterGroupContinuation ISetupNamesMethodInvocationImposterGroup.Throws(SetupNamesExceptionGeneratorDelegate exceptionGenerator_1)
                {
                    _currentInvocationImposter.Throws((int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback) =>
                    {
                        throw exceptionGenerator_1.Invoke(value, resultGenerator, exception, exceptionGenerator, callback);
                    });
                    return this;
                }

                ISetupNamesMethodInvocationImposterGroupContinuation ISetupNamesMethodInvocationImposterGroupCallback.Callback(SetupNamesCallbackDelegate callback_1)
                {
                    _currentInvocationImposter.Callback(callback_1);
                    return this;
                }

                ISetupNamesMethodInvocationImposterGroupContinuation ISetupNamesMethodInvocationImposterGroup.Returns(SetupNamesDelegate resultGenerator_1)
                {
                    _currentInvocationImposter.Returns(resultGenerator_1);
                    return this;
                }

                ISetupNamesMethodInvocationImposterGroupContinuation ISetupNamesMethodInvocationImposterGroup.Returns(int value_1)
                {
                    _currentInvocationImposter.Returns(value_1);
                    return this;
                }

                ISetupNamesMethodInvocationImposterGroup ISetupNamesMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void SetupNamesInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _setupNamesMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodParameterInvocationSetupCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._setupNamesMethodImposter = new SetupNamesMethodImposter(_setupNamesMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodParameterInvocationSetupCollisionTarget
        {
            IMethodParameterInvocationSetupCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodParameterInvocationSetupCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int SetupNames(int value, global::System.Func<int> resultGenerator, global::System.Exception exception, global::System.Func<global::System.Exception> exceptionGenerator, global::System.Action callback)
            {
                return _imposter._setupNamesMethodImposter.Invoke(value, resultGenerator, exception, exceptionGenerator, callback);
            }
        }
    }
}
#pragma warning restore nullable
