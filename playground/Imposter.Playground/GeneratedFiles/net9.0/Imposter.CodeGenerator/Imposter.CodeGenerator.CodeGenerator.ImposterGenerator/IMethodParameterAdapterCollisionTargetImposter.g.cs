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
    public class IMethodParameterAdapterCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterAdapterCollisionTarget>
    {
        private readonly AdapterNamesMethodImposter _adapterNamesMethodImposter;
        private readonly AdapterNamesMethodInvocationHistoryCollection _adapterNamesMethodInvocationHistoryCollection = new AdapterNamesMethodInvocationHistoryCollection();
        public IAdapterNamesMethodImposterBuilder AdapterNames(global::Imposter.Abstractions.Arg<int> result, global::Imposter.Abstractions.Arg<string> arguments, global::Imposter.Abstractions.Arg<object> target, global::Imposter.Abstractions.Arg<object> _target)
        {
            return new AdapterNamesMethodImposter.Builder(_adapterNamesMethodImposter, _adapterNamesMethodInvocationHistoryCollection, new AdapterNamesArgumentsCriteria(result, arguments, target, _target));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodParameterAdapterCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodParameterAdapterCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // int IMethodParameterAdapterCollisionTarget.AdapterNames(int result, string arguments, object target, object _target)
        public delegate int AdapterNamesDelegate(int result, string arguments, object target, object _target);
        // int IMethodParameterAdapterCollisionTarget.AdapterNames(int result, string arguments, object target, object _target)
        public delegate void AdapterNamesCallbackDelegate(int result, string arguments, object target, object _target);
        // int IMethodParameterAdapterCollisionTarget.AdapterNames(int result, string arguments, object target, object _target)
        public delegate System.Exception AdapterNamesExceptionGeneratorDelegate(int result, string arguments, object target, object _target);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class AdapterNamesArguments
        {
            public int result;
            public string arguments;
            public object target;
            public object _target;
            internal AdapterNamesArguments(int result, string arguments, object target, object _target)
            {
                this.result = result;
                this.arguments = arguments;
                this.target = target;
                this._target = _target;
            }
        }

        // int IMethodParameterAdapterCollisionTarget.AdapterNames(int result, string arguments, object target, object _target)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class AdapterNamesArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> result { get; }
            public global::Imposter.Abstractions.Arg<string> arguments { get; }
            public global::Imposter.Abstractions.Arg<object> target { get; }
            public global::Imposter.Abstractions.Arg<object> _target { get; }

            public AdapterNamesArgumentsCriteria(global::Imposter.Abstractions.Arg<int> result, global::Imposter.Abstractions.Arg<string> arguments, global::Imposter.Abstractions.Arg<object> target, global::Imposter.Abstractions.Arg<object> _target)
            {
                this.result = result;
                this.arguments = arguments;
                this.target = target;
                this._target = _target;
            }

            public bool Matches(AdapterNamesArguments arguments_1)
            {
                return ((result.Matches(arguments_1.result) && arguments.Matches(arguments_1.arguments)) && target.Matches(arguments_1.target)) && _target.Matches(arguments_1._target);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAdapterNamesMethodInvocationHistory
        {
            bool Matches(AdapterNamesArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AdapterNamesMethodInvocationHistory : IAdapterNamesMethodInvocationHistory
        {
            internal AdapterNamesArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public AdapterNamesMethodInvocationHistory(AdapterNamesArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(AdapterNamesArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AdapterNamesMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IAdapterNamesMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IAdapterNamesMethodInvocationHistory>();
            internal void Add(IAdapterNamesMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(AdapterNamesArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodParameterAdapterCollisionTarget.AdapterNames(int result, string arguments, object target, object _target)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class AdapterNamesMethodInvocationImposterGroup
        {
            internal static AdapterNamesMethodInvocationImposterGroup Default = new AdapterNamesMethodInvocationImposterGroup(new AdapterNamesArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any(), global::Imposter.Abstractions.Arg<string>.Any(), global::Imposter.Abstractions.Arg<object>.Any(), global::Imposter.Abstractions.Arg<object>.Any()));
            internal AdapterNamesArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public AdapterNamesMethodInvocationImposterGroup(AdapterNamesArgumentsCriteria argumentsCriteria)
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

            public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int result, string arguments, object target, object _target)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, result, arguments, target, _target);
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

                private AdapterNamesDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<AdapterNamesCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<AdapterNamesCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int result, string arguments, object target, object _target)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result_1 = _resultGenerator.Invoke(result, arguments, target, _target);
                    foreach (var callback in _callbacks)
                    {
                        callback(result, arguments, target, _target);
                    }

                    return result_1;
                }

                internal void Callback(AdapterNamesCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(AdapterNamesDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (int result, string arguments, object target, object _target) =>
                    {
                        return value;
                    };
                }

                internal void Throws(AdapterNamesExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int result, string arguments, object target, object _target) =>
                    {
                        throw exceptionGenerator(result, arguments, target, _target);
                    };
                }

                internal static int DefaultResultGenerator(int result, string arguments, object target, object _target)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAdapterNamesMethodInvocationImposterGroupCallback
        {
            IAdapterNamesMethodInvocationImposterGroupContinuation Callback(AdapterNamesCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAdapterNamesMethodInvocationImposterGroupContinuation : IAdapterNamesMethodInvocationImposterGroupCallback
        {
            IAdapterNamesMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAdapterNamesMethodInvocationImposterGroup : IAdapterNamesMethodInvocationImposterGroupCallback
        {
            IAdapterNamesMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IAdapterNamesMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IAdapterNamesMethodInvocationImposterGroupContinuation Throws(AdapterNamesExceptionGeneratorDelegate exceptionGenerator);
            IAdapterNamesMethodInvocationImposterGroupContinuation Returns(AdapterNamesDelegate resultGenerator);
            IAdapterNamesMethodInvocationImposterGroupContinuation Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface AdapterNamesInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodParameterAdapterCollisionTarget.AdapterNames(int result, string arguments, object target, object _target)
        public interface IAdapterNamesMethodImposterBuilder : IAdapterNamesMethodInvocationImposterGroup, IAdapterNamesMethodInvocationImposterGroupCallback, AdapterNamesInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AdapterNamesMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<AdapterNamesMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<AdapterNamesMethodInvocationImposterGroup>();
            private readonly AdapterNamesMethodInvocationHistoryCollection _adapterNamesMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public AdapterNamesMethodImposter(AdapterNamesMethodInvocationHistoryCollection _adapterNamesMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._adapterNamesMethodInvocationHistoryCollection = _adapterNamesMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(AdapterNamesArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private AdapterNamesMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(AdapterNamesArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(int result, string arguments, object target, object _target)
            {
                var arguments_1 = new AdapterNamesArguments(result, arguments, target, _target);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments_1);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("int IMethodParameterAdapterCollisionTarget.AdapterNames(int result, string arguments, object target, object _target)");
                    }

                    matchingInvocationImposterGroup = AdapterNamesMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result_1 = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "int IMethodParameterAdapterCollisionTarget.AdapterNames(int result, string arguments, object target, object _target)", result, arguments, target, _target);
                    _adapterNamesMethodInvocationHistoryCollection.Add(new AdapterNamesMethodInvocationHistory(arguments_1, result_1, default));
                    return result_1;
                }
                catch (System.Exception ex)
                {
                    _adapterNamesMethodInvocationHistoryCollection.Add(new AdapterNamesMethodInvocationHistory(arguments_1, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IAdapterNamesMethodImposterBuilder, IAdapterNamesMethodInvocationImposterGroupContinuation
            {
                private readonly AdapterNamesMethodImposter _imposter;
                private readonly AdapterNamesMethodInvocationHistoryCollection _adapterNamesMethodInvocationHistoryCollection;
                private readonly AdapterNamesArgumentsCriteria _argumentsCriteria;
                private readonly AdapterNamesMethodInvocationImposterGroup _invocationImposterGroup;
                private AdapterNamesMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(AdapterNamesMethodImposter _imposter, AdapterNamesMethodInvocationHistoryCollection _adapterNamesMethodInvocationHistoryCollection, AdapterNamesArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._adapterNamesMethodInvocationHistoryCollection = _adapterNamesMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new AdapterNamesMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IAdapterNamesMethodInvocationImposterGroupContinuation IAdapterNamesMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int result, string arguments, object target, object _target) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IAdapterNamesMethodInvocationImposterGroupContinuation IAdapterNamesMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int result, string arguments, object target, object _target) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IAdapterNamesMethodInvocationImposterGroupContinuation IAdapterNamesMethodInvocationImposterGroup.Throws(AdapterNamesExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int result, string arguments, object target, object _target) =>
                    {
                        throw exceptionGenerator.Invoke(result, arguments, target, _target);
                    });
                    return this;
                }

                IAdapterNamesMethodInvocationImposterGroupContinuation IAdapterNamesMethodInvocationImposterGroupCallback.Callback(AdapterNamesCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IAdapterNamesMethodInvocationImposterGroupContinuation IAdapterNamesMethodInvocationImposterGroup.Returns(AdapterNamesDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IAdapterNamesMethodInvocationImposterGroupContinuation IAdapterNamesMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IAdapterNamesMethodInvocationImposterGroup IAdapterNamesMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void AdapterNamesInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _adapterNamesMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodParameterAdapterCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._adapterNamesMethodImposter = new AdapterNamesMethodImposter(_adapterNamesMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodParameterAdapterCollisionTarget
        {
            IMethodParameterAdapterCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodParameterAdapterCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int AdapterNames(int result, string arguments, object target, object _target)
            {
                return _imposter._adapterNamesMethodImposter.Invoke(result, arguments, target, _target);
            }
        }
    }
}
#pragma warning restore nullable
