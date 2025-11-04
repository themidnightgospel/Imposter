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

        private ImposterTargetInstance _imposterInstance;
        public IOrdinaryMethodPocV1Imposter()
        {
            this._addMethodImposter = new AddMethodImposter(_addMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

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

        // int IOrdinaryMethodPocV1.Add(int a, int b)
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

            public int Invoke(int a, int b)
            {
                MethodInvocationImposter invocationImposter = GetInvocationImposter() ?? MethodInvocationImposter.Default;
                return invocationImposter.Invoke(a, b);
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

                public int Invoke(int a, int b)
                {
                    _resultGenerator = _resultGenerator ?? DefaultResultGenerator;
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
        public interface IAddMethodInvocationImposterBuilder
        {
            IAddMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            IAddMethodInvocationImposterBuilder Throws(System.Exception exception);
            IAddMethodInvocationImposterBuilder Throws(AddExceptionGeneratorDelegate exceptionGenerator);
            IAddMethodInvocationImposterBuilder Callback(AddCallbackDelegate callback);
            IAddMethodInvocationImposterBuilder Returns(AddDelegate resultGenerator);
            IAddMethodInvocationImposterBuilder Returns(int value);
            IAddMethodInvocationImposterBuilder Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface AddMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IOrdinaryMethodPocV1.Add(int a, int b)
        public interface IAddMethodImposterBuilder : IAddMethodInvocationImposterBuilder, AddMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AddMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<AddMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<AddMethodInvocationImposterGroup>();
            private readonly AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection;
            public AddMethodImposter(AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection)
            {
                this._addMethodInvocationHistoryCollection = _addMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(AddArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private AddMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(AddArguments arguments)
            {
                foreach (var setup in _invocationImposters)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(int a, int b)
            {
                var arguments = new AddArguments(a, b);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments) ?? AddMethodInvocationImposterGroup.Default;
                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(a, b);
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

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int a, int b) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int a, int b) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Throws(AddExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int a, int b) =>
                    {
                        throw exceptionGenerator.Invoke(a, b);
                    });
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Callback(AddCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Returns(AddDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void AddMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _addMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
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