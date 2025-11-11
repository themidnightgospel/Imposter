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
    public class IMethodGroupedTypeCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodGroupedTypeCollisionTarget>
    {
        private readonly MethodImposterMethodImposter _methodImposterMethodImposter;
        private readonly MethodInvocationImposterGroupMethodImposter _methodInvocationImposterGroupMethodImposter;
        private readonly MethodInvocationHistoryMethodImposter _methodInvocationHistoryMethodImposter;
        private readonly ArgumentsCriteriaMethodImposter _argumentsCriteriaMethodImposter;
        private readonly ArgumentsMethodImposter _argumentsMethodImposter;
        private readonly DelegateMethodImposter _delegateMethodImposter;
        private readonly CallbackDelegateMethodImposter _callbackDelegateMethodImposter;
        private readonly ExceptionGeneratorDelegateMethodImposter _exceptionGeneratorDelegateMethodImposter;
        private readonly CollectionMethodImposter _collectionMethodImposter;
        private readonly MethodImposterMethodInvocationHistoryCollection _methodImposterMethodInvocationHistoryCollection = new MethodImposterMethodInvocationHistoryCollection();
        private readonly MethodInvocationImposterGroupMethodInvocationHistoryCollection _methodInvocationImposterGroupMethodInvocationHistoryCollection = new MethodInvocationImposterGroupMethodInvocationHistoryCollection();
        private readonly MethodInvocationHistoryMethodInvocationHistoryCollection _methodInvocationHistoryMethodInvocationHistoryCollection = new MethodInvocationHistoryMethodInvocationHistoryCollection();
        private readonly ArgumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection = new ArgumentsCriteriaMethodInvocationHistoryCollection();
        private readonly ArgumentsMethodInvocationHistoryCollection _argumentsMethodInvocationHistoryCollection = new ArgumentsMethodInvocationHistoryCollection();
        private readonly DelegateMethodInvocationHistoryCollection _delegateMethodInvocationHistoryCollection = new DelegateMethodInvocationHistoryCollection();
        private readonly CallbackDelegateMethodInvocationHistoryCollection _callbackDelegateMethodInvocationHistoryCollection = new CallbackDelegateMethodInvocationHistoryCollection();
        private readonly ExceptionGeneratorDelegateMethodInvocationHistoryCollection _exceptionGeneratorDelegateMethodInvocationHistoryCollection = new ExceptionGeneratorDelegateMethodInvocationHistoryCollection();
        private readonly CollectionMethodInvocationHistoryCollection _collectionMethodInvocationHistoryCollection = new CollectionMethodInvocationHistoryCollection();
        public IMethodImposterMethodImposterBuilder MethodImposter(global::Imposter.Abstractions.Arg<int> value)
        {
            return new MethodImposterMethodImposter.Builder(_methodImposterMethodImposter, _methodImposterMethodInvocationHistoryCollection, new MethodImposterArgumentsCriteria(value));
        }

        public IMethodInvocationImposterGroupMethodImposterBuilder MethodInvocationImposterGroup(global::Imposter.Abstractions.Arg<string> label)
        {
            return new MethodInvocationImposterGroupMethodImposter.Builder(_methodInvocationImposterGroupMethodImposter, _methodInvocationImposterGroupMethodInvocationHistoryCollection, new MethodInvocationImposterGroupArgumentsCriteria(label));
        }

        public IMethodInvocationHistoryMethodImposterBuilder MethodInvocationHistory()
        {
            return new MethodInvocationHistoryMethodImposter.Builder(_methodInvocationHistoryMethodImposter, _methodInvocationHistoryMethodInvocationHistoryCollection);
        }

        public IArgumentsCriteriaMethodImposterBuilder ArgumentsCriteria(global::Imposter.Abstractions.Arg<int> left, global::Imposter.Abstractions.Arg<int> right)
        {
            return new ArgumentsCriteriaMethodImposter.Builder(_argumentsCriteriaMethodImposter, _argumentsCriteriaMethodInvocationHistoryCollection, new ArgumentsCriteriaArgumentsCriteria(left, right));
        }

        public IArgumentsMethodImposterBuilder Arguments(global::Imposter.Abstractions.Arg<string> label)
        {
            return new ArgumentsMethodImposter.Builder(_argumentsMethodImposter, _argumentsMethodInvocationHistoryCollection, new ArgumentsArgumentsCriteria(label));
        }

        public IDelegateMethodImposterBuilder Delegate(global::Imposter.Abstractions.Arg<int> value)
        {
            return new DelegateMethodImposter.Builder(_delegateMethodImposter, _delegateMethodInvocationHistoryCollection, new DelegateArgumentsCriteria(value));
        }

        public ICallbackDelegateMethodImposterBuilder CallbackDelegate(global::Imposter.Abstractions.Arg<int> first, global::Imposter.Abstractions.Arg<int> second)
        {
            return new CallbackDelegateMethodImposter.Builder(_callbackDelegateMethodImposter, _callbackDelegateMethodInvocationHistoryCollection, new CallbackDelegateArgumentsCriteria(first, second));
        }

        public IExceptionGeneratorDelegateMethodImposterBuilder ExceptionGeneratorDelegate(global::Imposter.Abstractions.Arg<global::System.Exception> error)
        {
            return new ExceptionGeneratorDelegateMethodImposter.Builder(_exceptionGeneratorDelegateMethodImposter, _exceptionGeneratorDelegateMethodInvocationHistoryCollection, new ExceptionGeneratorDelegateArgumentsCriteria(error));
        }

        public ICollectionMethodImposterBuilder Collection(global::Imposter.Abstractions.Arg<int> count)
        {
            return new CollectionMethodImposter.Builder(_collectionMethodImposter, _collectionMethodInvocationHistoryCollection, new CollectionArgumentsCriteria(count));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodGroupedTypeCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodGroupedTypeCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // void IMethodGroupedTypeCollisionTarget.Arguments(string label)
        public delegate void ArgumentsDelegate(string label);
        // void IMethodGroupedTypeCollisionTarget.Arguments(string label)
        public delegate void ArgumentsCallbackDelegate(string label);
        // void IMethodGroupedTypeCollisionTarget.Arguments(string label)
        public delegate System.Exception ArgumentsExceptionGeneratorDelegate(string label);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ArgumentsArguments
        {
            public string label;
            internal ArgumentsArguments(string label)
            {
                this.label = label;
            }
        }

        // void IMethodGroupedTypeCollisionTarget.Arguments(string label)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ArgumentsArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<string> label { get; }

            public ArgumentsArgumentsCriteria(global::Imposter.Abstractions.Arg<string> label)
            {
                this.label = label;
            }

            public bool Matches(ArgumentsArguments arguments)
            {
                return label.Matches(arguments.label);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IArgumentsMethodInvocationHistory
        {
            bool Matches(ArgumentsArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ArgumentsMethodInvocationHistory : IArgumentsMethodInvocationHistory
        {
            internal ArgumentsArguments Arguments;
            internal System.Exception Exception;
            public ArgumentsMethodInvocationHistory(ArgumentsArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(ArgumentsArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ArgumentsMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IArgumentsMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IArgumentsMethodInvocationHistory>();
            internal void Add(IArgumentsMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(ArgumentsArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodGroupedTypeCollisionTarget.Arguments(string label)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ArgumentsMethodInvocationImposterGroup
        {
            internal static ArgumentsMethodInvocationImposterGroup Default = new ArgumentsMethodInvocationImposterGroup(new ArgumentsArgumentsCriteria(global::Imposter.Abstractions.Arg<string>.Any()));
            internal ArgumentsArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ArgumentsMethodInvocationImposterGroup(ArgumentsArgumentsCriteria argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string label)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, label);
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

                private ArgumentsDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ArgumentsCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ArgumentsCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string label)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(label);
                    foreach (var callback in _callbacks)
                    {
                        callback(label);
                    }
                }

                internal void Callback(ArgumentsCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(ArgumentsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (string label) =>
                    {
                        throw exceptionGenerator(label);
                    };
                }

                internal static void DefaultResultGenerator(string label)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IArgumentsMethodInvocationImposterGroupCallback
        {
            IArgumentsMethodInvocationImposterGroupContinuation Callback(ArgumentsCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IArgumentsMethodInvocationImposterGroupContinuation : IArgumentsMethodInvocationImposterGroupCallback
        {
            IArgumentsMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IArgumentsMethodInvocationImposterGroup : IArgumentsMethodInvocationImposterGroupCallback
        {
            IArgumentsMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IArgumentsMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IArgumentsMethodInvocationImposterGroupContinuation Throws(ArgumentsExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ArgumentsInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodGroupedTypeCollisionTarget.Arguments(string label)
        public interface IArgumentsMethodImposterBuilder : IArgumentsMethodInvocationImposterGroup, IArgumentsMethodInvocationImposterGroupCallback, ArgumentsInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ArgumentsMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ArgumentsMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ArgumentsMethodInvocationImposterGroup>();
            private readonly ArgumentsMethodInvocationHistoryCollection _argumentsMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ArgumentsMethodImposter(ArgumentsMethodInvocationHistoryCollection _argumentsMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._argumentsMethodInvocationHistoryCollection = _argumentsMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(ArgumentsArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private ArgumentsMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(ArgumentsArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(string label)
            {
                var arguments = new ArgumentsArguments(label);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodGroupedTypeCollisionTarget.Arguments(string label)");
                    }

                    matchingInvocationImposterGroup = ArgumentsMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodGroupedTypeCollisionTarget.Arguments(string label)", label);
                    _argumentsMethodInvocationHistoryCollection.Add(new ArgumentsMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _argumentsMethodInvocationHistoryCollection.Add(new ArgumentsMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IArgumentsMethodImposterBuilder, IArgumentsMethodInvocationImposterGroupContinuation
            {
                private readonly ArgumentsMethodImposter _imposter;
                private readonly ArgumentsMethodInvocationHistoryCollection _argumentsMethodInvocationHistoryCollection;
                private readonly ArgumentsArgumentsCriteria _argumentsCriteria;
                private readonly ArgumentsMethodInvocationImposterGroup _invocationImposterGroup;
                private ArgumentsMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ArgumentsMethodImposter _imposter, ArgumentsMethodInvocationHistoryCollection _argumentsMethodInvocationHistoryCollection, ArgumentsArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._argumentsMethodInvocationHistoryCollection = _argumentsMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new ArgumentsMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IArgumentsMethodInvocationImposterGroupContinuation IArgumentsMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((string label) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IArgumentsMethodInvocationImposterGroupContinuation IArgumentsMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((string label) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IArgumentsMethodInvocationImposterGroupContinuation IArgumentsMethodInvocationImposterGroup.Throws(ArgumentsExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((string label) =>
                    {
                        throw exceptionGenerator.Invoke(label);
                    });
                    return this;
                }

                IArgumentsMethodInvocationImposterGroupContinuation IArgumentsMethodInvocationImposterGroupCallback.Callback(ArgumentsCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IArgumentsMethodInvocationImposterGroup IArgumentsMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ArgumentsInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _argumentsMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodGroupedTypeCollisionTarget.ArgumentsCriteria(int left, int right)
        public delegate void ArgumentsCriteriaDelegate(int left, int right);
        // void IMethodGroupedTypeCollisionTarget.ArgumentsCriteria(int left, int right)
        public delegate void ArgumentsCriteriaCallbackDelegate(int left, int right);
        // void IMethodGroupedTypeCollisionTarget.ArgumentsCriteria(int left, int right)
        public delegate System.Exception ArgumentsCriteriaExceptionGeneratorDelegate(int left, int right);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ArgumentsCriteriaArguments
        {
            public int left;
            public int right;
            internal ArgumentsCriteriaArguments(int left, int right)
            {
                this.left = left;
                this.right = right;
            }
        }

        // void IMethodGroupedTypeCollisionTarget.ArgumentsCriteria(int left, int right)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ArgumentsCriteriaArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> left { get; }
            public global::Imposter.Abstractions.Arg<int> right { get; }

            public ArgumentsCriteriaArgumentsCriteria(global::Imposter.Abstractions.Arg<int> left, global::Imposter.Abstractions.Arg<int> right)
            {
                this.left = left;
                this.right = right;
            }

            public bool Matches(ArgumentsCriteriaArguments arguments)
            {
                return left.Matches(arguments.left) && right.Matches(arguments.right);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IArgumentsCriteriaMethodInvocationHistory
        {
            bool Matches(ArgumentsCriteriaArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ArgumentsCriteriaMethodInvocationHistory : IArgumentsCriteriaMethodInvocationHistory
        {
            internal ArgumentsCriteriaArguments Arguments;
            internal System.Exception Exception;
            public ArgumentsCriteriaMethodInvocationHistory(ArgumentsCriteriaArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(ArgumentsCriteriaArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ArgumentsCriteriaMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IArgumentsCriteriaMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IArgumentsCriteriaMethodInvocationHistory>();
            internal void Add(IArgumentsCriteriaMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(ArgumentsCriteriaArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodGroupedTypeCollisionTarget.ArgumentsCriteria(int left, int right)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ArgumentsCriteriaMethodInvocationImposterGroup
        {
            internal static ArgumentsCriteriaMethodInvocationImposterGroup Default = new ArgumentsCriteriaMethodInvocationImposterGroup(new ArgumentsCriteriaArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any(), global::Imposter.Abstractions.Arg<int>.Any()));
            internal ArgumentsCriteriaArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ArgumentsCriteriaMethodInvocationImposterGroup(ArgumentsCriteriaArgumentsCriteria argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int left, int right)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, left, right);
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

                private ArgumentsCriteriaDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ArgumentsCriteriaCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ArgumentsCriteriaCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int left, int right)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(left, right);
                    foreach (var callback in _callbacks)
                    {
                        callback(left, right);
                    }
                }

                internal void Callback(ArgumentsCriteriaCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(ArgumentsCriteriaExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int left, int right) =>
                    {
                        throw exceptionGenerator(left, right);
                    };
                }

                internal static void DefaultResultGenerator(int left, int right)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IArgumentsCriteriaMethodInvocationImposterGroupCallback
        {
            IArgumentsCriteriaMethodInvocationImposterGroupContinuation Callback(ArgumentsCriteriaCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IArgumentsCriteriaMethodInvocationImposterGroupContinuation : IArgumentsCriteriaMethodInvocationImposterGroupCallback
        {
            IArgumentsCriteriaMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IArgumentsCriteriaMethodInvocationImposterGroup : IArgumentsCriteriaMethodInvocationImposterGroupCallback
        {
            IArgumentsCriteriaMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IArgumentsCriteriaMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IArgumentsCriteriaMethodInvocationImposterGroupContinuation Throws(ArgumentsCriteriaExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ArgumentsCriteriaInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodGroupedTypeCollisionTarget.ArgumentsCriteria(int left, int right)
        public interface IArgumentsCriteriaMethodImposterBuilder : IArgumentsCriteriaMethodInvocationImposterGroup, IArgumentsCriteriaMethodInvocationImposterGroupCallback, ArgumentsCriteriaInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ArgumentsCriteriaMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ArgumentsCriteriaMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ArgumentsCriteriaMethodInvocationImposterGroup>();
            private readonly ArgumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ArgumentsCriteriaMethodImposter(ArgumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._argumentsCriteriaMethodInvocationHistoryCollection = _argumentsCriteriaMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(ArgumentsCriteriaArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private ArgumentsCriteriaMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(ArgumentsCriteriaArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(int left, int right)
            {
                var arguments = new ArgumentsCriteriaArguments(left, right);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodGroupedTypeCollisionTarget.ArgumentsCriteria(int left, int right)");
                    }

                    matchingInvocationImposterGroup = ArgumentsCriteriaMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodGroupedTypeCollisionTarget.ArgumentsCriteria(int left, int right)", left, right);
                    _argumentsCriteriaMethodInvocationHistoryCollection.Add(new ArgumentsCriteriaMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _argumentsCriteriaMethodInvocationHistoryCollection.Add(new ArgumentsCriteriaMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IArgumentsCriteriaMethodImposterBuilder, IArgumentsCriteriaMethodInvocationImposterGroupContinuation
            {
                private readonly ArgumentsCriteriaMethodImposter _imposter;
                private readonly ArgumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection;
                private readonly ArgumentsCriteriaArgumentsCriteria _argumentsCriteria;
                private readonly ArgumentsCriteriaMethodInvocationImposterGroup _invocationImposterGroup;
                private ArgumentsCriteriaMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ArgumentsCriteriaMethodImposter _imposter, ArgumentsCriteriaMethodInvocationHistoryCollection _argumentsCriteriaMethodInvocationHistoryCollection, ArgumentsCriteriaArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._argumentsCriteriaMethodInvocationHistoryCollection = _argumentsCriteriaMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new ArgumentsCriteriaMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IArgumentsCriteriaMethodInvocationImposterGroupContinuation IArgumentsCriteriaMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int left, int right) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IArgumentsCriteriaMethodInvocationImposterGroupContinuation IArgumentsCriteriaMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int left, int right) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IArgumentsCriteriaMethodInvocationImposterGroupContinuation IArgumentsCriteriaMethodInvocationImposterGroup.Throws(ArgumentsCriteriaExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int left, int right) =>
                    {
                        throw exceptionGenerator.Invoke(left, right);
                    });
                    return this;
                }

                IArgumentsCriteriaMethodInvocationImposterGroupContinuation IArgumentsCriteriaMethodInvocationImposterGroupCallback.Callback(ArgumentsCriteriaCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IArgumentsCriteriaMethodInvocationImposterGroup IArgumentsCriteriaMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ArgumentsCriteriaInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _argumentsCriteriaMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodGroupedTypeCollisionTarget.CallbackDelegate(int first, int second)
        public delegate void CallbackDelegateDelegate(int first, int second);
        // void IMethodGroupedTypeCollisionTarget.CallbackDelegate(int first, int second)
        public delegate void CallbackDelegateCallbackDelegate(int first, int second);
        // void IMethodGroupedTypeCollisionTarget.CallbackDelegate(int first, int second)
        public delegate System.Exception CallbackDelegateExceptionGeneratorDelegate(int first, int second);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CallbackDelegateArguments
        {
            public int first;
            public int second;
            internal CallbackDelegateArguments(int first, int second)
            {
                this.first = first;
                this.second = second;
            }
        }

        // void IMethodGroupedTypeCollisionTarget.CallbackDelegate(int first, int second)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CallbackDelegateArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> first { get; }
            public global::Imposter.Abstractions.Arg<int> second { get; }

            public CallbackDelegateArgumentsCriteria(global::Imposter.Abstractions.Arg<int> first, global::Imposter.Abstractions.Arg<int> second)
            {
                this.first = first;
                this.second = second;
            }

            public bool Matches(CallbackDelegateArguments arguments)
            {
                return first.Matches(arguments.first) && second.Matches(arguments.second);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICallbackDelegateMethodInvocationHistory
        {
            bool Matches(CallbackDelegateArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CallbackDelegateMethodInvocationHistory : ICallbackDelegateMethodInvocationHistory
        {
            internal CallbackDelegateArguments Arguments;
            internal System.Exception Exception;
            public CallbackDelegateMethodInvocationHistory(CallbackDelegateArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(CallbackDelegateArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CallbackDelegateMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ICallbackDelegateMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ICallbackDelegateMethodInvocationHistory>();
            internal void Add(ICallbackDelegateMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(CallbackDelegateArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodGroupedTypeCollisionTarget.CallbackDelegate(int first, int second)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class CallbackDelegateMethodInvocationImposterGroup
        {
            internal static CallbackDelegateMethodInvocationImposterGroup Default = new CallbackDelegateMethodInvocationImposterGroup(new CallbackDelegateArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any(), global::Imposter.Abstractions.Arg<int>.Any()));
            internal CallbackDelegateArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public CallbackDelegateMethodInvocationImposterGroup(CallbackDelegateArgumentsCriteria argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int first, int second)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, first, second);
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

                private CallbackDelegateDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<CallbackDelegateCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<CallbackDelegateCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int first, int second)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(first, second);
                    foreach (var callback in _callbacks)
                    {
                        callback(first, second);
                    }
                }

                internal void Callback(CallbackDelegateCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(CallbackDelegateExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int first, int second) =>
                    {
                        throw exceptionGenerator(first, second);
                    };
                }

                internal static void DefaultResultGenerator(int first, int second)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICallbackDelegateMethodInvocationImposterGroupCallback
        {
            ICallbackDelegateMethodInvocationImposterGroupContinuation Callback(CallbackDelegateCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICallbackDelegateMethodInvocationImposterGroupContinuation : ICallbackDelegateMethodInvocationImposterGroupCallback
        {
            ICallbackDelegateMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICallbackDelegateMethodInvocationImposterGroup : ICallbackDelegateMethodInvocationImposterGroupCallback
        {
            ICallbackDelegateMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            ICallbackDelegateMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            ICallbackDelegateMethodInvocationImposterGroupContinuation Throws(CallbackDelegateExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CallbackDelegateInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodGroupedTypeCollisionTarget.CallbackDelegate(int first, int second)
        public interface ICallbackDelegateMethodImposterBuilder : ICallbackDelegateMethodInvocationImposterGroup, ICallbackDelegateMethodInvocationImposterGroupCallback, CallbackDelegateInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CallbackDelegateMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CallbackDelegateMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<CallbackDelegateMethodInvocationImposterGroup>();
            private readonly CallbackDelegateMethodInvocationHistoryCollection _callbackDelegateMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public CallbackDelegateMethodImposter(CallbackDelegateMethodInvocationHistoryCollection _callbackDelegateMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._callbackDelegateMethodInvocationHistoryCollection = _callbackDelegateMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(CallbackDelegateArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private CallbackDelegateMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(CallbackDelegateArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(int first, int second)
            {
                var arguments = new CallbackDelegateArguments(first, second);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodGroupedTypeCollisionTarget.CallbackDelegate(int first, int second)");
                    }

                    matchingInvocationImposterGroup = CallbackDelegateMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodGroupedTypeCollisionTarget.CallbackDelegate(int first, int second)", first, second);
                    _callbackDelegateMethodInvocationHistoryCollection.Add(new CallbackDelegateMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _callbackDelegateMethodInvocationHistoryCollection.Add(new CallbackDelegateMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ICallbackDelegateMethodImposterBuilder, ICallbackDelegateMethodInvocationImposterGroupContinuation
            {
                private readonly CallbackDelegateMethodImposter _imposter;
                private readonly CallbackDelegateMethodInvocationHistoryCollection _callbackDelegateMethodInvocationHistoryCollection;
                private readonly CallbackDelegateArgumentsCriteria _argumentsCriteria;
                private readonly CallbackDelegateMethodInvocationImposterGroup _invocationImposterGroup;
                private CallbackDelegateMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(CallbackDelegateMethodImposter _imposter, CallbackDelegateMethodInvocationHistoryCollection _callbackDelegateMethodInvocationHistoryCollection, CallbackDelegateArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._callbackDelegateMethodInvocationHistoryCollection = _callbackDelegateMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new CallbackDelegateMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ICallbackDelegateMethodInvocationImposterGroupContinuation ICallbackDelegateMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int first, int second) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ICallbackDelegateMethodInvocationImposterGroupContinuation ICallbackDelegateMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int first, int second) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ICallbackDelegateMethodInvocationImposterGroupContinuation ICallbackDelegateMethodInvocationImposterGroup.Throws(CallbackDelegateExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int first, int second) =>
                    {
                        throw exceptionGenerator.Invoke(first, second);
                    });
                    return this;
                }

                ICallbackDelegateMethodInvocationImposterGroupContinuation ICallbackDelegateMethodInvocationImposterGroupCallback.Callback(CallbackDelegateCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ICallbackDelegateMethodInvocationImposterGroup ICallbackDelegateMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void CallbackDelegateInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _callbackDelegateMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodGroupedTypeCollisionTarget.Collection(int count)
        public delegate void CollectionDelegate(int count);
        // void IMethodGroupedTypeCollisionTarget.Collection(int count)
        public delegate void CollectionCallbackDelegate(int count);
        // void IMethodGroupedTypeCollisionTarget.Collection(int count)
        public delegate System.Exception CollectionExceptionGeneratorDelegate(int count);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollectionArguments
        {
            public int count;
            internal CollectionArguments(int count)
            {
                this.count = count;
            }
        }

        // void IMethodGroupedTypeCollisionTarget.Collection(int count)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CollectionArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> count { get; }

            public CollectionArgumentsCriteria(global::Imposter.Abstractions.Arg<int> count)
            {
                this.count = count;
            }

            public bool Matches(CollectionArguments arguments)
            {
                return count.Matches(arguments.count);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollectionMethodInvocationHistory
        {
            bool Matches(CollectionArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollectionMethodInvocationHistory : ICollectionMethodInvocationHistory
        {
            internal CollectionArguments Arguments;
            internal System.Exception Exception;
            public CollectionMethodInvocationHistory(CollectionArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(CollectionArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollectionMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ICollectionMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ICollectionMethodInvocationHistory>();
            internal void Add(ICollectionMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(CollectionArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodGroupedTypeCollisionTarget.Collection(int count)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class CollectionMethodInvocationImposterGroup
        {
            internal static CollectionMethodInvocationImposterGroup Default = new CollectionMethodInvocationImposterGroup(new CollectionArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any()));
            internal CollectionArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public CollectionMethodInvocationImposterGroup(CollectionArgumentsCriteria argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int count)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, count);
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

                private CollectionDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<CollectionCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<CollectionCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int count)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(count);
                    foreach (var callback in _callbacks)
                    {
                        callback(count);
                    }
                }

                internal void Callback(CollectionCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(CollectionExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int count) =>
                    {
                        throw exceptionGenerator(count);
                    };
                }

                internal static void DefaultResultGenerator(int count)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollectionMethodInvocationImposterGroupCallback
        {
            ICollectionMethodInvocationImposterGroupContinuation Callback(CollectionCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollectionMethodInvocationImposterGroupContinuation : ICollectionMethodInvocationImposterGroupCallback
        {
            ICollectionMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ICollectionMethodInvocationImposterGroup : ICollectionMethodInvocationImposterGroupCallback
        {
            ICollectionMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            ICollectionMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            ICollectionMethodInvocationImposterGroupContinuation Throws(CollectionExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CollectionInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodGroupedTypeCollisionTarget.Collection(int count)
        public interface ICollectionMethodImposterBuilder : ICollectionMethodInvocationImposterGroup, ICollectionMethodInvocationImposterGroupCallback, CollectionInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CollectionMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CollectionMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<CollectionMethodInvocationImposterGroup>();
            private readonly CollectionMethodInvocationHistoryCollection _collectionMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public CollectionMethodImposter(CollectionMethodInvocationHistoryCollection _collectionMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._collectionMethodInvocationHistoryCollection = _collectionMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(CollectionArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private CollectionMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(CollectionArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(int count)
            {
                var arguments = new CollectionArguments(count);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodGroupedTypeCollisionTarget.Collection(int count)");
                    }

                    matchingInvocationImposterGroup = CollectionMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodGroupedTypeCollisionTarget.Collection(int count)", count);
                    _collectionMethodInvocationHistoryCollection.Add(new CollectionMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _collectionMethodInvocationHistoryCollection.Add(new CollectionMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ICollectionMethodImposterBuilder, ICollectionMethodInvocationImposterGroupContinuation
            {
                private readonly CollectionMethodImposter _imposter;
                private readonly CollectionMethodInvocationHistoryCollection _collectionMethodInvocationHistoryCollection;
                private readonly CollectionArgumentsCriteria _argumentsCriteria;
                private readonly CollectionMethodInvocationImposterGroup _invocationImposterGroup;
                private CollectionMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(CollectionMethodImposter _imposter, CollectionMethodInvocationHistoryCollection _collectionMethodInvocationHistoryCollection, CollectionArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._collectionMethodInvocationHistoryCollection = _collectionMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new CollectionMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ICollectionMethodInvocationImposterGroupContinuation ICollectionMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int count) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ICollectionMethodInvocationImposterGroupContinuation ICollectionMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int count) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ICollectionMethodInvocationImposterGroupContinuation ICollectionMethodInvocationImposterGroup.Throws(CollectionExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int count) =>
                    {
                        throw exceptionGenerator.Invoke(count);
                    });
                    return this;
                }

                ICollectionMethodInvocationImposterGroupContinuation ICollectionMethodInvocationImposterGroupCallback.Callback(CollectionCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ICollectionMethodInvocationImposterGroup ICollectionMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void CollectionInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _collectionMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodGroupedTypeCollisionTarget.Delegate(int value)
        public delegate void DelegateDelegate(int value);
        // void IMethodGroupedTypeCollisionTarget.Delegate(int value)
        public delegate void DelegateCallbackDelegate(int value);
        // void IMethodGroupedTypeCollisionTarget.Delegate(int value)
        public delegate System.Exception DelegateExceptionGeneratorDelegate(int value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class DelegateArguments
        {
            public int value;
            internal DelegateArguments(int value)
            {
                this.value = value;
            }
        }

        // void IMethodGroupedTypeCollisionTarget.Delegate(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class DelegateArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> value { get; }

            public DelegateArgumentsCriteria(global::Imposter.Abstractions.Arg<int> value)
            {
                this.value = value;
            }

            public bool Matches(DelegateArguments arguments)
            {
                return value.Matches(arguments.value);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDelegateMethodInvocationHistory
        {
            bool Matches(DelegateArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DelegateMethodInvocationHistory : IDelegateMethodInvocationHistory
        {
            internal DelegateArguments Arguments;
            internal System.Exception Exception;
            public DelegateMethodInvocationHistory(DelegateArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(DelegateArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DelegateMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IDelegateMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IDelegateMethodInvocationHistory>();
            internal void Add(IDelegateMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(DelegateArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodGroupedTypeCollisionTarget.Delegate(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class DelegateMethodInvocationImposterGroup
        {
            internal static DelegateMethodInvocationImposterGroup Default = new DelegateMethodInvocationImposterGroup(new DelegateArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any()));
            internal DelegateArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public DelegateMethodInvocationImposterGroup(DelegateArgumentsCriteria argumentsCriteria)
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

                private DelegateDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<DelegateCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<DelegateCallbackDelegate>();
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

                internal void Callback(DelegateCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(DelegateExceptionGeneratorDelegate exceptionGenerator)
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
        public interface IDelegateMethodInvocationImposterGroupCallback
        {
            IDelegateMethodInvocationImposterGroupContinuation Callback(DelegateCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDelegateMethodInvocationImposterGroupContinuation : IDelegateMethodInvocationImposterGroupCallback
        {
            IDelegateMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDelegateMethodInvocationImposterGroup : IDelegateMethodInvocationImposterGroupCallback
        {
            IDelegateMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IDelegateMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IDelegateMethodInvocationImposterGroupContinuation Throws(DelegateExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface DelegateInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodGroupedTypeCollisionTarget.Delegate(int value)
        public interface IDelegateMethodImposterBuilder : IDelegateMethodInvocationImposterGroup, IDelegateMethodInvocationImposterGroupCallback, DelegateInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DelegateMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<DelegateMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<DelegateMethodInvocationImposterGroup>();
            private readonly DelegateMethodInvocationHistoryCollection _delegateMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public DelegateMethodImposter(DelegateMethodInvocationHistoryCollection _delegateMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._delegateMethodInvocationHistoryCollection = _delegateMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(DelegateArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private DelegateMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(DelegateArguments arguments)
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
                var arguments = new DelegateArguments(value);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodGroupedTypeCollisionTarget.Delegate(int value)");
                    }

                    matchingInvocationImposterGroup = DelegateMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodGroupedTypeCollisionTarget.Delegate(int value)", value);
                    _delegateMethodInvocationHistoryCollection.Add(new DelegateMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _delegateMethodInvocationHistoryCollection.Add(new DelegateMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IDelegateMethodImposterBuilder, IDelegateMethodInvocationImposterGroupContinuation
            {
                private readonly DelegateMethodImposter _imposter;
                private readonly DelegateMethodInvocationHistoryCollection _delegateMethodInvocationHistoryCollection;
                private readonly DelegateArgumentsCriteria _argumentsCriteria;
                private readonly DelegateMethodInvocationImposterGroup _invocationImposterGroup;
                private DelegateMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(DelegateMethodImposter _imposter, DelegateMethodInvocationHistoryCollection _delegateMethodInvocationHistoryCollection, DelegateArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._delegateMethodInvocationHistoryCollection = _delegateMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new DelegateMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IDelegateMethodInvocationImposterGroupContinuation IDelegateMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IDelegateMethodInvocationImposterGroupContinuation IDelegateMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IDelegateMethodInvocationImposterGroupContinuation IDelegateMethodInvocationImposterGroup.Throws(DelegateExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exceptionGenerator.Invoke(value);
                    });
                    return this;
                }

                IDelegateMethodInvocationImposterGroupContinuation IDelegateMethodInvocationImposterGroupCallback.Callback(DelegateCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IDelegateMethodInvocationImposterGroup IDelegateMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void DelegateInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _delegateMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodGroupedTypeCollisionTarget.ExceptionGeneratorDelegate(Exception error)
        public delegate void ExceptionGeneratorDelegateDelegate(global::System.Exception error);
        // void IMethodGroupedTypeCollisionTarget.ExceptionGeneratorDelegate(Exception error)
        public delegate void ExceptionGeneratorDelegateCallbackDelegate(global::System.Exception error);
        // void IMethodGroupedTypeCollisionTarget.ExceptionGeneratorDelegate(Exception error)
        public delegate System.Exception ExceptionGeneratorDelegateExceptionGeneratorDelegate(global::System.Exception error);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ExceptionGeneratorDelegateArguments
        {
            public global::System.Exception error;
            internal ExceptionGeneratorDelegateArguments(global::System.Exception error)
            {
                this.error = error;
            }
        }

        // void IMethodGroupedTypeCollisionTarget.ExceptionGeneratorDelegate(Exception error)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class ExceptionGeneratorDelegateArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<global::System.Exception> error { get; }

            public ExceptionGeneratorDelegateArgumentsCriteria(global::Imposter.Abstractions.Arg<global::System.Exception> error)
            {
                this.error = error;
            }

            public bool Matches(ExceptionGeneratorDelegateArguments arguments)
            {
                return error.Matches(arguments.error);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IExceptionGeneratorDelegateMethodInvocationHistory
        {
            bool Matches(ExceptionGeneratorDelegateArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ExceptionGeneratorDelegateMethodInvocationHistory : IExceptionGeneratorDelegateMethodInvocationHistory
        {
            internal ExceptionGeneratorDelegateArguments Arguments;
            internal System.Exception Exception;
            public ExceptionGeneratorDelegateMethodInvocationHistory(ExceptionGeneratorDelegateArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(ExceptionGeneratorDelegateArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ExceptionGeneratorDelegateMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IExceptionGeneratorDelegateMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IExceptionGeneratorDelegateMethodInvocationHistory>();
            internal void Add(IExceptionGeneratorDelegateMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(ExceptionGeneratorDelegateArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodGroupedTypeCollisionTarget.ExceptionGeneratorDelegate(Exception error)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ExceptionGeneratorDelegateMethodInvocationImposterGroup
        {
            internal static ExceptionGeneratorDelegateMethodInvocationImposterGroup Default = new ExceptionGeneratorDelegateMethodInvocationImposterGroup(new ExceptionGeneratorDelegateArgumentsCriteria(global::Imposter.Abstractions.Arg<global::System.Exception>.Any()));
            internal ExceptionGeneratorDelegateArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public ExceptionGeneratorDelegateMethodInvocationImposterGroup(ExceptionGeneratorDelegateArgumentsCriteria argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, global::System.Exception error)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, error);
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

                private ExceptionGeneratorDelegateDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<ExceptionGeneratorDelegateCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<ExceptionGeneratorDelegateCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, global::System.Exception error)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(error);
                    foreach (var callback in _callbacks)
                    {
                        callback(error);
                    }
                }

                internal void Callback(ExceptionGeneratorDelegateCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(ExceptionGeneratorDelegateExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (global::System.Exception error) =>
                    {
                        throw exceptionGenerator(error);
                    };
                }

                internal static void DefaultResultGenerator(global::System.Exception error)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IExceptionGeneratorDelegateMethodInvocationImposterGroupCallback
        {
            IExceptionGeneratorDelegateMethodInvocationImposterGroupContinuation Callback(ExceptionGeneratorDelegateCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IExceptionGeneratorDelegateMethodInvocationImposterGroupContinuation : IExceptionGeneratorDelegateMethodInvocationImposterGroupCallback
        {
            IExceptionGeneratorDelegateMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IExceptionGeneratorDelegateMethodInvocationImposterGroup : IExceptionGeneratorDelegateMethodInvocationImposterGroupCallback
        {
            IExceptionGeneratorDelegateMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IExceptionGeneratorDelegateMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IExceptionGeneratorDelegateMethodInvocationImposterGroupContinuation Throws(ExceptionGeneratorDelegateExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ExceptionGeneratorDelegateInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodGroupedTypeCollisionTarget.ExceptionGeneratorDelegate(Exception error)
        public interface IExceptionGeneratorDelegateMethodImposterBuilder : IExceptionGeneratorDelegateMethodInvocationImposterGroup, IExceptionGeneratorDelegateMethodInvocationImposterGroupCallback, ExceptionGeneratorDelegateInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class ExceptionGeneratorDelegateMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ExceptionGeneratorDelegateMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<ExceptionGeneratorDelegateMethodInvocationImposterGroup>();
            private readonly ExceptionGeneratorDelegateMethodInvocationHistoryCollection _exceptionGeneratorDelegateMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public ExceptionGeneratorDelegateMethodImposter(ExceptionGeneratorDelegateMethodInvocationHistoryCollection _exceptionGeneratorDelegateMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._exceptionGeneratorDelegateMethodInvocationHistoryCollection = _exceptionGeneratorDelegateMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(ExceptionGeneratorDelegateArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private ExceptionGeneratorDelegateMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(ExceptionGeneratorDelegateArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(global::System.Exception error)
            {
                var arguments = new ExceptionGeneratorDelegateArguments(error);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodGroupedTypeCollisionTarget.ExceptionGeneratorDelegate(Exception error)");
                    }

                    matchingInvocationImposterGroup = ExceptionGeneratorDelegateMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodGroupedTypeCollisionTarget.ExceptionGeneratorDelegate(Exception error)", error);
                    _exceptionGeneratorDelegateMethodInvocationHistoryCollection.Add(new ExceptionGeneratorDelegateMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _exceptionGeneratorDelegateMethodInvocationHistoryCollection.Add(new ExceptionGeneratorDelegateMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IExceptionGeneratorDelegateMethodImposterBuilder, IExceptionGeneratorDelegateMethodInvocationImposterGroupContinuation
            {
                private readonly ExceptionGeneratorDelegateMethodImposter _imposter;
                private readonly ExceptionGeneratorDelegateMethodInvocationHistoryCollection _exceptionGeneratorDelegateMethodInvocationHistoryCollection;
                private readonly ExceptionGeneratorDelegateArgumentsCriteria _argumentsCriteria;
                private readonly ExceptionGeneratorDelegateMethodInvocationImposterGroup _invocationImposterGroup;
                private ExceptionGeneratorDelegateMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(ExceptionGeneratorDelegateMethodImposter _imposter, ExceptionGeneratorDelegateMethodInvocationHistoryCollection _exceptionGeneratorDelegateMethodInvocationHistoryCollection, ExceptionGeneratorDelegateArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._exceptionGeneratorDelegateMethodInvocationHistoryCollection = _exceptionGeneratorDelegateMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new ExceptionGeneratorDelegateMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IExceptionGeneratorDelegateMethodInvocationImposterGroupContinuation IExceptionGeneratorDelegateMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((global::System.Exception error) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IExceptionGeneratorDelegateMethodInvocationImposterGroupContinuation IExceptionGeneratorDelegateMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((global::System.Exception error) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IExceptionGeneratorDelegateMethodInvocationImposterGroupContinuation IExceptionGeneratorDelegateMethodInvocationImposterGroup.Throws(ExceptionGeneratorDelegateExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((global::System.Exception error) =>
                    {
                        throw exceptionGenerator.Invoke(error);
                    });
                    return this;
                }

                IExceptionGeneratorDelegateMethodInvocationImposterGroupContinuation IExceptionGeneratorDelegateMethodInvocationImposterGroupCallback.Callback(ExceptionGeneratorDelegateCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IExceptionGeneratorDelegateMethodInvocationImposterGroup IExceptionGeneratorDelegateMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void ExceptionGeneratorDelegateInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _exceptionGeneratorDelegateMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodGroupedTypeCollisionTarget.MethodImposter(int value)
        public delegate void MethodImposterDelegate(int value);
        // void IMethodGroupedTypeCollisionTarget.MethodImposter(int value)
        public delegate void MethodImposterCallbackDelegate(int value);
        // void IMethodGroupedTypeCollisionTarget.MethodImposter(int value)
        public delegate System.Exception MethodImposterExceptionGeneratorDelegate(int value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class MethodImposterArguments
        {
            public int value;
            internal MethodImposterArguments(int value)
            {
                this.value = value;
            }
        }

        // void IMethodGroupedTypeCollisionTarget.MethodImposter(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class MethodImposterArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> value { get; }

            public MethodImposterArgumentsCriteria(global::Imposter.Abstractions.Arg<int> value)
            {
                this.value = value;
            }

            public bool Matches(MethodImposterArguments arguments)
            {
                return value.Matches(arguments.value);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodImposterMethodInvocationHistory
        {
            bool Matches(MethodImposterArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodImposterMethodInvocationHistory : IMethodImposterMethodInvocationHistory
        {
            internal MethodImposterArguments Arguments;
            internal System.Exception Exception;
            public MethodImposterMethodInvocationHistory(MethodImposterArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(MethodImposterArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodImposterMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IMethodImposterMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IMethodImposterMethodInvocationHistory>();
            internal void Add(IMethodImposterMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(MethodImposterArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodGroupedTypeCollisionTarget.MethodImposter(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class MethodImposterMethodInvocationImposterGroup
        {
            internal static MethodImposterMethodInvocationImposterGroup Default = new MethodImposterMethodInvocationImposterGroup(new MethodImposterArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any()));
            internal MethodImposterArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public MethodImposterMethodInvocationImposterGroup(MethodImposterArgumentsCriteria argumentsCriteria)
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

                private MethodImposterDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<MethodImposterCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<MethodImposterCallbackDelegate>();
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

                internal void Callback(MethodImposterCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(MethodImposterExceptionGeneratorDelegate exceptionGenerator)
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
        public interface IMethodImposterMethodInvocationImposterGroupCallback
        {
            IMethodImposterMethodInvocationImposterGroupContinuation Callback(MethodImposterCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodImposterMethodInvocationImposterGroupContinuation : IMethodImposterMethodInvocationImposterGroupCallback
        {
            IMethodImposterMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodImposterMethodInvocationImposterGroup : IMethodImposterMethodInvocationImposterGroupCallback
        {
            IMethodImposterMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IMethodImposterMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IMethodImposterMethodInvocationImposterGroupContinuation Throws(MethodImposterExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface MethodImposterInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodGroupedTypeCollisionTarget.MethodImposter(int value)
        public interface IMethodImposterMethodImposterBuilder : IMethodImposterMethodInvocationImposterGroup, IMethodImposterMethodInvocationImposterGroupCallback, MethodImposterInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodImposterMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<MethodImposterMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<MethodImposterMethodInvocationImposterGroup>();
            private readonly MethodImposterMethodInvocationHistoryCollection _methodImposterMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public MethodImposterMethodImposter(MethodImposterMethodInvocationHistoryCollection _methodImposterMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._methodImposterMethodInvocationHistoryCollection = _methodImposterMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(MethodImposterArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private MethodImposterMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(MethodImposterArguments arguments)
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
                var arguments = new MethodImposterArguments(value);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodGroupedTypeCollisionTarget.MethodImposter(int value)");
                    }

                    matchingInvocationImposterGroup = MethodImposterMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodGroupedTypeCollisionTarget.MethodImposter(int value)", value);
                    _methodImposterMethodInvocationHistoryCollection.Add(new MethodImposterMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _methodImposterMethodInvocationHistoryCollection.Add(new MethodImposterMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IMethodImposterMethodImposterBuilder, IMethodImposterMethodInvocationImposterGroupContinuation
            {
                private readonly MethodImposterMethodImposter _imposter;
                private readonly MethodImposterMethodInvocationHistoryCollection _methodImposterMethodInvocationHistoryCollection;
                private readonly MethodImposterArgumentsCriteria _argumentsCriteria;
                private readonly MethodImposterMethodInvocationImposterGroup _invocationImposterGroup;
                private MethodImposterMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(MethodImposterMethodImposter _imposter, MethodImposterMethodInvocationHistoryCollection _methodImposterMethodInvocationHistoryCollection, MethodImposterArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._methodImposterMethodInvocationHistoryCollection = _methodImposterMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new MethodImposterMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IMethodImposterMethodInvocationImposterGroupContinuation IMethodImposterMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IMethodImposterMethodInvocationImposterGroupContinuation IMethodImposterMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IMethodImposterMethodInvocationImposterGroupContinuation IMethodImposterMethodInvocationImposterGroup.Throws(MethodImposterExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exceptionGenerator.Invoke(value);
                    });
                    return this;
                }

                IMethodImposterMethodInvocationImposterGroupContinuation IMethodImposterMethodInvocationImposterGroupCallback.Callback(MethodImposterCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IMethodImposterMethodInvocationImposterGroup IMethodImposterMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void MethodImposterInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _methodImposterMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodGroupedTypeCollisionTarget.MethodInvocationHistory()
        public delegate void MethodInvocationHistoryDelegate();
        // void IMethodGroupedTypeCollisionTarget.MethodInvocationHistory()
        public delegate void MethodInvocationHistoryCallbackDelegate();
        // void IMethodGroupedTypeCollisionTarget.MethodInvocationHistory()
        public delegate System.Exception MethodInvocationHistoryExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodInvocationHistoryMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodInvocationHistoryMethodInvocationHistory : IMethodInvocationHistoryMethodInvocationHistory
        {
            internal System.Exception Exception;
            public MethodInvocationHistoryMethodInvocationHistory(System.Exception Exception)
            {
                this.Exception = Exception;
            }

            public bool Matches()
            {
                return true;
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodInvocationHistoryMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IMethodInvocationHistoryMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IMethodInvocationHistoryMethodInvocationHistory>();
            internal void Add(IMethodInvocationHistoryMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // void IMethodGroupedTypeCollisionTarget.MethodInvocationHistory()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class MethodInvocationHistoryMethodInvocationImposterGroup
        {
            internal static MethodInvocationHistoryMethodInvocationImposterGroup Default = new MethodInvocationHistoryMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public MethodInvocationHistoryMethodInvocationImposterGroup()
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

                private MethodInvocationHistoryDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationHistoryCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationHistoryCallbackDelegate>();
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

                internal void Callback(MethodInvocationHistoryCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(MethodInvocationHistoryExceptionGeneratorDelegate exceptionGenerator)
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
        public interface IMethodInvocationHistoryMethodInvocationImposterGroupCallback
        {
            IMethodInvocationHistoryMethodInvocationImposterGroupContinuation Callback(MethodInvocationHistoryCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodInvocationHistoryMethodInvocationImposterGroupContinuation : IMethodInvocationHistoryMethodInvocationImposterGroupCallback
        {
            IMethodInvocationHistoryMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodInvocationHistoryMethodInvocationImposterGroup : IMethodInvocationHistoryMethodInvocationImposterGroupCallback
        {
            IMethodInvocationHistoryMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IMethodInvocationHistoryMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IMethodInvocationHistoryMethodInvocationImposterGroupContinuation Throws(MethodInvocationHistoryExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface MethodInvocationHistoryInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodGroupedTypeCollisionTarget.MethodInvocationHistory()
        public interface IMethodInvocationHistoryMethodImposterBuilder : IMethodInvocationHistoryMethodInvocationImposterGroup, IMethodInvocationHistoryMethodInvocationImposterGroupCallback, MethodInvocationHistoryInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodInvocationHistoryMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<MethodInvocationHistoryMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<MethodInvocationHistoryMethodInvocationImposterGroup>();
            private readonly MethodInvocationHistoryMethodInvocationHistoryCollection _methodInvocationHistoryMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public MethodInvocationHistoryMethodImposter(MethodInvocationHistoryMethodInvocationHistoryCollection _methodInvocationHistoryMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._methodInvocationHistoryMethodInvocationHistoryCollection = _methodInvocationHistoryMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private MethodInvocationHistoryMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodGroupedTypeCollisionTarget.MethodInvocationHistory()");
                    }

                    matchingInvocationImposterGroup = MethodInvocationHistoryMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodGroupedTypeCollisionTarget.MethodInvocationHistory()");
                    _methodInvocationHistoryMethodInvocationHistoryCollection.Add(new MethodInvocationHistoryMethodInvocationHistory(default));
                }
                catch (System.Exception ex)
                {
                    _methodInvocationHistoryMethodInvocationHistoryCollection.Add(new MethodInvocationHistoryMethodInvocationHistory(ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IMethodInvocationHistoryMethodImposterBuilder, IMethodInvocationHistoryMethodInvocationImposterGroupContinuation
            {
                private readonly MethodInvocationHistoryMethodImposter _imposter;
                private readonly MethodInvocationHistoryMethodInvocationHistoryCollection _methodInvocationHistoryMethodInvocationHistoryCollection;
                private readonly MethodInvocationHistoryMethodInvocationImposterGroup _invocationImposterGroup;
                private MethodInvocationHistoryMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(MethodInvocationHistoryMethodImposter _imposter, MethodInvocationHistoryMethodInvocationHistoryCollection _methodInvocationHistoryMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._methodInvocationHistoryMethodInvocationHistoryCollection = _methodInvocationHistoryMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new MethodInvocationHistoryMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IMethodInvocationHistoryMethodInvocationImposterGroupContinuation IMethodInvocationHistoryMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IMethodInvocationHistoryMethodInvocationImposterGroupContinuation IMethodInvocationHistoryMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IMethodInvocationHistoryMethodInvocationImposterGroupContinuation IMethodInvocationHistoryMethodInvocationImposterGroup.Throws(MethodInvocationHistoryExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IMethodInvocationHistoryMethodInvocationImposterGroupContinuation IMethodInvocationHistoryMethodInvocationImposterGroupCallback.Callback(MethodInvocationHistoryCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IMethodInvocationHistoryMethodInvocationImposterGroup IMethodInvocationHistoryMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void MethodInvocationHistoryInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _methodInvocationHistoryMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // void IMethodGroupedTypeCollisionTarget.MethodInvocationImposterGroup(string label)
        public delegate void MethodInvocationImposterGroupDelegate(string label);
        // void IMethodGroupedTypeCollisionTarget.MethodInvocationImposterGroup(string label)
        public delegate void MethodInvocationImposterGroupCallbackDelegate(string label);
        // void IMethodGroupedTypeCollisionTarget.MethodInvocationImposterGroup(string label)
        public delegate System.Exception MethodInvocationImposterGroupExceptionGeneratorDelegate(string label);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class MethodInvocationImposterGroupArguments
        {
            public string label;
            internal MethodInvocationImposterGroupArguments(string label)
            {
                this.label = label;
            }
        }

        // void IMethodGroupedTypeCollisionTarget.MethodInvocationImposterGroup(string label)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class MethodInvocationImposterGroupArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<string> label { get; }

            public MethodInvocationImposterGroupArgumentsCriteria(global::Imposter.Abstractions.Arg<string> label)
            {
                this.label = label;
            }

            public bool Matches(MethodInvocationImposterGroupArguments arguments)
            {
                return label.Matches(arguments.label);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodInvocationImposterGroupMethodInvocationHistory
        {
            bool Matches(MethodInvocationImposterGroupArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodInvocationImposterGroupMethodInvocationHistory : IMethodInvocationImposterGroupMethodInvocationHistory
        {
            internal MethodInvocationImposterGroupArguments Arguments;
            internal System.Exception Exception;
            public MethodInvocationImposterGroupMethodInvocationHistory(MethodInvocationImposterGroupArguments Arguments, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Exception = Exception;
            }

            public bool Matches(MethodInvocationImposterGroupArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodInvocationImposterGroupMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IMethodInvocationImposterGroupMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IMethodInvocationImposterGroupMethodInvocationHistory>();
            internal void Add(IMethodInvocationImposterGroupMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(MethodInvocationImposterGroupArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // void IMethodGroupedTypeCollisionTarget.MethodInvocationImposterGroup(string label)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class MethodInvocationImposterGroupMethodInvocationImposterGroup
        {
            internal static MethodInvocationImposterGroupMethodInvocationImposterGroup Default = new MethodInvocationImposterGroupMethodInvocationImposterGroup(new MethodInvocationImposterGroupArgumentsCriteria(global::Imposter.Abstractions.Arg<string>.Any()));
            internal MethodInvocationImposterGroupArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public MethodInvocationImposterGroupMethodInvocationImposterGroup(MethodInvocationImposterGroupArgumentsCriteria argumentsCriteria)
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

            public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string label)
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

                invocationImposter.Invoke(invocationBehavior, methodDisplayName, label);
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

                private MethodInvocationImposterGroupDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposterGroupCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposterGroupCallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public void Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string label)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    _resultGenerator.Invoke(label);
                    foreach (var callback in _callbacks)
                    {
                        callback(label);
                    }
                }

                internal void Callback(MethodInvocationImposterGroupCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Throws(MethodInvocationImposterGroupExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (string label) =>
                    {
                        throw exceptionGenerator(label);
                    };
                }

                internal static void DefaultResultGenerator(string label)
                {
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodInvocationImposterGroupMethodInvocationImposterGroupCallback
        {
            IMethodInvocationImposterGroupMethodInvocationImposterGroupContinuation Callback(MethodInvocationImposterGroupCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodInvocationImposterGroupMethodInvocationImposterGroupContinuation : IMethodInvocationImposterGroupMethodInvocationImposterGroupCallback
        {
            IMethodInvocationImposterGroupMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IMethodInvocationImposterGroupMethodInvocationImposterGroup : IMethodInvocationImposterGroupMethodInvocationImposterGroupCallback
        {
            IMethodInvocationImposterGroupMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IMethodInvocationImposterGroupMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IMethodInvocationImposterGroupMethodInvocationImposterGroupContinuation Throws(MethodInvocationImposterGroupExceptionGeneratorDelegate exceptionGenerator);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface MethodInvocationImposterGroupInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // void IMethodGroupedTypeCollisionTarget.MethodInvocationImposterGroup(string label)
        public interface IMethodInvocationImposterGroupMethodImposterBuilder : IMethodInvocationImposterGroupMethodInvocationImposterGroup, IMethodInvocationImposterGroupMethodInvocationImposterGroupCallback, MethodInvocationImposterGroupInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class MethodInvocationImposterGroupMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<MethodInvocationImposterGroupMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<MethodInvocationImposterGroupMethodInvocationImposterGroup>();
            private readonly MethodInvocationImposterGroupMethodInvocationHistoryCollection _methodInvocationImposterGroupMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public MethodInvocationImposterGroupMethodImposter(MethodInvocationImposterGroupMethodInvocationHistoryCollection _methodInvocationImposterGroupMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._methodInvocationImposterGroupMethodInvocationHistoryCollection = _methodInvocationImposterGroupMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(MethodInvocationImposterGroupArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private MethodInvocationImposterGroupMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(MethodInvocationImposterGroupArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public void Invoke(string label)
            {
                var arguments = new MethodInvocationImposterGroupArguments(label);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("void IMethodGroupedTypeCollisionTarget.MethodInvocationImposterGroup(string label)");
                    }

                    matchingInvocationImposterGroup = MethodInvocationImposterGroupMethodInvocationImposterGroup.Default;
                }

                try
                {
                    matchingInvocationImposterGroup.Invoke(_invocationBehavior, "void IMethodGroupedTypeCollisionTarget.MethodInvocationImposterGroup(string label)", label);
                    _methodInvocationImposterGroupMethodInvocationHistoryCollection.Add(new MethodInvocationImposterGroupMethodInvocationHistory(arguments, default));
                }
                catch (System.Exception ex)
                {
                    _methodInvocationImposterGroupMethodInvocationHistoryCollection.Add(new MethodInvocationImposterGroupMethodInvocationHistory(arguments, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IMethodInvocationImposterGroupMethodImposterBuilder, IMethodInvocationImposterGroupMethodInvocationImposterGroupContinuation
            {
                private readonly MethodInvocationImposterGroupMethodImposter _imposter;
                private readonly MethodInvocationImposterGroupMethodInvocationHistoryCollection _methodInvocationImposterGroupMethodInvocationHistoryCollection;
                private readonly MethodInvocationImposterGroupArgumentsCriteria _argumentsCriteria;
                private readonly MethodInvocationImposterGroupMethodInvocationImposterGroup _invocationImposterGroup;
                private MethodInvocationImposterGroupMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(MethodInvocationImposterGroupMethodImposter _imposter, MethodInvocationImposterGroupMethodInvocationHistoryCollection _methodInvocationImposterGroupMethodInvocationHistoryCollection, MethodInvocationImposterGroupArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._methodInvocationImposterGroupMethodInvocationHistoryCollection = _methodInvocationImposterGroupMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new MethodInvocationImposterGroupMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IMethodInvocationImposterGroupMethodInvocationImposterGroupContinuation IMethodInvocationImposterGroupMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((string label) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IMethodInvocationImposterGroupMethodInvocationImposterGroupContinuation IMethodInvocationImposterGroupMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((string label) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IMethodInvocationImposterGroupMethodInvocationImposterGroupContinuation IMethodInvocationImposterGroupMethodInvocationImposterGroup.Throws(MethodInvocationImposterGroupExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((string label) =>
                    {
                        throw exceptionGenerator.Invoke(label);
                    });
                    return this;
                }

                IMethodInvocationImposterGroupMethodInvocationImposterGroupContinuation IMethodInvocationImposterGroupMethodInvocationImposterGroupCallback.Callback(MethodInvocationImposterGroupCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IMethodInvocationImposterGroupMethodInvocationImposterGroup IMethodInvocationImposterGroupMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void MethodInvocationImposterGroupInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _methodInvocationImposterGroupMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodGroupedTypeCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._methodImposterMethodImposter = new MethodImposterMethodImposter(_methodImposterMethodInvocationHistoryCollection, invocationBehavior);
            this._methodInvocationImposterGroupMethodImposter = new MethodInvocationImposterGroupMethodImposter(_methodInvocationImposterGroupMethodInvocationHistoryCollection, invocationBehavior);
            this._methodInvocationHistoryMethodImposter = new MethodInvocationHistoryMethodImposter(_methodInvocationHistoryMethodInvocationHistoryCollection, invocationBehavior);
            this._argumentsCriteriaMethodImposter = new ArgumentsCriteriaMethodImposter(_argumentsCriteriaMethodInvocationHistoryCollection, invocationBehavior);
            this._argumentsMethodImposter = new ArgumentsMethodImposter(_argumentsMethodInvocationHistoryCollection, invocationBehavior);
            this._delegateMethodImposter = new DelegateMethodImposter(_delegateMethodInvocationHistoryCollection, invocationBehavior);
            this._callbackDelegateMethodImposter = new CallbackDelegateMethodImposter(_callbackDelegateMethodInvocationHistoryCollection, invocationBehavior);
            this._exceptionGeneratorDelegateMethodImposter = new ExceptionGeneratorDelegateMethodImposter(_exceptionGeneratorDelegateMethodInvocationHistoryCollection, invocationBehavior);
            this._collectionMethodImposter = new CollectionMethodImposter(_collectionMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodGroupedTypeCollisionTarget
        {
            IMethodGroupedTypeCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodGroupedTypeCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public void MethodImposter(int value)
            {
                _imposter._methodImposterMethodImposter.Invoke(value);
            }

            public void MethodInvocationImposterGroup(string label)
            {
                _imposter._methodInvocationImposterGroupMethodImposter.Invoke(label);
            }

            public void MethodInvocationHistory()
            {
                _imposter._methodInvocationHistoryMethodImposter.Invoke();
            }

            public void ArgumentsCriteria(int left, int right)
            {
                _imposter._argumentsCriteriaMethodImposter.Invoke(left, right);
            }

            public void Arguments(string label)
            {
                _imposter._argumentsMethodImposter.Invoke(label);
            }

            public void Delegate(int value)
            {
                _imposter._delegateMethodImposter.Invoke(value);
            }

            public void CallbackDelegate(int first, int second)
            {
                _imposter._callbackDelegateMethodImposter.Invoke(first, second);
            }

            public void ExceptionGeneratorDelegate(global::System.Exception error)
            {
                _imposter._exceptionGeneratorDelegateMethodImposter.Invoke(error);
            }

            public void Collection(int count)
            {
                _imposter._collectionMethodImposter.Invoke(count);
            }
        }
    }
}
#pragma warning restore nullable
