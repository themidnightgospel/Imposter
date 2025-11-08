using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Imposter.Abstractions;
using System.Collections.Concurrent;
using Imposter.Tests.Features.MethodImposter;

#pragma warning disable nullable
namespace Imposter.Tests.Features.MethodImposter
{
    [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
    public class MethodSetupFeatureClassSutImposter : Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.MethodImposter.MethodSetupFeatureClassSut>
    {
        private readonly IntSingleParamMethodImposter _intSingleParamMethodImposter;
        private readonly IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection = new IntSingleParamMethodInvocationHistoryCollection();
        public IIntSingleParamMethodImposterBuilder IntSingleParam(Imposter.Abstractions.Arg<int> age)
        {
            return new IntSingleParamMethodImposter.Builder(_intSingleParamMethodImposter, _intSingleParamMethodInvocationHistoryCollection, new IntSingleParamArgumentsCriteria(age));
        }

        private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
        private ImposterTargetInstance _imposterInstance;
        public global::Imposter.Tests.Features.MethodImposter.MethodSetupFeatureClassSut Instance() => _imposterInstance;
        global::Imposter.Tests.Features.MethodImposter.MethodSetupFeatureClassSut Imposter.Abstractions.IHaveImposterInstance<global::Imposter.Tests.Features.MethodImposter.MethodSetupFeatureClassSut>.Instance()
        {
            return _imposterInstance;
        }

        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        public delegate int IntSingleParamDelegate(int age);
        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        public delegate void IntSingleParamCallbackDelegate(int age);
        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        public delegate System.Exception IntSingleParamExceptionGeneratorDelegate(int age);
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntSingleParamArguments
        {
            public int age;
            internal IntSingleParamArguments(int age)
            {
                this.age = age;
            }
        }

        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public class IntSingleParamArgumentsCriteria
        {
            public Imposter.Abstractions.Arg<int> age { get; }

            public IntSingleParamArgumentsCriteria(Imposter.Abstractions.Arg<int> age)
            {
                this.age = age;
            }

            public bool Matches(IntSingleParamArguments arguments)
            {
                return age.Matches(arguments.age);
            }
        }

        public interface IIntSingleParamMethodInvocationHistory
        {
            bool Matches(IntSingleParamArgumentsCriteria criteria);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntSingleParamMethodInvocationHistory : IIntSingleParamMethodInvocationHistory
        {
            internal IntSingleParamArguments Arguments;
            internal int Result;
            internal System.Exception Exception;
            public IntSingleParamMethodInvocationHistory(IntSingleParamArguments Arguments, int Result, System.Exception Exception)
            {
                this.Arguments = Arguments;
                this.Result = Result;
                this.Exception = Exception;
            }

            public bool Matches(IntSingleParamArgumentsCriteria criteria)
            {
                return criteria.Matches(Arguments);
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntSingleParamMethodInvocationHistoryCollection
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IIntSingleParamMethodInvocationHistory> _invocationHistory = new System.Collections.Concurrent.ConcurrentStack<IIntSingleParamMethodInvocationHistory>();
            internal void Add(IIntSingleParamMethodInvocationHistory invocationHistory)
            {
                _invocationHistory.Push(invocationHistory);
            }

            internal int Count(IntSingleParamArgumentsCriteria argumentsCriteria)
            {
                return _invocationHistory.Count(it => it.Matches(argumentsCriteria));
            }
        }

        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class IntSingleParamMethodInvocationImposterGroup
        {
            internal static IntSingleParamMethodInvocationImposterGroup Default = new IntSingleParamMethodInvocationImposterGroup(new IntSingleParamArgumentsCriteria(Imposter.Abstractions.Arg<int>.Any()));
            internal IntSingleParamArgumentsCriteria ArgumentsCriteria { get; }

            private readonly Queue<MethodInvocationImposter> _invocationImposters = new Queue<MethodInvocationImposter>();
            private MethodInvocationImposter _lastestInvocationImposter;
            public IntSingleParamMethodInvocationImposterGroup(IntSingleParamArgumentsCriteria argumentsCriteria)
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

            public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int age)
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

                return invocationImposter.Invoke(invocationBehavior, methodDisplayName, age);
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

                private IntSingleParamDelegate _resultGenerator;
                private readonly System.Collections.Concurrent.ConcurrentQueue<IntSingleParamCallbackDelegate> _callbacks = new System.Collections.Concurrent.ConcurrentQueue<IntSingleParamCallbackDelegate>();
                internal bool IsEmpty => _resultGenerator == null && _callbacks.Count == 0;

                public int Invoke(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior, string methodDisplayName, int age)
                {
                    if (_resultGenerator == null)
                    {
                        if (invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                        {
                            throw new Imposter.Abstractions.MissingImposterException(methodDisplayName);
                        }

                        _resultGenerator = DefaultResultGenerator;
                    }

                    int result = _resultGenerator.Invoke(age);
                    foreach (var callback in _callbacks)
                    {
                        callback(age);
                    }

                    return result;
                }

                internal void Callback(IntSingleParamCallbackDelegate callback)
                {
                    _callbacks.Enqueue(callback);
                }

                internal void Returns(IntSingleParamDelegate resultGenerator)
                {
                    _resultGenerator = resultGenerator;
                }

                internal void Returns(int value)
                {
                    _resultGenerator = (int age) =>
                    {
                        return value;
                    };
                }

                internal void Throws(IntSingleParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    _resultGenerator = (int age) =>
                    {
                        throw exceptionGenerator(age);
                    };
                }

                internal static int DefaultResultGenerator(int age)
                {
                    return default;
                }
            }
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IIntSingleParamMethodInvocationImposterGroup
        {
            IIntSingleParamMethodInvocationImposterGroup Throws<TException>()
                where TException : Exception, new();
            IIntSingleParamMethodInvocationImposterGroup Throws(System.Exception exception);
            IIntSingleParamMethodInvocationImposterGroup Throws(IntSingleParamExceptionGeneratorDelegate exceptionGenerator);
            IIntSingleParamMethodInvocationImposterGroup Callback(IntSingleParamCallbackDelegate callback);
            IIntSingleParamMethodInvocationImposterGroup Returns(IntSingleParamDelegate resultGenerator);
            IIntSingleParamMethodInvocationImposterGroup Returns(int value);
            IIntSingleParamMethodInvocationImposterGroup Then();
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        public interface IntSingleParamInvocationVerifier
        {
            void Called(Count count);
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        // virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)
        public interface IIntSingleParamMethodImposterBuilder : IIntSingleParamMethodInvocationImposterGroup, IntSingleParamInvocationVerifier
        {
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        internal class IntSingleParamMethodImposter
        {
            private readonly System.Collections.Concurrent.ConcurrentStack<IntSingleParamMethodInvocationImposterGroup> _invocationImposters = new System.Collections.Concurrent.ConcurrentStack<IntSingleParamMethodInvocationImposterGroup>();
            private readonly IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection;
            private readonly Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior;
            public IntSingleParamMethodImposter(IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection, Imposter.Abstractions.ImposterInvocationBehavior _invocationBehavior)
            {
                this._intSingleParamMethodInvocationHistoryCollection = _intSingleParamMethodInvocationHistoryCollection;
                this._invocationBehavior = _invocationBehavior;
            }

            public bool HasMatchingSetup(IntSingleParamArguments arguments)
            {
                return FindMatchingInvocationImposterGroup(arguments) != null;
            }

            private IntSingleParamMethodInvocationImposterGroup? FindMatchingInvocationImposterGroup(IntSingleParamArguments arguments)
            {
                foreach (var invocationImposterGroup in _invocationImposters)
                {
                    if (invocationImposterGroup.ArgumentsCriteria.Matches(arguments))
                        return invocationImposterGroup;
                }

                return null;
            }

            public int Invoke(int age)
            {
                var arguments = new IntSingleParamArguments(age);
                var matchingInvocationImposterGroup = FindMatchingInvocationImposterGroup(arguments);
                if (matchingInvocationImposterGroup == default)
                {
                    if (_invocationBehavior == Imposter.Abstractions.ImposterInvocationBehavior.Explicit)
                    {
                        throw new Imposter.Abstractions.MissingImposterException("virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)");
                    }

                    matchingInvocationImposterGroup = IntSingleParamMethodInvocationImposterGroup.Default;
                }

                try
                {
                    var result = matchingInvocationImposterGroup.Invoke(_invocationBehavior, "virtual int MethodSetupFeatureClassSut.IntSingleParam(int age)", age);
                    _intSingleParamMethodInvocationHistoryCollection.Add(new IntSingleParamMethodInvocationHistory(arguments, result, default));
                    return result;
                }
                catch (System.Exception ex)
                {
                    _intSingleParamMethodInvocationHistoryCollection.Add(new IntSingleParamMethodInvocationHistory(arguments, default, ex));
                    throw;
                }
            }

            [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
            internal class Builder : IIntSingleParamMethodImposterBuilder
            {
                private readonly IntSingleParamMethodImposter _imposter;
                private readonly IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection;
                private readonly IntSingleParamArgumentsCriteria _argumentsCriteria;
                private readonly IntSingleParamMethodInvocationImposterGroup _invocationImposterGroup;
                private IntSingleParamMethodInvocationImposterGroup.MethodInvocationImposter _currentInvocationImposter;
                public Builder(IntSingleParamMethodImposter _imposter, IntSingleParamMethodInvocationHistoryCollection _intSingleParamMethodInvocationHistoryCollection, IntSingleParamArgumentsCriteria _argumentsCriteria)
                {
                    this._imposter = _imposter;
                    this._intSingleParamMethodInvocationHistoryCollection = _intSingleParamMethodInvocationHistoryCollection;
                    this._argumentsCriteria = _argumentsCriteria;
                    this._invocationImposterGroup = new IntSingleParamMethodInvocationImposterGroup(_argumentsCriteria);
                    _imposter._invocationImposters.Push(_invocationImposterGroup);
                    this._currentInvocationImposter = this._invocationImposterGroup.AddInvocationImposter();
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Throws<TException>()
                {
                    _currentInvocationImposter.Throws((int age) =>
                    {
                        throw new TException();
                    });
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Throws(System.Exception exception)
                {
                    _currentInvocationImposter.Throws((int age) =>
                    {
                        throw exception;
                    });
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Throws(IntSingleParamExceptionGeneratorDelegate exceptionGenerator)
                {
                    _currentInvocationImposter.Throws((int age) =>
                    {
                        throw exceptionGenerator.Invoke(age);
                    });
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Callback(IntSingleParamCallbackDelegate callback)
                {
                    _currentInvocationImposter.Callback(callback);
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Returns(IntSingleParamDelegate resultGenerator)
                {
                    _currentInvocationImposter.Returns(resultGenerator);
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Returns(int value)
                {
                    _currentInvocationImposter.Returns(value);
                    return this;
                }

                IIntSingleParamMethodInvocationImposterGroup IIntSingleParamMethodInvocationImposterGroup.Then()
                {
                    this._currentInvocationImposter = _invocationImposterGroup.AddInvocationImposter();
                    return this;
                }

                void IntSingleParamInvocationVerifier.Called(Imposter.Abstractions.Count count)
                {
                    var invocationCount = _intSingleParamMethodInvocationHistoryCollection.Count(_argumentsCriteria);
                    if (!count.Matches(invocationCount))
                    {
                        throw new Imposter.Abstractions.VerificationFailedException(count, invocationCount);
                    }
                }
            }
        }

        public MethodSetupFeatureClassSutImposter(Imposter.Abstractions.ImposterInvocationBehavior invocationBehavior = Imposter.Abstractions.ImposterInvocationBehavior.Implicit)
        {
            this._intSingleParamMethodImposter = new IntSingleParamMethodImposter(_intSingleParamMethodInvocationHistoryCollection, invocationBehavior);
            this._imposterInstance = new ImposterTargetInstance(this);
            this._invocationBehavior = invocationBehavior;
        }

        [global::System.CodeDom.Compiler.GeneratedCode("Imposter.CodeGenerator", "1.0.0.0")]
        class ImposterTargetInstance : global::Imposter.Tests.Features.MethodImposter.MethodSetupFeatureClassSut
        {
            MethodSetupFeatureClassSutImposter _imposter;
            public ImposterTargetInstance(MethodSetupFeatureClassSutImposter _imposter)
            {
                this._imposter = _imposter;
            }

            internal override int IntSingleParam(int age)
            {
                return _imposter._intSingleParamMethodImposter.Invoke(age);
            }
        }
    }
}