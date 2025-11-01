using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Benchmarks;

#pragma warning disable nullable
namespace Imposter.Benchmarks
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class ICalculatorImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Benchmarks.ImposterVsMoqVsNSub.ICalculator>
    {
        private readonly SquareMethodImposter _squareMethodImposter;
        private readonly SquareMethodInvocationHistoryCollection _squareMethodInvocationHistoryCollection = new SquareMethodInvocationHistoryCollection();
        public ISquareMethodImposterBuilder Square(Imposter.Abstractions.Arg<int> input)
        {
            return new SquareMethodImposter.Builder(_squareMethodImposter, _squareMethodInvocationHistoryCollection, new SquareArgumentsCriteria(input));
        }

        private ImposterTargetInstance _imposterInstance;
        public ICalculatorImposter()
        {
            this._squareMethodImposter = new SquareMethodImposter(_squareMethodInvocationHistoryCollection);
            this._imposterInstance = new ImposterTargetInstance(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Benchmarks.ImposterVsMoqVsNSub.ICalculator
        {
            ICalculatorImposter _imposter;
            public ImposterTargetInstance(ICalculatorImposter _imposter)
            {
                this._imposter = _imposter;
            }

            public int Square(int input)
            {
                return _imposter._squareMethodImposter.Invoke(input);
            }
        }

        global::Imposter.Benchmarks.ImposterVsMoqVsNSub.ICalculator Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Benchmarks.ImposterVsMoqVsNSub.ICalculator>.Instance()
        {
            return _imposterInstance;
        }

        // int ImposterVsMoqVsNSub.ICalculator.Square(int input)
        public delegate int SquareDelegate(int input);
        // int ImposterVsMoqVsNSub.ICalculator.Square(int input)
        public delegate void SquareCallbackDelegate(int input);
        // int ImposterVsMoqVsNSub.ICalculator.Square(int input)
        public delegate System.Exception SquareExceptionGeneratorDelegate(int input);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class SquareArguments
        {
            public int input;
            internal SquareArguments(int input)
            {
                this.input = input;
            }
        }

        // int ImposterVsMoqVsNSub.ICalculator.Square(int input)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class SquareArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> input { get; }

            public SquareArgumentsCriteria(Imposter.Abstractions.Arg<int> input)
            {
                this.input = input;
            }

            public bool Matches(SquareArguments arguments)
            {
                return input.Matches(arguments.input);
            }
        }

        public interface ISquareMethodInvocationHistory
        {
            bool Matches(SquareArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SquareMethodInvocationHistory : ISquareMethodInvocationHistory
        {
            internal SquareArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public SquareMethodInvocationHistory(SquareArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(SquareArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SquareMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ISquareMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ISquareMethodInvocationHistory>();
            internal void Add(ISquareMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(SquareArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class SquareMethodInvocationsSetup : ISquareMethodInvocationsSetup
        {
            internal static SquareMethodInvocationsSetup DefaultInvocationSetup = new SquareMethodInvocationsSetup(new SquareArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal SquareArgumentsCriteria ArgumentsCriteria { get; }

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

            internal static int DefaultResultGenerator(int input)
            {
                return default;
            }

            public SquareMethodInvocationsSetup(SquareArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddMethodSetup(it => true);
            }

            ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.Returns(SquareDelegate resultGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = resultGenerator;
                return this;
            }

            ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.Returns(int value)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    return value;
                };
                return this;
            }

            ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.Throws<TException>()
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    throw new TException();
                };
                return this;
            }

            ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.Throws(System.Exception exception)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    throw exception;
                };
                return this;
            }

            ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.Throws(SquareExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddMethodSetup(it => it.ResultGenerator != null).ResultGenerator = (int input) =>
                {
                    throw exceptionGenerator(input);
                };
                return this;
            }

            ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.CallBefore(SquareCallbackDelegate callback)
            {
                GetOrAddMethodSetup(it => it.CallBefore != null).CallBefore = callback;
                return this;
            }

            ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.CallAfter(SquareCallbackDelegate callback)
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

            public int Invoke(int input)
            {
                var nextSetup = GetNextSetup() ?? throw new InvalidOperationException("Invalid Setup");
                if (nextSetup.CallBefore != null)
                {
                    nextSetup.CallBefore(input);
                }

                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(input);
                if (nextSetup.CallAfter != null)
                {
                    nextSetup.CallAfter(input);
                }

                return result;
            }

            internal class MethodInvocationSetup
            {
                internal SquareDelegate? ResultGenerator { get; set; }
                internal SquareCallbackDelegate? CallBefore { get; set; }
                internal SquareCallbackDelegate? CallAfter { get; set; }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISquareMethodInvocationsSetup
        {
            ISquareMethodInvocationsSetup Throws<TException>()
                where TException : Exception, new();
            ISquareMethodInvocationsSetup Throws(System.Exception exception);
            ISquareMethodInvocationsSetup Throws(SquareExceptionGeneratorDelegate exceptionGenerator);
            ISquareMethodInvocationsSetup CallBefore(SquareCallbackDelegate callback);
            ISquareMethodInvocationsSetup CallAfter(SquareCallbackDelegate callback);
            ISquareMethodInvocationsSetup Returns(SquareDelegate resultGenerator);
            ISquareMethodInvocationsSetup Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface SquareMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISquareMethodImposterBuilder : ISquareMethodInvocationsSetup, SquareMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SquareMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<SquareMethodInvocationsSetup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<SquareMethodInvocationsSetup>();
            private readonly SquareMethodInvocationHistoryCollection _squareMethodInvocationHistoryCollection;
            public SquareMethodImposter(SquareMethodInvocationHistoryCollection _squareMethodInvocationHistoryCollection)
            {
                this._squareMethodInvocationHistoryCollection = _squareMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(SquareArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private SquareMethodInvocationsSetup? FindMatchingSetup(SquareArguments arguments)
            {
                foreach (var setup in _invocationSetups)
                {
                    if (setup.ArgumentsCriteria.Matches(arguments))
                        return setup;
                }

                return null;
            }

            public int Invoke(int input)
            {
                var arguments = new SquareArguments(input);
                var matchingSetup = FindMatchingSetup(arguments) ?? SquareMethodInvocationsSetup.DefaultInvocationSetup;
                try
                {
                    var result = matchingSetup.Invoke(input);
                    _squareMethodInvocationHistoryCollection.Add(new SquareMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _squareMethodInvocationHistoryCollection.Add(new SquareMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ISquareMethodImposterBuilder
            {
                private readonly SquareMethodImposter _imposter;
                private readonly SquareMethodInvocationHistoryCollection _squareMethodInvocationHistoryCollection;
                private readonly SquareArgumentsCriteria _argumentsCriteria;
                private SquareMethodInvocationsSetup? _existingInvocationSetup;
                public Builder(SquareMethodImposter _imposter, SquareMethodInvocationHistoryCollection _squareMethodInvocationHistoryCollection, SquareArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._squareMethodInvocationHistoryCollection = _squareMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ISquareMethodInvocationsSetup GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new SquareMethodInvocationsSetup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.Throws(SquareExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.CallBefore(SquareCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallBefore(callback);
                    return invocationSetup;
                }

                ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.CallAfter(SquareCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.CallAfter(callback);
                    return invocationSetup;
                }

                ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.Returns(SquareDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                ISquareMethodInvocationsSetup ISquareMethodInvocationsSetup.Returns(int value)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(value);
                    return invocationSetup;
                }

                void SquareMethodInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _squareMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }
    }
}