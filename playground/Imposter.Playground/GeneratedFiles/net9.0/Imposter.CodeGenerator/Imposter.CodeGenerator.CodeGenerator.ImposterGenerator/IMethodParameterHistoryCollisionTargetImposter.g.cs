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
    public class IMethodParameterHistoryCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterHistoryCollisionTarget>
    {
        private readonly HistoryNamesMethodImposter _historyNamesMethodImposter;
        private readonly HistoryNamesMethodInvocationHistoryCollection _historyNamesMethodInvocationHistoryCollection = new HistoryNamesMethodInvocationHistoryCollection();
        public IHistoryNamesMethodImposterBuilder HistoryNames(global::Imposter.Abstractions.Arg<int> Arguments, global::Imposter.Abstractions.OutArg<int> Result, global::Imposter.Abstractions.OutArg<global::System.Exception> Exception)
        {
            return new HistoryNamesMethodImposter.Builder(_historyNamesMethodImposter, _historyNamesMethodInvocationHistoryCollection, new HistoryNamesArgumentsCriteria(Arguments, Result, Exception));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodParameterHistoryCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterHistoryCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // void IMethodParameterHistoryCollisionTarget.HistoryNames(ref int Arguments, out int Result, out Exception Exception)
        public delegate void HistoryNamesDelegate(ref int Arguments, out int Result, out global::System.Exception Exception);
        // void IMethodParameterHistoryCollisionTarget.HistoryNames(ref int Arguments, out int Result, out Exception Exception)
        public delegate void HistoryNamesCallbackDelegate(ref int Arguments, out int Result, out global::System.Exception Exception);
        // void IMethodParameterHistoryCollisionTarget.HistoryNames(ref int Arguments, out int Result, out Exception Exception)
        public delegate System.Exception HistoryNamesExceptionGeneratorDelegate(ref int Arguments, out int Result, out global::System.Exception Exception);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class HistoryNamesArguments
        {
            public int Arguments;
            internal HistoryNamesArguments(int Arguments)
            {
                this.Arguments = Arguments;
            }
        }

        // void IMethodParameterHistoryCollisionTarget.HistoryNames(ref int Arguments, out int Result, out Exception Exception)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class HistoryNamesArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> Arguments { get; }
            public global::Imposter.Abstractions.OutArg<int> Result { get; }
            public global::Imposter.Abstractions.OutArg<global::System.Exception> Exception { get; }

            public HistoryNamesArgumentsCriteria(global::Imposter.Abstractions.Arg<int> Arguments, global::Imposter.Abstractions.OutArg<int> Result, global::Imposter.Abstractions.OutArg<global::System.Exception> Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(HistoryNamesArguments arguments)
            {
                return Arguments.Matches(arguments.Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IHistoryNamesMethodInvocationHistory
        {
            bool Matches(HistoryNamesArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class HistoryNamesMethodInvocationHistory : IHistoryNamesMethodInvocationHistory
        {
            internal HistoryNamesArguments Arguments;
            internal System.Exception Exception;
            public HistoryNamesMethodInvocationHistory(HistoryNamesArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(HistoryNamesArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class HistoryNamesMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IHistoryNamesMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IHistoryNamesMethodInvocationHistory>();
            internal void Add(IHistoryNamesMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(HistoryNamesArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodParameterHistoryCollisionTarget.HistoryNames(ref int Arguments, out int Result, out Exception Exception)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class HistoryNamesMethodInvocationImposterGroup
        {
            internal static HistoryNamesMethodInvocationImposterGroup Default = new HistoryNamesMethodInvocationImposterGroup(new HistoryNamesArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any(), global::Imposter.Abstractions.OutArg<int>.Any(), global::Imposter.Abstractions.OutArg<global::System.Exception>.Any()));
            internal HistoryNamesArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public HistoryNamesMethodInvocationImposterGroup(HistoryNamesArgumentsCriteria argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, ref int Arguments, out int Result, out global::System.Exception Exception)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, ref Arguments, out Result, out Exception);
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

                private HistoryNamesDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<HistoryNamesCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<HistoryNamesCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, ref int Arguments, out int Result, out global::System.Exception Exception)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(ref Arguments, out Result, out Exception);
                    foreach (var callback in _callbacks)
                    {
                        callback(ref Arguments, out Result, out Exception);
                    }
                }

                internal void Callback(HistoryNamesCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(HistoryNamesExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (ref int Arguments, out int Result, out global::System.Exception Exception) =>
                    {
                        throw exceptionGenerator(ref Arguments, out Result, out Exception);
                    };
                }

                private static void InitializeOutParametersWithDefaultValues(out int Result, out global::System.Exception Exception)
                {
                    Result = default(int);
                    Exception = default(global::System.Exception);
                }

                internal static void DefaultResultGenerator(ref int Arguments, out int Result, out global::System.Exception Exception)
                {
                    InitializeOutParametersWithDefaultValues(out Result, out Exception);
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IHistoryNamesMethodInvocationImposterGroupCallback
        {
            IHistoryNamesMethodInvocationImposterGroupContinuation Callback(HistoryNamesCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IHistoryNamesMethodInvocationImposterGroupContinuation : IHistoryNamesMethodInvocationImposterGroupCallback
        {
            IHistoryNamesMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IHistoryNamesMethodInvocationImposterGroup : IHistoryNamesMethodInvocationImposterGroupCallback
        {
            IHistoryNamesMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IHistoryNamesMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IHistoryNamesMethodInvocationImposterGroupContinuation Throws(HistoryNamesExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface HistoryNamesInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodParameterHistoryCollisionTarget.HistoryNames(ref int Arguments, out int Result, out Exception Exception)
        public interface IHistoryNamesMethodImposterBuilder : IHistoryNamesMethodInvocationImposterGroup, IHistoryNamesMethodInvocationImposterGroupCallback, HistoryNamesInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class HistoryNamesMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<HistoryNamesMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<HistoryNamesMethodInvocationImposterGroup>();
            private readonly HistoryNamesMethodInvocationHistoryCollection _historyNamesMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public HistoryNamesMethodImposter(HistoryNamesMethodInvocationHistoryCollection _historyNamesMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._historyNamesMethodInvocationHistoryCollection = _historyNamesMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            private static void InitializeOutParametersWithDefaultValues(out int Result, out global::System.Exception Exception)
            {
                Result = default(int);
                Exception = default(global::System.Exception);
            }

            public bool HasMatchingSetup(HistoryNamesArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private HistoryNamesMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(HistoryNamesArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(ref int Arguments, out int Result, out global::System.Exception Exception)
            {
                var arguments = new HistoryNamesArguments(Arguments);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodParameterHistoryCollisionTarget.HistoryNames(ref int Arguments, out int Result, out Exception Exception)");
                    }

                    matchingInvocationImposterGroup = HistoryNamesMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodParameterHistoryCollisionTarget.HistoryNames(ref int Arguments, out int Result, out Exception Exception)", ref Arguments, out Result, out Exception);
                    _historyNamesMethodInvocationHistoryCollection.Add(new HistoryNamesMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _historyNamesMethodInvocationHistoryCollection.Add(new HistoryNamesMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IHistoryNamesMethodImposterBuilder, IHistoryNamesMethodInvocationImposterGroupContinuation
            {
                private readonly HistoryNamesMethodImposter _imposter;
                private readonly HistoryNamesMethodInvocationHistoryCollection _historyNamesMethodInvocationHistoryCollection;
                private readonly HistoryNamesArgumentsCriteria _argumentsCriteria;
                private readonly HistoryNamesMethodInvocationImposterGroup _invocationImposterGroup;
                private HistoryNamesMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(HistoryNamesMethodImposter _imposter, HistoryNamesMethodInvocationHistoryCollection _historyNamesMethodInvocationHistoryCollection, HistoryNamesArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._historyNamesMethodInvocationHistoryCollection = _historyNamesMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new HistoryNamesMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IHistoryNamesMethodInvocationImposterGroupContinuation IHistoryNamesMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((ref int Arguments, out int Result, out global::System.Exception Exception) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IHistoryNamesMethodInvocationImposterGroupContinuation IHistoryNamesMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((ref int Arguments, out int Result, out global::System.Exception Exception) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IHistoryNamesMethodInvocationImposterGroupContinuation IHistoryNamesMethodInvocationImposterGroup.Throws(HistoryNamesExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((ref int Arguments, out int Result, out global::System.Exception Exception) =>
                    {
                        throw exceptionGenerator.Invoke(ref Arguments, out Result, out Exception);
                    });
                    return this;
                }

                IHistoryNamesMethodInvocationImposterGroupContinuation IHistoryNamesMethodInvocationImposterGroupCallback.Callback(HistoryNamesCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IHistoryNamesMethodInvocationImposterGroup IHistoryNamesMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void HistoryNamesInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _historyNamesMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodParameterHistoryCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._historyNamesMethodImposter = new HistoryNamesMethodImposter(_historyNamesMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodParameterHistoryCollisionTarget
        {
            IMethodParameterHistoryCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodParameterHistoryCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public void HistoryNames(ref int Arguments, out int Result, out global::System.Exception Exception)
            {
                _imposter._historyNamesMethodImposter.Invoke(ref Arguments, out Result, out Exception);
            }
        }
    }
}
#pragma warning restore nullable
