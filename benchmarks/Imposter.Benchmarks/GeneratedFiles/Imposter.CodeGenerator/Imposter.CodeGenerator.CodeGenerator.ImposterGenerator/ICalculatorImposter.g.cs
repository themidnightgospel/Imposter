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

        // int ImposterVsMoqVsNSub.ICalculator.Square(int input)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class SquareMethodInvocationImposterGroup : ISquareMethodInvocationImposterBuilder
        {
            internal static SquareMethodInvocationImposterGroup DefaultInvocationImposterGroup = new SquareMethodInvocationImposterGroup(new SquareArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal SquareArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _callSetups = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter? _currentlySetupCall;
            private MethodInvocationImposter GetOrAddInvocationImposter(Func<MethodInvocationImposter, bool> addNew)
            {
                if (_currentlySetupCall is null || addNew(_currentlySetupCall))
                {
                    _currentlySetupCall = new MethodInvocationImposter();
                    _callSetups.Enqueue(_currentlySetupCall);
                }

                return _currentlySetupCall;
            }

            internal static int DefaultResultGenerator(int input)
            {
                return default;
            }

            public SquareMethodInvocationImposterGroup(SquareArgumentsCriteria argumentsCriteria)
            {
                ArgumentsCriteria = argumentsCriteria;
                _nextSetup = GetOrAddInvocationImposter(it => true);
            }

            ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Returns(SquareDelegate resultGenerator)
            {
                GetOrAddInvocationImposter(it => false).ResultGenerator = resultGenerator;
                return this;
            }

            ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Returns(int value)
            {
                GetOrAddInvocationImposter(it => false).ResultGenerator = (int input) =>
                {
                    return value;
                };
                return this;
            }

            ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Throws<TException>()
            {
                GetOrAddInvocationImposter(it => false).ResultGenerator = (int input) =>
                {
                    throw new TException();
                };
                return this;
            }

            ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Throws(System.Exception exception)
            {
                GetOrAddInvocationImposter(it => false).ResultGenerator = (int input) =>
                {
                    throw exception;
                };
                return this;
            }

            ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Throws(SquareExceptionGeneratorDelegate exceptionGenerator)
            {
                GetOrAddInvocationImposter(it => false).ResultGenerator = (int input) =>
                {
                    throw exceptionGenerator(input);
                };
                return this;
            }

            ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Callback(SquareCallbackDelegate callback)
            {
                GetOrAddInvocationImposter(it => false).Callback = callback;
                return this;
            }

            ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Then()
            {
                GetOrAddInvocationImposter(it => true);
                return this;
            }

            private MethodInvocationImposter _nextSetup;
            private MethodInvocationImposter? GetNextSetup()
            {
                if (_callSetups.TryDequeue(out var callSetup))
                {
                    if (!callSetup.IsEmpty)
                    {
                        _nextSetup = callSetup;
                    }
                }

                return _nextSetup;
            }

            public int Invoke(int input)
            {
                var nextSetup = GetNextSetup() ?? MethodInvocationImposter.Default;
                if (nextSetup.ResultGenerator == null)
                {
                    nextSetup.ResultGenerator = DefaultResultGenerator;
                }

                var result = nextSetup.ResultGenerator.Invoke(input);
                if (nextSetup.Callback != null)
                {
                    nextSetup.Callback(input);
                }

                return result;
            }

            internal class MethodInvocationImposter
            {
                internal SquareDelegate? ResultGenerator { get; set; }
                internal bool IsEmpty => ResultGenerator == null && Callback == null;
                internal SquareCallbackDelegate? Callback { get; set; }

                internal static MethodInvocationImposter Default;
                static MethodInvocationImposter()
                {
                    Default = new MethodInvocationImposter();
                    Default.ResultGenerator = DefaultResultGenerator;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISquareMethodInvocationImposterBuilder
        {
            ISquareMethodInvocationImposterBuilder Throws<TException>()
                where TException : Exception, new();
            ISquareMethodInvocationImposterBuilder Throws(System.Exception exception);
            ISquareMethodInvocationImposterBuilder Throws(SquareExceptionGeneratorDelegate exceptionGenerator);
            ISquareMethodInvocationImposterBuilder Callback(SquareCallbackDelegate callback);
            ISquareMethodInvocationImposterBuilder Then();
            ISquareMethodInvocationImposterBuilder Returns(SquareDelegate resultGenerator);
            ISquareMethodInvocationImposterBuilder Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface SquareMethodInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int ImposterVsMoqVsNSub.ICalculator.Square(int input)
        public interface ISquareMethodImposterBuilder : ISquareMethodInvocationImposterBuilder, SquareMethodInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SquareMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<SquareMethodInvocationImposterGroup> _invocationSetups = new System.Collections.Concurrent.ConcurrentStack<SquareMethodInvocationImposterGroup>();
            private readonly SquareMethodInvocationHistoryCollection _squareMethodInvocationHistoryCollection;
            public SquareMethodImposter(SquareMethodInvocationHistoryCollection _squareMethodInvocationHistoryCollection)
            {
                this._squareMethodInvocationHistoryCollection = _squareMethodInvocationHistoryCollection;
            }

            public bool HasMatchingSetup(SquareArguments arguments)
            {
                return FindMatchingSetup(arguments) != null;
            }

            private SquareMethodInvocationImposterGroup? FindMatchingSetup(SquareArguments arguments)
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
                var matchingSetup = FindMatchingSetup(arguments) ?? SquareMethodInvocationImposterGroup.DefaultInvocationImposterGroup;
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
                private SquareMethodInvocationImposterGroup? _existingInvocationSetup;
                public Builder(SquareMethodImposter _imposter, SquareMethodInvocationHistoryCollection _squareMethodInvocationHistoryCollection, SquareArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._squareMethodInvocationHistoryCollection = _squareMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                }

                private ISquareMethodInvocationImposterBuilder GetOrAddInvocationSetup()
                {
                    if (_existingInvocationSetup is null)
                    {
                        _existingInvocationSetup = new SquareMethodInvocationImposterGroup(_argumentsCriteria);
                        _imposter._invocationSetups.Push(_existingInvocationSetup);
                    }

                    return _existingInvocationSetup;
                }

                ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Throws<TException>()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws<TException>();
                    return invocationSetup;
                }

                ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Throws(System.Exception exception)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exception);
                    return invocationSetup;
                }

                ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Throws(SquareExceptionGeneratorDelegate exceptionGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Throws(exceptionGenerator);
                    return invocationSetup;
                }

                ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Callback(SquareCallbackDelegate callback)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Callback(callback);
                    return invocationSetup;
                }

                ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Then()
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Then();
                    return invocationSetup;
                }

                ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Returns(SquareDelegate resultGenerator)
                {
                    var invocationSetup = GetOrAddInvocationSetup();
                    invocationSetup.Returns(resultGenerator);
                    return invocationSetup;
                }

                ISquareMethodInvocationImposterBuilder ISquareMethodInvocationImposterBuilder.Returns(int value)
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
    }
}