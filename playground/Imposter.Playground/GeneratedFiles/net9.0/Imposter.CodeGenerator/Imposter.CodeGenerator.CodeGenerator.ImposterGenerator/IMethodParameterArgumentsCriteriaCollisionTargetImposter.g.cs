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
    public class IMethodParameterArgumentsCriteriaCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterArgumentsCriteriaCollisionTarget>
    {
        private readonly CriteriaNamesMethodImposter _criteriaNamesMethodImposter;
        private readonly CriteriaNamesMethodInvocationHistoryCollection _criteriaNamesMethodInvocationHistoryCollection = new CriteriaNamesMethodInvocationHistoryCollection();
        public ICriteriaNamesMethodImposterBuilder CriteriaNames(global::Imposter.Abstractions.Arg<int> As, global::Imposter.Abstractions.Arg<int> Matches)
        {
            return new CriteriaNamesMethodImposter.Builder(_criteriaNamesMethodImposter, _criteriaNamesMethodInvocationHistoryCollection, new CriteriaNamesArgumentsCriteria(As, Matches));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodParameterArgumentsCriteriaCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterArgumentsCriteriaCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // void IMethodParameterArgumentsCriteriaCollisionTarget.CriteriaNames(int As, int Matches)
        public delegate void CriteriaNamesDelegate(int As, int Matches);
        // void IMethodParameterArgumentsCriteriaCollisionTarget.CriteriaNames(int As, int Matches)
        public delegate void CriteriaNamesCallbackDelegate(int As, int Matches);
        // void IMethodParameterArgumentsCriteriaCollisionTarget.CriteriaNames(int As, int Matches)
        public delegate System.Exception CriteriaNamesExceptionGeneratorDelegate(int As, int Matches);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CriteriaNamesArguments
        {
            public int As;
            public int Matches;
            internal CriteriaNamesArguments(int As, int Matches)
            {
                this.As = As;
                this.Matches = Matches;
            }
        }

        // void IMethodParameterArgumentsCriteriaCollisionTarget.CriteriaNames(int As, int Matches)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CriteriaNamesArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> As { get; }
            public global::Imposter.Abstractions.Arg<int> Matches { get; }

            public CriteriaNamesArgumentsCriteria(global::Imposter.Abstractions.Arg<int> As, global::Imposter.Abstractions.Arg<int> Matches)
            {
                this.As = As;
                this.Matches = Matches;
            }

            public bool Matches_1(CriteriaNamesArguments arguments)
            {
                return As.Matches(arguments.As) && Matches.Matches(arguments.Matches);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICriteriaNamesMethodInvocationHistory
        {
            bool Matches(CriteriaNamesArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CriteriaNamesMethodInvocationHistory : ICriteriaNamesMethodInvocationHistory
        {
            internal CriteriaNamesArguments Arguments;
            internal System.Exception Exception;
            public CriteriaNamesMethodInvocationHistory(CriteriaNamesArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(CriteriaNamesArgumentsCriteria criteria)
            {
                return criteria.Matches_1(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CriteriaNamesMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ICriteriaNamesMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ICriteriaNamesMethodInvocationHistory>();
            internal void Add(ICriteriaNamesMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(CriteriaNamesArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodParameterArgumentsCriteriaCollisionTarget.CriteriaNames(int As, int Matches)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class CriteriaNamesMethodInvocationImposterGroup
        {
            internal static CriteriaNamesMethodInvocationImposterGroup Default = new CriteriaNamesMethodInvocationImposterGroup(new CriteriaNamesArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any(), global::Imposter.Abstractions.Arg<int>.Any()));
            internal CriteriaNamesArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public CriteriaNamesMethodInvocationImposterGroup(CriteriaNamesArgumentsCriteria argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int As, int Matches)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, As, Matches);
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

                private CriteriaNamesDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<CriteriaNamesCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<CriteriaNamesCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int As, int Matches)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(As, Matches);
                    foreach (var callback in _callbacks)
                    {
                        callback(As, Matches);
                    }
                }

                internal void Callback(CriteriaNamesCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(CriteriaNamesExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int As, int Matches) =>
                    {
                        throw exceptionGenerator(As, Matches);
                    };
                }

                internal static void DefaultResultGenerator(int As, int Matches)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICriteriaNamesMethodInvocationImposterGroupCallback
        {
            ICriteriaNamesMethodInvocationImposterGroupContinuation Callback(CriteriaNamesCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICriteriaNamesMethodInvocationImposterGroupContinuation : ICriteriaNamesMethodInvocationImposterGroupCallback
        {
            ICriteriaNamesMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICriteriaNamesMethodInvocationImposterGroup : ICriteriaNamesMethodInvocationImposterGroupCallback
        {
            ICriteriaNamesMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            ICriteriaNamesMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            ICriteriaNamesMethodInvocationImposterGroupContinuation Throws(CriteriaNamesExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CriteriaNamesInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodParameterArgumentsCriteriaCollisionTarget.CriteriaNames(int As, int Matches)
        public interface ICriteriaNamesMethodImposterBuilder : ICriteriaNamesMethodInvocationImposterGroup, ICriteriaNamesMethodInvocationImposterGroupCallback, CriteriaNamesInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CriteriaNamesMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CriteriaNamesMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<CriteriaNamesMethodInvocationImposterGroup>();
            private readonly CriteriaNamesMethodInvocationHistoryCollection _criteriaNamesMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public CriteriaNamesMethodImposter(CriteriaNamesMethodInvocationHistoryCollection _criteriaNamesMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._criteriaNamesMethodInvocationHistoryCollection = _criteriaNamesMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(CriteriaNamesArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private CriteriaNamesMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(CriteriaNamesArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches_1(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(int As, int Matches)
            {
                var arguments = new CriteriaNamesArguments(As, Matches);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodParameterArgumentsCriteriaCollisionTarget.CriteriaNames(int As, int Matches)");
                    }

                    matchingInvocationImposterGroup = CriteriaNamesMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodParameterArgumentsCriteriaCollisionTarget.CriteriaNames(int As, int Matches)", As, Matches);
                    _criteriaNamesMethodInvocationHistoryCollection.Add(new CriteriaNamesMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _criteriaNamesMethodInvocationHistoryCollection.Add(new CriteriaNamesMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ICriteriaNamesMethodImposterBuilder, ICriteriaNamesMethodInvocationImposterGroupContinuation
            {
                private readonly CriteriaNamesMethodImposter _imposter;
                private readonly CriteriaNamesMethodInvocationHistoryCollection _criteriaNamesMethodInvocationHistoryCollection;
                private readonly CriteriaNamesArgumentsCriteria _argumentsCriteria;
                private readonly CriteriaNamesMethodInvocationImposterGroup _invocationImposterGroup;
                private CriteriaNamesMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(CriteriaNamesMethodImposter _imposter, CriteriaNamesMethodInvocationHistoryCollection _criteriaNamesMethodInvocationHistoryCollection, CriteriaNamesArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._criteriaNamesMethodInvocationHistoryCollection = _criteriaNamesMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new CriteriaNamesMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ICriteriaNamesMethodInvocationImposterGroupContinuation ICriteriaNamesMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int As, int Matches) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ICriteriaNamesMethodInvocationImposterGroupContinuation ICriteriaNamesMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int As, int Matches) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ICriteriaNamesMethodInvocationImposterGroupContinuation ICriteriaNamesMethodInvocationImposterGroup.Throws(CriteriaNamesExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int As, int Matches) =>
                    {
                        throw exceptionGenerator.Invoke(As, Matches);
                    });
                    return this;
                }

                ICriteriaNamesMethodInvocationImposterGroupContinuation ICriteriaNamesMethodInvocationImposterGroupCallback.Callback(CriteriaNamesCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ICriteriaNamesMethodInvocationImposterGroup ICriteriaNamesMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void CriteriaNamesInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _criteriaNamesMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodParameterArgumentsCriteriaCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._criteriaNamesMethodImposter = new CriteriaNamesMethodImposter(_criteriaNamesMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodParameterArgumentsCriteriaCollisionTarget
        {
            IMethodParameterArgumentsCriteriaCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodParameterArgumentsCriteriaCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public void CriteriaNames(int As, int Matches)
            {
                _imposter._criteriaNamesMethodImposter.Invoke(As, Matches);
            }
        }
    }
}
#pragma warning restore nullable
