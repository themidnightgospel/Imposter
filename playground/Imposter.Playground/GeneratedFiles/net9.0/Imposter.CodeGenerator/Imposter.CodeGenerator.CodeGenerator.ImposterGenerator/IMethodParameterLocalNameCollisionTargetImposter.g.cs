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
    public class IMethodParameterLocalNameCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterLocalNameCollisionTarget>
    {
        private readonly InvokeWithLocalsMethodImposter _invokeWithLocalsMethodImposter;
        private readonly InvokeWithLocalsMethodInvocationHistoryCollection _invokeWithLocalsMethodInvocationHistoryCollection = new InvokeWithLocalsMethodInvocationHistoryCollection();
        public IInvokeWithLocalsMethodImposterBuilder InvokeWithLocals(global::Imposter.Abstractions.Arg<int> ex, global::Imposter.Abstractions.Arg<int> result, global::Imposter.Abstractions.Arg<int> matchingInvocationImposterGroup, global::Imposter.Abstractions.Arg<int> arguments, global::Imposter.Abstractions.Arg<int> baseImplementation)
        {
            return new InvokeWithLocalsMethodImposter.Builder(_invokeWithLocalsMethodImposter, _invokeWithLocalsMethodInvocationHistoryCollection, new InvokeWithLocalsArgumentsCriteria(ex, result, matchingInvocationImposterGroup, arguments, baseImplementation));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodParameterLocalNameCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterLocalNameCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // int IMethodParameterLocalNameCollisionTarget.InvokeWithLocals(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
        public delegate int InvokeWithLocalsDelegate(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation);
        // int IMethodParameterLocalNameCollisionTarget.InvokeWithLocals(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
        public delegate void InvokeWithLocalsCallbackDelegate(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation);
        // int IMethodParameterLocalNameCollisionTarget.InvokeWithLocals(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
        public delegate System.Exception InvokeWithLocalsExceptionGeneratorDelegate(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class InvokeWithLocalsArguments
        {
            public int ex;
            public int result;
            public int matchingInvocationImposterGroup;
            public int arguments;
            public int baseImplementation;
            internal InvokeWithLocalsArguments(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
            {
                this.ex = ex;
                this.result = result;
                this.matchingInvocationImposterGroup = matchingInvocationImposterGroup;
                this.arguments = arguments;
                this.baseImplementation = baseImplementation;
            }
        }

        // int IMethodParameterLocalNameCollisionTarget.InvokeWithLocals(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class InvokeWithLocalsArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> ex { get; }
            public global::Imposter.Abstractions.Arg<int> result { get; }
            public global::Imposter.Abstractions.Arg<int> matchingInvocationImposterGroup { get; }
            public global::Imposter.Abstractions.Arg<int> arguments { get; }
            public global::Imposter.Abstractions.Arg<int> baseImplementation { get; }

            public InvokeWithLocalsArgumentsCriteria(global::Imposter.Abstractions.Arg<int> ex, global::Imposter.Abstractions.Arg<int> result, global::Imposter.Abstractions.Arg<int> matchingInvocationImposterGroup, global::Imposter.Abstractions.Arg<int> arguments, global::Imposter.Abstractions.Arg<int> baseImplementation)
            {
                this.ex = ex;
                this.result = result;
                this.matchingInvocationImposterGroup = matchingInvocationImposterGroup;
                this.arguments = arguments;
                this.baseImplementation = baseImplementation;
            }

            public bool Matches(InvokeWithLocalsArguments arguments)
            {
                return (((ex.Matches(arguments.ex) && result.Matches(arguments.result)) && matchingInvocationImposterGroup.Matches(arguments.matchingInvocationImposterGroup)) && arguments.Matches(arguments.arguments)) && baseImplementation.Matches(arguments.baseImplementation);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInvokeWithLocalsMethodInvocationHistory
        {
            bool Matches(InvokeWithLocalsArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InvokeWithLocalsMethodInvocationHistory : IInvokeWithLocalsMethodInvocationHistory
        {
            internal InvokeWithLocalsArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public InvokeWithLocalsMethodInvocationHistory(InvokeWithLocalsArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(InvokeWithLocalsArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InvokeWithLocalsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IInvokeWithLocalsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IInvokeWithLocalsMethodInvocationHistory>();
            internal void Add(IInvokeWithLocalsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(InvokeWithLocalsArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodParameterLocalNameCollisionTarget.InvokeWithLocals(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class InvokeWithLocalsMethodInvocationImposterGroup
        {
            internal static InvokeWithLocalsMethodInvocationImposterGroup Default = new InvokeWithLocalsMethodInvocationImposterGroup(new InvokeWithLocalsArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any(), global::Imposter.Abstractions.Arg<int>.Any(), global::Imposter.Abstractions.Arg<int>.Any(), global::Imposter.Abstractions.Arg<int>.Any(), global::Imposter.Abstractions.Arg<int>.Any()));
            internal InvokeWithLocalsArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public InvokeWithLocalsMethodInvocationImposterGroup(InvokeWithLocalsArgumentsCriteria argumentsCriteria)
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

            public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, ex, result, matchingInvocationImposterGroup, arguments, baseImplementation);
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

                private InvokeWithLocalsDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<InvokeWithLocalsCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<InvokeWithLocalsCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result_1 = _resultGenerator.Invoke(ex, result, matchingInvocationImposterGroup, arguments, baseImplementation);
                    foreach (var callback in _callbacks)
                    {
                        callback(ex, result, matchingInvocationImposterGroup, arguments, baseImplementation);
                    }

                    return result_1;
                }

                internal void Callback(InvokeWithLocalsCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(InvokeWithLocalsDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation) =>
                    {
                        return value;
                    };
                }

                internal void Throws(InvokeWithLocalsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation) =>
                    {
                        throw exceptionGenerator(ex, result, matchingInvocationImposterGroup, arguments, baseImplementation);
                    };
                }

                internal static int DefaultResultGenerator(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInvokeWithLocalsMethodInvocationImposterGroupCallback
        {
            IInvokeWithLocalsMethodInvocationImposterGroupContinuation Callback(InvokeWithLocalsCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInvokeWithLocalsMethodInvocationImposterGroupContinuation : IInvokeWithLocalsMethodInvocationImposterGroupCallback
        {
            IInvokeWithLocalsMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInvokeWithLocalsMethodInvocationImposterGroup : IInvokeWithLocalsMethodInvocationImposterGroupCallback
        {
            IInvokeWithLocalsMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            IInvokeWithLocalsMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IInvokeWithLocalsMethodInvocationImposterGroupContinuation Throws(InvokeWithLocalsExceptionGeneratorDelegate exceptionGenerator);
            IInvokeWithLocalsMethodInvocationImposterGroupContinuation Returns(InvokeWithLocalsDelegate resultGenerator);
            IInvokeWithLocalsMethodInvocationImposterGroupContinuation Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface InvokeWithLocalsInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodParameterLocalNameCollisionTarget.InvokeWithLocals(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
        public interface IInvokeWithLocalsMethodImposterBuilder : IInvokeWithLocalsMethodInvocationImposterGroup, IInvokeWithLocalsMethodInvocationImposterGroupCallback, InvokeWithLocalsInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InvokeWithLocalsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<InvokeWithLocalsMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<InvokeWithLocalsMethodInvocationImposterGroup>();
            private readonly InvokeWithLocalsMethodInvocationHistoryCollection _invokeWithLocalsMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public InvokeWithLocalsMethodImposter(InvokeWithLocalsMethodInvocationHistoryCollection _invokeWithLocalsMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._invokeWithLocalsMethodInvocationHistoryCollection = _invokeWithLocalsMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(InvokeWithLocalsArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private InvokeWithLocalsMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(InvokeWithLocalsArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
            {
                var arguments_1 = new InvokeWithLocalsArguments(ex, result, matchingInvocationImposterGroup, arguments, baseImplementation);
                var matchingInvocationImposterGroup_1 = FindMatchingInvocationImposterGroup(arguments_1);
                if (matchingInvocationImposterGroup_1 == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("int IMethodParameterLocalNameCollisionTarget.InvokeWithLocals(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)");
                    }

                    matchingInvocationImposterGroup_1 = InvokeWithLocalsMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result_1 = matchingInvocationImposterGroup_1.Invoke(_invocationBehavior, "int IMethodParameterLocalNameCollisionTarget.InvokeWithLocals(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)", ex, result, matchingInvocationImposterGroup, arguments, baseImplementation);
                    _invokeWithLocalsMethodInvocationHistoryCollection.Add(new InvokeWithLocalsMethodInvocationHistory(arguments_1, result_1, default));
                    return result_1;
                }
                catch (System.Exception ex_1)
                {
                    _invokeWithLocalsMethodInvocationHistoryCollection.Add(new InvokeWithLocalsMethodInvocationHistory(arguments_1, default, ex_1));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInvokeWithLocalsMethodImposterBuilder, IInvokeWithLocalsMethodInvocationImposterGroupContinuation
            {
                private readonly InvokeWithLocalsMethodImposter _imposter;
                private readonly InvokeWithLocalsMethodInvocationHistoryCollection _invokeWithLocalsMethodInvocationHistoryCollection;
                private readonly InvokeWithLocalsArgumentsCriteria _argumentsCriteria;
                private readonly InvokeWithLocalsMethodInvocationImposterGroup _invocationImposterGroup;
                private InvokeWithLocalsMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(InvokeWithLocalsMethodImposter _imposter, InvokeWithLocalsMethodInvocationHistoryCollection _invokeWithLocalsMethodInvocationHistoryCollection, InvokeWithLocalsArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._invokeWithLocalsMethodInvocationHistoryCollection = _invokeWithLocalsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new InvokeWithLocalsMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IInvokeWithLocalsMethodInvocationImposterGroupContinuation IInvokeWithLocalsMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IInvokeWithLocalsMethodInvocationImposterGroupContinuation IInvokeWithLocalsMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IInvokeWithLocalsMethodInvocationImposterGroupContinuation IInvokeWithLocalsMethodInvocationImposterGroup.Throws(InvokeWithLocalsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation) =>
                    {
                        throw exceptionGenerator.Invoke(ex, result, matchingInvocationImposterGroup, arguments, baseImplementation);
                    });
                    return this;
                }

                IInvokeWithLocalsMethodInvocationImposterGroupContinuation IInvokeWithLocalsMethodInvocationImposterGroupCallback.Callback(InvokeWithLocalsCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IInvokeWithLocalsMethodInvocationImposterGroupContinuation IInvokeWithLocalsMethodInvocationImposterGroup.Returns(InvokeWithLocalsDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IInvokeWithLocalsMethodInvocationImposterGroupContinuation IInvokeWithLocalsMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IInvokeWithLocalsMethodInvocationImposterGroup IInvokeWithLocalsMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void InvokeWithLocalsInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _invokeWithLocalsMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodParameterLocalNameCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._invokeWithLocalsMethodImposter = new InvokeWithLocalsMethodImposter(_invokeWithLocalsMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodParameterLocalNameCollisionTarget
        {
            IMethodParameterLocalNameCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodParameterLocalNameCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int InvokeWithLocals(int ex, int result, int matchingInvocationImposterGroup, int arguments, int baseImplementation)
            {
                return _imposter._invokeWithLocalsMethodImposter.Invoke(ex, result, matchingInvocationImposterGroup, arguments, baseImplementation);
            }
        }
    }
}
#pragma warning restore nullable
