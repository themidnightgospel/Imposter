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
        class AddMethodInvocationsSetup : IAddMethodInvocationsSetup
        {
            internal static AddMethodInvocationsSetup DefaultInvocationSetup = new AddMethodInvocationsSetup(new AddArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any(), Imposter.Abstractions.Arg<int>.Any()));
            internal AddArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationSetup> _callSetups = new Queue<MethodInvocationSetup>();
            private MethodInvocationSetup? _currentlySetupCall;
            private MethodInvocationSetup GetOrAddMethodSetup(Func<MethodInvocationSetup, bool> addNew)
            {
                if (_currentlySetupCall is null || addNew(_currentlySetupCall))
                {
                    _currentlySetupCall = new MethodInvocationSetup();
                    _callSetups.Enqueue(_currentlySetupCall);
                }

                return _currentlySetupCall;
            }

            internal static int DefaultResultGenerator(int a, int b)
            {
                return default;
            }

            public AddMethodInvocationsSetup(AddArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            IAddMethodInvocationsSetup IAddMethodInvocationsSetup.Returns(AddDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            IAddMethodInvocationsSetup IAddMethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int a, int b) =>
                {
                    return value;
                };
                return this;
            }

            IAddMethodInvocationsSetup IAddMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int a, int b) =>
                {
                    throw new TException();
                };
                return this;
            }

            IAddMethodInvocationsSetup IAddMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int a, int b) =>
                {
                    throw exception;
                };
                return this;
            }

            IAddMethodInvocationsSetup IAddMethodInvocationsSetup.Throws(AddExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int a, int b) =>
                {
                    throw exceptionGenerator(a, b);
                };
                return this;
            }

            IAddMethodInvocationsSetup IAddMethodInvocationsSetup.CallBefore(AddCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            IAddMethodInvocationsSetup IAddMethodInvocationsSetup.CallAfter(AddCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallAfter != null).CallAfter = callback;
                return this;
            }

            private MethodInvocationSetup _nextSetup;
            private MethodInvocationSetup? GetNextSetup()
            {
                if (_callSetups.TryDequeue(out var callSetup))
                {
                    _nextSetup = callSetup;
                }

                return _nextSetup;
            }

            public int Invoke(int a, int b)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(a, b);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(a, b);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(a, b);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal AddDelegate? ResultGenerator { get; set; }
                internal AddCallbackDelegate? CallBefore { get; set; }
                internal AddCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IAddMethodInvocationsSetup
        {
            IAddMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            IAddMethodInvocationsSetup Throws(System.Exception exception);
            IAddMethodInvocationsSetup Throws(AddExceptionGeneratorDelegate exceptionGenerator);
            IAddMethodInvocationsSetup CallBefore(AddCallbackDelegate callback);
            IAddMethodInvocationsSetup CallAfter(AddCallbackDelegate callback);
            IAddMethodInvocationsSetup Returns(AddDelegate resultGenerator);
            IAddMethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface AddMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int IOrdinaryMethodPocV1.Add(int a, int b)
        public interface IAddMethodImposterBuilder : IAddMethodInvocationsSetup, AddMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class AddMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<AddMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<AddMethodInvocationsSetup>();
            private readonly AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection;
            public AddMethodImposter(AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection)
            {
                this._addMethodInvocationHistoryCollection = _addMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(AddArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private AddMethodInvocationsSetup? FindMatchingSetup(AddArguments arguments)
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
                var matchingSetup = FindMatchingSetup(arguments) ?? AddMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(a, b);
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
                private AddMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(AddMethodImposter _imposter, AddMethodInvocationHistoryCollection _addMethodInvocationHistoryCollection, AddArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._addMethodInvocationHistoryCollection = _addMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private IAddMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new AddMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                IAddMethodInvocationsSetup IAddMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                IAddMethodInvocationsSetup IAddMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                IAddMethodInvocationsSetup IAddMethodInvocationsSetup.Throws(AddExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                IAddMethodInvocationsSetup IAddMethodInvocationsSetup.CallBefore(AddCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                IAddMethodInvocationsSetup IAddMethodInvocationsSetup.CallAfter(AddCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                IAddMethodInvocationsSetup IAddMethodInvocationsSetup.Returns(AddDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                IAddMethodInvocationsSetup IAddMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
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
    
    public interface IOrdinaryMethodPocV1
    {
        int Add(int a, int b);
    }
}