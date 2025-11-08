using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Tests.Features.ClassImposter;

#pragma warning disable nullable
namespace Imposter.Tests.Features.ClassImposter
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class MultiConstructorClassImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.MultiConstructorClass>
    {
        private readonly CalculateMethodImposter _calculateMethodImposter;
        private readonly CalculateMethodInvocationHistoryCollection _calculateMethodInvocationHistoryCollection = new CalculateMethodInvocationHistoryCollection();
        public ICalculateMethodImposterBuilder Calculate(Imposter.Abstractions.Arg<int> input)
        {
            return new CalculateMethodImposter.Builder(_calculateMethodImposter, _calculateMethodInvocationHistoryCollection, new CalculateArgumentsCriteria(input));
        }

        private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        public global::Imposter.Tests.Features.ClassImposter.MultiConstructorClass Instance() => _imposterInstance;
        global::Imposter.Tests.Features.ClassImposter.MultiConstructorClass Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.ClassImposter.MultiConstructorClass>.Instance()
        {
            return _imposterInstance;
        }

        // virtual int MultiConstructorClass.Calculate(int input)
        public delegate int CalculateDelegate(int input);
        // virtual int MultiConstructorClass.Calculate(int input)
        public delegate void CalculateCallbackDelegate(int input);
        // virtual int MultiConstructorClass.Calculate(int input)
        public delegate System.Exception CalculateExceptionGeneratorDelegate(int input);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CalculateArguments
        {
            public int input;
            internal CalculateArguments(int input)
            {
                this.input = input;
            }
        }

        // virtual int MultiConstructorClass.Calculate(int input)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class CalculateArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> input { get; }

            public CalculateArgumentsCriteria(Imposter.Abstractions.Arg<int> input)
            {
                this.input = input;
            }

            public bool Matches(CalculateArguments arguments)
            {
                return input.Matches(arguments.input);
            }
        }

        public interface ICalculateMethodInvocationHistory
        {
            bool Matches(CalculateArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CalculateMethodInvocationHistory : ICalculateMethodInvocationHistory
        {
            internal CalculateArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public CalculateMethodInvocationHistory(CalculateArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(CalculateArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CalculateMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<ICalculateMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<ICalculateMethodInvocationHistory>();
            internal void Add(ICalculateMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(CalculateArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // virtual int MultiConstructorClass.Calculate(int input)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class CalculateMethodInvocationImposterGroup
        {
            internal static CalculateMethodInvocationImposterGroup Default = new CalculateMethodInvocationImposterGroup(new CalculateArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal CalculateArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public CalculateMethodInvocationImposterGroup(CalculateArgumentsCriteria argumentsCriteria)
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

                private CalculateDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<CalculateCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<CalculateCallbackDelegate>();
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

                internal void Callback(CalculateCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(CalculateDelegate resultGenerator)
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

                internal void Throws(CalculateExceptionGeneratorDelegate exceptionGenerator)
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
        public interface ICalculateMethodInvocationImposterGroup
        {
            ICalculateMethodInvocationImposterGroup Throws<TException>()
                where TException : Exception, new();
            ICalculateMethodInvocationImposterGroup Throws(System.Exception exception);
            ICalculateMethodInvocationImposterGroup Throws(CalculateExceptionGeneratorDelegate exceptionGenerator);
            ICalculateMethodInvocationImposterGroup Callback(CalculateCallbackDelegate callback);
            ICalculateMethodInvocationImposterGroup Returns(CalculateDelegate resultGenerator);
            ICalculateMethodInvocationImposterGroup Returns(int value);
            ICalculateMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface CalculateInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual int MultiConstructorClass.Calculate(int input)
        public interface ICalculateMethodImposterBuilder : ICalculateMethodInvocationImposterGroup, CalculateInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class CalculateMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<CalculateMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<CalculateMethodInvocationImposterGroup>();
            private readonly CalculateMethodInvocationHistoryCollection _calculateMethodInvocationHistoryCollection;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public CalculateMethodImposter(CalculateMethodInvocationHistoryCollection _calculateMethodInvocationHistoryCollection, Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._calculateMethodInvocationHistoryCollection = _calculateMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(CalculateArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private CalculateMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(CalculateArguments arguments)
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
                var arguments = new CalculateArguments(input);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException("virtual int MultiConstructorClass.Calculate(int input)");
                    }

                    matchingInvocationImposterGroup = CalculateMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual int MultiConstructorClass.Calculate(int input)", input);
                    _calculateMethodInvocationHistoryCollection.Add(new CalculateMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _calculateMethodInvocationHistoryCollection.Add(new CalculateMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : ICalculateMethodImposterBuilder
            {
                private readonly CalculateMethodImposter _imposter;
                private readonly CalculateMethodInvocationHistoryCollection _calculateMethodInvocationHistoryCollection;
                private readonly CalculateArgumentsCriteria _argumentsCriteria;
                private readonly CalculateMethodInvocationImposterGroup _invocationImposterGroup;
                private CalculateMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(CalculateMethodImposter _imposter, CalculateMethodInvocationHistoryCollection _calculateMethodInvocationHistoryCollection, CalculateArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._calculateMethodInvocationHistoryCollection = _calculateMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new CalculateMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                ICalculateMethodInvocationImposterGroup ICalculateMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int input) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                ICalculateMethodInvocationImposterGroup ICalculateMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int input) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                ICalculateMethodInvocationImposterGroup ICalculateMethodInvocationImposterGroup.Throws(CalculateExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int input) =>
                    {
                        throw exceptionGenerator.Invoke(input);
                    });
                    return this;
                }

                ICalculateMethodInvocationImposterGroup ICalculateMethodInvocationImposterGroup.Callback(CalculateCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                ICalculateMethodInvocationImposterGroup ICalculateMethodInvocationImposterGroup.Returns(CalculateDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                ICalculateMethodInvocationImposterGroup ICalculateMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                ICalculateMethodInvocationImposterGroup ICalculateMethodInvocationImposterGroup.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void CalculateInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _calculateMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public MultiConstructorClassImposter(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._calculateMethodImposter = new CalculateMethodImposter(_calculateMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance();
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        public MultiConstructorClassImposter(int value, string label, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._calculateMethodImposter = new CalculateMethodImposter(_calculateMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(value, label);
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        public MultiConstructorClassImposter(global::System.Guid correlationId, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._calculateMethodImposter = new CalculateMethodImposter(_calculateMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(correlationId);
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        public MultiConstructorClassImposter(bool enabled, Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._calculateMethodImposter = new CalculateMethodImposter(_calculateMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(enabled);
            this._imposterInstance.InitializeImposter(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.ClassImposter.MultiConstructorClass
        {
            MultiConstructorClassImposter _imposter;
            internal void InitializeImposter(MultiConstructorClassImposter imposter)
            {
                _imposter = imposter;
            }

            internal ImposterTargetInstance() : base()
            {
            }

            internal ImposterTargetInstance(int value, string label) : base(value, label)
            {
            }

            internal ImposterTargetInstance(global::System.Guid correlationId) : base(correlationId)
            {
            }

            internal ImposterTargetInstance(bool enabled) : base(enabled)
            {
            }

            public override int Calculate(int input)
            {
                return _imposter._calculateMethodImposter.Invoke(input);
            }
        }
    }
}