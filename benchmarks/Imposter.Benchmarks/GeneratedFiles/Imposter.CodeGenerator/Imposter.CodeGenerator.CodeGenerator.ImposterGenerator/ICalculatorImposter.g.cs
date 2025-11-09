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

        private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        public global::Imposter.Benchmarks.ImposterVsMoqVsNSub.ICalculator Instance() => _imposterInstance;
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
        class SquareMethodInvocationImposterGroup
        {
            internal static SquareMethodInvocationImposterGroup Default = new SquareMethodInvocationImposterGroup(new SquareArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal SquareArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public SquareMethodInvocationImposterGroup(SquareArgumentsCriteria argumentsCriteria)
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

            public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int input)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, input);
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

                private SquareDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<SquareCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<SquareCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int input)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke(input);
                    foreach (var callback in _callbacks)
                    {
                        callback(input);
                    }

                    return result;
                }

                internal void Callback(SquareCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(SquareDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (int input) =>
                    {
                        return value;
                    };
                }

                internal void Throws(SquareExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int input) =>
                    {
                        throw exceptionGenerator(input);
                    };
                }

                internal static int DefaultResultGenerator(int input)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISquareMethodInvocationImposterGroupContinuation
        {
            ISquareMethodInvocationImposterGroupContinuation Callback(SquareCallbackDelegate callback);
            ISquareMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface ISquareMethodInvocationImposterGroup : ISquareMethodInvocationImposterGroupContinuation
        {
            ISquareMethodInvocationImposterGroupContinuation Throws<TException>()
                where TException : Exception, new();
            ISquareMethodInvocationImposterGroupContinuation Throws(System.Exception exception);
            ISquareMethodInvocationImposterGroupContinuation Throws(SquareExceptionGeneratorDelegate exceptionGenerator);
            ISquareMethodInvocationImposterGroupContinuation Returns(SquareDelegate resultGenerator);
            ISquareMethodInvocationImposterGroupContinuation Returns(int value);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface SquareInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // int ImposterVsMoqVsNSub.ICalculator.Square(int input)
        public interface ISquareMethodImposterBuilder : ISquareMethodInvocationImposterGroup, SquareInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class SquareMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<SquareMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<SquareMethodInvocationImposterGroup>();
            private readonly SquareMethodInvocationHistoryCollection _squareMethodInvocationHistoryCollection;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public SquareMethodImposter(SquareMethodInvocationHistoryCollection _squareMethodInvocationHistoryCollection, Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._squareMethodInvocationHistoryCollection = _squareMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(SquareArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private SquareMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(SquareArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(int input)
            {
                var arguments = new SquareArguments(input);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException("int ImposterVsMoqVsNSub.ICalculator.Square(int input)");
                    }

                    matchingInvocationImposterGroup = SquareMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "int ImposterVsMoqVsNSub.ICalculator.Square(int input)", input);
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
                private readonly SquareMethodInvocationImposterGroup _invocationImposterGroup;
                private SquareMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(SquareMethodImposter _imposter, SquareMethodInvocationHistoryCollection _squareMethodInvocationHistoryCollection, SquareArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._squareMethodInvocationHistoryCollection = _squareMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new SquareMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ISquareMethodInvocationImposterGroupContinuation ISquareMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int input) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ISquareMethodInvocationImposterGroupContinuation ISquareMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int input) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ISquareMethodInvocationImposterGroupContinuation ISquareMethodInvocationImposterGroup.Throws(SquareExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int input) =>
                    {
                        throw exceptionGenerator.Invoke(input);
                    });
                    return this;
                }

                ISquareMethodInvocationImposterGroupContinuation ISquareMethodInvocationImposterGroupContinuation.Callback(SquareCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ISquareMethodInvocationImposterGroupContinuation ISquareMethodInvocationImposterGroup.Returns(SquareDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                ISquareMethodInvocationImposterGroupContinuation ISquareMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                ISquareMethodInvocationImposterGroup ISquareMethodInvocationImposterGroupContinuation.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void SquareInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _squareMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public ICalculatorImposter(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._squareMethodImposter = new SquareMethodImposter(_squareMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
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