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
    public class IMethodHelperNameCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodHelperNameCollisionTarget>
    {
        private readonly AdapterMethodImposter _adapterMethodImposter;
        private readonly HasMatchingSetupMethodImposter _hasMatchingSetupMethodImposter;
        private readonly AsMethodImposter _asMethodImposter;
        private readonly InvokeMethodImposter _invokeMethodImposter;
        private readonly AdapterMethodInvocationHistoryCollection _adapterMethodInvocationHistoryCollection = new AdapterMethodInvocationHistoryCollection();
        private readonly HasMatchingSetupMethodInvocationHistoryCollection _hasMatchingSetupMethodInvocationHistoryCollection = new HasMatchingSetupMethodInvocationHistoryCollection();
        private readonly AsMethodInvocationHistoryCollection _asMethodInvocationHistoryCollection = new AsMethodInvocationHistoryCollection();
        private readonly InvokeMethodInvocationHistoryCollection _invokeMethodInvocationHistoryCollection = new InvokeMethodInvocationHistoryCollection();
        public IAdapterMethodImposterBuilder Adapter()
        {
            return new AdapterMethodImposter.Builder(_adapterMethodImposter, _adapterMethodInvocationHistoryCollection);
        }

        public IHasMatchingSetupMethodImposterBuilder HasMatchingSetup()
        {
            return new HasMatchingSetupMethodImposter.Builder(_hasMatchingSetupMethodImposter, _hasMatchingSetupMethodInvocationHistoryCollection);
        }

        public IAsMethodImposterBuilder As(global::Imposter.Abstractions.Arg<string> hint)
        {
            return new AsMethodImposter.Builder(_asMethodImposter, _asMethodInvocationHistoryCollection, new AsArgumentsCriteria(hint));
        }

        public IInvokeMethodImposterBuilder Invoke(global::Imposter.Abstractions.Arg<int> value)
        {
            return new InvokeMethodImposter.Builder(_invokeMethodImposter, _invokeMethodInvocationHistoryCollection, new InvokeArgumentsCriteria(value));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodHelperNameCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodHelperNameCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // void IMethodHelperNameCollisionTarget.Adapter()
        public delegate void AdapterDelegate();
        // void IMethodHelperNameCollisionTarget.Adapter()
        public delegate void AdapterCallbackDelegate();
        // void IMethodHelperNameCollisionTarget.Adapter()
        public delegate System.Exception AdapterExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAdapterMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AdapterMethodInvocationHistory : IAdapterMethodInvocationHistory
        {
            internal System.Exception Exception;
            public AdapterMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AdapterMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IAdapterMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IAdapterMethodInvocationHistory>();
            internal void Add(IAdapterMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodHelperNameCollisionTarget.Adapter()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class AdapterMethodInvocationImposterGroup
        {
            internal static AdapterMethodInvocationImposterGroup Default = new AdapterMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public AdapterMethodInvocationImposterGroup()
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

                private AdapterDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<AdapterCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<AdapterCallbackDelegate>();
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

                internal void Callback(AdapterCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(AdapterExceptionGeneratorDelegate exceptionGenerator)
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
        public interface IAdapterMethodInvocationImposterGroupCallback
        {
            IAdapterMethodInvocationImposterGroupContinuation Callback(AdapterCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAdapterMethodInvocationImposterGroupContinuation : IAdapterMethodInvocationImposterGroupCallback
        {
            IAdapterMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAdapterMethodInvocationImposterGroup : IAdapterMethodInvocationImposterGroupCallback
        {
            IAdapterMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IAdapterMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IAdapterMethodInvocationImposterGroupContinuation Throws(AdapterExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface AdapterInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodHelperNameCollisionTarget.Adapter()
        public interface IAdapterMethodImposterBuilder : IAdapterMethodInvocationImposterGroup, IAdapterMethodInvocationImposterGroupCallback, AdapterInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AdapterMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<AdapterMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<AdapterMethodInvocationImposterGroup>();
            private readonly AdapterMethodInvocationHistoryCollection _adapterMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public AdapterMethodImposter(AdapterMethodInvocationHistoryCollection _adapterMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._adapterMethodInvocationHistoryCollection = _adapterMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private AdapterMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodHelperNameCollisionTarget.Adapter()");
                    }

                    matchingInvocationImposterGroup = AdapterMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodHelperNameCollisionTarget.Adapter()");
                    _adapterMethodInvocationHistoryCollection.Add(new AdapterMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    _adapterMethodInvocationHistoryCollection.Add(new AdapterMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IAdapterMethodImposterBuilder, IAdapterMethodInvocationImposterGroupContinuation
            {
                private readonly AdapterMethodImposter _imposter;
                private readonly AdapterMethodInvocationHistoryCollection _adapterMethodInvocationHistoryCollection;
                private readonly AdapterMethodInvocationImposterGroup _invocationImposterGroup;
                private AdapterMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(AdapterMethodImposter _imposter, AdapterMethodInvocationHistoryCollection _adapterMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._adapterMethodInvocationHistoryCollection = _adapterMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new AdapterMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IAdapterMethodInvocationImposterGroupContinuation IAdapterMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IAdapterMethodInvocationImposterGroupContinuation IAdapterMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IAdapterMethodInvocationImposterGroupContinuation IAdapterMethodInvocationImposterGroup.Throws(AdapterExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IAdapterMethodInvocationImposterGroupContinuation IAdapterMethodInvocationImposterGroupCallback.Callback(AdapterCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IAdapterMethodInvocationImposterGroup IAdapterMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void AdapterInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _adapterMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodHelperNameCollisionTarget.As(string hint)
        public delegate void AsDelegate(string hint);
        // void IMethodHelperNameCollisionTarget.As(string hint)
        public delegate void AsCallbackDelegate(string hint);
        // void IMethodHelperNameCollisionTarget.As(string hint)
        public delegate System.Exception AsExceptionGeneratorDelegate(string hint);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class AsArguments
        {
            public string hint;
            internal AsArguments(string hint)
            {
                this.hint = hint;
            }
        }

        // void IMethodHelperNameCollisionTarget.As(string hint)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class AsArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<string> hint { get; }

            public AsArgumentsCriteria(global::Imposter.Abstractions.Arg<string> hint)
            {
                this.hint = hint;
            }

            public bool Matches(AsArguments arguments)
            {
                return hint.Matches(arguments.hint);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAsMethodInvocationHistory
        {
            bool Matches(AsArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AsMethodInvocationHistory : IAsMethodInvocationHistory
        {
            internal AsArguments Arguments;
            internal System.Exception Exception;
            public AsMethodInvocationHistory(AsArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(AsArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IAsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IAsMethodInvocationHistory>();
            internal void Add(IAsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(AsArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodHelperNameCollisionTarget.As(string hint)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class AsMethodInvocationImposterGroup
        {
            internal static AsMethodInvocationImposterGroup Default = new AsMethodInvocationImposterGroup(new AsArgumentsCriteria(global::Imposter.Abstractions.Arg<string>.Any()));
            internal AsArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public AsMethodInvocationImposterGroup(AsArgumentsCriteria argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string hint)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, hint);
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

                private AsDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<AsCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<AsCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string hint)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(hint);
                    foreach (var callback in _callbacks)
                    {
                        callback(hint);
                    }
                }

                internal void Callback(AsCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(AsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (string hint) =>
                    {
                        throw exceptionGenerator(hint);
                    };
                }

                internal static void DefaultResultGenerator(string hint)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAsMethodInvocationImposterGroupCallback
        {
            IAsMethodInvocationImposterGroupContinuation Callback(AsCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAsMethodInvocationImposterGroupContinuation : IAsMethodInvocationImposterGroupCallback
        {
            IAsMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAsMethodInvocationImposterGroup : IAsMethodInvocationImposterGroupCallback
        {
            IAsMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IAsMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IAsMethodInvocationImposterGroupContinuation Throws(AsExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface AsInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodHelperNameCollisionTarget.As(string hint)
        public interface IAsMethodImposterBuilder : IAsMethodInvocationImposterGroup, IAsMethodInvocationImposterGroupCallback, AsInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<AsMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<AsMethodInvocationImposterGroup>();
            private readonly AsMethodInvocationHistoryCollection _asMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public AsMethodImposter(AsMethodInvocationHistoryCollection _asMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._asMethodInvocationHistoryCollection = _asMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(AsArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private AsMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(AsArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(string hint)
            {
                var arguments = new AsArguments(hint);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodHelperNameCollisionTarget.As(string hint)");
                    }

                    matchingInvocationImposterGroup = AsMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodHelperNameCollisionTarget.As(string hint)", hint);
                    _asMethodInvocationHistoryCollection.Add(new AsMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _asMethodInvocationHistoryCollection.Add(new AsMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IAsMethodImposterBuilder, IAsMethodInvocationImposterGroupContinuation
            {
                private readonly AsMethodImposter _imposter;
                private readonly AsMethodInvocationHistoryCollection _asMethodInvocationHistoryCollection;
                private readonly AsArgumentsCriteria _argumentsCriteria;
                private readonly AsMethodInvocationImposterGroup _invocationImposterGroup;
                private AsMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(AsMethodImposter _imposter, AsMethodInvocationHistoryCollection _asMethodInvocationHistoryCollection, AsArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._asMethodInvocationHistoryCollection = _asMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new AsMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IAsMethodInvocationImposterGroupContinuation IAsMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((string hint) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IAsMethodInvocationImposterGroupContinuation IAsMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((string hint) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IAsMethodInvocationImposterGroupContinuation IAsMethodInvocationImposterGroup.Throws(AsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((string hint) =>
                    {
                        throw exceptionGenerator.Invoke(hint);
                    });
                    return this;
                }

                IAsMethodInvocationImposterGroupContinuation IAsMethodInvocationImposterGroupCallback.Callback(AsCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IAsMethodInvocationImposterGroup IAsMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void AsInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _asMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodHelperNameCollisionTarget.HasMatchingSetup()
        public delegate void HasMatchingSetupDelegate();
        // void IMethodHelperNameCollisionTarget.HasMatchingSetup()
        public delegate void HasMatchingSetupCallbackDelegate();
        // void IMethodHelperNameCollisionTarget.HasMatchingSetup()
        public delegate System.Exception HasMatchingSetupExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IHasMatchingSetupMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class HasMatchingSetupMethodInvocationHistory : IHasMatchingSetupMethodInvocationHistory
        {
            internal System.Exception Exception;
            public HasMatchingSetupMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class HasMatchingSetupMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IHasMatchingSetupMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IHasMatchingSetupMethodInvocationHistory>();
            internal void Add(IHasMatchingSetupMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodHelperNameCollisionTarget.HasMatchingSetup()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class HasMatchingSetupMethodInvocationImposterGroup
        {
            internal static HasMatchingSetupMethodInvocationImposterGroup Default = new HasMatchingSetupMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public HasMatchingSetupMethodInvocationImposterGroup()
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

                private HasMatchingSetupDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<HasMatchingSetupCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<HasMatchingSetupCallbackDelegate>();
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

                internal void Callback(HasMatchingSetupCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(HasMatchingSetupExceptionGeneratorDelegate exceptionGenerator)
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
        public interface IHasMatchingSetupMethodInvocationImposterGroupCallback
        {
            IHasMatchingSetupMethodInvocationImposterGroupContinuation Callback(HasMatchingSetupCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IHasMatchingSetupMethodInvocationImposterGroupContinuation : IHasMatchingSetupMethodInvocationImposterGroupCallback
        {
            IHasMatchingSetupMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IHasMatchingSetupMethodInvocationImposterGroup : IHasMatchingSetupMethodInvocationImposterGroupCallback
        {
            IHasMatchingSetupMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IHasMatchingSetupMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IHasMatchingSetupMethodInvocationImposterGroupContinuation Throws(HasMatchingSetupExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface HasMatchingSetupInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodHelperNameCollisionTarget.HasMatchingSetup()
        public interface IHasMatchingSetupMethodImposterBuilder : IHasMatchingSetupMethodInvocationImposterGroup, IHasMatchingSetupMethodInvocationImposterGroupCallback, HasMatchingSetupInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class HasMatchingSetupMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<HasMatchingSetupMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<HasMatchingSetupMethodInvocationImposterGroup>();
            private readonly HasMatchingSetupMethodInvocationHistoryCollection _hasMatchingSetupMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public HasMatchingSetupMethodImposter(HasMatchingSetupMethodInvocationHistoryCollection _hasMatchingSetupMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._hasMatchingSetupMethodInvocationHistoryCollection = _hasMatchingSetupMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private HasMatchingSetupMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodHelperNameCollisionTarget.HasMatchingSetup()");
                    }

                    matchingInvocationImposterGroup = HasMatchingSetupMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodHelperNameCollisionTarget.HasMatchingSetup()");
                    _hasMatchingSetupMethodInvocationHistoryCollection.Add(new HasMatchingSetupMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    _hasMatchingSetupMethodInvocationHistoryCollection.Add(new HasMatchingSetupMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IHasMatchingSetupMethodImposterBuilder, IHasMatchingSetupMethodInvocationImposterGroupContinuation
            {
                private readonly HasMatchingSetupMethodImposter _imposter;
                private readonly HasMatchingSetupMethodInvocationHistoryCollection _hasMatchingSetupMethodInvocationHistoryCollection;
                private readonly HasMatchingSetupMethodInvocationImposterGroup _invocationImposterGroup;
                private HasMatchingSetupMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(HasMatchingSetupMethodImposter _imposter, HasMatchingSetupMethodInvocationHistoryCollection _hasMatchingSetupMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._hasMatchingSetupMethodInvocationHistoryCollection = _hasMatchingSetupMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new HasMatchingSetupMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IHasMatchingSetupMethodInvocationImposterGroupContinuation IHasMatchingSetupMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IHasMatchingSetupMethodInvocationImposterGroupContinuation IHasMatchingSetupMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IHasMatchingSetupMethodInvocationImposterGroupContinuation IHasMatchingSetupMethodInvocationImposterGroup.Throws(HasMatchingSetupExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IHasMatchingSetupMethodInvocationImposterGroupContinuation IHasMatchingSetupMethodInvocationImposterGroupCallback.Callback(HasMatchingSetupCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IHasMatchingSetupMethodInvocationImposterGroup IHasMatchingSetupMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void HasMatchingSetupInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _hasMatchingSetupMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodHelperNameCollisionTarget.Invoke(int value)
        public delegate void InvokeDelegate(int value);
        // void IMethodHelperNameCollisionTarget.Invoke(int value)
        public delegate void InvokeCallbackDelegate(int value);
        // void IMethodHelperNameCollisionTarget.Invoke(int value)
        public delegate System.Exception InvokeExceptionGeneratorDelegate(int value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class InvokeArguments
        {
            public int value;
            internal InvokeArguments(int value)
            {
                this.value = value;
            }
        }

        // void IMethodHelperNameCollisionTarget.Invoke(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class InvokeArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> value { get; }

            public InvokeArgumentsCriteria(global::Imposter.Abstractions.Arg<int> value)
            {
                this.value = value;
            }

            public bool Matches(InvokeArguments arguments)
            {
                return value.Matches(arguments.value);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInvokeMethodInvocationHistory
        {
            bool Matches(InvokeArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InvokeMethodInvocationHistory : IInvokeMethodInvocationHistory
        {
            internal InvokeArguments Arguments;
            internal System.Exception Exception;
            public InvokeMethodInvocationHistory(InvokeArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(InvokeArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InvokeMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IInvokeMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IInvokeMethodInvocationHistory>();
            internal void Add(IInvokeMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(InvokeArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodHelperNameCollisionTarget.Invoke(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class InvokeMethodInvocationImposterGroup
        {
            internal static InvokeMethodInvocationImposterGroup Default = new InvokeMethodInvocationImposterGroup(new InvokeArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any()));
            internal InvokeArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public InvokeMethodInvocationImposterGroup(InvokeArgumentsCriteria argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, value);
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

                private InvokeDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<InvokeCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<InvokeCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(value);
                    foreach (var callback in _callbacks)
                    {
                        callback(value);
                    }
                }

                internal void Callback(InvokeCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(InvokeExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int value) =>
                    {
                        throw exceptionGenerator(value);
                    };
                }

                internal static void DefaultResultGenerator(int value)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInvokeMethodInvocationImposterGroupCallback
        {
            IInvokeMethodInvocationImposterGroupContinuation Callback(InvokeCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInvokeMethodInvocationImposterGroupContinuation : IInvokeMethodInvocationImposterGroupCallback
        {
            IInvokeMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IInvokeMethodInvocationImposterGroup : IInvokeMethodInvocationImposterGroupCallback
        {
            IInvokeMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IInvokeMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IInvokeMethodInvocationImposterGroupContinuation Throws(InvokeExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface InvokeInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodHelperNameCollisionTarget.Invoke(int value)
        public interface IInvokeMethodImposterBuilder : IInvokeMethodInvocationImposterGroup, IInvokeMethodInvocationImposterGroupCallback, InvokeInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class InvokeMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<InvokeMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<InvokeMethodInvocationImposterGroup>();
            private readonly InvokeMethodInvocationHistoryCollection _invokeMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public InvokeMethodImposter(InvokeMethodInvocationHistoryCollection _invokeMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._invokeMethodInvocationHistoryCollection = _invokeMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(InvokeArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private InvokeMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(InvokeArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(int value)
            {
                var arguments = new InvokeArguments(value);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodHelperNameCollisionTarget.Invoke(int value)");
                    }

                    matchingInvocationImposterGroup = InvokeMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodHelperNameCollisionTarget.Invoke(int value)", value);
                    _invokeMethodInvocationHistoryCollection.Add(new InvokeMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _invokeMethodInvocationHistoryCollection.Add(new InvokeMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IInvokeMethodImposterBuilder, IInvokeMethodInvocationImposterGroupContinuation
            {
                private readonly InvokeMethodImposter _imposter;
                private readonly InvokeMethodInvocationHistoryCollection _invokeMethodInvocationHistoryCollection;
                private readonly InvokeArgumentsCriteria _argumentsCriteria;
                private readonly InvokeMethodInvocationImposterGroup _invocationImposterGroup;
                private InvokeMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(InvokeMethodImposter _imposter, InvokeMethodInvocationHistoryCollection _invokeMethodInvocationHistoryCollection, InvokeArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._invokeMethodInvocationHistoryCollection = _invokeMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new InvokeMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IInvokeMethodInvocationImposterGroupContinuation IInvokeMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IInvokeMethodInvocationImposterGroupContinuation IInvokeMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IInvokeMethodInvocationImposterGroupContinuation IInvokeMethodInvocationImposterGroup.Throws(InvokeExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exceptionGenerator.Invoke(value);
                    });
                    return this;
                }

                IInvokeMethodInvocationImposterGroupContinuation IInvokeMethodInvocationImposterGroupCallback.Callback(InvokeCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IInvokeMethodInvocationImposterGroup IInvokeMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void InvokeInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _invokeMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodHelperNameCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._adapterMethodImposter = new AdapterMethodImposter(_adapterMethodInvocationHistoryCollection, invocationBehavior);
            this._hasMatchingSetupMethodImposter = new HasMatchingSetupMethodImposter(_hasMatchingSetupMethodInvocationHistoryCollection, invocationBehavior);
            this._asMethodImposter = new AsMethodImposter(_asMethodInvocationHistoryCollection, invocationBehavior);
            this._invokeMethodImposter = new InvokeMethodImposter(_invokeMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodHelperNameCollisionTarget
        {
            IMethodHelperNameCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodHelperNameCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public void Adapter()
            {
                _imposter._adapterMethodImposter.Invoke();
            }

            public void HasMatchingSetup()
            {
                _imposter._hasMatchingSetupMethodImposter.Invoke();
            }

            public void As(string hint)
            {
                _imposter._asMethodImposter.Invoke(hint);
            }

            public void Invoke(int value)
            {
                _imposter._invokeMethodImposter.Invoke(value);
            }
        }
    }
}
#pragma warning restore nullable
