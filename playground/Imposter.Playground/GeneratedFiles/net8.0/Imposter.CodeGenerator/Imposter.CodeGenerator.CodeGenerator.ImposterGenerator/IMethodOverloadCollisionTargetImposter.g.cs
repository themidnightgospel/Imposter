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
    public class IMethodOverloadCollisionTargetImposter : global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodOverloadCollisionTarget>
    {
        private readonly DuplicateMethodImposter _duplicateMethodImposter;
        private readonly Duplicate_1MethodImposter _duplicate_1MethodImposter;
        private readonly Duplicate_2MethodImposter _duplicate_2MethodImposter;
        private readonly DuplicateMethodInvocationHistoryCollection _duplicateMethodInvocationHistoryCollection = new DuplicateMethodInvocationHistoryCollection();
        private readonly Duplicate_1MethodInvocationHistoryCollection _duplicate_1MethodInvocationHistoryCollection = new Duplicate_1MethodInvocationHistoryCollection();
        private readonly Duplicate_2MethodInvocationHistoryCollection _duplicate_2MethodInvocationHistoryCollection = new Duplicate_2MethodInvocationHistoryCollection();
        public IDuplicateMethodImposterBuilder Duplicate()
        {
            return new DuplicateMethodImposter.Builder(_duplicateMethodImposter, _duplicateMethodInvocationHistoryCollection);
        }

        public IDuplicate_1MethodImposterBuilder Duplicate(global::Imposter.Abstractions.Arg<int> value)
        {
            return new Duplicate_1MethodImposter.Builder(_duplicate_1MethodImposter, _duplicate_1MethodInvocationHistoryCollection, new Duplicate_1ArgumentsCriteria(value));
        }

        public IDuplicate_2MethodImposterBuilder Duplicate(global::Imposter.Abstractions.Arg<string> name, global::Imposter.Abstractions.Arg<int> value)
        {
            return new Duplicate_2MethodImposter.Builder(_duplicate_2MethodImposter, _duplicate_2MethodInvocationHistoryCollection, new Duplicate_2ArgumentsCriteria(name, value));
        }

        private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IMethodOverloadCollisionTarget global::Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IMethodOverloadCollisionTarget>.Instance()
        {
            return _imposterInstance;
        }

        // int IMethodOverloadCollisionTarget.Duplicate()
        public delegate int DuplicateDelegate();
        // int IMethodOverloadCollisionTarget.Duplicate()
        public delegate void DuplicateCallbackDelegate();
        // int IMethodOverloadCollisionTarget.Duplicate()
        public delegate System.Exception DuplicateExceptionGeneratorDelegate();
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDuplicateMethodInvocationHistory
        {
            bool Matches();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DuplicateMethodInvocationHistory : IDuplicateMethodInvocationHistory
        {
            internal int Result;
            internal System.Exception Exception;
            public DuplicateMethodInvocationHistory(int Result, System.Exception Exception)
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
        internal class DuplicateMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IDuplicateMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IDuplicateMethodInvocationHistory>();
            internal void Add(IDuplicateMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count()
            {
                return _invocationHistory.Count(it => it.Matches());
            }
        }

        // int IMethodOverloadCollisionTarget.Duplicate()
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class DuplicateMethodInvocationImposterGroup
        {
            internal static DuplicateMethodInvocationImposterGroup Default = new DuplicateMethodInvocationImposterGroup();
            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public DuplicateMethodInvocationImposterGroup()
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

                private DuplicateDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<DuplicateCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<DuplicateCallbackDelegate>();
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

                internal void Callback(DuplicateCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(DuplicateDelegate resultGenerator)
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

                internal void Throws(DuplicateExceptionGeneratorDelegate exceptionGenerator)
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
        public interface IDuplicateMethodInvocationImposterGroupCallback
        {
            IDuplicateMethodInvocationImposterGroupContinuation Callback(DuplicateCallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDuplicateMethodInvocationImposterGroupContinuation : IDuplicateMethodInvocationImposterGroupCallback
        {
            IDuplicateMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDuplicateMethodInvocationImposterGroup : IDuplicateMethodInvocationImposterGroupCallback
        {
            IDuplicateMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IDuplicateMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IDuplicateMethodInvocationImposterGroupContinuation Throws(DuplicateExceptionGeneratorDelegate exceptionGenerator);
            IDuplicateMethodInvocationImposterGroupContinuation Returns(DuplicateDelegate resultGenerator);
            IDuplicateMethodInvocationImposterGroupContinuation Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface DuplicateInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodOverloadCollisionTarget.Duplicate()
        public interface IDuplicateMethodImposterBuilder : IDuplicateMethodInvocationImposterGroup, IDuplicateMethodInvocationImposterGroupCallback, DuplicateInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class DuplicateMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<DuplicateMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<DuplicateMethodInvocationImposterGroup>();
            private readonly DuplicateMethodInvocationHistoryCollection _duplicateMethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public DuplicateMethodImposter(DuplicateMethodInvocationHistoryCollection _duplicateMethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._duplicateMethodInvocationHistoryCollection = _duplicateMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup()
            {
                return FindMatchingInvocationImposterGroup() != null;
            }

            private DuplicateMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup()
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
                        throw new global::Imposter.Abstractions.MissingImposterException("int IMethodOverloadCollisionTarget.Duplicate()");
                    }

                    matchingInvocationImposterGroup = DuplicateMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "int IMethodOverloadCollisionTarget.Duplicate()");
                    _duplicateMethodInvocationHistoryCollection.Add(new DuplicateMethodInvocationHistory(result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _duplicateMethodInvocationHistoryCollection.Add(new DuplicateMethodInvocationHistory(default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IDuplicateMethodImposterBuilder, IDuplicateMethodInvocationImposterGroupContinuation
            {
                private readonly DuplicateMethodImposter _imposter;
                private readonly DuplicateMethodInvocationHistoryCollection _duplicateMethodInvocationHistoryCollection;
                private readonly DuplicateMethodInvocationImposterGroup _invocationImposterGroup;
                private DuplicateMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(DuplicateMethodImposter _imposter, DuplicateMethodInvocationHistoryCollection _duplicateMethodInvocationHistoryCollection)
                {
                    this._imposter = _imposter;
                    this._duplicateMethodInvocationHistoryCollection = _duplicateMethodInvocationHistoryCollection;
                    this._invocationImposterGroup = new DuplicateMethodInvocationImposterGroup();
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IDuplicateMethodInvocationImposterGroupContinuation IDuplicateMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IDuplicateMethodInvocationImposterGroupContinuation IDuplicateMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IDuplicateMethodInvocationImposterGroupContinuation IDuplicateMethodInvocationImposterGroup.Throws(DuplicateExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws(() =>
                    {
                        throw exceptionGenerator.Invoke();
                    });
                    return this;
                }

                IDuplicateMethodInvocationImposterGroupContinuation IDuplicateMethodInvocationImposterGroupCallback.Callback(DuplicateCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IDuplicateMethodInvocationImposterGroupContinuation IDuplicateMethodInvocationImposterGroup.Returns(DuplicateDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IDuplicateMethodInvocationImposterGroupContinuation IDuplicateMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IDuplicateMethodInvocationImposterGroup IDuplicateMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void DuplicateInvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _duplicateMethodInvocationHistoryCollection.Count();
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodOverloadCollisionTarget.Duplicate(int value)
        public delegate int Duplicate_1Delegate(int value);
        // int IMethodOverloadCollisionTarget.Duplicate(int value)
        public delegate void Duplicate_1CallbackDelegate(int value);
        // int IMethodOverloadCollisionTarget.Duplicate(int value)
        public delegate System.Exception Duplicate_1ExceptionGeneratorDelegate(int value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Duplicate_1Arguments
        {
            public int value;
            internal Duplicate_1Arguments(int value)
            {
                this.value = value;
            }
        }

        // int IMethodOverloadCollisionTarget.Duplicate(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Duplicate_1ArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<int> value { get; }

            public Duplicate_1ArgumentsCriteria(global::Imposter.Abstractions.Arg<int> value)
            {
                this.value = value;
            }

            public bool Matches(Duplicate_1Arguments arguments)
            {
                return value.Matches(arguments.value);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDuplicate_1MethodInvocationHistory
        {
            bool Matches(Duplicate_1ArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Duplicate_1MethodInvocationHistory : IDuplicate_1MethodInvocationHistory
        {
            internal Duplicate_1Arguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public Duplicate_1MethodInvocationHistory(Duplicate_1Arguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(Duplicate_1ArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Duplicate_1MethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IDuplicate_1MethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IDuplicate_1MethodInvocationHistory>();
            internal void Add(IDuplicate_1MethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(Duplicate_1ArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodOverloadCollisionTarget.Duplicate(int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Duplicate_1MethodInvocationImposterGroup
        {
            internal static Duplicate_1MethodInvocationImposterGroup Default = new Duplicate_1MethodInvocationImposterGroup(new Duplicate_1ArgumentsCriteria(global::Imposter.Abstractions.Arg<int>.Any()));
            internal Duplicate_1ArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public Duplicate_1MethodInvocationImposterGroup(Duplicate_1ArgumentsCriteria argumentsCriteria)
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

            public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, value);
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

                private Duplicate_1Delegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<Duplicate_1CallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<Duplicate_1CallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int value)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke(value);
                    foreach (var callback in _callbacks)
                    {
                        callback(value);
                    }

                    return result;
                }

                internal void Callback(Duplicate_1CallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(Duplicate_1Delegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value_1)
                {
                    _resultGenerator = (int value) =>
                    {
                        return value_1;
                    };
                }

                internal void Throws(Duplicate_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int value) =>
                    {
                        throw exceptionGenerator(value);
                    };
                }

                internal static int DefaultResultGenerator(int value)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDuplicate_1MethodInvocationImposterGroupCallback
        {
            IDuplicate_1MethodInvocationImposterGroupContinuation Callback(Duplicate_1CallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDuplicate_1MethodInvocationImposterGroupContinuation : IDuplicate_1MethodInvocationImposterGroupCallback
        {
            IDuplicate_1MethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDuplicate_1MethodInvocationImposterGroup : IDuplicate_1MethodInvocationImposterGroupCallback
        {
            IDuplicate_1MethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IDuplicate_1MethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IDuplicate_1MethodInvocationImposterGroupContinuation Throws(Duplicate_1ExceptionGeneratorDelegate exceptionGenerator);
            IDuplicate_1MethodInvocationImposterGroupContinuation Returns(Duplicate_1Delegate resultGenerator);
            IDuplicate_1MethodInvocationImposterGroupContinuation Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface Duplicate_1InvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodOverloadCollisionTarget.Duplicate(int value)
        public interface IDuplicate_1MethodImposterBuilder : IDuplicate_1MethodInvocationImposterGroup, IDuplicate_1MethodInvocationImposterGroupCallback, Duplicate_1InvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Duplicate_1MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<Duplicate_1MethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<Duplicate_1MethodInvocationImposterGroup>();
            private readonly Duplicate_1MethodInvocationHistoryCollection _duplicate_1MethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public Duplicate_1MethodImposter(Duplicate_1MethodInvocationHistoryCollection _duplicate_1MethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._duplicate_1MethodInvocationHistoryCollection = _duplicate_1MethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(Duplicate_1Arguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private Duplicate_1MethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(Duplicate_1Arguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(int value)
            {
                var arguments = new Duplicate_1Arguments(value);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("int IMethodOverloadCollisionTarget.Duplicate(int value)");
                    }

                    matchingInvocationImposterGroup = Duplicate_1MethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "int IMethodOverloadCollisionTarget.Duplicate(int value)", value);
                    _duplicate_1MethodInvocationHistoryCollection.Add(new Duplicate_1MethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _duplicate_1MethodInvocationHistoryCollection.Add(new Duplicate_1MethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IDuplicate_1MethodImposterBuilder, IDuplicate_1MethodInvocationImposterGroupContinuation
            {
                private readonly Duplicate_1MethodImposter _imposter;
                private readonly Duplicate_1MethodInvocationHistoryCollection _duplicate_1MethodInvocationHistoryCollection;
                private readonly Duplicate_1ArgumentsCriteria _argumentsCriteria;
                private readonly Duplicate_1MethodInvocationImposterGroup _invocationImposterGroup;
                private Duplicate_1MethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(Duplicate_1MethodImposter _imposter, Duplicate_1MethodInvocationHistoryCollection _duplicate_1MethodInvocationHistoryCollection, Duplicate_1ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._duplicate_1MethodInvocationHistoryCollection = _duplicate_1MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new Duplicate_1MethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IDuplicate_1MethodInvocationImposterGroupContinuation IDuplicate_1MethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IDuplicate_1MethodInvocationImposterGroupContinuation IDuplicate_1MethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IDuplicate_1MethodInvocationImposterGroupContinuation IDuplicate_1MethodInvocationImposterGroup.Throws(Duplicate_1ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int value) =>
                    {
                        throw exceptionGenerator.Invoke(value);
                    });
                    return this;
                }

                IDuplicate_1MethodInvocationImposterGroupContinuation IDuplicate_1MethodInvocationImposterGroupCallback.Callback(Duplicate_1CallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IDuplicate_1MethodInvocationImposterGroupContinuation IDuplicate_1MethodInvocationImposterGroup.Returns(Duplicate_1Delegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IDuplicate_1MethodInvocationImposterGroupContinuation IDuplicate_1MethodInvocationImposterGroup.Returns(int value_1)
                {
                    _currentInvocationImposter.Returns(value_1);
                    return this;
                }

                IDuplicate_1MethodInvocationImposterGroup IDuplicate_1MethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void Duplicate_1InvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _duplicate_1MethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        // int IMethodOverloadCollisionTarget.Duplicate(string name, int value)
        public delegate int Duplicate_2Delegate(string name, int value);
        // int IMethodOverloadCollisionTarget.Duplicate(string name, int value)
        public delegate void Duplicate_2CallbackDelegate(string name, int value);
        // int IMethodOverloadCollisionTarget.Duplicate(string name, int value)
        public delegate System.Exception Duplicate_2ExceptionGeneratorDelegate(string name, int value);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Duplicate_2Arguments
        {
            public string name;
            public int value;
            internal Duplicate_2Arguments(string name, int value)
            {
                this.name = name;
                this.value = value;
            }
        }

        // int IMethodOverloadCollisionTarget.Duplicate(string name, int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class Duplicate_2ArgumentsCriteria
        {
            public global::Imposter.Abstractions.Arg<string> name { get; }
            public global::Imposter.Abstractions.Arg<int> value { get; }

            public Duplicate_2ArgumentsCriteria(global::Imposter.Abstractions.Arg<string> name, global::Imposter.Abstractions.Arg<int> value)
            {
                this.name = name;
                this.value = value;
            }

            public bool Matches(Duplicate_2Arguments arguments)
            {
                return name.Matches(arguments.name) && value.Matches(arguments.value);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDuplicate_2MethodInvocationHistory
        {
            bool Matches(Duplicate_2ArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Duplicate_2MethodInvocationHistory : IDuplicate_2MethodInvocationHistory
        {
            internal Duplicate_2Arguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public Duplicate_2MethodInvocationHistory(Duplicate_2Arguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(Duplicate_2ArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Duplicate_2MethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IDuplicate_2MethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IDuplicate_2MethodInvocationHistory>();
            internal void Add(IDuplicate_2MethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(Duplicate_2ArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // int IMethodOverloadCollisionTarget.Duplicate(string name, int value)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class Duplicate_2MethodInvocationImposterGroup
        {
            internal static Duplicate_2MethodInvocationImposterGroup Default = new Duplicate_2MethodInvocationImposterGroup(new Duplicate_2ArgumentsCriteria(global::Imposter.Abstractions.Arg<string>.Any(), global::Imposter.Abstractions.Arg<int>.Any()));
            internal Duplicate_2ArgumentsCriteria ArgumentsCriteria { get; }

            private readonly System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter> _invocationImposters = new System.Collections.Concurrent.ConcurrentQueue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public Duplicate_2MethodInvocationImposterGroup(Duplicate_2ArgumentsCriteria argumentsCriteria)
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

            public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string name, int value)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, name, value);
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

                private Duplicate_2Delegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<Duplicate_2CallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<Duplicate_2CallbackDelegate>();
                internal bool IsEmpty => (_resultGenerator == null) && (_callbacks.Count == 0);

                public int Invoke(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, string name, int value)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new global::Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke(name, value);
                    foreach (var callback in _callbacks)
                    {
                        callback(name, value);
                    }

                    return result;
                }

                internal void Callback(Duplicate_2CallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(Duplicate_2Delegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value_1)
                {
                    _resultGenerator = (string name, int value) =>
                    {
                        return value_1;
                    };
                }

                internal void Throws(Duplicate_2ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (string name, int value) =>
                    {
                        throw exceptionGenerator(name, value);
                    };
                }

                internal static int DefaultResultGenerator(string name, int value)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDuplicate_2MethodInvocationImposterGroupCallback
        {
            IDuplicate_2MethodInvocationImposterGroupContinuation Callback(Duplicate_2CallbackDelegate callback);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDuplicate_2MethodInvocationImposterGroupContinuation : IDuplicate_2MethodInvocationImposterGroupCallback
        {
            IDuplicate_2MethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IDuplicate_2MethodInvocationImposterGroup : IDuplicate_2MethodInvocationImposterGroupCallback
        {
            IDuplicate_2MethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : System.Exception, new();
            IDuplicate_2MethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            IDuplicate_2MethodInvocationImposterGroupContinuation Throws(Duplicate_2ExceptionGeneratorDelegate exceptionGenerator);
            IDuplicate_2MethodInvocationImposterGroupContinuation Returns(Duplicate_2Delegate resultGenerator);
            IDuplicate_2MethodInvocationImposterGroupContinuation Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface Duplicate_2InvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IMethodOverloadCollisionTarget.Duplicate(string name, int value)
        public interface IDuplicate_2MethodImposterBuilder : IDuplicate_2MethodInvocationImposterGroup, IDuplicate_2MethodInvocationImposterGroupCallback, Duplicate_2InvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class Duplicate_2MethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<Duplicate_2MethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<Duplicate_2MethodInvocationImposterGroup>();
            private readonly Duplicate_2MethodInvocationHistoryCollection _duplicate_2MethodInvocationHistoryCollection;
            private readonly global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public Duplicate_2MethodImposter(Duplicate_2MethodInvocationHistoryCollection _duplicate_2MethodInvocationHistoryCollection, global::Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._duplicate_2MethodInvocationHistoryCollection = _duplicate_2MethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(Duplicate_2Arguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private Duplicate_2MethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(Duplicate_2Arguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(string name, int value)
            {
                var arguments = new Duplicate_2Arguments(name, value);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == global::Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new global::Imposter.Abstractions.MissingImposterException("int IMethodOverloadCollisionTarget.Duplicate(string name, int value)");
                    }

                    matchingInvocationImposterGroup = Duplicate_2MethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "int IMethodOverloadCollisionTarget.Duplicate(string name, int value)", name, value);
                    _duplicate_2MethodInvocationHistoryCollection.Add(new Duplicate_2MethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _duplicate_2MethodInvocationHistoryCollection.Add(new Duplicate_2MethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IDuplicate_2MethodImposterBuilder, IDuplicate_2MethodInvocationImposterGroupContinuation
            {
                private readonly Duplicate_2MethodImposter _imposter;
                private readonly Duplicate_2MethodInvocationHistoryCollection _duplicate_2MethodInvocationHistoryCollection;
                private readonly Duplicate_2ArgumentsCriteria _argumentsCriteria;
                private readonly Duplicate_2MethodInvocationImposterGroup _invocationImposterGroup;
                private Duplicate_2MethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(Duplicate_2MethodImposter _imposter, Duplicate_2MethodInvocationHistoryCollection _duplicate_2MethodInvocationHistoryCollection, Duplicate_2ArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._duplicate_2MethodInvocationHistoryCollection = _duplicate_2MethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new Duplicate_2MethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IDuplicate_2MethodInvocationImposterGroupContinuation IDuplicate_2MethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((string name, int value) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IDuplicate_2MethodInvocationImposterGroupContinuation IDuplicate_2MethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((string name, int value) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IDuplicate_2MethodInvocationImposterGroupContinuation IDuplicate_2MethodInvocationImposterGroup.Throws(Duplicate_2ExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((string name, int value) =>
                    {
                        throw exceptionGenerator.Invoke(name, value);
                    });
                    return this;
                }

                IDuplicate_2MethodInvocationImposterGroupContinuation IDuplicate_2MethodInvocationImposterGroupCallback.Callback(Duplicate_2CallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IDuplicate_2MethodInvocationImposterGroupContinuation IDuplicate_2MethodInvocationImposterGroup.Returns(Duplicate_2Delegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IDuplicate_2MethodInvocationImposterGroupContinuation IDuplicate_2MethodInvocationImposterGroup.Returns(int value_1)
                {
                    _currentInvocationImposter.Returns(value_1);
                    return this;
                }

                IDuplicate_2MethodInvocationImposterGroup IDuplicate_2MethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void Duplicate_2InvocationVerifier.Called(global::Imposter.Abstractions.Count count)
                {
                    var invocationCount = _duplicate_2MethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new global::Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IMethodOverloadCollisionTargetImposter(global::Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = global::Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._duplicateMethodImposter = new DuplicateMethodImposter(_duplicateMethodInvocationHistoryCollection, invocationBehavior);
            this._duplicate_1MethodImposter = new Duplicate_1MethodImposter(_duplicate_1MethodInvocationHistoryCollection, invocationBehavior);
            this._duplicate_2MethodImposter = new Duplicate_2MethodImposter(_duplicate_2MethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IMethodOverloadCollisionTarget
        {
            IMethodOverloadCollisionTargetImposter _imposter;
            public ImposterTargetInstance(IMethodOverloadCollisionTargetImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int Duplicate()
            {
                return _imposter._duplicateMethodImposter.Invoke();
            }

            public int Duplicate(int value)
            {
                return _imposter._duplicate_1MethodImposter.Invoke(value);
            }

            public int Duplicate(string name, int value)
            {
                return _imposter._duplicate_2MethodImposter.Invoke(name, value);
            }
        }
    }
}
#pragma warning restore nullable
