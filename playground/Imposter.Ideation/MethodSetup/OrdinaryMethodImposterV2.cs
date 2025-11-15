using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Imposter.Abstractions;

#pragma warning disable nullable
namespace Imposter.Playground
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class IOrdinaryMethodPocImposter
        : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IOrdinaryMethodPoc>
    {
        private readonly AddMethodImposter _addMethodImposter;
        private readonly AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection =
            new AddMethodInvocationHistoryCollection();

        public IAddMethodImposterBuilder Add(
            Imposter.Abstractions.Arg<int> a,
            Imposter.Abstractions.Arg<int> b
        )
        {
            return new AddMethodImposter.Builder(
                _addMethodImposter,
                _addMethodInvocationHistoryCollection,
                new AddArgumentsCriteria(a, b)
            );
        }

        private ImposterTargetInstance _imposterInstance;

        public IOrdinaryMethodPocImposter()
        {
            this._addMethodImposter = new AddMethodImposter(_addMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        global::Imposter.Playground.IOrdinaryMethodPoc Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Playground.IOrdinaryMethodPoc>.Instance()
        {
            return _imposterInstance;
        }

        // int IOrdinaryMethodPoc.Add(int a, int b)
        public delegate int AddDelegate(int a, int b);

        // int IOrdinaryMethodPoc.Add(int a, int b)
        public delegate void AddCallbackDelegate(int a, int b);

        // int IOrdinaryMethodPoc.Add(int a, int b)
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

        // int IOrdinaryMethodPoc.Add(int a, int b)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class AddArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> a { get; }
            public Imposter.Abstractions.Arg<int> b { get; }

            public AddArgumentsCriteria(
                Imposter.Abstractions.Arg<int> a,
                Imposter.Abstractions.Arg<int> b
            )
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

            public AddMethodInvocationHistory(
                AddArguments Arguments,
                int Result,
                System.Exception Exception
            )
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
            private readonly System.Collections.Concurrent.ConcurrentStack<IAddMethodInvocationHistory> _invocationHistory =
                new System.Collections.Concurrent.ConcurrentStack<IAddMethodInvocationHistory>();

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
        public interface IAddMethodInvocationImposterBuilder
        {
            IAddMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();

            IAddMethodInvocationImposterBuilder Throws(System.Exception exception);
            IAddMethodInvocationImposterBuilder Throws(
                AddExceptionGeneratorDelegate exceptionGenerator
            );
            IAddMethodInvocationImposterBuilder Callback(AddCallbackDelegate callback);
            IAddMethodInvocationImposterBuilder Returns(AddDelegate resultGenerator);
            IAddMethodInvocationImposterBuilder Returns(int value);

            IAddMethodInvocationImposterBuilder Then();
        }

        public interface IAddMethodInvocationImposterBuilderReturnsOrThrows { }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface AddMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IOrdinaryMethodPoc.Add(int a, int b)
        public interface IAddMethodImposterBuilder
            : IAddMethodInvocationImposterBuilder,
                AddMethodInvocationVerifier { }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class AddMethodInvocationImposterGroup
        {
            internal static AddMethodInvocationImposterGroup Default =
                new AddMethodInvocationImposterGroup(
                    new AddArgumentsCriteria(
                        Imposter.Abstractions.Arg<int>.Any(),
                        Imposter.Abstractions.Arg<int>.Any()
                    )
                );

            internal AddArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<AddMethodInvocationImposter> _invocationImposters =
                new Queue<AddMethodInvocationImposter>();

            public AddMethodInvocationImposterGroup(AddArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
            }

            internal AddMethodInvocationImposter AddInvocationImposter()
            {
                var invocationImposter = new AddMethodInvocationImposter();
                _invocationImposters.Enqueue(invocationImposter);
                return invocationImposter;
            }

            private AddMethodInvocationImposter _lastestInvocationImposter;

            private AddMethodInvocationImposter? GetInvocationImposter()
            {
                if (_invocationImposters.TryDequeue(out var callSetup) && !callSetup.IsEmpty)
                {
                    _lastestInvocationImposter = callSetup;
                }

                return _lastestInvocationImposter;
            }

            public int Invoke(int a, int b)
            {
                var invocationImposter =
                    GetInvocationImposter() ?? AddMethodInvocationImposter.Default;
                return invocationImposter.Invoke(a, b);
            }
        }

        internal class AddMethodInvocationImposter
        {
            internal static AddMethodInvocationImposter Default;

            static AddMethodInvocationImposter()
            {
                Default = new AddMethodInvocationImposter();
                Default.Returns(DefaultResultGenerator);
            }

            internal static int DefaultResultGenerator(int a, int b)
            {
                return default;
            }

            private AddDelegate? _resultGenerator;
            private readonly ConcurrentQueue<AddCallbackDelegate> _callbacks =
                new ConcurrentQueue<AddCallbackDelegate>();

            internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

            public int Invoke(int a, int b)
            {
                _resultGenerator ??= DefaultResultGenerator;

                var result = _resultGenerator.Invoke(a, b);

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

            internal void Returns(int value)
            {
                _resultGenerator = (i, i1) =>
                {
                    return value;
                };
            }

            internal void Returns(AddDelegate resultGenerator)
            {
                _resultGenerator = resultGenerator;
            }

            internal void Throws(AddExceptionGeneratorDelegate exceptionGenerator)
            {
                _resultGenerator = (a, b) => throw exceptionGenerator(a, b);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AddMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<AddMethodInvocationImposterGroup> _invocationSetups =
                new System.Collections.Concurrent.ConcurrentStack<AddMethodInvocationImposterGroup>();
            private readonly AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection;

            public AddMethodImposter(
                AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection
            )
            {
                this._addMethodInvocationHistoryCollection = _addMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(AddArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private AddMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(
                AddArguments arguments
            )
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(int a, int b)
            {
                var arguments = new AddArguments(a, b);
                var matchingInvocationImposterGroup =
                    FindMatchingInvocationImposterGroup(arguments)
                    ?? AddMethodInvocationImposterGroup.Default;
                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(a, b);
                    _addMethodInvocationHistoryCollection.Add(
                        new AddMethodInvocationHistory(arguments, result, default)
                    );
                    return result;
                }
                catch (System.Exception ex)
                {
                    _addMethodInvocationHistoryCollection.Add(
                        new AddMethodInvocationHistory(arguments, default, ex)
                    );
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IAddMethodImposterBuilder
            {
                private readonly AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection;
                private readonly AddArgumentsCriteria _argumentsCriteria;
                private readonly AddMethodInvocationImposterGroup _invocationImposterGroup;
                private AddMethodInvocationImposter _currentInvocationSetup;

                public Builder(
                    AddMethodImposter _imposter,
                    AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection,
                    AddArgumentsCriteria _argumentsCriteria
                )
                {
                    this._addMethodInvocationHistoryCollection =
                        _addMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;

                    _invocationImposterGroup = new AddMethodInvocationImposterGroup(
                        _argumentsCriteria
                    );
                    _imposter._invocationSetups.Push(_invocationImposterGroup);

                    _currentInvocationSetup = _invocationImposterGroup.AddInvocationImposter();
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Throws<TException>()
                {
                    _currentInvocationSetup.Throws((a, b) => throw new TException());
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Throws(
                    System.Exception exception
                )
                {
                    _currentInvocationSetup.Throws((a, b) => throw exception);
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Throws(
                    AddExceptionGeneratorDelegate exceptionGenerator
                )
                {
                    _currentInvocationSetup.Throws((a, b) => throw exceptionGenerator.Invoke(a, b));
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Callback(
                    AddCallbackDelegate callback
                )
                {
                    _currentInvocationSetup.Callback(callback);
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Returns(
                    AddDelegate resultGenerator
                )
                {
                    _currentInvocationSetup.Returns(resultGenerator);
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Returns(
                    int value
                )
                {
                    _currentInvocationSetup.Returns((a, b) => value);
                    return this;
                }

                IAddMethodInvocationImposterBuilder IAddMethodInvocationImposterBuilder.Then()
                {
                    _currentInvocationSetup = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void AddMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _addMethodInvocationHistoryCollection.Count(
                        _argumentsCriteria
                    );
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(
                            count,
                            invocationCount
                        );
                    }
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Playground.IOrdinaryMethodPoc
        {
            IOrdinaryMethodPocImposter _imposter;

            public ImposterTargetInstance(IOrdinaryMethodPocImposter _imposter)
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

namespace Imposter.Playground
{
    public interface IOrdinaryMethodPoc
    {
        int Add(int a, int b);
    }
}
