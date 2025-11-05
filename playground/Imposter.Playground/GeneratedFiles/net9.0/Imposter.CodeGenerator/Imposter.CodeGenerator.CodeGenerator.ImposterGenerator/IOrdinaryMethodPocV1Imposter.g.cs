using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Playground;

#pragma warning disable nullable
namespace Imposter.Playground
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IOrdinaryMethodPocV1Imposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IOrdinaryMethodPocV1>
    {
        private readonly AddMethodImposter _addMethodImposter;
        private readonly AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection = new AddMethodInvocationHistoryCollection();
        public IAddMethodImposterBuilder Add(Imposter.Abstractions.Arg<int> a, Imposter.Abstractions.Arg<int> b)
        {
            return new AddMethodImposter.Builder(_addMethodImposter, _addMethodInvocationHistoryCollection, new AddArgumentsCriteria(a, b));
        }

        private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        global::Imposter.Playground.IOrdinaryMethodPocV1 Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IOrdinaryMethodPocV1>.Instance()
        {
            return _imposterInstance;
        }

        // int IOrdinaryMethodPocV1.Add(int a, int b)
        public delegate int AddDelegate(int a, int b);
        // int IOrdinaryMethodPocV1.Add(int a, int b)
        public delegate void AddCallbackDelegate(int a, int b);
        // int IOrdinaryMethodPocV1.Add(int a, int b)
        public delegate System.Exception AddExceptionGeneratorDelegate(int a, int b);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class AddArguments
        {
            public int a;
            public int b;
            internal AddArguments(int a, int b)
            {
                this.a = a;
                this.b = b;
            }
        }

        // int IOrdinaryMethodPocV1.Add(int a, int b)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class AddArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> a { get; }
            public Imposter.Abstractions.Arg<int> b { get; }

            public AddArgumentsCriteria(Imposter.Abstractions.Arg<int> a, Imposter.Abstractions.Arg<int> b)
            {
                this.a = a;
                this.b = b;
            }

            public bool Matches(AddArguments arguments)
            {
                return a.Matches(arguments.a) && b.Matches(arguments.b);
            }
        }

        public interface IAddMethodInvocationHistory
        {
            bool Matches(AddArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AddMethodInvocationHistory : IAddMethodInvocationHistory
        {
            internal AddArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public AddMethodInvocationHistory(AddArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(AddArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AddMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IAddMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IAddMethodInvocationHistory>();
            internal void Add(IAddMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(AddArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class AddMethodInvocationImposterGroup
        {
            internal static AddMethodInvocationImposterGroup Default = new AddMethodInvocationImposterGroup(new AddArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<int>.Any()));
            internal AddArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public AddMethodInvocationImposterGroup(AddArgumentsCriteria argumentsCriteria)
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

            public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int a, int b)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter();
                if (invocationImposter == null)
                {
                    if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                    }

                    invocationImposter = MethodInvocationImposter.Default;
                }

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, a, b);
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

                private AddDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<AddCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<AddCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int a, int b)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke(a, b);
                    foreach (var callback in _callbacks)
                    {
                        callback(a, b);
                    }

                    return result;
                }

                internal void Callback(AddCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(AddDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (int a, int b) =>
                    {
                        return value;
                    };
                }

                internal void Throws(AddExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int a, int b) =>
                    {
                        throw exceptionGenerator(a, b);
                    };
                }

                internal static int DefaultResultGenerator(int a, int b)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAddMethodInvocationImposterGroup
        {
            IAddMethodInvocationImposterGroup Throws<TException>()
                where TException : Exception, new();
            IAddMethodInvocationImposterGroup Throws(System.Exception exception);
            IAddMethodInvocationImposterGroup Throws(AddExceptionGeneratorDelegate exceptionGenerator);
            IAddMethodInvocationImposterGroup Callback(AddCallbackDelegate callback);
            IAddMethodInvocationImposterGroup Returns(AddDelegate resultGenerator);
            IAddMethodInvocationImposterGroup Returns(int value);
            IAddMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface AddInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAddMethodImposterBuilder : IAddMethodInvocationImposterGroup, AddInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AddMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<AddMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<AddMethodInvocationImposterGroup>();
            private readonly AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public AddMethodImposter(AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection, Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._addMethodInvocationHistoryCollection = _addMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(AddArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private AddMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(AddArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(int a, int b)
            {
                var arguments = new AddArguments(a, b);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException("int IOrdinaryMethodPocV1.Add(int a, int b)");
                    }

                    matchingInvocationImposterGroup = AddMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "int IOrdinaryMethodPocV1.Add(int a, int b)", a, b);
                    _addMethodInvocationHistoryCollection.Add(new AddMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _addMethodInvocationHistoryCollection.Add(new AddMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IAddMethodImposterBuilder
            {
                private readonly AddMethodImposter _imposter;
                private readonly AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection;
                private readonly AddArgumentsCriteria _argumentsCriteria;
                private readonly AddMethodInvocationImposterGroup _invocationImposterGroup;
                private AddMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(AddMethodImposter _imposter, AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection, AddArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._addMethodInvocationHistoryCollection = _addMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new AddMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IAddMethodInvocationImposterGroup IAddMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int a, int b) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IAddMethodInvocationImposterGroup IAddMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int a, int b) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IAddMethodInvocationImposterGroup IAddMethodInvocationImposterGroup.Throws(AddExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int a, int b) =>
                    {
                        throw exceptionGenerator.Invoke(a, b);
                    });
                    return this;
                }

                IAddMethodInvocationImposterGroup IAddMethodInvocationImposterGroup.Callback(AddCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IAddMethodInvocationImposterGroup IAddMethodInvocationImposterGroup.Returns(AddDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IAddMethodInvocationImposterGroup IAddMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IAddMethodInvocationImposterGroup IAddMethodInvocationImposterGroup.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void AddInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _addMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public IOrdinaryMethodPocV1Imposter(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._addMethodImposter = new AddMethodImposter(_addMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IOrdinaryMethodPocV1
        {
            IOrdinaryMethodPocV1Imposter _imposter;
            public ImposterTargetInstance(IOrdinaryMethodPocV1Imposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int Add(int a, int b)
            {
                return _imposter._addMethodImposter.Invoke(a, b);
            }
        }
    }
}